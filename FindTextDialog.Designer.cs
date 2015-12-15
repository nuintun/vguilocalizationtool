namespace VGUILocalizationTool
{
  partial class FindTextDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindTextDialog));
      this.label1 = new System.Windows.Forms.Label();
      this.btNext = new System.Windows.Forms.Button();
      this.btCancel = new System.Windows.Forms.Button();
      this.rbID = new System.Windows.Forms.RadioButton();
      this.rbLocalized = new System.Windows.Forms.RadioButton();
      this.rbEnglish = new System.Windows.Forms.RadioButton();
      this.tbText = new System.Windows.Forms.TextBox();
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
      // btNext
      // 
      this.btNext.Location = new System.Drawing.Point(294, 12);
      this.btNext.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btNext.Name = "btNext";
      this.btNext.Size = new System.Drawing.Size(65, 25);
      this.btNext.TabIndex = 5;
      this.btNext.Text = "下一个";
      this.btNext.UseVisualStyleBackColor = true;
      this.btNext.Click += new System.EventHandler(this.btNext_Click);
      // 
      // btCancel
      // 
      this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btCancel.Location = new System.Drawing.Point(294, 43);
      this.btCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btCancel.Name = "btCancel";
      this.btCancel.Size = new System.Drawing.Size(65, 25);
      this.btCancel.TabIndex = 6;
      this.btCancel.Text = "取消";
      this.btCancel.UseVisualStyleBackColor = true;
      this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
      // 
      // rbID
      // 
      this.rbID.AutoSize = true;
      this.rbID.Checked = global::VGUILocalizationTool.Properties.Settings.Default.IDDefault;
      this.rbID.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::VGUILocalizationTool.Properties.Settings.Default, "IDDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.rbID.Location = new System.Drawing.Point(50, 45);
      this.rbID.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.rbID.Name = "rbID";
      this.rbID.Size = new System.Drawing.Size(39, 21);
      this.rbID.TabIndex = 2;
      this.rbID.Text = "ID";
      this.rbID.UseVisualStyleBackColor = true;
      this.rbID.Click += new System.EventHandler(this.rb_Click);
      // 
      // rbLocalized
      // 
      this.rbLocalized.AutoSize = true;
      this.rbLocalized.Checked = global::VGUILocalizationTool.Properties.Settings.Default.LocalizedDefault;
      this.rbLocalized.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::VGUILocalizationTool.Properties.Settings.Default, "LocalizedDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.rbLocalized.Location = new System.Drawing.Point(171, 45);
      this.rbLocalized.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.rbLocalized.Name = "rbLocalized";
      this.rbLocalized.Size = new System.Drawing.Size(86, 21);
      this.rbLocalized.TabIndex = 4;
      this.rbLocalized.Text = "本地化语言";
      this.rbLocalized.UseVisualStyleBackColor = true;
      this.rbLocalized.Click += new System.EventHandler(this.rb_Click);
      // 
      // rbEnglish
      // 
      this.rbEnglish.AutoSize = true;
      this.rbEnglish.Checked = global::VGUILocalizationTool.Properties.Settings.Default.OriginDefault;
      this.rbEnglish.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::VGUILocalizationTool.Properties.Settings.Default, "EnglishDefault", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.rbEnglish.Location = new System.Drawing.Point(93, 45);
      this.rbEnglish.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.rbEnglish.Name = "rbEnglish";
      this.rbEnglish.Size = new System.Drawing.Size(74, 21);
      this.rbEnglish.TabIndex = 3;
      this.rbEnglish.TabStop = true;
      this.rbEnglish.Text = "原始语言";
      this.rbEnglish.UseVisualStyleBackColor = true;
      this.rbEnglish.Click += new System.EventHandler(this.rb_Click);
      // 
      // tbText
      // 
      this.tbText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::VGUILocalizationTool.Properties.Settings.Default, "FindText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.tbText.Location = new System.Drawing.Point(50, 13);
      this.tbText.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tbText.Name = "tbText";
      this.tbText.Size = new System.Drawing.Size(240, 23);
      this.tbText.TabIndex = 1;
      this.tbText.Text = global::VGUILocalizationTool.Properties.Settings.Default.FindText;
      // 
      // FindTextDialog
      // 
      this.AcceptButton = this.btNext;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.CancelButton = this.btCancel;
      this.ClientSize = new System.Drawing.Size(370, 79);
      this.Controls.Add(this.rbID);
      this.Controls.Add(this.rbLocalized);
      this.Controls.Add(this.rbEnglish);
      this.Controls.Add(this.btCancel);
      this.Controls.Add(this.tbText);
      this.Controls.Add(this.btNext);
      this.Controls.Add(this.label1);
      this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FindTextDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "查找";
      this.TopMost = true;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindTextDialog_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btNext;
    private System.Windows.Forms.Button btCancel;
    internal System.Windows.Forms.TextBox tbText;
    internal System.Windows.Forms.RadioButton rbEnglish;
    internal System.Windows.Forms.RadioButton rbID;
    internal System.Windows.Forms.RadioButton rbLocalized;
  }
}