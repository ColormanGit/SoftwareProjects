namespace SPV_Minerva_Loader
{
    partial class Form1
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
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.XmlDataGridView = new System.Windows.Forms.DataGridView();
            this.ImportXmlButton = new System.Windows.Forms.Button();
            this.IntegratorsList = new System.Windows.Forms.ComboBox();
            this.IntegratorLabel = new System.Windows.Forms.Label();
            this.RegionLabel = new System.Windows.Forms.Label();
            this.RegionList = new System.Windows.Forms.ComboBox();
            this.ActivationTypeLabel = new System.Windows.Forms.Label();
            this.ActivationTypeList = new System.Windows.Forms.ComboBox();
            this.JobQtyTextBox = new System.Windows.Forms.TextBox();
            this.JobQtyLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XmlDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(48, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(48, 339);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1003, 198);
            this.dataGridView2.TabIndex = 3;
            // 
            // XmlDataGridView
            // 
            this.XmlDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.XmlDataGridView.Location = new System.Drawing.Point(12, 41);
            this.XmlDataGridView.Name = "XmlDataGridView";
            this.XmlDataGridView.Size = new System.Drawing.Size(1445, 33);
            this.XmlDataGridView.TabIndex = 2;
            // 
            // ImportXmlButton
            // 
            this.ImportXmlButton.Location = new System.Drawing.Point(12, 12);
            this.ImportXmlButton.Name = "ImportXmlButton";
            this.ImportXmlButton.Size = new System.Drawing.Size(75, 23);
            this.ImportXmlButton.TabIndex = 3;
            this.ImportXmlButton.Text = "Import XML";
            this.ImportXmlButton.UseVisualStyleBackColor = true;
            this.ImportXmlButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // IntegratorsList
            // 
            this.IntegratorsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.IntegratorsList.FormattingEnabled = true;
            this.IntegratorsList.Items.AddRange(new object[] {
            "AOS",
            "BlackHawk",
            "EPay",
            "Incomm"});
            this.IntegratorsList.Location = new System.Drawing.Point(12, 125);
            this.IntegratorsList.Name = "IntegratorsList";
            this.IntegratorsList.Size = new System.Drawing.Size(200, 21);
            this.IntegratorsList.TabIndex = 4;
            // 
            // IntegratorLabel
            // 
            this.IntegratorLabel.AutoSize = true;
            this.IntegratorLabel.Location = new System.Drawing.Point(12, 109);
            this.IntegratorLabel.Name = "IntegratorLabel";
            this.IntegratorLabel.Size = new System.Drawing.Size(52, 13);
            this.IntegratorLabel.TabIndex = 5;
            this.IntegratorLabel.Text = "Integrator";
            // 
            // RegionLabel
            // 
            this.RegionLabel.AutoSize = true;
            this.RegionLabel.Location = new System.Drawing.Point(12, 156);
            this.RegionLabel.Name = "RegionLabel";
            this.RegionLabel.Size = new System.Drawing.Size(41, 13);
            this.RegionLabel.TabIndex = 7;
            this.RegionLabel.Text = "Region";
            // 
            // RegionList
            // 
            this.RegionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RegionList.FormattingEnabled = true;
            this.RegionList.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.RegionList.Location = new System.Drawing.Point(12, 172);
            this.RegionList.Name = "RegionList";
            this.RegionList.Size = new System.Drawing.Size(200, 21);
            this.RegionList.TabIndex = 6;
            // 
            // ActivationTypeLabel
            // 
            this.ActivationTypeLabel.AutoSize = true;
            this.ActivationTypeLabel.Location = new System.Drawing.Point(12, 203);
            this.ActivationTypeLabel.Name = "ActivationTypeLabel";
            this.ActivationTypeLabel.Size = new System.Drawing.Size(81, 13);
            this.ActivationTypeLabel.TabIndex = 9;
            this.ActivationTypeLabel.Text = "Activation Type";
            // 
            // ActivationTypeList
            // 
            this.ActivationTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ActivationTypeList.FormattingEnabled = true;
            this.ActivationTypeList.Items.AddRange(new object[] {
            "Barocde Only",
            "Magstripe Only",
            "Hybrid (Barcode + Magstripe)"});
            this.ActivationTypeList.Location = new System.Drawing.Point(12, 219);
            this.ActivationTypeList.Name = "ActivationTypeList";
            this.ActivationTypeList.Size = new System.Drawing.Size(200, 21);
            this.ActivationTypeList.TabIndex = 8;
            // 
            // JobQtyTextBox
            // 
            this.JobQtyTextBox.Location = new System.Drawing.Point(12, 266);
            this.JobQtyTextBox.Name = "JobQtyTextBox";
            this.JobQtyTextBox.Size = new System.Drawing.Size(200, 20);
            this.JobQtyTextBox.TabIndex = 10;
            this.JobQtyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.JobQtyTextBox.WordWrap = false;
            // 
            // JobQtyLabel
            // 
            this.JobQtyLabel.AutoSize = true;
            this.JobQtyLabel.Location = new System.Drawing.Point(12, 250);
            this.JobQtyLabel.Name = "JobQtyLabel";
            this.JobQtyLabel.Size = new System.Drawing.Size(66, 13);
            this.JobQtyLabel.TabIndex = 11;
            this.JobQtyLabel.Text = "Job Quantity";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Currency";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "EUR",
            "GBP",
            "PLN",
            "HKD"});
            this.comboBox1.Location = new System.Drawing.Point(12, 312);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 609);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.JobQtyLabel);
            this.Controls.Add(this.JobQtyTextBox);
            this.Controls.Add(this.ActivationTypeLabel);
            this.Controls.Add(this.ActivationTypeList);
            this.Controls.Add(this.RegionLabel);
            this.Controls.Add(this.RegionList);
            this.Controls.Add(this.IntegratorLabel);
            this.Controls.Add(this.IntegratorsList);
            this.Controls.Add(this.ImportXmlButton);
            this.Controls.Add(this.XmlDataGridView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XmlDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView XmlDataGridView;
        private System.Windows.Forms.Button ImportXmlButton;
        private System.Windows.Forms.ComboBox IntegratorsList;
        private System.Windows.Forms.Label IntegratorLabel;
        private System.Windows.Forms.Label RegionLabel;
        private System.Windows.Forms.ComboBox RegionList;
        private System.Windows.Forms.Label ActivationTypeLabel;
        private System.Windows.Forms.ComboBox ActivationTypeList;
        private System.Windows.Forms.TextBox JobQtyTextBox;
        private System.Windows.Forms.Label JobQtyLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

