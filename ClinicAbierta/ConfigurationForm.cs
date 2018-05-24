using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace ClinicAbierta
{
    public partial class ConfigurationForm : Form
    {
        private Thread _cameraThread;
        private Thread _commandThread;
        private WebClient wc;
        private HttpWebRequest req;
        private WebResponse res;
        private Stream stream;
        private string cameraUrl = "http://{0}/snapshot.cgi?user={1}&pwd={2}";
        private string commandUrl = "http://{0}/cgi-bin/CGIProxy.fcgi?usr={1}&pwd={2}&cmd={3}";
        private string controlUrl = "http://{0}/cgi-bin/CGIProxy.fcgi?usr={1}&pwd={2}&param={3}&value={4}";

        public ConfigurationForm()
        {
            InitializeComponent();
            tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch ((sender as TabControl).SelectedIndex)
            {
                case 0:
                    this.cameraBindingNavigator.Visible = false;
                    break;
                case 1:
                    this.cameraBindingNavigator.Visible = false;
                    break;
                case 2:
                    this.cameraBindingNavigator.Visible = false;
                    break;
                case 3:
                    this.cameraBindingNavigator.Visible = false;
                    break;
                case 4:
                    this.cameraBindingNavigator.Visible = false;
                    break;
                case 5:
                    this.cameraBindingNavigator.Visible = false;
                    break;
                case 6:
                    this.cameraBindingNavigator.Visible = false;
                    break;
                case 7:
                    this.cameraBindingNavigator.Visible = true;
                    break;

            }
        }

        private void cameraBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.cameraBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.oPENSIMDataSet);

        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'oPENSIMDataSet.Camera' table. You can move, or remove it, as needed.
            this.cameraTableAdapter.Fill(this.oPENSIMDataSet.Camera);

        }

        string txtHost = "192.168.1.80:88";
        string txtUsername = "foscam1";
        string txtPassword = "password";

        private void SendCommand(string commandUrl, string command, string optionalValue = "")
        {
            string url = "";
            if (!optionalValue.Equals("")) url = String.Format(commandUrl, txtHost, txtUsername, txtPassword, command, optionalValue);
            else url = String.Format(commandUrl, txtHost, txtUsername, txtPassword, command);
            WebClient wc = new WebClient();
            wc.DownloadString(url);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                MessageBox.Show("What the Ctrl+F?");
                return true;
            }

            else if (keyData == (Keys.Right))
            {
                SendCommand(commandUrl, "ptzMoveRight");
                //Thread.Sleep(250);
                SendCommand(commandUrl, "1");
                return true;
            }


            else if (keyData == (Keys.Left))
            {
                SendCommand(commandUrl, "ptzMoveLeft");
                //Thread.Sleep(250);
                SendCommand(commandUrl, "1");
                return true;
            }


            else if (keyData == (Keys.Up))
            {
                SendCommand(commandUrl, "ptzMoveUp");
                //Thread.Sleep(250);
                SendCommand(commandUrl, "1");
                return true;
            }


            else if (keyData == (Keys.Down))
            {
                SendCommand(commandUrl, "ptzMoveDown");
                //Thread.Sleep(250);
                SendCommand(commandUrl, "1");
            }

            else if (keyData == (Keys.PageUp))
            {
                SendCommand(commandUrl, "ptzStopRun");
                //Thread.Sleep(250);
                SendCommand(commandUrl, "1");
            }
            return base.ProcessCmdKey(ref msg, keyData);


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cameraDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cameraBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }







    }

    /*
        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
        
        }

    */
    

      /*  private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'opensimDataSet.camera' table. You can move, or remove it, as needed.



        }

        private void ConfigurationForm_Close(object sender, EventArgs e)
        {


        }

    */


      
    }
 
