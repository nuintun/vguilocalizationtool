namespace VGUILocalizationTool
{
  partial class AddLocal
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddLocal));
      this.tbLocal = new System.Windows.Forms.TextBox();
      this.btnAdd = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // tbLocal
      // 
      this.tbLocal.BackColor = System.Drawing.SystemColors.Info;
      this.tbLocal.Location = new System.Drawing.Point(14, 19);
      this.tbLocal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tbLocal.Name = "tbLocal";
      this.tbLocal.Size = new System.Drawing.Size(235, 23);
      this.tbLocal.TabIndex = 0;
      // 
      // btnAdd
      // 
      this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 9F);
      this.btnAdd.Location = new System.Drawing.Point(255, 18);
      this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(65, 25);
      this.btnAdd.TabIndex = 1;
      this.btnAdd.Text = "添加";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // AddLocal
      // 
      this.AcceptButton = this.btnAdd;
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(333, 59);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.tbLocal);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("微软雅黑", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AddLocal";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "添加本地化语言";
      this.TopMost = true;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddLocal_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.TextBox tbLocal;
    private System.Windows.Forms.Button btnAdd;
  }
}