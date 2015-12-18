using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VGUILocalizationTool
{
  public partial class MainForm : Form
  {
    // 语言文件
    ValveLocalizationFile file;
    // 添加本地化语言窗口
    AddLocal localDialog = new AddLocal();
    // 搜索窗口
    FindTextDialog searchDialog = new FindTextDialog();

    // 初始化
    public MainForm()
    {
      InitializeComponent();
    }

    // 打开文件
    private void openFile()
    {
      if (cbLocal.Text != "")
      {
        Properties.Settings.Default.DefLang = cbLocal.Text;
      }

      string path = Path.GetDirectoryName(tbOrigin.Text);

      openOriginFile.InitialDirectory = path;

      string ext = Path.GetExtension(tbOrigin.Text);
      string originName = Path.GetFileNameWithoutExtension(tbOrigin.Text);
      int pos = originName.LastIndexOf("_");

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

      // 启用添加按钮
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

        openFile();
      }
    }

    // 语言是否已经存在
    private bool isHasLocal(string tokens)
    {
      int i = 0;
      var count = cbLocal.Items.Count;

      tokens = tokens.ToLower();

      for (; i < count; i++)
      {
        if (((string)cbLocal.Items[i]).ToLower() == tokens)
        {
          return true;
        }
      }

      return false;
    }

    // 添加本地化语言
    private void btnAdd_Click(object sender, EventArgs e)
    {
      if (localDialog.ShowDialog() == DialogResult.OK)
      {

        string tokens = localDialog.tbLocal.Text.Trim();

        if (tokens != "" && !isHasLocal(tokens))
        {
          btnSave.Enabled = true;
          cbLocal.Items.Add(tokens);
          cbLocal.SelectedIndex = cbLocal.Items.Count - 1;
        }
        else
        {
          showStatus("本地化语言类型已经存在");
        }
      }
    }

    // 是否已本地化
    bool Locolaized(string or, string lc)
    {
      if (lc == null)
      {
        return false;
      }
      else if (or.Length <= 2)
      {
        return true;
      }
      else if (or.Length == lc.Length)
      {
        if (or != lc)
        {
          return true;
        }
        else
        {
          foreach (char c in or)
          {
            if (Char.IsLetter(c))
            {
              return false;
            }
          }

          // 跳过没有字母的文本
          return true;
        }
      }
      else
      {
        return true;
      }
    }

    // 选择本地化语言
    private void cbLocal_SelectedIndexChanged(object sender, EventArgs e)
    {
      file = new ValveLocalizationFile(tbOrigin.Text);

      List<LocalizationData> origin = file.ReadData();
      List<LocalizationData> local = file.ReadData((string)cbLocal.SelectedItem);

      cbSaveWithOrigin.Checked = file.WithOriginText;

      int tcount = 0;
      int lcount = 0;

      foreach (var or in origin)
      {
        if (or.ID == null)
        {
          continue;
        }

        tcount++;

        var lc = (
          from l in local
          where or.ID == l.ID
          select l
        ).SingleOrDefault();

        or.Origin = or.Localized;
        or.DelimeterOrigin = or.DelimeterLocalized;

        if (lc != null && lc.Localized != null)
        {
          or.OriginTextChanged = (lc.Origin != null && or.Origin != lc.Origin);
          or.OriginOld = lc.Origin;
          or.Localized = lc.Localized;
          or.UseSlashN = lc.UseSlashN;
          or.DelimeterLocalized = lc.DelimeterLocalized;

          if (Locolaized(or.Origin, lc.Localized))
          {
            lcount++;
          }
        }
      }

      if (tcount == 0)
      {
        tcount = 1;
      }

      localizationDataBindingSource.DataSource = origin;

      //TODO 本地化百分比，暂时无用
      // lblPerc.Text = String.Format("{0:F}%", (1.0f * lcount / tcount) * 100);

      // 设置查找，上一个，下一个的状态
      moveBtnState();
      btnFind.Enabled = localizationDataBindingSource.Count > 0;
    }

    // 保存
    private void btnSave_Click(object sender, EventArgs e)
    {
      if (file != null)
      {
        List<LocalizationData> local = (List<LocalizationData>)localizationDataBindingSource.DataSource;

        file.WithOriginText = cbSaveWithOrigin.Checked;
        file.DontSaveNotLocalized = cbDontSaveNotLocalized.Checked;
        file.WriteData((string)cbLocal.SelectedItem, local);
        showStatus("保存成功");
      }
    }

    // 上一个，下一个按钮状体
    private void moveBtnState()
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

          moveBtnState();
          break;
        }
      } while (pos > 0);
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

          moveBtnState();
          break;
        }
      } while (pos + 1 < count);
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

            showStatus("在" + pos + "行找到匹配项");
            return;
          }
        } while (gotoPrev);

        showStatus("未找到匹配项");
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

            showStatus("在" + pos + "行找到匹配项");
            return;
          }
        } while (gotoNext);

        showStatus("未找到匹配项");
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
    }

    // 选择文件成功
    private void openOriginFile_FileOk(object sender, CancelEventArgs e)
    {
      btnAdd.Enabled = true;
      cbLocal.Enabled = true;
    }

    // 数据列表选择索引变更
    private void dataGridView_SelectChanged(object sender, EventArgs e)
    {
      moveBtnState();

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

    // 数据列表鼠标进入事件
    private void dataGridView_MouseEnter(object sender, EventArgs e)
    {
      dataGridView.Focus();
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

      openFile();
    }

    // 显示信息
    private void showStatus(string message)
    {
      string timestamp = DateTime.Now.ToString("yyyy年MM月dd日 hh点mm分ss秒 - ");

      tsStatusLabel.Visible = true;
      tsStatusLabel.Text = timestamp + message;
    }
  }
}
