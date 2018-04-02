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
            if (wtcCheckBox.Checked)
            {
                wtcQtyNumericUpDown.Visible = true;
                wtcQuantityLabel.Visible = true;
                wtcEnvironmentLabel.Visible = true;
                wtcEnvironmentComboBox.Visible = true;
                wtcDenomDecimalcheckBox.Visible = true;
                wtcDenomLabel.Visible = true;
                wtcDenomNumericUpDown.Visible = true;
            }
            else
            {
                wtcQtyNumericUpDown.Visible = false;
                wtcQuantityLabel.Visible = false;
                wtcEnvironmentLabel.Visible = false;
                wtcEnvironmentComboBox.Visible = false;
                wtcDenomDecimalcheckBox.Visible = false;
                wtcDenomLabel.Visible = false;
                wtcDenomNumericUpDown.Visible = false;
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

        // Turn on/off visibility for BHN packaging details input
        private void integratorsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (integratorComboCox.SelectedIndex == 1)
            {
                bhnPackRetailCodeLabel.Visible = true;
                bhnPackRetailCodeNumericUpDown.Visible = true;

                bhnBoxRetailCodeLabel.Visible = true;
                bhnBoxRetailCodeNumericUpDown.Visible = true;

                bhnPalletRetailCodeLabel.Visible = true;
                bhnPalletRetailCodeNumericUpDown.Visible = true;
            }
            else
            {
                bhnPackRetailCodeLabel.Visible = false;
                bhnPackRetailCodeNumericUpDown.Visible = false;

                bhnBoxRetailCodeLabel.Visible = false;
                bhnBoxRetailCodeNumericUpDown.Visible = false;

                bhnPalletRetailCodeLabel.Visible = false;
                bhnPalletRetailCodeNumericUpDown.Visible = false;
            }
        }

        // Turn on/off decimal places for Job Denomination
        private void denomDecimalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (denomDecimalCheckBox.Checked)
            {
                denominationNumericUpDown.DecimalPlaces = 2;
                wtcDenomNumericUpDown.DecimalPlaces = 2;
                wtcDenomDecimalcheckBox.Checked = true;                
            }
            else
            {
                denominationNumericUpDown.DecimalPlaces = 0;
                wtcDenomNumericUpDown.DecimalPlaces = 0;
                wtcDenomDecimalcheckBox.Checked = false;
            }
        }

        // Turn on/off decimal places for WTC Denomination
        private void wtcDenomDecimalcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (wtcDenomDecimalcheckBox.Checked)
            {
                denominationNumericUpDown.DecimalPlaces = 2;
                wtcDenomNumericUpDown.DecimalPlaces = 2;
                denomDecimalCheckBox.Checked = true;

                if (ordersArray != null)
                {
                    ordersArray[(int)orderIDNumericUpDown.Value - 1].isDecimal = true;
                }
            }
            else
            {
                denominationNumericUpDown.DecimalPlaces = 0;
                wtcDenomNumericUpDown.DecimalPlaces = 0;
                denomDecimalCheckBox.Checked = false;

                if (ordersArray != null)
                {
                    ordersArray[(int)orderIDNumericUpDown.Value - 1].isDecimal = false;
                }
            }

        }

        // Update denomination field
        private void denominationNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            wtcDenomNumericUpDown.Value = denominationNumericUpDown.Value;
        }

        // Update WTC denomionation field
        private void wtcDenomNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
           denominationNumericUpDown.Value = wtcDenomNumericUpDown.Value;
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
                totalOrdersTextBox.Text = orderIDNumericUpDown.Maximum + "";
                currentOrderTextBox.Text = orderIDNumericUpDown.Value + "";
            }
        }

        // Change Active Order
        private void orderIDNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ordersArray != null)
            {
                fillGuiWithAutoInput((int)orderIDNumericUpDown.Value - 1, ordersArray);
                fillGuiWithManualInput((int)orderIDNumericUpDown.Value - 1, ordersArray);
                currentOrderTextBox.Text = orderIDNumericUpDown.Value + "";
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
                jobTypeComboBox.ResetText();
                jobTypeComboBox.SelectedIndex = -1;
            }
            else
            {
                jobTypeComboBox.SelectedItem = orders[index].jobType;
            }

            if (orders[index].integrator == null || orders[index].integrator == "Not Selected")
            {
                integratorComboCox.ResetText();
                integratorComboCox.SelectedIndex = -1;
            }
            else
            {
                integratorComboCox.SelectedItem = orders[index].integrator;
            }

            if (orders[index].region == null || orders[index].region == "Not Selected")
            {
                regionComboBox.ResetText();
                regionComboBox.SelectedIndex = -1;
            }
            else
            {
                regionComboBox.SelectedItem = orders[index].region;
            }
            
            if (orders[index].currency == null || orders[index].currency == "Not Selected")
            {
                currencyComboBox.ResetText();
                currencyComboBox.SelectedIndex = -1;
            }
            else
            {
                currencyComboBox.SelectedItem = orders[index].currency;
            }

            if (orders[index].productDescription == null || orders[index].productDescription == "Not Selected")
            {
                productDescriptionTextBox.Clear();
            }
            else
            {
                productDescriptionTextBox.Text = orders[index].productDescription;
            }

            if (orders[index].regionIntegratorID == null || orders[index].regionIntegratorID == "Not Selected")
            {
                regionIntegratorIDTextBox.Clear();
            }
            else
            {
                regionIntegratorIDTextBox.Text = orders[index].regionIntegratorID;
            }

            if (orders[index].countryIncommRetailer == null || orders[index].countryIncommRetailer == "Not Selected")
            {
                countryIncommRetailerTextBox.Clear();
            }
            else
            {
                countryIncommRetailerTextBox.Text = orders[index].countryIncommRetailer;
            }

            if (orders[index].packQuantity == null || orders[index].packQuantity == "Not Selected")
            {
                packQuantityComboBox.ResetText();
                packQuantityComboBox.SelectedIndex = -1;
            }
            else
            {
                packQuantityComboBox.SelectedItem = orders[index].packQuantity;
            }

            if (orders[index].boxQuantitySize == null || orders[index].boxQuantitySize == "Not Selected")
            {
                boxQuantityComboBox.ResetText();
                boxQuantityComboBox.SelectedIndex = -1;
            }
            else
            {
                boxQuantityComboBox.SelectedItem = orders[index].boxQuantitySize;
            }

            if (orders[index].specialInstructions == null || orders[index].specialInstructions == "Not Selected")
            {
                specialInstructionsTextBox.Clear();
            }
            else
            {
                specialInstructionsTextBox.Text = orders[index].specialInstructions;
            }

            if (orders[index].isDecimal)
            {
                denomDecimalCheckBox.Checked = true;
            }
            else
            {
                denomDecimalCheckBox.Checked = false;
            }

            if (orders[index].hasPPT)
            {
                pptCheckBox.Checked = true;
            }
            else
            {
                pptCheckBox.Checked = false;
            }

            if (orders[index].hasWTC)
            {
                wtcCheckBox.Checked = true;
            }
            else
            {
                wtcCheckBox.Checked = false;
            }

            if (pptCheckBox.Checked)
            {
                if (orders[index].pptQuanity != null)
                {
                    pptQtyNumericUpDown.Value = Int32.Parse(orders[index].pptQuanity);
                }
                else
                {
                    pptQtyNumericUpDown.Value = 0;
                }
            }
            else
            {
                pptQtyNumericUpDown.Value = 0;
            }

            if (wtcCheckBox.Checked)
            {
                if (orders[index].wtcQuantity != null)
                {
                    wtcQtyNumericUpDown.Value = Int32.Parse(orders[index].wtcQuantity);
                }
                else
                {
                    wtcQtyNumericUpDown.Value = 0;
                }


                if (orders[index].wtcDenomination != null)
                {
                    wtcDenomNumericUpDown.Value = Int32.Parse(orders[index].wtcDenomination);
                }
                else
                {
                    wtcDenomNumericUpDown.Value = 16;
                }
            }
            else
            {
                wtcQtyNumericUpDown.Value = 0;
                wtcDenomNumericUpDown.Value = 0;
            }

            humanReadableCheckBox.Checked = orders[index].dodHumanReadable.Equals("True") ? true : false;

            if (orders[index].denomination != null)
            {
                denominationNumericUpDown.Value = Int32.Parse(orders[index].denomination);
            }
            else
            {
                denominationNumericUpDown.Value = 0;
            }

            if (orders[index].jobQuantity != null)
            {
                jobQtyNumericUpDown.Value = Int32.Parse(orders[index].jobQuantity);
            }
            else
            {
                jobQtyNumericUpDown.Value = 0;
            }

            if (orders[index].palletQuantity != null)
            {
                palletQtyNumericUpDown.Value = Int32.Parse(orders[index].palletQuantity);
            }
            else
            {
                palletQtyNumericUpDown.Value = 0;
            }

            if (orders[index].bhnPackRetailCode != null)
            {
                bhnPackRetailCodeNumericUpDown.Value = Int32.Parse(orders[index].bhnPackRetailCode);
            }
            else
            {
                bhnPackRetailCodeNumericUpDown.Value = 0;
            }

            if (orders[index].bhnBoxRetailCode != null)
            {
                bhnBoxRetailCodeNumericUpDown.Value = Int32.Parse(orders[index].bhnBoxRetailCode);
            }
            else
            {
                bhnBoxRetailCodeNumericUpDown.Value = 0;
            }

            if (orders[index].bhnPalletRetailCode != null)
            {
                bhnPalletRetailCodeNumericUpDown.Value = Int32.Parse(orders[index].bhnPalletRetailCode);
            }
            else
            {
                bhnPalletRetailCodeNumericUpDown.Value = 0;
            }
        }

        // Update manual input in the Order object
        private void updateManualInputData(int index, Order[] orders) {
            if (jobTypeComboBox.SelectedItem != null)
            {
                orders[index].jobType = jobTypeComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].jobType = "Not Selected";
            }

            if (integratorComboCox.SelectedItem != null)
            {
                orders[index].integrator = integratorComboCox.SelectedItem.ToString();
            }
            else
            {
                orders[index].integrator = "Not Selected";
            }

            if (regionComboBox.SelectedItem != null)
            {
                orders[index].region = regionComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].region = "Not Selected";
            }

            if (currencyComboBox.SelectedItem != null)
            {
                orders[index].currency = currencyComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].currency = "Not Selected";
            }
            
            if (packQuantityComboBox.SelectedItem != null)
            {
                orders[index].packQuantity = packQuantityComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].packQuantity = "Not Selected";
            }

            if (boxQuantityComboBox.SelectedItem != null)
            {
                orders[index].boxQuantitySize = boxQuantityComboBox.SelectedItem.ToString();
            }
            else
            {
                orders[index].boxQuantitySize =  "Not Selected";
            }

            if (orders[index].productDescription != null)
            {
                orders[index].productDescription = productDescriptionTextBox.Text;
            }
            else
            {
                orders[index].productDescription = "Not Selected";
            }

            if (orders[index].regionIntegratorID != null)
            {
                orders[index].regionIntegratorID = regionIntegratorIDTextBox.Text;
            }
            else
            {
                orders[index].regionIntegratorID = "Not Selected";
            }

            if (orders[index].countryIncommRetailer != null)
            {
                orders[index].countryIncommRetailer = countryIncommRetailerTextBox.Text;
            }
            else
            {
                orders[index].countryIncommRetailer = "Not Selected";
            }

            if (orders[index].specialInstructions != null)
            {
                orders[index].specialInstructions = specialInstructionsTextBox.Text;
            }
            else
            {
                orders[index].specialInstructions = "Not Selected";
            }

            if (denominationNumericUpDown.Value == 0)
            {
                if (orders[index].isDecimal)
                {
                    orders[index].denomination = denominationNumericUpDown.Value + ".00";
                }
                else
                {
                    orders[index].denomination = denominationNumericUpDown.Value + "";
                }
            }
            else
            {
                orders[index].denomination = denominationNumericUpDown.Value + "";
            }

            if (denomDecimalCheckBox.Checked)
            {
                orders[index].isDecimal = true;
            }
            else
            {
                orders[index].isDecimal = false;
            }

            if (pptCheckBox.Checked)
            {
                orders[index].hasPPT = true;
            }
            else
            {
                orders[index].hasPPT = false;
            }

            if (wtcCheckBox.Checked)
            {
                orders[index].hasWTC = true;
            }
            else
            {
                orders[index].hasWTC = false;
            }

            orders[index].dodHumanReadable = humanReadableCheckBox.Checked ? true : false;
            orders[index].jobQuantity = jobQtyNumericUpDown.Value + "";
            orders[index].palletQuantity = palletQtyNumericUpDown.Value + "";
            orders[index].pptQuanity = pptQtyNumericUpDown.Value + "";
            orders[index].wtcQuantity = wtcQtyNumericUpDown.Value + "";
            orders[index].wtcDenomination = wtcDenomNumericUpDown.Value + "";
            orders[index].bhnPackRetailCode = bhnPackRetailCodeNumericUpDown.Value + "";
            orders[index].bhnBoxRetailCode = bhnBoxRetailCodeNumericUpDown.Value + "";
            orders[index].bhnPalletRetailCode = bhnPalletRetailCodeNumericUpDown.Value + "";
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
                //Handle Exception Code
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
