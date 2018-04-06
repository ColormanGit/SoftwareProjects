using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SPV_Minerva_Loader 
{
    public partial class MinervaForm : Form
    {

        private MinervaJob[] jobsArray;

        public MinervaForm()
        {
            InitializeComponent();
            jobIDNumericUpDown.Minimum = 1;
        }

        // Turn on/off visibility for WTCs settings
        private void wtcCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (minervaWtcCheckBox.Checked)
            {
                minervaWtcQtyNumericUpDown.Visible = true;
                minervaWtcQuantityLabel.Visible = true;
                minervaWtcEnvironmentLabel.Visible = true;
                minervaWtcEnvironmentComboBox.Visible = true;
                minervaWtcDenomDecimalcheckBox.Visible = true;
                minervaWtcDenomLabel.Visible = true;
                minervaWtcDenomNumericUpDown.Visible = true;
            }
            else
            {
                minervaWtcQtyNumericUpDown.Visible = false;
                minervaWtcQuantityLabel.Visible = false;
                minervaWtcEnvironmentLabel.Visible = false;
                minervaWtcEnvironmentComboBox.Visible = false;
                minervaWtcDenomDecimalcheckBox.Visible = false;
                minervaWtcDenomLabel.Visible = false;
                minervaWtcDenomNumericUpDown.Visible = false;
            }
        }
        
        // Turn on/off visibility for PPT cards settings
        private void pptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (minervaPptCheckBox.Checked)
            {
                minervaPptQtyNumericUpDown.Visible = true;
                minervaPptQuantityLabel.Visible = true;
            }
            else
            {
                minervaPptQtyNumericUpDown.Visible = false;
                minervaPptQuantityLabel.Visible = false;
            }
        }

        // Turn on/off visibility for BHN packaging details input
        private void integratorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (minervaIntegratorComboCox.SelectedIndex == 1)
            {
                minervaBhnPackRetailCodeLabel.Visible = true;
                minervaBhnPackRetailCodeNumericUpDown.Visible = true;

                minervaBhnBoxRetailCodeLabel.Visible = true;
                minervaBhnBoxRetailCodeNumericUpDown.Visible = true;

                minervaBhnPalletRetailCodeLabel.Visible = true;
                minervaBhnPalletRetailCodeNumericUpDown.Visible = true;
            }
            else
            {
                minervaBhnPackRetailCodeLabel.Visible = false;
                minervaBhnPackRetailCodeNumericUpDown.Visible = false;

                minervaBhnBoxRetailCodeLabel.Visible = false;
                minervaBhnBoxRetailCodeNumericUpDown.Visible = false;

                minervaBhnPalletRetailCodeLabel.Visible = false;
                minervaBhnPalletRetailCodeNumericUpDown.Visible = false;
            }
        }

        // Turn on/off decimal places for Job Denomination
        private void denomDecimalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (minervaDenomDecimalCheckBox.Checked)
            {
                minervaDenominationNumericUpDown.DecimalPlaces = 2;
                minervaWtcDenomNumericUpDown.DecimalPlaces = 2;
                minervaWtcDenomDecimalcheckBox.Checked = true;                
            }
            else
            {
                minervaDenominationNumericUpDown.DecimalPlaces = 0;
                minervaWtcDenomNumericUpDown.DecimalPlaces = 0;
                minervaWtcDenomDecimalcheckBox.Checked = false;
            }
        }

        // Turn on/off decimal places for WTC Denomination
        private void wtcDenomDecimalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (minervaWtcDenomDecimalcheckBox.Checked)
            {
                minervaDenominationNumericUpDown.DecimalPlaces = 2;
                minervaWtcDenomNumericUpDown.DecimalPlaces = 2;
                minervaDenomDecimalCheckBox.Checked = true;

                if (jobsArray != null)
                {
                    jobsArray[(int)jobIDNumericUpDown.Value - 1].isDecimal = true;
                }
            }
            else
            {
                minervaDenominationNumericUpDown.DecimalPlaces = 0;
                minervaWtcDenomNumericUpDown.DecimalPlaces = 0;
                minervaDenomDecimalCheckBox.Checked = false;

                if (jobsArray != null)
                {
                    jobsArray[(int)jobIDNumericUpDown.Value - 1].isDecimal = false;
                }
            }

        }

        // Update denomination field
        private void denominationNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            minervaWtcDenomNumericUpDown.Value = minervaDenominationNumericUpDown.Value;
        }

        // Update WTC denomionation field
        private void wtcDenomNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
           minervaDenominationNumericUpDown.Value = minervaWtcDenomNumericUpDown.Value;
        }

        // Import XML
        private void importXmlButton_Click(object sender, EventArgs e)
        {
            // Create OpenFile Dialog object to allow the user select XML file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = ".";
            openFileDialog.Filter = "Xml files (*.xml)|*.xml";
            openFileDialog.FilterIndex = 2;
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

                    jobsArray = new MinervaJob[xmlDoc.Root.Elements().ElementAt(0).Elements().Count()];

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
                        jobsArray[i] = new MinervaJob((i + 1) + "", values);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                // Initiate GUI and order elements for first order
                fillGuiWithAutoInput(0, jobsArray);
                fillGuiWithManualInput(0, jobsArray);
                updateManualInputData(0, jobsArray);
                minervaTotaljobsTextBox.Text = jobIDNumericUpDown.Maximum + "";
                currentOrderTextBox.Text = jobIDNumericUpDown.Value + "";
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
        private void fillGuiWithAutoInput(int index, MinervaJob[] jobs) {

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
        private void fillGuiWithManualInput(int index, MinervaJob[] jobs)
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

        // Update manual input in the MinervaJob object
        private void updateManualInputData(int index, MinervaJob[] jobs) {
            if (minervaJobTypeComboBox.SelectedItem != null)
            {
                jobs[index].jobType = minervaJobTypeComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].jobType = "N/A";
            }

            if (minervaIntegratorComboCox.SelectedItem != null)
            {
                jobs[index].integrator = minervaIntegratorComboCox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].integrator = "N/A";
            }

            if (minervaRegionComboBox.SelectedItem != null)
            {
                jobs[index].region = minervaRegionComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].region = "N/A";
            }

            if (minervaCurrencyComboBox.SelectedItem != null)
            {
                jobs[index].currency = minervaCurrencyComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].currency = "N/A";
            }
            
            if (minervaPackQuantityComboBox.SelectedItem != null)
            {
                jobs[index].packQuantity = minervaPackQuantityComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].packQuantity = "N/A";
            }

            if (minervaBoxQuantityComboBox.SelectedItem != null)
            {
                jobs[index].boxQuantitySize = minervaBoxQuantityComboBox.SelectedItem.ToString();
            }
            else
            {
                jobs[index].boxQuantitySize =  "N/A";
            }

            if (jobs[index].productDescription != null)
            {
                jobs[index].productDescription = minervaProductDescriptionTextBox.Text;
            }
            else
            {
                jobs[index].productDescription = "N/A";
            }

            if (jobs[index].regionIntegratorID != null)
            {
                jobs[index].regionIntegratorID = minervaRegionIntegratorIDTextBox.Text;
            }
            else
            {
                jobs[index].regionIntegratorID = "N/A";
            }

            if (jobs[index].countryIncommRetailer != null)
            {
                jobs[index].countryIncommRetailer = minervaCountryIncommRetailerTextBox.Text;
            }
            else
            {
                jobs[index].countryIncommRetailer = "N/A";
            }

            if (jobs[index].specialInstructions != null)
            {
                jobs[index].specialInstructions = minervaSpecialInstructionsTextBox.Text;
            }
            else
            {
                jobs[index].specialInstructions = "N/A";
            }

            if (minervaDenominationNumericUpDown.Value == 0)
            {
                if (jobs[index].isDecimal)
                {
                    jobs[index].denomination = minervaDenominationNumericUpDown.Value + ".00";
                }
                else
                {
                    jobs[index].denomination = minervaDenominationNumericUpDown.Value + "";
                }
            }
            else
            {
                jobs[index].denomination = minervaDenominationNumericUpDown.Value + "";
            }

            if (minervaDenomDecimalCheckBox.Checked)
            {
                jobs[index].isDecimal = true;
            }
            else
            {
                jobs[index].isDecimal = false;
            }

            if (minervaPptCheckBox.Checked)
            {
                jobs[index].hasPPT = true;
            }
            else
            {
                jobs[index].hasPPT = false;
            }

            if (minervaWtcCheckBox.Checked)
            {
                jobs[index].hasWTC = true;
            }
            else
            {
                jobs[index].hasWTC = false;
            }

            jobs[index].dodHumanReadable = minervaHumanReadableCheckBox.Checked ? true : false;
            jobs[index].jobQuantity = minervaJobQtyNumericUpDown.Value + "";
            jobs[index].palletQuantity = minervaPalletQtyNumericUpDown.Value + "";
            jobs[index].pptQuanity = minervaPptQtyNumericUpDown.Value + "";
            jobs[index].wtcQuantity = minervaWtcQtyNumericUpDown.Value + "";
            jobs[index].wtcDenomination = minervaWtcDenomNumericUpDown.Value + "";
            jobs[index].bhnPackRetailCode = minervaBhnPackRetailCodeNumericUpDown.Value + "";
            jobs[index].bhnBoxRetailCode = minervaBhnBoxRetailCodeNumericUpDown.Value + "";
            jobs[index].bhnPalletRetailCode = minervaBhnPalletRetailCodeNumericUpDown.Value + "";
        }

        // Save manual input in order object button
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (jobsArray != null)
            {
                updateManualInputData((int)jobIDNumericUpDown.Value - 1, jobsArray);
            }
        }

        // Generate XML string fron an MinervaJob Object Array
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

        //  Generate XML File
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
    }
}
