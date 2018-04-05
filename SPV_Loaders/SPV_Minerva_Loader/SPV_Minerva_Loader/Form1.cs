using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace SPV_Minerva_Loader 
{
    public partial class loaderWindow : Form
    {
        private Order[] ordersArray;

        public loaderWindow()
        {
            InitializeComponent();
            orderIDNumericUpDown.Minimum = 1;
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

                if (ordersArray != null)
                {
                    ordersArray[(int)orderIDNumericUpDown.Value - 1].isDecimal = true;
                }
            }
            else
            {
                minervaDenominationNumericUpDown.DecimalPlaces = 0;
                minervaWtcDenomNumericUpDown.DecimalPlaces = 0;
                minervaDenomDecimalCheckBox.Checked = false;

                if (ordersArray != null)
                {
                    ordersArray[(int)orderIDNumericUpDown.Value - 1].isDecimal = false;
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
            orderIDNumericUpDown.Minimum = 1;
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

                    ordersArray = new Order[xmlDoc.Root.Elements().ElementAt(0).Elements().Count()];

                    // Create orders from XML file
                    for (int i = 0; i < xmlDoc.Root.Elements().ElementAt(0).Elements().Count(); i++)
                    {
                        // Create and fill string array with values from XML file
                        string[] values = new string[xmlDoc.Root.Elements().ElementAt(0).Elements().ElementAt(0).Elements().Count()];
                        for (int j = 0; j < xmlDoc.Root.Elements().ElementAt(0).Elements().ElementAt(0).Elements().Count(); j++)
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
                        orderIDNumericUpDown.Maximum = xmlDoc.Root.Elements().ElementAt(0).Elements().Count();
                        // Create new order and place it in array of orders
                        ordersArray[i] = new Order((i + 1) + "", values);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }

                // Initiate GUI and order elements for first order
                fillGuiWithAutoInput(0, ordersArray);
                fillGuiWithManualInput(0, ordersArray);
                updateManualInputData(0, ordersArray);
                minervaTotalOrdersTextBox.Text = orderIDNumericUpDown.Maximum + "";
                minervaCurrentOrderTextBox.Text = orderIDNumericUpDown.Value + "";
            }
        }
//
        // Change Active Order
        private void orderIDNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ordersArray != null)
            {
                fillGuiWithAutoInput((int)orderIDNumericUpDown.Value - 1, ordersArray);
                fillGuiWithManualInput((int)orderIDNumericUpDown.Value - 1, ordersArray);
                minervaCurrentOrderTextBox.Text = orderIDNumericUpDown.Value + "";
            }
        }

        // Populate GUI fields with input from a file
        private void fillGuiWithAutoInput(int index, Order[] orders) {

            orderIDNumericUpDown.Value = Int32.Parse(orders[index].autoInputArray[0]);
            jobNumberTextBox.Text = orders[index].autoInputArray[1];
            dueDateTextBox.Text = orders[index].autoInputArray[2];
            purchaseOrderNoTextBox.Text = orders[index].autoInputArray[3];
            purchaseOrderLineTextBox.Text = orders[index].autoInputArray[4];
            salesOrderNumberTextBox.Text = orders[index].autoInputArray[5];
            customerAccountTextBox.Text = orders[index].autoInputArray[6];
            buildQuantityTextBox.Text = orders[index].autoInputArray[7];
            ascmOrderIDTextBox.Text = orders[index].autoInputArray[8];
            endCustomerTextBox.Text = orders[index].autoInputArray[9];
            activationSystemTextBox.Text = orders[index].autoInputArray[10];
            productTypeTextBox.Text = orders[index].autoInputArray[11];
            erpMaterialCodeTextBox.Text = orders[index].autoInputArray[12];
            integratorPartIDTextBox.Text = orders[index].autoInputArray[13];
            integratorIDTextBox.Text = orders[index].autoInputArray[14];
            activationTypeTextBox.Text = orders[index].autoInputArray[15];
            partNumberTextBox.Text = orders[index].autoInputArray[16];
            retailBarcodeTextBox.Text = orders[index].autoInputArray[17];
            retailBarcodeTypeTextBox.Text = orders[index].autoInputArray[18];

        }

        // Populate GUI fields with manual input if it's not absent
        private void fillGuiWithManualInput(int index, Order[] orders)
        {
            if (orders[index].jobType == null || orders[index].jobType == "Not Selected")
            {
                minervaJobTypeComboBox.ResetText();
                minervaJobTypeComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaJobTypeComboBox.SelectedItem = orders[index].jobType;
            }

            if (orders[index].integrator == null || orders[index].integrator == "Not Selected")
            {
                minervaIntegratorComboCox.ResetText();
                minervaIntegratorComboCox.SelectedIndex = -1;
            }
            else
            {
                minervaIntegratorComboCox.SelectedItem = orders[index].integrator;
            }

            if (orders[index].region == null || orders[index].region == "Not Selected")
            {
                minervaRegionComboBox.ResetText();
                minervaRegionComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaRegionComboBox.SelectedItem = orders[index].region;
            }
            
            if (orders[index].currency == null || orders[index].currency == "Not Selected")
            {
                minervaCurrencyComboBox.ResetText();
                minervaCurrencyComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaCurrencyComboBox.SelectedItem = orders[index].currency;
            }

            if (orders[index].productDescription == null || orders[index].productDescription == "Not Selected")
            {
                minervaProductDescriptionTextBox.Clear();
            }
            else
            {
                minervaProductDescriptionTextBox.Text = orders[index].productDescription;
            }

            if (orders[index].regionIntegratorID == null || orders[index].regionIntegratorID == "Not Selected")
            {
                minervaRegionIntegratorIDTextBox.Clear();
            }
            else
            {
                minervaRegionIntegratorIDTextBox.Text = orders[index].regionIntegratorID;
            }

            if (orders[index].countryIncommRetailer == null || orders[index].countryIncommRetailer == "Not Selected")
            {
                minervaCountryIncommRetailerTextBox.Clear();
            }
            else
            {
                minervaCountryIncommRetailerTextBox.Text = orders[index].countryIncommRetailer;
            }

            if (orders[index].packQuantity == null || orders[index].packQuantity == "Not Selected")
            {
                minervaPackQuantityComboBox.ResetText();
                minervaPackQuantityComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaPackQuantityComboBox.SelectedItem = orders[index].packQuantity;
            }

            if (orders[index].boxQuantitySize == null || orders[index].boxQuantitySize == "Not Selected")
            {
                minervaBoxQuantityComboBox.ResetText();
                minervaBoxQuantityComboBox.SelectedIndex = -1;
            }
            else
            {
                minervaBoxQuantityComboBox.SelectedItem = orders[index].boxQuantitySize;
            }

            if (orders[index].specialInstructions == null || orders[index].specialInstructions == "Not Selected")
            {
                minervaSpecialInstructionsTextBox.Clear();
            }
            else
            {
                minervaSpecialInstructionsTextBox.Text = orders[index].specialInstructions;
            }

            if (orders[index].isDecimal)
            {
                minervaDenomDecimalCheckBox.Checked = true;
            }
            else
            {
                minervaDenomDecimalCheckBox.Checked = false;
            }

            if (orders[index].hasPPT)
            {
                minervaPptCheckBox.Checked = true;
            }
            else
            {
                minervaPptCheckBox.Checked = false;
            }

            if (orders[index].hasWTC)
            {
                minervaWtcCheckBox.Checked = true;
            }
            else
            {
                minervaWtcCheckBox.Checked = false;
            }

            if (minervaPptCheckBox.Checked)
            {
                if (orders[index].pptQuanity != null)
                {
                    minervaPptQtyNumericUpDown.Value = Int32.Parse(orders[index].pptQuanity);
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
                if (orders[index].wtcQuantity != null)
                {
                    minervaWtcQtyNumericUpDown.Value = Int32.Parse(orders[index].wtcQuantity);
                }
                else
                {
                    minervaWtcQtyNumericUpDown.Value = 0;
                }


                if (orders[index].wtcDenomination != null)
                {
                    minervaWtcDenomNumericUpDown.Value = Int32.Parse(orders[index].wtcDenomination);
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

            minervaHumanReadableCheckBox.Checked = orders[index].dodHumanReadable.Equals("True") ? true : false;

            if (orders[index].denomination != null)
            {
                minervaDenominationNumericUpDown.Value = Int32.Parse(orders[index].denomination);
            }
            else
            {
                minervaDenominationNumericUpDown.Value = 0;
            }

            if (orders[index].jobQuantity != null)
            {
                minervaJobQtyNumericUpDown.Value = Int32.Parse(orders[index].jobQuantity);
            }
            else
            {
                minervaJobQtyNumericUpDown.Value = 0;
            }

            if (orders[index].palletQuantity != null)
            {
                minervaPalletQtyNumericUpDown.Value = Int32.Parse(orders[index].palletQuantity);
            }
            else
            {
                minervaPalletQtyNumericUpDown.Value = 0;
            }

            if (orders[index].bhnPackRetailCode != null)
            {
                minervaBhnPackRetailCodeNumericUpDown.Value = Int32.Parse(orders[index].bhnPackRetailCode);
            }
            else
            {
                minervaBhnPackRetailCodeNumericUpDown.Value = 0;
            }

            if (orders[index].bhnBoxRetailCode != null)
            {
                minervaBhnBoxRetailCodeNumericUpDown.Value = Int32.Parse(orders[index].bhnBoxRetailCode);
            }
            else
            {
                minervaBhnBoxRetailCodeNumericUpDown.Value = 0;
            }

            if (orders[index].bhnPalletRetailCode != null)
            {
                minervaBhnPalletRetailCodeNumericUpDown.Value = Int32.Parse(orders[index].bhnPalletRetailCode);
            }
            else
            {
                minervaBhnPalletRetailCodeNumericUpDown.Value = 0;
            }
        }

        // Update manual input in the Order object
        private void updateManualInputData(int index, Order[] orders) {
            if (minervaJobTypeComboBox.SelectedItem != null)
            {
                orders[index].jobType = minervaJobTypeComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].jobType = "Not Selected";
            }

            if (minervaIntegratorComboCox.SelectedItem != null)
            {
                orders[index].integrator = minervaIntegratorComboCox.SelectedItem.ToString();
            }
            else
            {
                orders[index].integrator = "Not Selected";
            }

            if (minervaRegionComboBox.SelectedItem != null)
            {
                orders[index].region = minervaRegionComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].region = "Not Selected";
            }

            if (minervaCurrencyComboBox.SelectedItem != null)
            {
                orders[index].currency = minervaCurrencyComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].currency = "Not Selected";
            }
            
            if (minervaPackQuantityComboBox.SelectedItem != null)
            {
                orders[index].packQuantity = minervaPackQuantityComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].packQuantity = "Not Selected";
            }

            if (minervaBoxQuantityComboBox.SelectedItem != null)
            {
                orders[index].boxQuantitySize = minervaBoxQuantityComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].boxQuantitySize =  "Not Selected";
            }

            if (orders[index].productDescription != null)
            {
                orders[index].productDescription = minervaProductDescriptionTextBox.Text;
            }
            else
            {
                orders[index].productDescription = "Not Selected";
            }

            if (orders[index].regionIntegratorID != null)
            {
                orders[index].regionIntegratorID = minervaRegionIntegratorIDTextBox.Text;
            }
            else
            {
                orders[index].regionIntegratorID = "Not Selected";
            }

            if (orders[index].countryIncommRetailer != null)
            {
                orders[index].countryIncommRetailer = minervaCountryIncommRetailerTextBox.Text;
            }
            else
            {
                orders[index].countryIncommRetailer = "Not Selected";
            }

            if (orders[index].specialInstructions != null)
            {
                orders[index].specialInstructions = minervaSpecialInstructionsTextBox.Text;
            }
            else
            {
                orders[index].specialInstructions = "Not Selected";
            }

            if (minervaDenominationNumericUpDown.Value == 0)
            {
                if (orders[index].isDecimal)
                {
                    orders[index].denomination = minervaDenominationNumericUpDown.Value + ".00";
                }
                else
                {
                    orders[index].denomination = minervaDenominationNumericUpDown.Value + "";
                }
            }
            else
            {
                orders[index].denomination = minervaDenominationNumericUpDown.Value + "";
            }

            if (minervaDenomDecimalCheckBox.Checked)
            {
                orders[index].isDecimal = true;
            }
            else
            {
                orders[index].isDecimal = false;
            }

            if (minervaPptCheckBox.Checked)
            {
                orders[index].hasPPT = true;
            }
            else
            {
                orders[index].hasPPT = false;
            }

            if (minervaWtcCheckBox.Checked)
            {
                orders[index].hasWTC = true;
            }
            else
            {
                orders[index].hasWTC = false;
            }

            orders[index].dodHumanReadable = minervaHumanReadableCheckBox.Checked ? true : false;
            orders[index].jobQuantity = minervaJobQtyNumericUpDown.Value + "";
            orders[index].palletQuantity = minervaPalletQtyNumericUpDown.Value + "";
            orders[index].pptQuanity = minervaPptQtyNumericUpDown.Value + "";
            orders[index].wtcQuantity = minervaWtcQtyNumericUpDown.Value + "";
            orders[index].wtcDenomination = minervaWtcDenomNumericUpDown.Value + "";
            orders[index].bhnPackRetailCode = minervaBhnPackRetailCodeNumericUpDown.Value + "";
            orders[index].bhnBoxRetailCode = minervaBhnBoxRetailCodeNumericUpDown.Value + "";
            orders[index].bhnPalletRetailCode = minervaBhnPalletRetailCodeNumericUpDown.Value + "";
        }

        // Save manual input in order object button
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (ordersArray != null)
            {
                updateManualInputData((int)orderIDNumericUpDown.Value - 1, ordersArray);
            }
        }

        // Generate XML string fron an Order Object Array
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
            if (ordersArray != null)
            {
                string xml = getXMLFromObject(ordersArray);
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
