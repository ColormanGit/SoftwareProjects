using System;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Timers;
using System.Windows.Forms;

namespace ScheduleChecker
{
    public partial class Form1 : Form
    {
        DateTime CurrentDate = DateTime.Now;
        string ConnectionString = Convert.ToString("Dsn=tharData;uid=tharuser");

        public Form1()
        {
            InitializeComponent();
            this.Text = "Schedule Checker";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string CommandText = "SELECT * from app_ScheduleChecker";
            OdbcConnection myConnection = new OdbcConnection(ConnectionString);
            OdbcCommand myCommand = new OdbcCommand(CommandText, myConnection);
            OdbcDataAdapter myAdapter = new OdbcDataAdapter();
            myAdapter.SelectCommand = myCommand;
            DataSet tharData = new DataSet();
            try
            {
                myConnection.Open();
                myAdapter.Fill(tharData);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                myConnection.Close();
            }

            using (DataTable ScheduleData = new DataTable())
            {
                myAdapter.Fill(ScheduleData);
                dataGridView1.DataSource = ScheduleData;
                dataGridView1.AllowUserToAddRows = false;
            }

            CurrentDate = DateTime.Now;
            string sqlFormattedDate = CurrentDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string constring = "Data Source=APPSHARE01\\SQLEXPRESS01;Initial Catalog=ScheduleChecker;Persist Security Info=True;User ID=PalletCardAdmin;password=Pa!!etCard01";
            SqlConnection con = new SqlConnection(constring);

            try
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO ScheduleLog ([JobNo], [ResourceID], [OrigDuration], [LastUpdate], [ID], [StartOp], [EndOp], [Duration], [JobCompleted], [JobCancelled], [IsAltered], [UpdateUserName], [UpdateDateTime], [Timestamp1])VALUES(@JobNo, @ResourceID, @OrigDuration, @LastUpdate, @ID, @StartOp, @EndOp, @Duration, @JobCompleted, @JobCancelled, @IsAltered, @UpdateUserName, @UpdateDateTime, '" + CurrentDate + "')", con))
                    {
                        cmd.Parameters.AddWithValue("@JobNo", row.Cells["JobNo"].Value);
                        cmd.Parameters.AddWithValue("@ResourceID", row.Cells["ResourceID"].Value);
                        cmd.Parameters.AddWithValue("@OrigDuration", row.Cells["OrigDuration"].Value);
                        cmd.Parameters.AddWithValue("@LastUpdate", row.Cells["LastUpdate"].Value);
                        cmd.Parameters.AddWithValue("@ID", row.Cells["ID"].Value);
                        cmd.Parameters.AddWithValue("@StartOp", row.Cells["StartOp"].Value);
                        cmd.Parameters.AddWithValue("@EndOp", row.Cells["EndOp"].Value);
                        cmd.Parameters.AddWithValue("@Duration", row.Cells["Duration"].Value);
                        cmd.Parameters.AddWithValue("@JobCompleted", row.Cells["JobCompleted"].Value);
                        cmd.Parameters.AddWithValue("@JobCancelled", row.Cells["JobCancelled"].Value);
                        cmd.Parameters.AddWithValue("@IsAltered", row.Cells["IsAltered"].Value);
                        cmd.Parameters.AddWithValue("@UpdateUserName", row.Cells["UpdateUserName"].Value);
                        cmd.Parameters.AddWithValue("@UpdateDateTime", row.Cells["UpdateDateTime"].Value);

                        cmd.Transaction = transaction;
                        cmd.ExecuteNonQuery();
                    }
                }
                transaction.Commit();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
