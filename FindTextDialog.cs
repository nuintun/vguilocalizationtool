﻿using System;
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

    public FindTextDialog()
    {
      InitializeComponent();
    }

    private void btNext_Click(object sender, EventArgs e)
    {
      mainForm.FindNext();
    }

    private void btnPrev_Click(object sender, EventArgs e)
    {
      mainForm.FindPrev();
    }

    private void FindTextDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = true;
      Hide();
    }

    private void rb_Click(object sender, EventArgs e)
    {
      ((RadioButton)sender).Checked = true;
    }
  }
}
