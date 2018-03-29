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
            this.xmlDataGridView = new System.Windows.Forms.DataGridView();
            this.importXmlButton = new System.Windows.Forms.Button();
            this.integratorsList = new System.Windows.Forms.ComboBox();
            this.integratorLabel = new System.Windows.Forms.Label();
            this.regionLabel = new System.Windows.Forms.Label();
            this.regionList = new System.Windows.Forms.ComboBox();
            this.activationTypeLabel = new System.Windows.Forms.Label();
            this.activationTypeList = new System.Windows.Forms.ComboBox();
            this.jobQtyTextBox = new System.Windows.Forms.TextBox();
            this.jobQtyLabel = new System.Windows.Forms.Label();
            this.currencyLabel = new System.Windows.Forms.Label();
            this.currensyList = new System.Windows.Forms.ComboBox();
            this.productDescriptionLabel = new System.Windows.Forms.Label();
            this.productDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.nanLabel = new System.Windows.Forms.Label();
            this.nanTextBox = new System.Windows.Forms.TextBox();
            this.iidLabel = new System.Windows.Forms.Label();
            this.iidTextBox = new System.Windows.Forms.TextBox();
            this.pidLabel = new System.Windows.Forms.Label();
            this.pidTextBox = new System.Windows.Forms.TextBox();
            this.epayIDLabel = new System.Windows.Forms.Label();
            this.epayIdTextBox = new System.Windows.Forms.TextBox();
            this.ipCodeLabel = new System.Windows.Forms.Label();
            this.ipCodeTextBox = new System.Windows.Forms.TextBox();
            this.retailBarcodeLAbel = new System.Windows.Forms.Label();
            this.retailBarcodeTextBox = new System.Windows.Forms.TextBox();
            this.jobTypeLabel = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.denominationLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.humanReadableCheckBox = new System.Windows.Forms.CheckBox();
            this.wtcCheckBox = new System.Windows.Forms.CheckBox();
            this.wtcQuantityLabel = new System.Windows.Forms.Label();
            this.wtcQuantitTextBox = new System.Windows.Forms.TextBox();
            this.environmentLabel = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.xmlDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // xmlDataGridView
            // 
            this.xmlDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xmlDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.xmlDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.xmlDataGridView.Location = new System.Drawing.Point(12, 41);
            this.xmlDataGridView.Name = "xmlDataGridView";
            this.xmlDataGridView.Size = new System.Drawing.Size(1445, 53);
            this.xmlDataGridView.TabIndex = 2;
            // 
            // importXmlButton
            // 
            this.importXmlButton.Location = new System.Drawing.Point(12, 12);
            this.importXmlButton.Name = "importXmlButton";
            this.importXmlButton.Size = new System.Drawing.Size(75, 23);
            this.importXmlButton.TabIndex = 3;
            this.importXmlButton.Text = "Import XML";
            this.importXmlButton.UseVisualStyleBackColor = true;
            this.importXmlButton.Click += new System.EventHandler(this.importXmlButtonClick);
            // 
            // integratorsList
            // 
            this.integratorsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.integratorsList.FormattingEnabled = true;
            this.integratorsList.Items.AddRange(new object[] {
            "AOS",
            "BlackHawk",
            "EPay",
            "Incomm"});
            this.integratorsList.Location = new System.Drawing.Point(12, 120);
            this.integratorsList.Name = "integratorsList";
            this.integratorsList.Size = new System.Drawing.Size(200, 21);
            this.integratorsList.TabIndex = 4;
            // 
            // integratorLabel
            // 
            this.integratorLabel.AutoSize = true;
            this.integratorLabel.Location = new System.Drawing.Point(12, 104);
            this.integratorLabel.Name = "integratorLabel";
            this.integratorLabel.Size = new System.Drawing.Size(52, 13);
            this.integratorLabel.TabIndex = 5;
            this.integratorLabel.Text = "Integrator";
            // 
            // regionLabel
            // 
            this.regionLabel.AutoSize = true;
            this.regionLabel.Location = new System.Drawing.Point(12, 151);
            this.regionLabel.Name = "regionLabel";
            this.regionLabel.Size = new System.Drawing.Size(41, 13);
            this.regionLabel.TabIndex = 7;
            this.regionLabel.Text = "Region";
            // 
            // regionList
            // 
            this.regionList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionList.FormattingEnabled = true;
            this.regionList.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.regionList.Location = new System.Drawing.Point(12, 167);
            this.regionList.Name = "regionList";
            this.regionList.Size = new System.Drawing.Size(200, 21);
            this.regionList.TabIndex = 6;
            // 
            // activationTypeLabel
            // 
            this.activationTypeLabel.AutoSize = true;
            this.activationTypeLabel.Location = new System.Drawing.Point(12, 198);
            this.activationTypeLabel.Name = "activationTypeLabel";
            this.activationTypeLabel.Size = new System.Drawing.Size(81, 13);
            this.activationTypeLabel.TabIndex = 9;
            this.activationTypeLabel.Text = "Activation Type";
            // 
            // activationTypeList
            // 
            this.activationTypeList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activationTypeList.FormattingEnabled = true;
            this.activationTypeList.Items.AddRange(new object[] {
            "Barocde Only",
            "Magstripe Only",
            "Hybrid (Barcode + Magstripe)"});
            this.activationTypeList.Location = new System.Drawing.Point(12, 214);
            this.activationTypeList.Name = "activationTypeList";
            this.activationTypeList.Size = new System.Drawing.Size(200, 21);
            this.activationTypeList.TabIndex = 8;
            // 
            // jobQtyTextBox
            // 
            this.jobQtyTextBox.Location = new System.Drawing.Point(12, 400);
            this.jobQtyTextBox.Name = "jobQtyTextBox";
            this.jobQtyTextBox.Size = new System.Drawing.Size(200, 20);
            this.jobQtyTextBox.TabIndex = 10;
            this.jobQtyTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.jobQtyTextBox.WordWrap = false;
            // 
            // jobQtyLabel
            // 
            this.jobQtyLabel.AutoSize = true;
            this.jobQtyLabel.Location = new System.Drawing.Point(12, 384);
            this.jobQtyLabel.Name = "jobQtyLabel";
            this.jobQtyLabel.Size = new System.Drawing.Size(66, 13);
            this.jobQtyLabel.TabIndex = 11;
            this.jobQtyLabel.Text = "Job Quantity";
            // 
            // currencyLabel
            // 
            this.currencyLabel.AutoSize = true;
            this.currencyLabel.Location = new System.Drawing.Point(12, 291);
            this.currencyLabel.Name = "currencyLabel";
            this.currencyLabel.Size = new System.Drawing.Size(49, 13);
            this.currencyLabel.TabIndex = 13;
            this.currencyLabel.Text = "Currency";
            // 
            // currensyList
            // 
            this.currensyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.currensyList.FormattingEnabled = true;
            this.currensyList.Items.AddRange(new object[] {
            "EUR",
            "GBP",
            "PLN",
            "HKD"});
            this.currensyList.Location = new System.Drawing.Point(12, 307);
            this.currensyList.Name = "currensyList";
            this.currensyList.Size = new System.Drawing.Size(200, 21);
            this.currensyList.TabIndex = 12;
            // 
            // productDescriptionLabel
            // 
            this.productDescriptionLabel.AutoSize = true;
            this.productDescriptionLabel.Location = new System.Drawing.Point(12, 430);
            this.productDescriptionLabel.Name = "productDescriptionLabel";
            this.productDescriptionLabel.Size = new System.Drawing.Size(100, 13);
            this.productDescriptionLabel.TabIndex = 15;
            this.productDescriptionLabel.Text = "Product Description";
            // 
            // productDescriptionTextBox
            // 
            this.productDescriptionTextBox.Location = new System.Drawing.Point(12, 446);
            this.productDescriptionTextBox.Name = "productDescriptionTextBox";
            this.productDescriptionTextBox.Size = new System.Drawing.Size(200, 20);
            this.productDescriptionTextBox.TabIndex = 14;
            this.productDescriptionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.productDescriptionTextBox.WordWrap = false;
            // 
            // nanLabel
            // 
            this.nanLabel.AutoSize = true;
            this.nanLabel.Location = new System.Drawing.Point(12, 476);
            this.nanLabel.Name = "nanLabel";
            this.nanLabel.Size = new System.Drawing.Size(30, 13);
            this.nanLabel.TabIndex = 17;
            this.nanLabel.Text = "NAN";
            // 
            // nanTextBox
            // 
            this.nanTextBox.Location = new System.Drawing.Point(12, 492);
            this.nanTextBox.Name = "nanTextBox";
            this.nanTextBox.Size = new System.Drawing.Size(200, 20);
            this.nanTextBox.TabIndex = 16;
            this.nanTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nanTextBox.WordWrap = false;
            // 
            // iidLabel
            // 
            this.iidLabel.AutoSize = true;
            this.iidLabel.Location = new System.Drawing.Point(12, 522);
            this.iidLabel.Name = "iidLabel";
            this.iidLabel.Size = new System.Drawing.Size(21, 13);
            this.iidLabel.TabIndex = 19;
            this.iidLabel.Text = "IID";
            // 
            // iidTextBox
            // 
            this.iidTextBox.Location = new System.Drawing.Point(12, 538);
            this.iidTextBox.Name = "iidTextBox";
            this.iidTextBox.Size = new System.Drawing.Size(200, 20);
            this.iidTextBox.TabIndex = 18;
            this.iidTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.iidTextBox.WordWrap = false;
            // 
            // pidLabel
            // 
            this.pidLabel.AutoSize = true;
            this.pidLabel.Location = new System.Drawing.Point(12, 568);
            this.pidLabel.Name = "pidLabel";
            this.pidLabel.Size = new System.Drawing.Size(25, 13);
            this.pidLabel.TabIndex = 21;
            this.pidLabel.Text = "PID";
            // 
            // pidTextBox
            // 
            this.pidTextBox.Location = new System.Drawing.Point(12, 584);
            this.pidTextBox.Name = "pidTextBox";
            this.pidTextBox.Size = new System.Drawing.Size(200, 20);
            this.pidTextBox.TabIndex = 20;
            this.pidTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.pidTextBox.WordWrap = false;
            // 
            // epayIDLabel
            // 
            this.epayIDLabel.AutoSize = true;
            this.epayIDLabel.Location = new System.Drawing.Point(238, 104);
            this.epayIDLabel.Name = "epayIDLabel";
            this.epayIDLabel.Size = new System.Drawing.Size(45, 13);
            this.epayIDLabel.TabIndex = 23;
            this.epayIDLabel.Text = "Epay ID";
            // 
            // epayIdTextBox
            // 
            this.epayIdTextBox.Location = new System.Drawing.Point(238, 120);
            this.epayIdTextBox.Name = "epayIdTextBox";
            this.epayIdTextBox.Size = new System.Drawing.Size(200, 20);
            this.epayIdTextBox.TabIndex = 22;
            this.epayIdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.epayIdTextBox.WordWrap = false;
            // 
            // ipCodeLabel
            // 
            this.ipCodeLabel.AutoSize = true;
            this.ipCodeLabel.Location = new System.Drawing.Point(238, 151);
            this.ipCodeLabel.Name = "ipCodeLabel";
            this.ipCodeLabel.Size = new System.Drawing.Size(45, 13);
            this.ipCodeLabel.TabIndex = 25;
            this.ipCodeLabel.Text = "IP Code";
            // 
            // ipCodeTextBox
            // 
            this.ipCodeTextBox.Location = new System.Drawing.Point(238, 167);
            this.ipCodeTextBox.Name = "ipCodeTextBox";
            this.ipCodeTextBox.Size = new System.Drawing.Size(200, 20);
            this.ipCodeTextBox.TabIndex = 24;
            this.ipCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ipCodeTextBox.WordWrap = false;
            // 
            // retailBarcodeLAbel
            // 
            this.retailBarcodeLAbel.AutoSize = true;
            this.retailBarcodeLAbel.Location = new System.Drawing.Point(238, 199);
            this.retailBarcodeLAbel.Name = "retailBarcodeLAbel";
            this.retailBarcodeLAbel.Size = new System.Drawing.Size(77, 13);
            this.retailBarcodeLAbel.TabIndex = 27;
            this.retailBarcodeLAbel.Text = "Retail Barcode";
            // 
            // retailBarcodeTextBox
            // 
            this.retailBarcodeTextBox.Location = new System.Drawing.Point(238, 215);
            this.retailBarcodeTextBox.Name = "retailBarcodeTextBox";
            this.retailBarcodeTextBox.Size = new System.Drawing.Size(200, 20);
            this.retailBarcodeTextBox.TabIndex = 26;
            this.retailBarcodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.retailBarcodeTextBox.WordWrap = false;
            // 
            // jobTypeLabel
            // 
            this.jobTypeLabel.AutoSize = true;
            this.jobTypeLabel.Location = new System.Drawing.Point(12, 245);
            this.jobTypeLabel.Name = "jobTypeLabel";
            this.jobTypeLabel.Size = new System.Drawing.Size(51, 13);
            this.jobTypeLabel.TabIndex = 29;
            this.jobTypeLabel.Text = "Job Type";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Barocde Only",
            "Magstripe Only",
            "Hybrid (Barcode + Magstripe)"});
            this.comboBox1.Location = new System.Drawing.Point(12, 261);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(200, 21);
            this.comboBox1.TabIndex = 28;
            // 
            // denominationLabel
            // 
            this.denominationLabel.AutoSize = true;
            this.denominationLabel.Location = new System.Drawing.Point(12, 338);
            this.denominationLabel.Name = "denominationLabel";
            this.denominationLabel.Size = new System.Drawing.Size(72, 13);
            this.denominationLabel.TabIndex = 31;
            this.denominationLabel.Text = "Denomination";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 354);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 20);
            this.textBox1.TabIndex = 30;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox1.WordWrap = false;
            // 
            // humanReadableCheckBox
            // 
            this.humanReadableCheckBox.AutoSize = true;
            this.humanReadableCheckBox.Location = new System.Drawing.Point(241, 263);
            this.humanReadableCheckBox.Name = "humanReadableCheckBox";
            this.humanReadableCheckBox.Size = new System.Drawing.Size(136, 17);
            this.humanReadableCheckBox.TabIndex = 32;
            this.humanReadableCheckBox.Text = "DOD Human Readable";
            this.humanReadableCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.humanReadableCheckBox.UseVisualStyleBackColor = true;
            // 
            // wtcCheckBox
            // 
            this.wtcCheckBox.AutoSize = true;
            this.wtcCheckBox.Location = new System.Drawing.Point(241, 309);
            this.wtcCheckBox.Name = "wtcCheckBox";
            this.wtcCheckBox.Size = new System.Drawing.Size(51, 17);
            this.wtcCheckBox.TabIndex = 33;
            this.wtcCheckBox.Text = "WTC";
            this.wtcCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.wtcCheckBox.UseVisualStyleBackColor = true;
            // 
            // wtcQuantityLabel
            // 
            this.wtcQuantityLabel.AutoSize = true;
            this.wtcQuantityLabel.Location = new System.Drawing.Point(298, 292);
            this.wtcQuantityLabel.Name = "wtcQuantityLabel";
            this.wtcQuantityLabel.Size = new System.Drawing.Size(46, 13);
            this.wtcQuantityLabel.TabIndex = 35;
            this.wtcQuantityLabel.Text = "Quantity";
            this.wtcQuantityLabel.Visible = false;
            this.wtcQuantityLabel.Click += new System.EventHandler(this.wtcQuantityLabel_Click);
            // 
            // wtcQuantitTextBox
            // 
            this.wtcQuantitTextBox.Location = new System.Drawing.Point(298, 308);
            this.wtcQuantitTextBox.Name = "wtcQuantitTextBox";
            this.wtcQuantitTextBox.Size = new System.Drawing.Size(64, 20);
            this.wtcQuantitTextBox.TabIndex = 34;
            this.wtcQuantitTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.wtcQuantitTextBox.Visible = false;
            this.wtcQuantitTextBox.WordWrap = false;
            this.wtcQuantitTextBox.TextChanged += new System.EventHandler(this.wtcQuantitTextBox_TextChanged);
            // 
            // environmentLabel
            // 
            this.environmentLabel.AutoSize = true;
            this.environmentLabel.Location = new System.Drawing.Point(368, 290);
            this.environmentLabel.Name = "environmentLabel";
            this.environmentLabel.Size = new System.Drawing.Size(66, 13);
            this.environmentLabel.TabIndex = 37;
            this.environmentLabel.Text = "Environment";
            this.environmentLabel.Visible = false;
            this.environmentLabel.Click += new System.EventHandler(this.environmentLabel_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "IT2",
            "UT2"});
            this.comboBox2.Location = new System.Drawing.Point(368, 306);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(70, 21);
            this.comboBox2.TabIndex = 36;
            this.comboBox2.Visible = false;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1469, 638);
            this.Controls.Add(this.environmentLabel);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.wtcQuantityLabel);
            this.Controls.Add(this.wtcQuantitTextBox);
            this.Controls.Add(this.wtcCheckBox);
            this.Controls.Add(this.humanReadableCheckBox);
            this.Controls.Add(this.denominationLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.jobTypeLabel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.retailBarcodeLAbel);
            this.Controls.Add(this.retailBarcodeTextBox);
            this.Controls.Add(this.ipCodeLabel);
            this.Controls.Add(this.ipCodeTextBox);
            this.Controls.Add(this.epayIDLabel);
            this.Controls.Add(this.epayIdTextBox);
            this.Controls.Add(this.pidLabel);
            this.Controls.Add(this.pidTextBox);
            this.Controls.Add(this.iidLabel);
            this.Controls.Add(this.iidTextBox);
            this.Controls.Add(this.nanLabel);
            this.Controls.Add(this.nanTextBox);
            this.Controls.Add(this.productDescriptionLabel);
            this.Controls.Add(this.productDescriptionTextBox);
            this.Controls.Add(this.currencyLabel);
            this.Controls.Add(this.currensyList);
            this.Controls.Add(this.jobQtyLabel);
            this.Controls.Add(this.jobQtyTextBox);
            this.Controls.Add(this.activationTypeLabel);
            this.Controls.Add(this.activationTypeList);
            this.Controls.Add(this.regionLabel);
            this.Controls.Add(this.regionList);
            this.Controls.Add(this.integratorLabel);
            this.Controls.Add(this.integratorsList);
            this.Controls.Add(this.importXmlButton);
            this.Controls.Add(this.xmlDataGridView);
            this.Name = "Form1";
            this.Text = "Minerva Loader v1.0";
            ((System.ComponentModel.ISupportInitialize)(this.xmlDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView xmlDataGridView;
        private System.Windows.Forms.Button importXmlButton;
        private System.Windows.Forms.ComboBox integratorsList;
        private System.Windows.Forms.Label integratorLabel;
        private System.Windows.Forms.Label regionLabel;
        private System.Windows.Forms.ComboBox regionList;
        private System.Windows.Forms.Label activationTypeLabel;
        private System.Windows.Forms.ComboBox activationTypeList;
        private System.Windows.Forms.TextBox jobQtyTextBox;
        private System.Windows.Forms.Label jobQtyLabel;
        private System.Windows.Forms.Label currencyLabel;
        private System.Windows.Forms.ComboBox currensyList;
        private System.Windows.Forms.Label productDescriptionLabel;
        private System.Windows.Forms.TextBox productDescriptionTextBox;
        private System.Windows.Forms.Label nanLabel;
        private System.Windows.Forms.TextBox nanTextBox;
        private System.Windows.Forms.Label iidLabel;
        private System.Windows.Forms.TextBox iidTextBox;
        private System.Windows.Forms.Label pidLabel;
        private System.Windows.Forms.TextBox pidTextBox;
        private System.Windows.Forms.Label epayIDLabel;
        private System.Windows.Forms.TextBox epayIdTextBox;
        private System.Windows.Forms.Label ipCodeLabel;
        private System.Windows.Forms.TextBox ipCodeTextBox;
        private System.Windows.Forms.Label retailBarcodeLAbel;
        private System.Windows.Forms.TextBox retailBarcodeTextBox;
        private System.Windows.Forms.Label jobTypeLabel;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label denominationLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox humanReadableCheckBox;
        private System.Windows.Forms.CheckBox wtcCheckBox;
        private System.Windows.Forms.Label wtcQuantityLabel;
        private System.Windows.Forms.TextBox wtcQuantitTextBox;
        private System.Windows.Forms.Label environmentLabel;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

