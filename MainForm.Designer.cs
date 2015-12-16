namespace VGUILocalizationTool
{
  partial class MainForm
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.dataGridView = new System.Windows.Forms.DataGridView();
      this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.originDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.OriginTextChanged = new System.Windows.Forms.DataGridViewCheckBoxColumn();
      this.localizedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.localizationDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.tbOrigin = new System.Windows.Forms.TextBox();
      this.btnOpen = new System.Windows.Forms.Button();
      this.cbLocal = new System.Windows.Forms.ComboBox();
      this.btnAdd = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.panel = new System.Windows.Forms.Panel();
      this.btnPrev = new System.Windows.Forms.Button();
      this.btnFind = new System.Windows.Forms.Button();
      this.cbUseSlashN = new System.Windows.Forms.CheckBox();
      this.cbSaveWithOrigin = new System.Windows.Forms.CheckBox();
      this.cbDontSaveNotLocalized = new System.Windows.Forms.CheckBox();
      this.btnNext = new System.Windows.Forms.Button();
      this.localTextBox = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.originTextBox = new System.Windows.Forms.TextBox();
      this.openOriginFile = new System.Windows.Forms.OpenFileDialog();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.localizationDataBindingSource)).BeginInit();
      this.panel.SuspendLayout();
      this.SuspendLayout();
      // 
      // dataGridView
      // 
      this.dataGridView.AllowUserToAddRows = false;
      this.dataGridView.AllowUserToDeleteRows = false;
      this.dataGridView.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dataGridView.AutoGenerateColumns = false;
      this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
      this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      this.dataGridView.ColumnHeadersHeight = 26;
      this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.originDataGridViewTextBoxColumn,
            this.OriginTextChanged,
            this.localizedDataGridViewTextBoxColumn});
      this.dataGridView.DataSource = this.localizationDataBindingSource;
      this.dataGridView.GridColor = System.Drawing.SystemColors.Control;
      this.dataGridView.Location = new System.Drawing.Point(10, 80);
      this.dataGridView.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.dataGridView.MultiSelect = false;
      this.dataGridView.Name = "dataGridView";
      this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle6;
      this.dataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.dataGridView.RowTemplate.Height = 26;
      this.dataGridView.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dataGridView.Size = new System.Drawing.Size(832, 305);
      this.dataGridView.TabIndex = 7;
      this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectChanged);
      // 
      // ID
      // 
      this.ID.DataPropertyName = "ID";
      dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.ID.DefaultCellStyle = dataGridViewCellStyle2;
      this.ID.FillWeight = 50F;
      this.ID.HeaderText = "字段";
      this.ID.Name = "ID";
      this.ID.ReadOnly = true;
      // 
      // originDataGridViewTextBoxColumn
      // 
      this.originDataGridViewTextBoxColumn.DataPropertyName = "Origin";
      dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.originDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
      this.originDataGridViewTextBoxColumn.FillWeight = 67.47509F;
      this.originDataGridViewTextBoxColumn.HeaderText = "原始语言";
      this.originDataGridViewTextBoxColumn.Name = "originDataGridViewTextBoxColumn";
      this.originDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // OriginTextChanged
      // 
      this.OriginTextChanged.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.OriginTextChanged.DataPropertyName = "OriginTextChanged";
      dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      dataGridViewCellStyle4.NullValue = false;
      this.OriginTextChanged.DefaultCellStyle = dataGridViewCellStyle4;
      this.OriginTextChanged.FalseValue = "";
      this.OriginTextChanged.FillWeight = 40F;
      this.OriginTextChanged.HeaderText = "已失效";
      this.OriginTextChanged.Name = "OriginTextChanged";
      this.OriginTextChanged.ReadOnly = true;
      this.OriginTextChanged.ToolTipText = "原始语言有更改，需要重新本地化！";
      this.OriginTextChanged.TrueValue = "";
      this.OriginTextChanged.Width = 50;
      // 
      // localizedDataGridViewTextBoxColumn
      // 
      this.localizedDataGridViewTextBoxColumn.DataPropertyName = "Localized";
      dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.localizedDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle5;
      this.localizedDataGridViewTextBoxColumn.FillWeight = 67.47509F;
      this.localizedDataGridViewTextBoxColumn.HeaderText = "本地化语言";
      this.localizedDataGridViewTextBoxColumn.Name = "localizedDataGridViewTextBoxColumn";
      // 
      // localizationDataBindingSource
      // 
      this.localizationDataBindingSource.AllowNew = false;
      this.localizationDataBindingSource.DataSource = typeof(VGUILocalizationTool.LocalizationData);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(22, 13);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(68, 17);
      this.label1.TabIndex = 0;
      this.label1.Text = "原始语言：";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(10, 48);
      this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(80, 17);
      this.label2.TabIndex = 3;
      this.label2.Text = "本地化语言：";
      // 
      // tbOrigin
      // 
      this.tbOrigin.AllowDrop = true;
      this.tbOrigin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tbOrigin.BackColor = System.Drawing.SystemColors.Info;
      this.tbOrigin.Location = new System.Drawing.Point(91, 10);
      this.tbOrigin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.tbOrigin.Name = "tbOrigin";
      this.tbOrigin.ReadOnly = true;
      this.tbOrigin.Size = new System.Drawing.Size(679, 23);
      this.tbOrigin.TabIndex = 1;
      // 
      // btnOpen
      // 
      this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnOpen.Location = new System.Drawing.Point(777, 9);
      this.btnOpen.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnOpen.Name = "btnOpen";
      this.btnOpen.Size = new System.Drawing.Size(65, 25);
      this.btnOpen.TabIndex = 2;
      this.btnOpen.Text = "打开";
      this.btnOpen.UseVisualStyleBackColor = true;
      this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
      // 
      // cbLocal
      // 
      this.cbLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.cbLocal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbLocal.Enabled = false;
      this.cbLocal.FormattingEnabled = true;
      this.cbLocal.Location = new System.Drawing.Point(91, 44);
      this.cbLocal.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.cbLocal.Name = "cbLocal";
      this.cbLocal.Size = new System.Drawing.Size(608, 25);
      this.cbLocal.TabIndex = 4;
      this.cbLocal.SelectedIndexChanged += new System.EventHandler(this.cbLocal_SelectedIndexChanged);
      // 
      // btnAdd
      // 
      this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnAdd.Enabled = false;
      this.btnAdd.Location = new System.Drawing.Point(706, 44);
      this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(65, 25);
      this.btnAdd.TabIndex = 5;
      this.btnAdd.Text = "添加";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // btnSave
      // 
      this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnSave.Enabled = false;
      this.btnSave.Location = new System.Drawing.Point(777, 44);
      this.btnSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(65, 25);
      this.btnSave.TabIndex = 6;
      this.btnSave.Text = "保存";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // panel
      // 
      this.panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.panel.BackColor = System.Drawing.SystemColors.Control;
      this.panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.panel.Controls.Add(this.btnPrev);
      this.panel.Controls.Add(this.btnFind);
      this.panel.Controls.Add(this.cbUseSlashN);
      this.panel.Controls.Add(this.cbSaveWithOrigin);
      this.panel.Controls.Add(this.cbDontSaveNotLocalized);
      this.panel.Controls.Add(this.btnNext);
      this.panel.Controls.Add(this.localTextBox);
      this.panel.Controls.Add(this.label4);
      this.panel.Controls.Add(this.label3);
      this.panel.Controls.Add(this.originTextBox);
      this.panel.Location = new System.Drawing.Point(0, 385);
      this.panel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.panel.Name = "panel";
      this.panel.Size = new System.Drawing.Size(852, 312);
      this.panel.TabIndex = 0;
      // 
      // btnPrev
      // 
      this.btnPrev.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.btnPrev.Enabled = false;
      this.btnPrev.Location = new System.Drawing.Point(155, 10);
      this.btnPrev.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnPrev.Name = "btnPrev";
      this.btnPrev.Size = new System.Drawing.Size(65, 25);
      this.btnPrev.TabIndex = 9;
      this.btnPrev.Text = "上一个";
      this.btnPrev.UseVisualStyleBackColor = true;
      this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
      // 
      // btnFind
      // 
      this.btnFind.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.btnFind.Enabled = false;
      this.btnFind.Location = new System.Drawing.Point(86, 10);
      this.btnFind.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnFind.Name = "btnFind";
      this.btnFind.Size = new System.Drawing.Size(65, 25);
      this.btnFind.TabIndex = 8;
      this.btnFind.Text = "查找";
      this.btnFind.UseVisualStyleBackColor = true;
      this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
      // 
      // cbUseSlashN
      // 
      this.cbUseSlashN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbUseSlashN.AutoSize = true;
      this.cbUseSlashN.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.localizationDataBindingSource, "UseSlashN", true));
      this.cbUseSlashN.Location = new System.Drawing.Point(483, 11);
      this.cbUseSlashN.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.cbUseSlashN.Name = "cbUseSlashN";
      this.cbUseSlashN.Size = new System.Drawing.Size(107, 21);
      this.cbUseSlashN.TabIndex = 5;
      this.cbUseSlashN.Text = "使用 LF 换行符";
      this.cbUseSlashN.UseVisualStyleBackColor = true;
      // 
      // cbSaveWithOrigin
      // 
      this.cbSaveWithOrigin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbSaveWithOrigin.AutoSize = true;
      this.cbSaveWithOrigin.Location = new System.Drawing.Point(594, 11);
      this.cbSaveWithOrigin.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.cbSaveWithOrigin.Name = "cbSaveWithOrigin";
      this.cbSaveWithOrigin.Size = new System.Drawing.Size(99, 21);
      this.cbSaveWithOrigin.TabIndex = 6;
      this.cbSaveWithOrigin.Text = "保存原始语言";
      this.cbSaveWithOrigin.UseVisualStyleBackColor = true;
      // 
      // cbDontSaveNotLocalized
      // 
      this.cbDontSaveNotLocalized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cbDontSaveNotLocalized.AutoSize = true;
      this.cbDontSaveNotLocalized.Location = new System.Drawing.Point(698, 11);
      this.cbDontSaveNotLocalized.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.cbDontSaveNotLocalized.Name = "cbDontSaveNotLocalized";
      this.cbDontSaveNotLocalized.Size = new System.Drawing.Size(147, 21);
      this.cbDontSaveNotLocalized.TabIndex = 7;
      this.cbDontSaveNotLocalized.Text = "不保存未本地化的语言";
      this.cbDontSaveNotLocalized.UseVisualStyleBackColor = true;
      // 
      // btnNext
      // 
      this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Left;
      this.btnNext.Enabled = false;
      this.btnNext.Location = new System.Drawing.Point(224, 10);
      this.btnNext.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(65, 25);
      this.btnNext.TabIndex = 10;
      this.btnNext.Text = "下一个";
      this.btnNext.UseVisualStyleBackColor = true;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // localTextBox
      // 
      this.localTextBox.AcceptsReturn = true;
      this.localTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.localTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.localizationDataBindingSource, "Localized", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.localTextBox.Location = new System.Drawing.Point(86, 174);
      this.localTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.localTextBox.Multiline = true;
      this.localTextBox.Name = "localTextBox";
      this.localTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.localTextBox.Size = new System.Drawing.Size(755, 120);
      this.localTextBox.TabIndex = 4;
      this.localTextBox.WordWrap = false;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(4, 174);
      this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(80, 17);
      this.label4.TabIndex = 3;
      this.label4.Text = "本地化语言：";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(16, 43);
      this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(68, 17);
      this.label3.TabIndex = 1;
      this.label3.Text = "原始语言：";
      // 
      // originTextBox
      // 
      this.originTextBox.AcceptsReturn = true;
      this.originTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.originTextBox.BackColor = System.Drawing.SystemColors.Info;
      this.originTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.localizationDataBindingSource, "Origin", true, System.Windows.Forms.DataSourceUpdateMode.Never));
      this.originTextBox.Location = new System.Drawing.Point(86, 43);
      this.originTextBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.originTextBox.Multiline = true;
      this.originTextBox.Name = "originTextBox";
      this.originTextBox.ReadOnly = true;
      this.originTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.originTextBox.Size = new System.Drawing.Size(755, 120);
      this.originTextBox.TabIndex = 2;
      this.originTextBox.WordWrap = false;
      // 
      // openOriginFile
      // 
      this.openOriginFile.DefaultExt = "txt";
      this.openOriginFile.Filter = "英语语言文件|*_english.txt|所有语言文件|*.txt;*.res";
      this.openOriginFile.InitialDirectory = "C:\\Program Files\\Steam\\";
      this.openOriginFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openOriginFile_FileOk);
      // 
      // statusStrip
      // 
      this.statusStrip.Location = new System.Drawing.Point(0, 687);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.ShowItemToolTips = true;
      this.statusStrip.Size = new System.Drawing.Size(852, 22);
      this.statusStrip.TabIndex = 8;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.ClientSize = new System.Drawing.Size(852, 709);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.panel);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.cbLocal);
      this.Controls.Add(this.btnOpen);
      this.Controls.Add(this.tbOrigin);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.dataGridView);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "VGUI本地化工具";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.localizationDataBindingSource)).EndInit();
      this.panel.ResumeLayout(false);
      this.panel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbOrigin;
    private System.Windows.Forms.Button btnOpen;
    private System.Windows.Forms.ComboBox cbLocal;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.BindingSource localizationDataBindingSource;
    private System.Windows.Forms.Panel panel;
    private System.Windows.Forms.Button btnNext;
    private System.Windows.Forms.TextBox localTextBox;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox originTextBox;
    private System.Windows.Forms.CheckBox cbDontSaveNotLocalized;
    private System.Windows.Forms.CheckBox cbSaveWithOrigin;
    private System.Windows.Forms.CheckBox cbUseSlashN;
    private System.Windows.Forms.Button btnFind;
    private System.Windows.Forms.OpenFileDialog openOriginFile;
    private System.Windows.Forms.DataGridViewTextBoxColumn ID;
    private System.Windows.Forms.DataGridViewTextBoxColumn originDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewCheckBoxColumn OriginTextChanged;
    private System.Windows.Forms.DataGridViewTextBoxColumn localizedDataGridViewTextBoxColumn;
    private System.Windows.Forms.Button btnPrev;
    private System.Windows.Forms.StatusStrip statusStrip;
  }
}

