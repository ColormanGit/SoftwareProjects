using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SPV_Athena_Loader
{
    public partial class AthenaForm : Form
    {

        private AthenaJob[] jobsArray;

        public AthenaForm()
        {
            InitializeComponent();
            jobIDNumericUpDown.Minimum = 1;
        }

        // Import Excel file
        private void importExcelButton_Click(object sender, EventArgs e)
        {
            // Create OpenFile Dialog object to allow the user select Excel file
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Browse Excel Files";

            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            openFileDialog.DefaultExt = "xls";
            openFileDialog.Filter = "Excel Worksheets| *.xls|Excel 2007 files| *.xlsx";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            openFileDialog.ReadOnlyChecked = true;
            openFileDialog.ShowReadOnly = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openFileDialog.FileName;

                try
                {
                    string text = File.ReadAllText(file);
                    int size = text.Length;
                    Debug.WriteLine(text);
                }
                catch (IOException)
                {
                }

            }
        }

        // Import XML file
        private void importXmlButton_Click(object sender, EventArgs e)
        {

            // Create OpenFile Dialog object to allow the user select XML file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "Xml files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            // Create Xdocument object and declare xmlReader
            XDocument xmlDoc = new XDocument();
            XmlReader xmlReader;


            // Load in XML file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    String xmlPath = openFileDialog.FileName;   // XML file path
                    xmlReader = XmlReader.Create(xmlPath); // Create XmlReader with XML file path
                    xmlDoc = XDocument.Load(xmlReader); // Load XML file

                    int numOfJobs = xmlDoc.Root.Elements().Count();
                    int numOfFieldsInJob = xmlDoc.Root.Elements().ElementAt(0).Elements().ElementAt(0).Elements().Count();

                    jobsArray = new AthenaJob[numOfJobs];
                    jobIDNumericUpDown.Maximum = numOfJobs;

                    for (int i = 0; i < numOfJobs; i++)
                    {
                        // Create and fill string array with values from XML file
                        string[] values = new string[numOfFieldsInJob];
                        for (int j = 0; j < numOfFieldsInJob; j++)
                        {
                            if (xmlDoc.Root.Elements().ElementAt(i).Elements().ElementAt(0).Elements().ElementAt(j).Elements().ElementAt(0).Value == "")
                            {
                                values[j] = "N/A";
                            }
                            else
                            {
                                values[j] = xmlDoc.Root.Elements().ElementAt(i).Elements().ElementAt(0).Elements().ElementAt(j).Elements().ElementAt(0).Value;
                            }
                        }
                        // Create new order and place it in array of orders
                        jobsArray[i] = new AthenaJob((i + 1) + "", values);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                // Initiate GUI and order elements for first order
                fillGuiWithAutoInput(0, jobsArray);
                fillGuiWithManualInput(0, jobsArray);
                for (int i = 0; i < jobsArray.Length; i++)
                {
                    updateManualInputData(i, jobsArray);
                }
                totalJobsTextBox.Text = jobIDNumericUpDown.Maximum + "";
                currentJobTextBox.Text = jobIDNumericUpDown.Value + "";
            }
        }

        // Generate XML string fron an AthenaJob Object Array
        public static string getXMLFromObject(object[] o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
        
        // Change Active AthenaJob
        private void jobIDNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (jobsArray != null)
            {
                fillGuiWithAutoInput((int)jobIDNumericUpDown.Value - 1, jobsArray);
                fillGuiWithManualInput((int)jobIDNumericUpDown.Value - 1, jobsArray);
                currentJobTextBox.Text = jobIDNumericUpDown.Value + "";
            }

        }

        // Populate GUI fields with input from a file
        private void fillGuiWithAutoInput(int index, AthenaJob[] jobs)
        {

            jobIDNumericUpDown.Value = Int32.Parse(jobs[index].autoInputArray[0]);
            jobNumberTextBox.Text = jobs[index].autoInputArray[1];
            dueDateTextBox.Text = jobs[index].autoInputArray[2];
            purchaseOrderNoTextBox.Text = jobs[index].autoInputArray[3];
            purchaseOrderLineTextBox.Text = jobs[index].autoInputArray[4];
            salesOrderNumberTextBox.Text = jobs[index].autoInputArray[5];
            customerAccountTextBox.Text = jobs[index].autoInputArray[6];
            buildQuantityTextBox.Text = jobs[index].autoInputArray[7];
            ascmOrderIDTextBox.Text = jobs[index].autoInputArray[8];
            endCustomerTextBox.Text = jobs[index].autoInputArray[9];
            activationSystemTextBox.Text = jobs[index].autoInputArray[10];
            productTypeTextBox.Text = jobs[index].autoInputArray[11];
            erpMaterialCodeTextBox.Text = jobs[index].autoInputArray[12];
            integratorPartIDTextBox.Text = jobs[index].autoInputArray[13];
            integratorIDTextBox.Text = jobs[index].autoInputArray[14];
            activationTypeTextBox.Text = jobs[index].autoInputArray[15];
            partNumberTextBox.Text = jobs[index].autoInputArray[16];
            retailBarcodeTextBox.Text = jobs[index].autoInputArray[17];
            retailBarcodeTypeTextBox.Text = jobs[index].autoInputArray[18];

        }

        // Populate GUI fields with manual input if it's not absent
        private void fillGuiWithManualInput(int index, AthenaJob[] jobs)
        {
            if (jobs[index].client == null || jobs[index].client == "N/A")
            {
                clientComboBox.ResetText();
                clientComboBox.SelectedIndex = -1;
            }
            else
            {
                clientComboBox.SelectedItem = jobs[index].client;
            }

            if (jobs[index].contractType == null || jobs[index].client == "N/A")
            {
                contractTypeComboBox.ResetText();
                contractTypeComboBox.SelectedIndex = -1;
            }
            else
            {
                contractTypeComboBox.SelectedItem = jobs[index].contractType;
            }

            if (jobs[index].artworkPartNumber == null || jobs[index].artworkPartNumber == "N/A")
            {
                artworkPartNumberSkuTextBox.Clear();
            }
            else
            {
                artworkPartNumberSkuTextBox.Text = jobs[index].artworkPartNumber;
            }

            if (jobs[index].skuDescription == null || jobs[index].skuDescription == "N/A")
            {
                skuDescriptionTextBox.Clear();;
            }
            else
            {
                skuDescriptionTextBox.Text = jobs[index].skuDescription;
            }

            if (jobs[index].jobQty != null || jobs[index].jobQty == "0")
            {
                jobQtyNumericUpDown.Value = Int32.Parse(jobs[index].jobQty);
            }
            else
            {
                jobQtyNumericUpDown.Value = 0;
            }

            if (jobs[index].faiStart == null || jobs[index].faiStart == "N/A")
            {
                faiStartComboBox.ResetText();
                faiStartComboBox.SelectedIndex = -1;
            }
            else
            {
                faiStartComboBox.SelectedItem = jobs[index].faiStart;
            }

            if (jobs[index].faiEnd == null || jobs[index].faiEnd == "N/A")
            {
                faiEndComboBox.ResetText();
                faiEndComboBox.SelectedIndex = -1;
            }
            else
            {
                faiEndComboBox.SelectedItem = jobs[index].faiEnd;
            }

            if (jobs[index].packQty == null || jobs[index].packQty == "N/A")
            {
                packQtyComboBox.ResetText();
                packQtyComboBox.SelectedIndex = -1;
            }
            else
            {
                packQtyComboBox.SelectedItem = jobs[index].packQty;
            }

            if (jobs[index].boxQty == null || jobs[index].boxQty == "N/A")
            {
                boxQtyComboBox.ResetText();
                boxQtyComboBox.SelectedIndex = -1;
            }
            else
            {
                boxQtyComboBox.SelectedItem = jobs[index].boxQty;
            }

            if (jobs[index].palletQty != null || jobs[index].palletQty == "0")
            {
                palletQtyNumericUpDown.Value = Int32.Parse(jobs[index].palletQty);
            }
            else
            {
                palletQtyNumericUpDown.Value = 0;
            }

            if (jobs[index].jobType == null || jobs[index].jobType == "N/A")
            {
                jobTypeComboBox.ResetText();
                jobTypeComboBox.SelectedIndex = -1;
            }
            else
            {
                jobTypeComboBox.SelectedItem = jobs[index].jobType;
            }

            if (jobs[index].activationTypeID == null || jobs[index].activationTypeID == "N/A")
            {
                activationTypeIdTextBox.Clear();
            }
            else
            {
                activationTypeIdTextBox.Text = jobs[index].activationTypeID;
            }

            if (jobs[index].denomination != null || jobs[index].denomination == "0")
            {
                denominationNumericUpDown.Value = Int32.Parse(jobs[index].denomination);
            }
            else
            {
                denominationNumericUpDown.Value = 0;
            }

            if (jobs[index].denominationCurrency == null || jobs[index].denominationCurrency == "N/A")
            {
                currencyComboBox.ResetText();
                currencyComboBox.SelectedIndex = -1;
            }
            else
            {
                currencyComboBox.SelectedItem = jobs[index].denominationCurrency;
            }

            if (jobs[index].isDecimal)
            {
                decimalCheckBox.Checked = true;
            }
            else
            {
                decimalCheckBox.Checked = false;
            }

            if (jobs[index].intelJobType == null || jobs[index].intelJobType == "N/A")
            {
                intelJobTypeComboBox.ResetText();
                intelJobTypeComboBox.SelectedIndex = -1;
            }
            else
            {
                intelJobTypeComboBox.SelectedItem = jobs[index].intelJobType;
            }

            if (jobs[index].jobComments == null || jobs[index].jobComments == "N/A")
            {
                jobCommentsTextBox.Clear(); ;
            }
            else
            {
                jobCommentsTextBox.Text = jobs[index].jobComments;
            }

            if (jobs[index].country == null || jobs[index].country == "N/A")
            {
                countryTextBox.Clear(); ;
            }
            else
            {
                countryTextBox.Text = jobs[index].country;
            }

            if (jobs[index].alternativePartNumber == null || jobs[index].alternativePartNumber == "N/A")
            {
                alternativePartNumberTextBox.Clear(); ;
            }
            else
            {
                alternativePartNumberTextBox.Text = jobs[index].alternativePartNumber;
            }

            if (jobs[index].alternativePartNumber == null || jobs[index].alternativePartNumber == "N/A")
            {
                alternativePartNumberTextBox.Clear(); ;
            }
            else
            {
                alternativePartNumberTextBox.Text = jobs[index].alternativePartNumber;
            }

            if (jobs[index].packagingGTIN == null || jobs[index].packagingGTIN == "N/A")
            {
                packagingGtinTextBox.Clear(); ;
            }
            else
            {
                packagingGtinTextBox.Text = jobs[index].packagingGTIN;
            }

            if (jobs[index].incommProductDescription == null || jobs[index].incommProductDescription == "N/A")
            {
                incommProductDescriptionTextBox.Clear(); ;
            }
            else
            {
                incommProductDescriptionTextBox.Text = jobs[index].incommProductDescription;
            }

            if (jobs[index].pkpn == null || jobs[index].pkpn == "N/A")
            {
                pkpnTextBox.Clear(); ;
            }
            else
            {
                pkpnTextBox.Text = jobs[index].pkpn;
            }

            if (jobs[index].bomFileName == null || jobs[index].bomFileName == "N/A")
            {
                bomFileNameTextBox.Clear(); ;
            }
            else
            {
                bomFileNameTextBox.Text = jobs[index].bomFileName;
            }

            if (jobs[index].bomComment1 == null || jobs[index].bomComment1 == "N/A")
            {
                bomComment1TextBox.Clear(); ;
            }
            else
            {
                bomComment1TextBox.Text = jobs[index].bomComment1;
            }

            if (jobs[index].bomComment2 == null || jobs[index].bomComment2 == "N/A")
            {
                bomComment2TextBox.Clear(); ;
            }
            else
            {
                bomComment2TextBox.Text = jobs[index].bomComment2;
            }

            if (jobs[index].bomComment3 == null || jobs[index].bomComment3 == "N/A")
            {
                bomComment3TextBox.Clear(); ;
            }
            else
            {
                bomComment3TextBox.Text = jobs[index].bomComment3;
            }

            if (jobs[index].bomComment4 == null || jobs[index].bomComment4 == "N/A")
            {
                bomComment4TextBox.Clear(); ;
            }
            else
            {
                bomComment4TextBox.Text = jobs[index].bomComment4;
            }

            if (jobs[index].bomComment5 == null || jobs[index].bomComment5 == "N/A")
            {
                bomComment5TextBox.Clear(); ;
            }
            else
            {
                bomComment5TextBox.Text = jobs[index].bomComment5;
            }

        }

        // Update manual input in the AthenaJob object
        private void updateManualInputData(int index, AthenaJob[] jobs)
        {
            if (clientComboBox.SelectedItem != null)
            {
                jobs[index].client = clientComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].client = "N/A";
            }

            if (contractTypeComboBox.SelectedItem != null)
            {
                jobs[index].contractType = contractTypeComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].contractType = "N/A";
            }

            if (artworkPartNumberSkuTextBox.Text == "")
            {
                jobs[index].artworkPartNumber = "N/A";
            }
            else
            {
                 jobs[index].artworkPartNumber = artworkPartNumberSkuTextBox.Text;
            }

            if (skuDescriptionTextBox.Text == "")
            {
                jobs[index].skuDescription = "N/A";
            }
            else
            {
                jobs[index].skuDescription = skuDescriptionTextBox.Text;
            }

            if (jobQtyNumericUpDown.Value == 0)
            {
                jobs[index].jobQty = "0";
            }
            else
            {
                jobQtyNumericUpDown.Value = Int32.Parse(jobs[index].jobQty);
            }

            if (faiStartComboBox.SelectedItem != null)
            {
                jobs[index].faiStart = faiStartComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].faiStart = "N/A";
            }

            if (faiEndComboBox.SelectedItem != null)
            {
                jobs[index].faiEnd = faiEndComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].faiEnd = "N/A";
            }

            if (packQtyComboBox.SelectedItem != null)
            {
                jobs[index].packQty = packQtyComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].packQty = "N/A";
            }

            if (boxQtyComboBox.SelectedItem != null)
            {
                jobs[index].boxQty = boxQtyComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].boxQty = "N/A";
            }

            if (palletQtyNumericUpDown.Value == 0)
            {
                jobs[index].palletQty = "0";
            }
            else
            {
                jobs[index].palletQty = palletQtyNumericUpDown.Value.ToString();
            }

            if (jobTypeComboBox.SelectedItem != null)
            {
                jobs[index].jobType = jobTypeComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].jobType = "N/A";
            }

            if (activationTypeIdTextBox.Text == "")
            {
                jobs[index].activationTypeID = "N/A";
            }
            else
            {
                jobs[index].activationTypeID = activationTypeIdTextBox.Text;
            }

            if (denominationNumericUpDown.Value == 0)
            {
                jobs[index].denomination = "0";
            }
            else
            {
                jobs[index].denomination = denominationNumericUpDown.Value.ToString();
            }

            if (currencyComboBox.SelectedItem != null)
            {
                jobs[index].denominationCurrency = currencyComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].denominationCurrency = "N/A";
            }

            if (decimalCheckBox.Checked)
            {
                jobs[index].isDecimal = true;
            }
            else
            {
                jobs[index].isDecimal = false;
            }

            if (intelJobTypeComboBox.SelectedItem != null)
            {
                jobs[index].intelJobType = intelJobTypeComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].intelJobType = "N/A";
            }

            if (jobCommentsTextBox.Text == "")
            {
                jobs[index].jobComments = "N/A";
            }
            else
            {
                jobs[index].jobComments = jobCommentsTextBox.Text;
            }
            
            if (countryTextBox.Text == "")
            {
                jobs[index].country = "N/A";
            }
            else
            {
                jobs[index].country = countryTextBox.Text;
            }

            if (alternativePartNumberTextBox.Text == "")
            {
                jobs[index].alternativePartNumber = "N/A";
            }
            else
            {
                jobs[index].alternativePartNumber = alternativePartNumberTextBox.Text;
            }

            if (packagingGtinTextBox.Text == "")
            {
                jobs[index].packagingGTIN = "N/A";
            }
            else
            {
                jobs[index].packagingGTIN = packagingGtinTextBox.Text;
            }

            if (incommProductDescriptionTextBox.Text == "")
            {
                jobs[index].incommProductDescription = "N/A";
            }
            else
            {
                jobs[index].incommProductDescription = incommProductDescriptionTextBox.Text;
            }

            if (pkpnTextBox.Text == "")
            {
                jobs[index].pkpn = "N/A";
            }
            else
            {
                jobs[index].pkpn = pkpnTextBox.Text;
            }

            if (bomFileNameTextBox.Text == "")
            {
                jobs[index].bomFileName = "N/A";
            }
            else
            {
               jobs[index].bomFileName = bomFileNameTextBox.Text;
            }

            if (bomComment1TextBox.Text == "")
            {
                jobs[index].bomComment1 = "N/A";
            }
            else
            {
                jobs[index].bomComment1 = bomComment1TextBox.Text;
            }

            if (bomComment2TextBox.Text == "")
            {
                jobs[index].bomComment2 = "N/A";
            }
            else
            {
               jobs[index].bomComment2 = bomComment2TextBox.Text;
            }

            if (bomComment3TextBox.Text == "")
            {
                jobs[index].bomComment3 = "N/A";
            }
            else
            {
                jobs[index].bomComment3 = bomComment3TextBox.Text;
            }

            if (bomComment4TextBox.Text == "")
            {
                jobs[index].bomComment4 = "N/A";
            }
            else
            {
                jobs[index].bomComment4 = bomComment4TextBox.Text;
            }

            if (bomComment5TextBox.Text == "")
            {
                jobs[index].bomComment5 = "N/A";
            }
            else
            {
                jobs[index].bomComment5 = bomComment5TextBox.Text;
            }

        }

        // Save manual input in order object button
        private void jobDetailsButton_Click(object sender, EventArgs e)
        {
            if (jobsArray != null)
            {
                updateManualInputData((int)jobIDNumericUpDown.Value - 1, jobsArray);
            }
        }
        
        // Generate XML File
        private void generateXmlButton_Click(object sender, EventArgs e)
        {
            if (jobsArray != null)
            {
                string xml = getXMLFromObject(jobsArray);
                xml = xml.Replace("ArrayOfOrder", "ListOfOrders");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = "unknown.xml";  // set a default file name
                savefile.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";    // set filters - this can be done in properties as well

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter wIn = new StreamWriter(savefile.FileName, false))
                    using (XmlTextWriter wr = new XmlTextWriter(wIn))
                    {
                        wr.Formatting = Formatting.Indented;
                        doc.WriteTo(wr);
                    }
                }
            }
        }

        // Enable/Disable Envirocard fields
        private void contractTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (contractTypeComboBox.SelectedIndex == 1)
            {
                pkpnLabel.Enabled = true;
                pkpnTextBox.Enabled = true;
                bomFileNameLabel.Enabled = true;
                bomFileNameTextBox.Enabled = true;
                bomComment1Label.Enabled = true;
                bomComment1TextBox.Enabled = true;
                bomComment2Label.Enabled = true;
                bomComment2TextBox.Enabled = true;
                bomComment3Label.Enabled = true;
                bomComment3TextBox.Enabled = true;
                bomComment4Label.Enabled = true;
                bomComment4TextBox.Enabled = true;
                bomComment5Label.Enabled = true;
                bomComment5TextBox.Enabled = true;


                pkpnLabel.Visible = true;
                pkpnTextBox.Visible = true;
                bomFileNameLabel.Visible = true;
                bomFileNameTextBox.Visible = true;
                bomComment1Label.Visible = true;
                bomComment1TextBox.Visible = true;
                bomComment2Label.Visible = true;
                bomComment2TextBox.Visible = true;
                bomComment3Label.Visible = true;
                bomComment3TextBox.Visible = true;
                bomComment4Label.Visible = true;
                bomComment4TextBox.Visible = true;
                bomComment5Label.Visible = true;
                bomComment5TextBox.Visible = true;
            }
            else
            {

                pkpnLabel.Visible = false;
                pkpnTextBox.Visible = false;
                bomFileNameLabel.Visible = false;
                bomFileNameTextBox.Visible = false;
                bomComment1Label.Visible = false;
                bomComment1TextBox.Visible = false;
                bomComment2Label.Visible = false;
                bomComment2TextBox.Visible = false;
                bomComment3Label.Visible = false;
                bomComment3TextBox.Visible = false;
                bomComment4Label.Visible = false;
                bomComment4TextBox.Visible = false;
                bomComment5Label.Visible = false;
                bomComment5TextBox.Visible = false;

                pkpnLabel.Enabled = false;
                pkpnTextBox.Enabled = false;
                bomFileNameLabel.Enabled = false;
                bomFileNameTextBox.Enabled = false;
                bomComment1Label.Enabled = false;
                bomComment1TextBox.Enabled = false;
                bomComment2Label.Enabled = false;
                bomComment2TextBox.Enabled = false;
                bomComment3Label.Enabled = false;
                bomComment3TextBox.Enabled = false;
                bomComment4Label.Enabled = false;
                bomComment4TextBox.Enabled = false;
                bomComment5Label.Enabled = false;
                bomComment5TextBox.Enabled = false;
            }
        }

    }
}
