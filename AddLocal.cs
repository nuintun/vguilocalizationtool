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
  public partial class AddLocal : Form
  {
    internal MainForm EntryForm;

    // 初始化
    public AddLocal()
    {
      InitializeComponent();
    }

    // 添加语言
    private void btnAdd_Click(object sender, EventArgs e)
    {
      EntryForm.AddLocalLanguage();
    }

    // 阻止窗口关闭
    private void AddLocal_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;

      this.Hide();
    }
  }
}
