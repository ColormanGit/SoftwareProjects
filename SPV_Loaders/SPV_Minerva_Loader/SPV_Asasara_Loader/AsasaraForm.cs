using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Data.OleDb;
using System.Data;

namespace SPV_Asasara_Loader
{
    public partial class AsasaraForm : Form
    {
        private AsasaraJob[] jobsArray;

        public AsasaraForm()
        {
            InitializeComponent();
        }


        // Import Excel file
        private void importExcelButton_Click(object sender, EventArgs e)
        {
            //// Create OpenFile Dialog object to allow the user select Excel file
            //OpenFileDialog openFileDialog = new OpenFileDialog();

            //openFileDialog.InitialDirectory = @"C:\";
            //openFileDialog.Title = "Browse Excel Files";

            //openFileDialog.CheckFileExists = true;
            //openFileDialog.CheckPathExists = true;

            //openFileDialog.DefaultExt = "xls";
            //openFileDialog.Filter = "Excel Worksheets| *.xls|Excel 2007 files| *.xlsx";
            //openFileDialog.FilterIndex = 2;
            //openFileDialog.RestoreDirectory = true;

            //openFileDialog.ReadOnlyChecked = true;
            //openFileDialog.ShowReadOnly = true;

            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string file = openFileDialog.FileName;

            //    try
            //    {
            //        string text = File.ReadAllText(file);
            //        int size = text.Length;
            //        Debug.WriteLine(text);
            //    }
            //    catch (IOException)
            //    {
            //    }
            //}


            string filePath = string.Empty;
            string fileExt = string.Empty;
            OpenFileDialog file = new OpenFileDialog();//open dialog to choose file
            if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK)//if there is a file choosen by the user
            {
                filePath = file.FileName;//get the path of the file
                fileExt = Path.GetExtension(filePath);//get the file extension
                if (fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                {
                    try
                    {
                        DataTable dtExcel = new DataTable();
                        dtExcel = ReadExcel(filePath, fileExt);//read excel file
                        dataGridView1.Visible = true;
                        dataGridView1.DataSource = dtExcel;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);//custom messageBox to show error
                }
            }
        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)//compare the extension of the file
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';";//for below excel 2007
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";//for above excel 2007
            using (OleDbConnection con = new OleDbConnection(conn))
            {

                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("select * from [OCS (Google_generates)$]", con);//read data from sheet "OCS (Google_generates)"
                    oleAdpt.Fill(dtexcel);//fill excel data into dataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            return dtexcel;
        }


        private void importXmlButton2_Click(object sender, EventArgs e)
        {
            // Create OpenFile Dialog object to allow the user select XML file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "Xml files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            // Create Xdocument object and declare xmlReader
            System.Xml.Linq.XDocument xmlDoc = new XDocument();
            XmlReader xmlReader;


            // Load in XML file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String xmlPath = openFileDialog.FileName;   // XML file path
                    xmlReader = XmlReader.Create(xmlPath); // Create XmlReader with XML file path
                    xmlDoc = XDocument.Load(xmlReader); // Load XML file

                    jobsArray = new AsasaraJob[xmlDoc.Root.Elements().ElementAt(0).Elements().Count()];

                    int numOfJobs = xmlDoc.Root.Elements().ElementAt(0).Elements().Count();
                    int numOfFieldsInJob = xmlDoc.Root.Elements().ElementAt(0).Elements().ElementAt(0).Elements().Count();

                    // Create orders from XML file
                    for (int i = 0; i < numOfJobs; i++)
                    {
                        // Create and fill string array with values from XML file
                        string[] values = new string[xmlDoc.Root.Elements().ElementAt(0).Elements().ElementAt(0).Elements().Count()];
                        for (int j = 0; j < numOfFieldsInJob; j++)
                        {
                            if (xmlDoc.Root.Elements().ElementAt(0).Elements().ElementAt(i).Elements().ElementAt(j).Elements().ElementAt(0).Value == "")
                            {
                                values[j] = "N/A";
                            }
                            else
                            {
                                values[j] = xmlDoc.Root.Elements().ElementAt(0).Elements().ElementAt(i).Elements().ElementAt(j).Elements().ElementAt(0).Value;
                            }
                        }
                        jobIDNumericUpDown.Maximum = xmlDoc.Root.Elements().ElementAt(0).Elements().Count();
                        // Create new order and place it in array of orders
                        jobsArray[i] = new AsasaraJob((i + 1) + "", values);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                // Initiate GUI and order elements for first order
                fillGuiWithAutoInput(0, jobsArray);
                fillGuiWithManualInput(0, jobsArray);
                //updateManualInputData(0, jobsArray);
                //minervaTotaljobsTextBox.Text = jobIDNumericUpDown.Maximum + "";
                //currentOrderTextBox.Text = jobIDNumericUpDown.Value + "";
            }
        }


        // Change Active MinervaJob
        private void jobIDNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (jobsArray != null)
            {
                fillGuiWithAutoInput((int)jobIDNumericUpDown.Value - 1, jobsArray);
                fillGuiWithManualInput((int)jobIDNumericUpDown.Value - 1, jobsArray);
                currentOrderTextBox.Text = jobIDNumericUpDown.Value + "";
            }
        }

        // Populate GUI fields with input from a file
        private void fillGuiWithAutoInput(int index, AsasaraJob[] jobs)
        {

            jobIDNumericUpDown.Value = Int32.Parse(jobs[index].autoInputArray[0]);
            jobNumberTextBox.Text = jobs[index].autoInputArray[1];
            //dueDateTextBox.Text = jobs[index].autoInputArray[2];
            //purchaseOrderNoTextBox.Text = jobs[index].autoInputArray[3];
            //purchaseOrderLineTextBox.Text = jobs[index].autoInputArray[4];
            salesOrderNumberTextBox.Text = jobs[index].autoInputArray[5];
            //customerAccountTextBox.Text = jobs[index].autoInputArray[6];
            buildQuantityTextBox.Text = jobs[index].autoInputArray[7];
            //ascmOrderIDTextBox.Text = jobs[index].autoInputArray[8];
            //endCustomerTextBox.Text = jobs[index].autoInputArray[9];
            //activationSystemTextBox.Text = jobs[index].autoInputArray[10];
            //productTypeTextBox.Text = jobs[index].autoInputArray[11];
            //erpMaterialCodeTextBox.Text = jobs[index].autoInputArray[12];
            //integratorPartIDTextBox.Text = jobs[index].autoInputArray[13];
            //integratorIDTextBox.Text = jobs[index].autoInputArray[14];
            //activationTypeTextBox.Text = jobs[index].autoInputArray[15];
            //partNumberTextBox.Text = jobs[index].autoInputArray[16];
            //retailBarcodeTextBox.Text = jobs[index].autoInputArray[17];
            //retailBarcodeTypeTextBox.Text = jobs[index].autoInputArray[18];

        }

        // Populate GUI fields with manual input if it's not absent
        private void fillGuiWithManualInput(int index, AsasaraJob[] jobs)
        {
            if (jobs[index].jobType == null || jobs[index].jobType == "N/A")
            {
                minervaJobTypeComboBox.ResetText();
                minervaJobTypeComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaJobTypeComboBox.SelectedItem = jobs[index].jobType;
            }

            if (jobs[index].integrator == null || jobs[index].integrator == "N/A")
            {
                minervaIntegratorComboCox.ResetText();
                minervaIntegratorComboCox.SelectedIndex = -1;
            }
            else
            {
                minervaIntegratorComboCox.SelectedItem = jobs[index].integrator;
            }

            if (jobs[index].region == null || jobs[index].region == "N/A")
            {
                minervaRegionComboBox.ResetText();
                minervaRegionComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaRegionComboBox.SelectedItem = jobs[index].region;
            }

            if (jobs[index].currency == null || jobs[index].currency == "N/A")
            {
                minervaCurrencyComboBox.ResetText();
                minervaCurrencyComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaCurrencyComboBox.SelectedItem = jobs[index].currency;
            }

            if (jobs[index].productDescription == null || jobs[index].productDescription == "N/A")
            {
                minervaProductDescriptionTextBox.Clear();
            }
            else
            {
                minervaProductDescriptionTextBox.Text = jobs[index].productDescription;
            }

            if (jobs[index].regionIntegratorID == null || jobs[index].regionIntegratorID == "N/A")
            {
                minervaRegionIntegratorIDTextBox.Clear();
            }
            else
            {
                minervaRegionIntegratorIDTextBox.Text = jobs[index].regionIntegratorID;
            }

            if (jobs[index].countryIncommRetailer == null || jobs[index].countryIncommRetailer == "N/A")
            {
                minervaCountryIncommRetailerTextBox.Clear();
            }
            else
            {
                minervaCountryIncommRetailerTextBox.Text = jobs[index].countryIncommRetailer;
            }

            if (jobs[index].packQuantity == null || jobs[index].packQuantity == "N/A")
            {
                minervaPackQuantityComboBox.ResetText();
                minervaPackQuantityComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaPackQuantityComboBox.SelectedItem = jobs[index].packQuantity;
            }

            if (jobs[index].boxQuantitySize == null || jobs[index].boxQuantitySize == "N/A")
            {
                minervaBoxQuantityComboBox.ResetText();
                minervaBoxQuantityComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaBoxQuantityComboBox.SelectedItem = jobs[index].boxQuantitySize;
            }

            if (jobs[index].specialInstructions == null || jobs[index].specialInstructions == "N/A")
            {
                minervaSpecialInstructionsTextBox.Clear();
            }
            else
            {
                minervaSpecialInstructionsTextBox.Text = jobs[index].specialInstructions;
            }

            if (jobs[index].isDecimal)
            {
                minervaDenomDecimalCheckBox.Checked = true;
            }
            else
            {
                minervaDenomDecimalCheckBox.Checked = false;
            }

            if (jobs[index].hasPPT)
            {
                minervaPptCheckBox.Checked = true;
            }
            else
            {
                minervaPptCheckBox.Checked = false;
            }

            if (jobs[index].hasWTC)
            {
                minervaWtcCheckBox.Checked = true;
            }
            else
            {
                minervaWtcCheckBox.Checked = false;
            }

            if (minervaPptCheckBox.Checked)
            {
                if (jobs[index].pptQuanity != null)
                {
                    minervaPptQtyNumericUpDown.Value = Int32.Parse(jobs[index].pptQuanity);
                }
                else
                {
                    minervaPptQtyNumericUpDown.Value = 0;
                }
            }
            else
            {
                minervaPptQtyNumericUpDown.Value = 0;
            }

            if (minervaWtcCheckBox.Checked)
            {
                if (jobs[index].wtcQuantity != null)
                {
                    minervaWtcQtyNumericUpDown.Value = Int32.Parse(jobs[index].wtcQuantity);
                }
                else
                {
                    minervaWtcQtyNumericUpDown.Value = 0;
                }


                if (jobs[index].wtcDenomination != null)
                {
                    minervaWtcDenomNumericUpDown.Value = Int32.Parse(jobs[index].wtcDenomination);
                }
                else
                {
                    minervaWtcDenomNumericUpDown.Value = 16;
                }
            }
            else
            {
                minervaWtcQtyNumericUpDown.Value = 0;
                minervaWtcDenomNumericUpDown.Value = 0;
            }

            minervaHumanReadableCheckBox.Checked = jobs[index].dodHumanReadable.Equals("True") ? true : false;

            if (jobs[index].denomination != null)
            {
                minervaDenominationNumericUpDown.Value = Int32.Parse(jobs[index].denomination);
            }
            else
            {
                minervaDenominationNumericUpDown.Value = 0;
            }

            if (jobs[index].jobQuantity != null)
            {
                minervaJobQtyNumericUpDown.Value = Int32.Parse(jobs[index].jobQuantity);
            }
            else
            {
                minervaJobQtyNumericUpDown.Value = 0;
            }

            if (jobs[index].palletQuantity != null)
            {
                minervaPalletQtyNumericUpDown.Value = Int32.Parse(jobs[index].palletQuantity);
            }
            else
            {
                minervaPalletQtyNumericUpDown.Value = 0;
            }

            if (jobs[index].bhnPackRetailCode != null)
            {
                minervaBhnPackRetailCodeNumericUpDown.Value = Int32.Parse(jobs[index].bhnPackRetailCode);
            }
            else
            {
                minervaBhnPackRetailCodeNumericUpDown.Value = 0;
            }

            if (jobs[index].bhnBoxRetailCode != null)
            {
                minervaBhnBoxRetailCodeNumericUpDown.Value = Int32.Parse(jobs[index].bhnBoxRetailCode);
            }
            else
            {
                minervaBhnBoxRetailCodeNumericUpDown.Value = 0;
            }

            if (jobs[index].bhnPalletRetailCode != null)
            {
                minervaBhnPalletRetailCodeNumericUpDown.Value = Int32.Parse(jobs[index].bhnPalletRetailCode);
            }
            else
            {
                minervaBhnPalletRetailCodeNumericUpDown.Value = 0;
            }
        }



    }
}
