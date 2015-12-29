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
    private ValveLocalizationFile valveFile;
    // 添加本地化语言窗口
    private AddLocal addLocal = new AddLocal();
    // 搜索窗口
    private FindText findText = new FindText();
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
      // 清除缓存
      foreach (var item in CACHE)
      {
        CACHE.Remove(item.Key);
      }

      string path = tbOrigin.Text;
      string dir = Path.GetDirectoryName(path);
      string ext = Path.GetExtension(path);
      string originName = Path.GetFileNameWithoutExtension(path);
      int pos = originName.LastIndexOf("_");

      valveFile = new ValveLocalizationFile(path);
      openOriginFile.InitialDirectory = dir;

      var allfiles =
        from s in Directory.GetFiles(dir + "\\", (pos >= 0 ? originName.Remove(pos) : originName) + "_*" + ext)
        orderby s
        select Path.GetFileNameWithoutExtension(s);

      cbLocal.Items.Clear();

      pos = pos >= 0 ? pos + 1 : originName.Length + 1;

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

      localizationDataBindingSource.DataSource = new List<LocalizationListData>();

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
      // 关闭已开弹窗
      addLocal.Hide();
      findText.Hide();

      if (openOriginFile.ShowDialog() == DialogResult.OK)
      {
        tbOrigin.Text = openOriginFile.FileName;

        OpenFile();
      }
    }

    // 添加本地化语言
    private void btnAdd_Click(object sender, EventArgs e)
    {
      TextBox tbLocal = addLocal.tbLocal;

      addLocal.EntryForm = this;

      addLocal.Show();
      tbLocal.Focus();
      tbLocal.SelectAll();
    }

    /// <summary>
    /// 添加本地语言方法
    /// </summary>
    internal void AddLocalLanguage()
    {
      TextBox tbLocal = addLocal.tbLocal;
      string language = tbLocal.Text.Trim();

      if (language != "")
      {
        if (cbLocal.Items.IndexOf(language) >= 0)
        {
          tbLocal.Focus();
          tbLocal.SelectAll();
          ShowStatus("本地化语言类型已经存在");
        }
        else
        {
          btnSave.Enabled = true;
          cbLocal.Items.Add(language);
          addLocal.Hide();
        }

        cbLocal.SelectedIndex = cbLocal.Items.IndexOf(language);
      }
      else
      {
        tbLocal.Focus();
        tbLocal.SelectAll();
        ShowStatus("本地化语言类型不能为空");
      }
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <param name="language"></param>
    /// <param name="data"></param>
    private void SetCache(string language, ValveLocalizationData data)
    {
      CacheItemPolicy policy = new CacheItemPolicy();
      policy.Priority = CacheItemPriority.NotRemovable;

      CACHE.Set(language, data, policy);
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="language"></param>
    /// <returns></returns>
    private ValveLocalizationData GetCache(string language = null)
    {
      if (language == null)
      {
        language = cbLocal.SelectedItem.ToString();
      }

      return (ValveLocalizationData)CACHE.Get(language);
    }

    // 选择本地化语言
    private void cbLocal_SelectedIndexChanged(object sender, EventArgs e)
    {
      ValveLocalizationData localData = null;
      string localLanguage = cbLocal.SelectedItem.ToString();

      if (!CACHE.Contains(localLanguage))
      {
        localData = valveFile.ReadData(localLanguage);

        SetCache(localLanguage, localData);
      }
      else
      {
        localData = GetCache(localLanguage);
      }

      // 切换数据源
      localizationDataBindingSource.DataSource = localData.List;
      // 切换选择状态
      cbSaveWithOrigin.Checked = localData.WithOriginText;
      btnFind.Enabled = localizationDataBindingSource.Count > 0;

      // 设置查找，上一个，下一个的状态
      MoveBtnState();

      // 保存用户操作
      Properties.Settings.Default.DefLang = cbLocal.Text;
    }

    // 保存
    private void btnSave_Click(object sender, EventArgs e)
    {
      if (btnSave.Enabled && valveFile != null)
      {
        valveFile.WriteData(GetCache());
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
      if (btnPrev.Enabled)
      {
        LocalizationListData data;
        int pos = localizationDataBindingSource.Position;

        do
        {
          data = (LocalizationListData)localizationDataBindingSource.List[--pos];

          if (data.ID != null && (data.OriginTextChanged || !ValveLocalizationFile.Locolaized(data.Origin, data.Localized)))
          {
            localizationDataBindingSource.Position = pos;

            MoveBtnState();
            localTextBox.Focus();

            localTextBox.SelectionStart = localTextBox.Text.Length;
            return;
          }
        } while (pos > 0);

        ShowStatus("已移动到开始行");
      }
    }

    // 下一个
    private void btnNext_Click(object sender, EventArgs e)
    {
      if (btnNext.Enabled)
      {
        LocalizationListData data;
        int count = localizationDataBindingSource.Count;
        int pos = localizationDataBindingSource.Position;

        do
        {
          data = (LocalizationListData)localizationDataBindingSource.List[++pos];

          if (data.ID != null && (data.OriginTextChanged || !ValveLocalizationFile.Locolaized(data.Origin, data.Localized)))
          {
            localizationDataBindingSource.Position = pos;

            MoveBtnState();
            localTextBox.Focus();

            localTextBox.SelectionStart = localTextBox.Text.Length;
            return;
          }
        } while (pos + 1 < count);

        ShowStatus("已移动到结尾行");
      }
    }

    // 程序退出
    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Properties.Settings.Default.InitialDir = openOriginFile.InitialDirectory;
      Properties.Settings.Default.FindText = findText.tbText.Text;
      Properties.Settings.Default.IDDefault = findText.rbID.Checked;
      Properties.Settings.Default.OriginDefault = findText.rbOrigin.Checked;
      Properties.Settings.Default.LocalizedDefault = findText.rbLocalized.Checked;

      if (cbLocal.Text != "")
      {
        Properties.Settings.Default.DefLang = cbLocal.Text;
      }

      // 记录上次操作
      Properties.Settings.Default.Save();

      // 释放资源
      addLocal.Dispose();
      findText.Dispose();
      this.Dispose();
    }

    // 向上查找
    internal void FindPrev()
    {
      TextBox tbText = findText.tbText;
      string text = tbText.Text.ToLower();

      if (text != "")
      {
        bool gotoPrev;
        bool isFound = false;
        LocalizationListData data;
        int count = localizationDataBindingSource.Count;
        int pos = localizationDataBindingSource.Position;
        int ind = findText.rbID.Checked ? 0 : findText.rbOrigin.Checked ? 1 : 2;

        do
        {
          pos--;
          gotoPrev = pos > 0;

          if (gotoPrev)
          {
            data = (LocalizationListData)localizationDataBindingSource.List[pos];

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
        tbText.Focus();
        tbText.SelectAll();
        ShowStatus("请输入搜索内容");
      }
    }

    // 向下查找
    internal void FindNext()
    {
      TextBox tbText = findText.tbText;
      string text = tbText.Text.ToLower();

      if (text != "")
      {
        bool gotoNext;
        bool isFound = false;
        LocalizationListData data;
        int count = localizationDataBindingSource.Count;
        int pos = localizationDataBindingSource.Position;
        int ind = findText.rbID.Checked ? 0 : findText.rbOrigin.Checked ? 1 : 2;

        do
        {
          pos++;
          gotoNext = pos + 1 < count;

          if (gotoNext)
          {
            data = (LocalizationListData)localizationDataBindingSource.List[pos];

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
        tbText.Focus();
        tbText.SelectAll();
        ShowStatus("请输入搜索内容");
      }
    }

    // 注册热键
    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.S)
      {
        btnSave_Click(null, null);
      }
      if (e.Alt && e.KeyCode == Keys.Left)
      {
        btnPrev_Click(null, null);
      }
      if (e.Alt && e.KeyCode == Keys.Right)
      {
        btnNext_Click(null, null);
      }
      else if (e.Control && e.KeyCode == Keys.F)
      {
        btnFind_Click(null, null);
      }
      else if (findText.Visible)
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
      if (btnFind.Enabled)
      {
        TextBox tbText = findText.tbText;

        findText.EntryForm = this;

        findText.Show();
        tbText.Focus();
        tbText.SelectAll();
      }
    }

    // 数据列表选择索引变更
    private void dataGridView_SelectChanged(object sender, EventArgs e)
    {
      MoveBtnState();

      LocalizationListData data = (LocalizationListData)localizationDataBindingSource.Current;

      if (data != null)
      {
        string platform = null;

        if (data.ID == null)
        {
          platform = "";
          dataGridView.CurrentRow.ReadOnly = true;
          localTextBox.ReadOnly = true;
          localTextBox.Cursor = Cursors.Default;
        }
        else
        {
          platform = "平台：";

          if (data.Platform == null)
          {
            platform += "通用";
          }
          else
          {
            platform += data.Platform.Substring(1, data.Platform.Length - 2);
          }

          localTextBox.ReadOnly = false;
          localTextBox.Cursor = Cursors.IBeam;
        }

        laPlatform.Text = platform;

        toolTip.SetToolTip(laPlatform, platform);
      }
    }

    // 文件拖拽进入
    private void MainForm_DragEnter(object sender, DragEventArgs e)
    {
      string filePath = ((string[])e.Data.GetData(DataFormats.FileDrop, false))[0];
      string ext = Path.GetExtension(filePath).ToLower();

      if (ext == ".txt")
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
      if (valveFile != null)
      {
        string tokens = cbLocal.SelectedItem.ToString();
        bool isChecked = cbSaveWithOrigin.Checked;

        valveFile.WithOriginText = isChecked;

        if (CACHE.Contains(tokens))
        {
          ValveLocalizationData localCache = (ValveLocalizationData)CACHE.Get(tokens);

          localCache.WithOriginText = isChecked;
        }
      }
    }

    // 绑定本地化文本框更改事件更新数据
    private void localTextBox_TextChanged(object sender, EventArgs e)
    {
      if (localizationDataBindingSource.Count > 0)
      {
        LocalizationListData data = (LocalizationListData)localizationDataBindingSource.Current;

        data.Localized = localTextBox.Text;

        dataGridView.InvalidateRow(localizationDataBindingSource.Position);
      }
    }

    // 本地化文本 Ctrl + A 全选
    private void localTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.A)
      {
        localTextBox.Focus();
        localTextBox.SelectAll();
      }
    }
  }
}
