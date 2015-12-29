using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VGUILocalizationTool
{
  public partial class FindText : Form
  {
    internal MainForm EntryForm;

    // 初始化
    public FindText()
    {
      InitializeComponent();
    }

    // 向上查找
    private void btnPrev_Click(object sender, EventArgs e)
    {
      EntryForm.FindPrev();
    }

    // 向下查找
    private void btNext_Click(object sender, EventArgs e)
    {
      EntryForm.FindNext();
    }

    // 搜索条件
    private void rb_Click(object sender, EventArgs e)
    {
      ((RadioButton)sender).Checked = true;
    }

    // 注册热键
    private void FindTextDialog_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Escape)
      {
        this.Hide();
      }
      if (e.KeyCode == Keys.F2)
      {
        EntryForm.FindPrev();
      }
      else if (e.KeyCode == Keys.F3)
      {
        EntryForm.FindNext();
      }
    }

    // 阻止窗口关闭
    private void FindText_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;

      this.Hide();
    }
  }
}
