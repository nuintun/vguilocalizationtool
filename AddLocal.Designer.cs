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
      resources.ApplyResources(this.tbLocal, "tbLocal");
      this.tbLocal.Name = "tbLocal";
      // 
      // btnAdd
      // 
      this.btnAdd.DialogResult = System.Windows.Forms.DialogResult.OK;
      resources.ApplyResources(this.btnAdd, "btnAdd");
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.UseVisualStyleBackColor = true;
      // 
      // AddLocal
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.tbLocal);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AddLocal";
      this.ShowInTaskbar = false;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    internal System.Windows.Forms.TextBox tbLocal;
    private System.Windows.Forms.Button btnAdd;
  }
}