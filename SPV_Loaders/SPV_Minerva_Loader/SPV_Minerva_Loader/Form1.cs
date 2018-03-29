using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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

        private void button3_Click(object sender, EventArgs e)
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
                    dataGridView3.DataSource = ids;


                    //foreach (var cd in result)
                    //{
                    //    //MessageBox.Show("Artist: {0}", cd.Artist.ToString());
                    //    MessageBox.Show(ids.ToString());
                    //}
                }
            }
        }

        public  class CD
        {
            public string Artist { get; set; }
            public string Title { get; set; }
        }
    }
}
