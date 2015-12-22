using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace VGUILocalizationTool
{
  public partial class MainForm : Form
  {
    // 语言文件
    private ValveLocalizationFile file;
    // 添加本地化语言窗口
    private AddLocal localDialog = new AddLocal();
    // 搜索窗口
    private FindTextDialog searchDialog = new FindTextDialog();
    // 内存缓存
    private MemoryCache CACHE = MemoryCache.Default;

    // 初始化
    public MainForm()
    {
      InitializeComponent();
    }

    // 打开文件
    private void OpenFile()
    {
      // 清除文件缓存
      file = null;

      // 清除缓存
      foreach (var item in CACHE)
      {
        CACHE.Remove(item.Key);
      }

      string path = Path.GetDirectoryName(tbOrigin.Text);
      string ext = Path.GetExtension(tbOrigin.Text);
      string originName = Path.GetFileNameWithoutExtension(tbOrigin.Text);
      int pos = originName.LastIndexOf("_");

      openOriginFile.InitialDirectory = path;
      pos = pos == -1 ? originName.Length - 1 : pos + 1;

      var allfiles =
        from s in Directory.GetFiles(path + "\\", originName.Remove(pos) + "*" + ext)
        orderby s
        select Path.GetFileNameWithoutExtension(s);

      cbLocal.Items.Clear();

      foreach (string fileName in allfiles)
      {
        string fname = Path.GetFileNameWithoutExtension(fileName);

        if (fname != originName)
        {
          cbLocal.Items.Add(fname.Substring(pos));
        }
      }

      // 启用按钮
      btnAdd.Enabled = true;
      cbLocal.Enabled = true;

      if (cbLocal.Items.Count > 0)
      {
        btnSave.Enabled = true;
      }

      localizationDataBindingSource.DataSource = new List<LocalizationData>();

      if (Properties.Settings.Default.DefLang != "")
      {
        int i = cbLocal.Items.IndexOf(Properties.Settings.Default.DefLang);

        if (i >= 0)
        {
          cbLocal.SelectedIndex = i;
        }
        else
        {
          cbLocal.SelectedIndex = cbLocal.Items.Count > 0 ? 0 : -1;
        }
      }
    }

    // 选择文件
    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (openOriginFile.ShowDialog() == DialogResult.OK)
      {
        tbOrigin.Text = openOriginFile.FileName;

        OpenFile();
      }
    }

    // 添加本地化语言
    private void btnAdd_Click(object sender, EventArgs e)
    {
      DialogResult result = localDialog.ShowDialog();

      localDialog.tbLocal.Focus();
      localDialog.tbLocal.SelectAll();

      if (result == DialogResult.OK)
      {

        string tokens = localDialog.tbLocal.Text.Trim();

        if (tokens != "")
        {
          if (cbLocal.Items.IndexOf(tokens) >= 0)
          {
            ShowStatus("本地化语言类型已经存在");
          }
          else
          {
            btnSave.Enabled = true;
            cbLocal.Items.Add(tokens);
          }

          cbLocal.SelectedIndex = cbLocal.Items.IndexOf(tokens);
        }
        else
        {
          ShowStatus("本地化语言类型不能为空");
        }
      }
    }

    // 是否已本地化
    bool Locolaized(string or, string lc)
    {
      if (or == null || lc == null)
      {
        return or != lc;
      }
      else
      {
        or = Regex.Replace(or.Trim(), @"%[A-z]\d*", "");
        lc = Regex.Replace(lc.Trim(), @"%[A-z]\d*", "");

        if (or == "" && lc == "")
        {
          return true;
        }
        else
        {
          return or != lc;
        }
      }
    }

    // 设置缓存
    private void SetCache(string localTokens, ValveLocalizationCache localCache)
    {
      CacheItemPolicy policy = new CacheItemPolicy();
      policy.Priority = CacheItemPriority.NotRemovable;

      CACHE.Set(localTokens, localCache, policy);
    }

    // 选择本地化语言
    private void cbLocal_SelectedIndexChanged(object sender, EventArgs e)
    {
      ValveLocalizationCache localCache = null;
      string localTokens = cbLocal.SelectedItem.ToString();

      if (file == null)
      {
        file = new ValveLocalizationFile(tbOrigin.Text);
      }


      if (!CACHE.Contains(localTokens))
      {
        List<LocalizationData> origin = file.ReadData();
        List<LocalizationData> local = file.ReadData(localTokens);

        foreach (var or in origin)
        {
          if (or.ID == null)
          {
            continue;
          }

          var lc = (
            from l in local
            where or.ID == l.ID
            select l
          ).SingleOrDefault();

          or.Origin = or.Localized;
          or.DelimeterOrigin = or.DelimeterLocalized;

          if (lc != null && lc.Localized != null)
          {
            or.OriginOld = lc.Origin;
            or.Localized = lc.Localized;
            or.UseSlashN = lc.UseSlashN;
            or.DelimeterLocalized = lc.DelimeterLocalized;
            or.OriginTextChanged = (lc.Origin != null && or.Origin != lc.Origin);
          }
        }

        localCache = new ValveLocalizationCache();
        localCache.Data = origin;
        localCache.WithOriginText = file.WithOriginText;
        localCache.DontSaveNotLocalized = file.DontSaveNotLocalized;

        SetCache(localTokens, localCache);
      }
      else
      {
        localCache = (ValveLocalizationCache)CACHE.Get(localTokens);
      }

      // 切换数据源
      localizationDataBindingSource.DataSource = localCache.Data;
      // 切换选择状态
      cbSaveWithOrigin.Checked = localCache.WithOriginText;
      cbDontSaveNotLocalized.Checked = localCache.DontSaveNotLocalized;
      btnFind.Enabled = localizationDataBindingSource.Count > 0;

      // 设置查找，上一个，下一个的状态
      MoveBtnState();

      // 保存用户操作
      Properties.Settings.Default.DefLang = cbLocal.Text;
    }

    // 保存
    private void btnSave_Click(object sender, EventArgs e)
    {
      if (file != null)
      {
        List<LocalizationData> local = (List<LocalizationData>)localizationDataBindingSource.DataSource;

        file.WriteData(cbLocal.SelectedItem.ToString(), local);
        ShowStatus("保存成功");
      }
    }

    // 上一个，下一个按钮状体
    private void MoveBtnState()
    {
      btnPrev.Enabled = localizationDataBindingSource.Position > 0;
      btnNext.Enabled = localizationDataBindingSource.Position + 1 < localizationDataBindingSource.Count;
    }

    // 上一个
    private void btnPrev_Click(object sender, EventArgs e)
    {
      LocalizationData data;
      int pos = localizationDataBindingSource.Position;

      do
      {
        data = (LocalizationData)localizationDataBindingSource.List[--pos];

        if (data.ID != null && (data.OriginTextChanged || !Locolaized(data.Origin, data.Localized)))
        {
          localizationDataBindingSource.Position = pos;

          MoveBtnState();
          return;
        }
      } while (pos > 0);

      ShowStatus("已移动到开始行");
    }

    // 下一个
    private void btnNext_Click(object sender, EventArgs e)
    {
      LocalizationData data;
      int count = localizationDataBindingSource.Count;
      int pos = localizationDataBindingSource.Position;

      do
      {
        data = (LocalizationData)localizationDataBindingSource.List[++pos];

        if (data.ID != null && (data.OriginTextChanged || !Locolaized(data.Origin, data.Localized)))
        {
          localizationDataBindingSource.Position = pos;

          MoveBtnState();
          return;
        }
      } while (pos + 1 < count);

      ShowStatus("已移动到结尾行");
    }

    // 程序退出
    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Properties.Settings.Default.InitialDir = openOriginFile.InitialDirectory;
      Properties.Settings.Default.FindText = searchDialog.tbText.Text;
      Properties.Settings.Default.IDDefault = searchDialog.rbID.Checked;
      Properties.Settings.Default.OriginDefault = searchDialog.rbOrigin.Checked;
      Properties.Settings.Default.LocalizedDefault = searchDialog.rbLocalized.Checked;

      if (cbLocal.Text != "")
      {
        Properties.Settings.Default.DefLang = cbLocal.Text;
      }

      // 记录上次操作
      Properties.Settings.Default.Save();

      // 释放资源
      localDialog.Dispose();
      searchDialog.Dispose();
      this.Dispose();
    }

    // 向上查找
    internal void FindPrev()
    {
      string text = searchDialog.tbText.Text.ToLower();

      if (text != "")
      {
        bool gotoPrev;
        bool isFound = false;
        LocalizationData data;
        int count = localizationDataBindingSource.Count;
        int pos = localizationDataBindingSource.Position;
        int ind = searchDialog.rbID.Checked ? 0 : searchDialog.rbOrigin.Checked ? 1 : 2;

        do
        {
          pos--;
          gotoPrev = pos > 0;

          if (gotoPrev)
          {
            data = (LocalizationData)localizationDataBindingSource.List[pos];

            if (data.ID != null)
            {
              switch (ind)
              {
                case 0:
                  isFound = data.ID.ToLower().IndexOf(text) >= 0;
                  break;
                case 1:
                  isFound = data.Origin.ToLower().IndexOf(text) >= 0;
                  break;
                case 2:
                  isFound = data.Localized.ToLower().IndexOf(text) >= 0;
                  break;
              }
            }
          }

          if (isFound)
          {
            localizationDataBindingSource.Position = pos;

            ShowStatus("在" + pos + "行找到匹配项");
            return;
          }
        } while (gotoPrev);

        ShowStatus("未找到匹配项");
      }
      else
      {
        ShowStatus("请输入搜索内容");
      }
    }

    // 向下查找
    internal void FindNext()
    {
      string text = searchDialog.tbText.Text.ToLower();

      if (text != "")
      {
        bool gotoNext;
        bool isFound = false;
        LocalizationData data;
        int count = localizationDataBindingSource.Count;
        int pos = localizationDataBindingSource.Position;
        int ind = searchDialog.rbID.Checked ? 0 : searchDialog.rbOrigin.Checked ? 1 : 2;

        do
        {
          pos++;
          gotoNext = pos + 1 < count;

          if (gotoNext)
          {
            data = (LocalizationData)localizationDataBindingSource.List[pos];

            if (data.ID != null)
            {
              switch (ind)
              {
                case 0:
                  isFound = data.ID.ToLower().IndexOf(text) >= 0;
                  break;
                case 1:
                  isFound = data.Origin.ToLower().IndexOf(text) >= 0;
                  break;
                case 2:
                  isFound = data.Localized.ToLower().IndexOf(text) >= 0;
                  break;
              }
            }
          }

          if (isFound)
          {
            localizationDataBindingSource.Position = pos;

            ShowStatus("在" + pos + "行找到匹配项");
            return;
          }
        } while (gotoNext);

        ShowStatus("未找到匹配项");
      }
      else
      {
        ShowStatus("请输入搜索内容");
      }
    }

    // 注册热键
    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (btnSave.Enabled && e.Control && e.KeyCode == Keys.S)
      {
        btnSave_Click(sender, null);
      }
      if (btnPrev.Enabled && e.Control && e.KeyCode == Keys.Left)
      {
        btnPrev_Click(sender, null);
      }
      if (btnNext.Enabled && e.Control && e.KeyCode == Keys.Right)
      {
        btnNext_Click(sender, null);
      }
      else if (btnFind.Enabled && e.Control && e.KeyCode == Keys.F)
      {
        btnFind_Click(sender, null);
      }
      else if (searchDialog.Visible)
      {
        if (e.KeyCode == Keys.F2)
        {
          FindPrev();
        }
        else if (e.KeyCode == Keys.F3)
        {
          FindNext();
        }
      }
    }

    // 查找
    private void btnFind_Click(object sender, EventArgs e)
    {
      searchDialog.mainForm = this;

      searchDialog.Show();
      searchDialog.tbText.Focus();
      searchDialog.tbText.SelectAll();
    }

    // 数据列表选择索引变更
    private void dataGridView_SelectChanged(object sender, EventArgs e)
    {
      MoveBtnState();

      LocalizationData data = (LocalizationData)localizationDataBindingSource.Current;

      if (data != null && data.ID == null)
      {
        dataGridView.CurrentRow.ReadOnly = true;
        localTextBox.ReadOnly = true;
        localTextBox.Cursor = Cursors.Default;
      }
      else
      {
        localTextBox.ReadOnly = false;
        localTextBox.Cursor = Cursors.IBeam;
      }
    }

    // 文件拖拽进入
    private void MainForm_DragEnter(object sender, DragEventArgs e)
    {
      string filePath = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
      string ext = Path.GetExtension(filePath).ToLower();

      if (ext == ".txt" || ext == ".res")
      {
        e.Effect = DragDropEffects.All;
      }
      else
      {
        e.Effect = DragDropEffects.None;
      }
    }

    // 文件拖拽放下
    private void MainForm_DragDrop(object sender, DragEventArgs e)
    {
      string filePath = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];

      tbOrigin.Text = filePath;

      OpenFile();
    }

    // 显示信息
    private void ShowStatus(string message)
    {
      string timestamp = DateTime.Now.ToString("yyyy年MM月dd日 hh点mm分ss秒 - ");

      tsStatusLabel.Visible = true;
      tsStatusLabel.Text = timestamp + message;
    }

    // 是否保存原始语言
    private void cbSaveWithOrigin_CheckedChanged(object sender, EventArgs e)
    {
      if (file != null)
      {
        string tokens = cbLocal.SelectedItem.ToString();
        bool isChecked = cbSaveWithOrigin.Checked;

        file.WithOriginText = isChecked;

        if (CACHE.Contains(tokens))
        {
          ValveLocalizationCache localCache = (ValveLocalizationCache)CACHE.Get(tokens);

          localCache.WithOriginText = isChecked;
        }
      }
    }

    // 是否保存未本地化的项
    private void cbDontSaveNotLocalized_CheckedChanged(object sender, EventArgs e)
    {
      if (file != null)
      {
        string tokens = cbLocal.SelectedItem.ToString();
        bool isChecked = cbDontSaveNotLocalized.Checked;

        file.DontSaveNotLocalized = isChecked;

        if (CACHE.Contains(tokens))
        {
          ValveLocalizationCache localCache = (ValveLocalizationCache)CACHE.Get(tokens);

          localCache.DontSaveNotLocalized = isChecked;
        }
      }
    }

    // 绑定本地化文本框更改事件更新数据
    private void localTextBox_TextChanged(object sender, EventArgs e)
    {
      LocalizationData data = (LocalizationData)localizationDataBindingSource.Current;

      data.Localized = localTextBox.Text;

      dataGridView.InvalidateRow(localizationDataBindingSource.Position);
    }
  }
}
