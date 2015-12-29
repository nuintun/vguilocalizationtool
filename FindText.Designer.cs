namespace VGUILocalizationTool
{
  partial class FindText
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindText));
      this.label1 = new System.Windows.Forms.Label();
      this.btnNext = new System.Windows.Forms.Button();
      this.btnPrev = new System.Windows.Forms.Button();
      this.rbID = new System.Windows.Forms.RadioButton();
      this.rbLocalized = new System.Windows.Forms.RadioButton();
      this.rbOrigin = new System.Windows.Forms.RadioButton();
      this.tbText = new System.Windows.Forms.TextBox();
      this.toolTip = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(9, 16);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(44, 17);
      this.label1.TabIndex = 0;
      this.label1.Text = "文本：";
      // 
      // btnNext
      // 
      this.btnNext.Location = new System.Drawing.Point(294, 12);
      this.btnNext.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(65, 25);
      this.btnNext.TabIndex = 5;
      this.btnNext.Text = "下一个";
      this.toolTip.SetToolTip(this.btnNext, "使用 F3 快速查找");
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btNext_Click);
      // 
      // btnPrev
      // 
      this.btnPrev.Location = new System.Drawing.Point(294, 43);
      this.btnPrev.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnPrev.Name = "btnPrev";
      this.btnPrev.Size = new System.Drawing.Size(65, 25);
      this.btnPrev.TabIndex = 6;
      this.btnPrev.Text = "上一个";
      this.toolTip.SetToolTip(this.btnPrev, "使用 F2 快速查找");
      this.btnPrev.UseVisualStyleBackColor = true;
      this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
      // 
      // rbID
      // 
      this.rbID.AutoSize = true;
      this.rbID.Checked = global::VGUILocalizationTool.Properties.Settings.Default.IDDefault;
      this.rbID.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::VGUILocalizationTool.Properties.Settings.Default, "IDDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.rbID.Location = new System.Drawing.Point(50, 45);
      this.rbID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.rbID.Name = "rbID";
      this.rbID.Size = new System.Drawing.Size(50, 21);
      this.rbID.TabIndex = 2;
      this.rbID.Text = "字段";
      this.rbID.UseVisualStyleBackColor = true;
      this.rbID.Click += new System.EventHandler(this.rb_Click);
      // 
      // rbLocalized
      // 
      this.rbLocalized.AutoSize = true;
      this.rbLocalized.Checked = global::VGUILocalizationTool.Properties.Settings.Default.LocalizedDefault;
      this.rbLocalized.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::VGUILocalizationTool.Properties.Settings.Default, "LocalizedDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.rbLocalized.Location = new System.Drawing.Point(182, 45);
      this.rbLocalized.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.rbLocalized.Name = "rbLocalized";
      this.rbLocalized.Size = new System.Drawing.Size(86, 21);
      this.rbLocalized.TabIndex = 4;
      this.rbLocalized.Text = "本地化语言";
      this.rbLocalized.UseVisualStyleBackColor = true;
      this.rbLocalized.Click += new System.EventHandler(this.rb_Click);
      // 
      // rbOrigin
      // 
      this.rbOrigin.AutoSize = true;
      this.rbOrigin.Checked = global::VGUILocalizationTool.Properties.Settings.Default.OriginDefault;
      this.rbOrigin.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::VGUILocalizationTool.Properties.Settings.Default, "OriginDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.rbOrigin.Location = new System.Drawing.Point(104, 45);
      this.rbOrigin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.rbOrigin.Name = "rbOrigin";
      this.rbOrigin.Size = new System.Drawing.Size(74, 21);
      this.rbOrigin.TabIndex = 3;
      this.rbOrigin.TabStop = true;
      this.rbOrigin.Text = "原始语言";
      this.rbOrigin.UseVisualStyleBackColor = true;
      this.rbOrigin.Click += new System.EventHandler(this.rb_Click);
      // 
      // tbText
      // 
      this.tbText.BackColor = System.Drawing.SystemColors.Info;
      this.tbText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::VGUILocalizationTool.Properties.Settings.Default, "FindText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tbText.Location = new System.Drawing.Point(50, 13);
      this.tbText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tbText.Name = "tbText";
      this.tbText.Size = new System.Drawing.Size(240, 23);
      this.tbText.TabIndex = 1;
      this.tbText.Text = global::VGUILocalizationTool.Properties.Settings.Default.FindText;
      // 
      // FindText
      // 
      this.AcceptButton = this.btnNext;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(370, 79);
      this.Controls.Add(this.rbID);
      this.Controls.Add(this.rbLocalized);
      this.Controls.Add(this.rbOrigin);
      this.Controls.Add(this.btnPrev);
      this.Controls.Add(this.tbText);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.label1);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FindText";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "查找";
      this.TopMost = true;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindText_FormClosing);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindTextDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.Button btnPrev;
    internal System.Windows.Forms.TextBox tbText;
    internal System.Windows.Forms.RadioButton rbOrigin;
    internal System.Windows.Forms.RadioButton rbID;
    internal System.Windows.Forms.RadioButton rbLocalized;
    private System.Windows.Forms.ToolTip toolTip;
  }
}