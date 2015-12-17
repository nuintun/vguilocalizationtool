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
  public partial class FindTextDialog : Form
  {
    internal MainForm mainForm;

    // 初始化
    public FindTextDialog()
    {
      InitializeComponent();
    }

    // 向上查找
    private void btnPrev_Click(object sender, EventArgs e)
    {
      mainForm.FindPrev();
    }

    // 向下查找
    private void btNext_Click(object sender, EventArgs e)
    {
      mainForm.FindNext();
    }

    // 窗口关闭
    private void FindTextDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;

      this.Hide();
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
        this.Close();
      }
      if (e.KeyCode == Keys.F2)
      {
        mainForm.FindPrev();
      }
      else if (e.KeyCode == Keys.F3)
      {
        mainForm.FindNext();
      }
    }
  }
}
