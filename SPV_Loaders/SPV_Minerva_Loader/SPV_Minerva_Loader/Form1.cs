using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;


namespace SPV_Minerva_Loader 
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ////IMPORT EXCEL FILE
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string filePath = string.Empty;
        //    string fileExt = string.Empty;
        //    OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
        //    if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
        //    {
        //        filePath = file.FileName; //get the path of the file  
        //        fileExt = Path.GetExtension(filePath); //get the file extension  
        //        if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
        //        {
        //            try
        //            {
        //                DataTable dtExcel = new DataTable();
        //                dtExcel = ReadExcel(filePath, fileExt); //read excel file  
        //                dataGridView1.Visible = true;
        //                dataGridView1.DataSource = dtExcel;
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message.ToString());
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
        //        }
        //    }
        //}

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  
            using (OleDbConnection con = new OleDbConnection(conn))
            {
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [Sheet1$]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  
                }
                catch (Exception ex)
                { throw ex; }
            }
            return dtexcel;
        }

        private void importXmlButtonClick(object sender, EventArgs e)
        {
            //XmlReader xmlFile;
            //xmlFile = XmlReader.Create("Orders_Test.xml", new XmlReaderSettings());
            //DataSet ds = new DataSet();
            //ds.ReadXml(xmlFile);
            //dataGridView3.DataSource = ds.Tables[0];

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML Files (*.xml)|*.xml";
            ofd.FilterIndex = 0;
            ofd.DefaultExt = "xml";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (!String.Equals(Path.GetExtension(ofd.FileName),
                                   ".xml",
                                   StringComparison.OrdinalIgnoreCase))
                {
                    // Invalid file type selected; display an error.
                    MessageBox.Show("The type of the selected file is not supported by this application. You must select an XML file.",
                                    "Invalid File Type",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    // Optionally, force the user to select another file.
                    // ...
                }
                else
                {
                    XDocument xDoc;
                    xDoc = XDocument.Load(ofd.FileName);
                    var result = from q in xDoc.Descendants("CD")
                                 select new CD

                                 {
                                     Title = q.Element("TITLE").Value,
                                     Artist = q.Element("ARTIST").Value
                                 };


                    string[] ids = result.Select(x => x.Title).ToArray();
                    xmlDataGridView.DataSource = ids;

                    //foreach (var cd in result)
                    //{
                    //    //MessageBox.Show("Artist: {0}", cd.Artist.ToString());
                    //    MessageBox.Show(ids.ToString());
                    //}
                }
            }
        }

        public class CD
        {
            public string Artist { get; set; }
            public string Title { get; set; }
        }

        // Turn on/off visibility for WTCs settings
        private void wtcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (wtcCheckBox.Checked)
            {
                wtcQuantitTextBox.Visible = true;
                wtcQuantityLabel.Visible = true;
                wtcEnvironmentLabel.Visible = true;
                wtcEnvironmentComboBox.Visible = true;
            }
            else
            {
                wtcQuantitTextBox.Visible = false;
                wtcQuantityLabel.Visible = false;
                wtcEnvironmentLabel.Visible = false;
                wtcEnvironmentComboBox.Visible = false;
            }
        }

        // Turn on/off visibility for PPT cards settings
        private void pptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (pptCheckBox.Checked)
            {
                pptQtyNumericUpDown.Visible = true;
                pptQuantityLabel.Visible = true;
            }
            else
            {
                pptQtyNumericUpDown.Visible = false;
                pptQuantityLabel.Visible = false;
            }
        }

        // Turn on/off visibility for BHN packaging details input and set the correct Region comboBox
        private void integratorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (integratorsList.SelectedIndex == 1)
            {
                bhnPackRetailCodeLabel.Visible = true;
                bhnPackRetailCodeTextBox.Visible = true;

                bhnBoxRetailCodeLabel.Visible = true;
                bhnBoxRetailCodeTextBox.Visible = true;

                bhnPalletRetailCodeLabel.Visible = true;
                bhnPalletRetailCodeTextBox.Visible = true;
            }
            else
            {
                bhnPackRetailCodeLabel.Visible = false;
                bhnPackRetailCodeTextBox.Visible = false;

                bhnBoxRetailCodeLabel.Visible = false;
                bhnBoxRetailCodeTextBox.Visible = false;

                bhnPalletRetailCodeLabel.Visible = false;
                bhnPalletRetailCodeTextBox.Visible = false;
            }

            switch (integratorsList.SelectedIndex)
            {
                case 0:
                    aosRegionComboBox.Visible = true;
                    bhnRegionComboBox.Visible = false;
                    epayRegionComboBox.Visible = false;
                    incommRegionComboBox.Visible = false;
                    break;

                case 1:
                    bhnRegionComboBox.Visible = true;
                    aosRegionComboBox.Visible = false;
                    epayRegionComboBox.Visible = false;
                    incommRegionComboBox.Visible = false;
                    break;

                case 2:
                    epayRegionComboBox.Visible = true;
                    aosRegionComboBox.Visible = false;
                    bhnRegionComboBox.Visible = false;
                    incommRegionComboBox.Visible = false;
                    break;

                case 3:
                    incommRegionComboBox.Visible = true;
                    aosRegionComboBox.Visible = false;
                    bhnRegionComboBox.Visible = false;
                    epayRegionComboBox.Visible = false;
                    break;
                default:
                    aosRegionComboBox.Visible = false;
                    bhnRegionComboBox.Visible = false;
                    epayRegionComboBox.Visible = false;
                    incommRegionComboBox.Visible = false;
                    break;
            }
        }

        // Turn on/off decimal places for Job Denomination
        private void denomDecimalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (denomDecimalcheckBox.Checked)
            {
                denominationNumericUpDown.DecimalPlaces = 2;
            }
            else
            {
                denominationNumericUpDown.DecimalPlaces = 0;
            }
        }

        // Turn on/off decimal places for WTCs Denomination
        private void wtcDenomDecimalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (wtcDenomDecimalcheckBox.Checked)
            {
                wtcDenomNumericUpDown.DecimalPlaces = 2;
            }
            else
            {
                wtcDenomNumericUpDown.DecimalPlaces = 0;
            }
        }
        
        // Import XML testing
        private void importXmlButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "Xml files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XDocument xmlDoc;
                    xmlDoc = XDocument.Load(openFileDialog.FileName);
                    DataSet ds = new DataSet();
                    ds.ReadXml(openFileDialog.FileName);

                    xmlDataGridView.AutoGenerateColumns = true;
                    xmlDataGridView.DataSource = ds; // dataset
                    xmlDataGridView.DataMember = "Order"; // table name you need to show

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
