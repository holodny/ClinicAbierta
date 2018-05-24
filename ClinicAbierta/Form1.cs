//    nVLC
//    
//    Author:  Roman Ginzburg
//
//    nVLC is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    nVLC is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//    GNU General Public License for more details.
//     
// ========================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Declarations;
using Declarations.Events;
using Declarations.Media;
using Declarations.Players;
using Implementation;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;


namespace ClinicAbierta
{




    public partial class Form1 : Form
    {
        IMediaPlayerFactory m_factory1;
        IMediaPlayerFactory m_factory2;
        IMediaPlayerFactory m_factory3;
        IMediaPlayerFactory m_factory4;
        IMediaPlayerFactory m_factory5;
        IMediaPlayerFactory m_factory6;
        IMediaPlayerFactory m_factory7;
        IMediaPlayerFactory m_factory8;
        IMediaPlayerFactory[] m_factory;


        /* public static IDiskPlayer m_player {get;set;} */
        IDiskPlayer m_player1;
        IDiskPlayer m_player2;
        IDiskPlayer m_player3;
        IDiskPlayer m_player4;
        IDiskPlayer[] m_player;

        IVideoPlayer m_player5;
        IVideoPlayer m_player6;
        IVideoPlayer m_player7;
        IVideoPlayer m_player8;
        IVideoPlayer[] m_playerr;

        IMedia[] m_media;

        IMediaFromFile[] m_filemedia;

        float MediaPosition = 0f;
        long MediaTimeSpan;
        long MediaDuration;
        Boolean shownDuration;

        private int tempHeight = 0, tempWidth = 0;

        // Camera Pan-Tilt controls

        private Thread _cameraThread;
        private Thread _commandThread;
        private WebClient wc;
        private HttpWebRequest req;
        private WebResponse res;
        private Stream stream;
        private string cameraUrl = "http://{0}/snapshot.cgi?user={1}&pwd={2}";
        //private string cameraUrl;
        private string commandUrl = "http://{0}/cgi-bin/CGIProxy.fcgi?usr={1}&pwd={2}&cmd={3}";
        //private string commandUrl;
        private string controlUrl = "http://{0}/cgi-bin/CGIProxy.fcgi?usr={1}&pwd={2}&param={3}&value={4}";
        //private string controlUrl;
        // private string ptzurl = "192.168.1.80:88";
        private string ptzurl;

        public Form1()
        {


            InitializeComponent();
            Screen Srn = Screen.PrimaryScreen;             /*  This works well...  */
            tempHeight = Srn.Bounds.Height;
            tempWidth = Srn.Bounds.Width;

            this.Height = tempHeight;
            this.Width = tempWidth;
            tableLayoutPanel1.Width = tempWidth;
            tableLayoutPanel1.Height = (tempHeight * 9 / 10);
            //tableLayoutPanel1.Height = tempHeight;

            m_factory = new IMediaPlayerFactory[4];

            m_media = new IMedia[4];

            m_filemedia = new IMediaFromFile[4];

            m_player = new IDiskPlayer[4];

            m_playerr = new IVideoPlayer[4];

            m_factory1 = new MediaPlayerFactory(true);
            m_factory2 = new MediaPlayerFactory(true);
            m_factory3 = new MediaPlayerFactory(true);
            m_factory4 = new MediaPlayerFactory(true);
            m_factory5 = new MediaPlayerFactory(true);
            m_factory6 = new MediaPlayerFactory(true);
            m_factory7 = new MediaPlayerFactory(true);
            m_factory8 = new MediaPlayerFactory(true);

            m_player1 = m_factory1.CreatePlayer<IDiskPlayer>();
            m_player2 = m_factory2.CreatePlayer<IDiskPlayer>();
            m_player3 = m_factory3.CreatePlayer<IDiskPlayer>();
            m_player4 = m_factory4.CreatePlayer<IDiskPlayer>();

            m_player5 = m_factory5.CreatePlayer<IVideoPlayer>();
            m_player6 = m_factory6.CreatePlayer<IVideoPlayer>();
            m_player7 = m_factory7.CreatePlayer<IVideoPlayer>();
            m_player8 = m_factory8.CreatePlayer<IVideoPlayer>();

           // m_player1.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
           // m_player1.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
           // m_player1.Events.MediaEnded += new EventHandler(Events_MediaEnded);
           // m_player1.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

           // m_player2.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
           // m_player2.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
           // m_player2.Events.MediaEnded += new EventHandler(Events_MediaEnded);
           // m_player2.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

           // m_player3.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
           // m_player3.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
           // m_player3.Events.MediaEnded += new EventHandler(Events_MediaEnded);
           // m_player3.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

           // m_player4.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
           // m_player4.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
           // m_player4.Events.MediaEnded += new EventHandler(Events_MediaEnded);
           // m_player4.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

            m_player5.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
            m_player5.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
            m_player5.Events.MediaEnded += new EventHandler(Events_MediaEnded);
            m_player5.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

            /*
            m_player6.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
            m_player6.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
            m_player6.Events.MediaEnded += new EventHandler(Events_MediaEnded);
            m_player6.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

            m_player7.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
            m_player7.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
            m_player7.Events.MediaEnded += new EventHandler(Events_MediaEnded);
            m_player7.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);

            m_player8.Events.PlayerPositionChanged += new EventHandler<MediaPlayerPositionChanged>(Events_PlayerPositionChanged);
            m_player8.Events.TimeChanged += new EventHandler<MediaPlayerTimeChanged>(Events_TimeChanged);
            m_player8.Events.MediaEnded += new EventHandler(Events_MediaEnded);
            m_player8.Events.PlayerStopped += new EventHandler(Events_PlayerStopped);
             */

            m_player1.WindowHandle = panel1.Handle;

            // You don't need volume control when every computer already has it.
            trackBar2.Value = 100;//m_player1.Volume > 0 ? m_player1.Volume : 0;  
            /* Fix trackbar functionality later */

            m_player2.WindowHandle = panel2.Handle;
            /* trackBar3.Value = m_player2.Volume > 0 ? m_player2.Volume : 0; */

            m_player3.WindowHandle = panel3.Handle;

            m_player4.WindowHandle = panel4.Handle;

            m_player5.WindowHandle = panel1.Handle;

            m_player6.WindowHandle = panel2.Handle;

            m_player7.WindowHandle = panel3.Handle;

            m_player8.WindowHandle = panel4.Handle;

            UISync.Init(this);


        }

        // myObject is part of an attempt to use datatables
        public class myObject
        {
            public string stream { get; set; }
            public int panel { get; set; }
            public string parameters { get; set; }
            public string cam_url { get; set; }
        }

        // Used with four buttons to show a full screen version of panels 1-4.
        public static void ShowFullScreen(Control ctl)
        {
            // Setup host form to be full screen
            var host = new Form();
            host.StartPosition = FormStartPosition.CenterScreen;
            host.FormBorderStyle = FormBorderStyle.None;
            host.WindowState = FormWindowState.Maximized;
            host.ShowInTaskbar = false;
            // Save properties of control
            var loc = ctl.Location;
            var dock = ctl.Dock;
            var parent = ctl.Parent;
            var form = parent;
            while (!(form is Form)) form = form.Parent;
            // Move control to host
            ctl.Parent = host;
            ctl.Location = Point.Empty;
            ctl.Dock = DockStyle.Fill;
            // Setup event handler to restore control back to form
            host.FormClosing += delegate
            {
                ctl.Parent = parent;
                ctl.Dock = dock;
                ctl.Location = loc;
                form.Show();
            };
            // Exit full screen with escape key
            host.KeyPreview = true;
            host.KeyDown += (KeyEventHandler)((s, e) =>
            {
                if (e.KeyCode == Keys.Escape) host.Close();
            });
            // And go full screen
            host.Show();
            form.Hide();
        }

        public static void ShowPartialScreen(Control ctl)
        {
            // Setup host form to be full screen
            Screen Srn = Screen.PrimaryScreen;
            var host = new Form();
            host.StartPosition = FormStartPosition.Manual;
            int floatbar = Srn.Bounds.Height - 70;
            host.Location = new Point(0, floatbar);
            host.FormBorderStyle = FormBorderStyle.Sizable;
            host.WindowState = FormWindowState.Normal;
            host.ShowInTaskbar = true;
            host.Width = Srn.Bounds.Width;
            host.Height = 75;
            var loc = new Point(0, 1000);
            var parent = ctl.Parent;
            var form = parent;
            while (!(form is Form)) form = form.Parent;

            // Move control to host
            ctl.Parent = host;

            // Setup event handler to restore control back to form
            host.FormClosing += delegate
            {
                ctl.Parent = parent;
                ctl.Location = loc;
                form.Show();
                //form.Height = tempHeight;
            };
            // Exit full screen with escape key
            host.KeyPreview = true;
            host.KeyDown += (KeyEventHandler)((s, e) =>
            {
                if (e.KeyCode == Keys.Escape) host.Close();
            });
            // And go full screen
            host.Show();
            host.Height = 110;
            // host.Width = 500;
            // form.Hide();
        }



        void Events_PlayerStopped(object sender, EventArgs e)
        {
            UISync.Execute(() => InitControls());
        }

        void Events_MediaEnded(object sender, EventArgs e)
        {
            UISync.Execute(() => InitControls());
        }

        private void InitControls()
        {
            trackBar1.Value = 0;
            lblTime.Text = "00:00:00";
            lblDuration.Text = "00:00:00";
            Tbar.Value = 0;
            Tbar.ValueChanged += Tbar_ValueChanged;
            PLayTimer.Tick += PlayTimerTick;
            PLayTimer.Enabled = true;
            PLayTimer.Interval = 1000;
            PLayTimer.Start();
        }

        void Events_TimeChanged(object sender, MediaPlayerTimeChanged e)
        {
           // UISync.Execute(() => lblTime.Text = TimeSpan.FromMilliseconds(e.NewTime).ToString().Substring(0, 8));
        }

        /* for some reason, it continues past the end and errors out here....   */
        void Events_PlayerPositionChanged(object sender, MediaPlayerPositionChanged e)
        {
            if (e.NewPosition <= 1)
            {
                UISync.Execute(() => trackBar1.Value = (int)(e.NewPosition * 1000));
            }
        }

        private void LoadMedia()
        {
            int place;
            int place2;
            string panelplace;
            string common;
            string common1;
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                place = textBox1.Text.LastIndexOf('\\');
                place2 = textBox1.Text.IndexOf(".asf");
                panelplace = textBox1.Text.Substring(place2 - 1,1);
                common = textBox1.Text.Substring((place + 1), ((place2 - 2) - place));
                common1 = textBox1.Text.Substring(0, (place2 - 1));
            }
        }

        void Events_StateChanged(object sender, MediaStateChange e)
        {
            UISync.Execute(() => label1.Text = e.NewState.ToString());
        }

        void Events_DurationChanged(object sender, MediaDurationChange e)
        {
            UISync.Execute(() => lblDuration.Text = TimeSpan.FromMilliseconds(e.NewDuration).ToString().Substring(0, 8));
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            LoadMedia();
        }



        private void button10_Click_1(object sender, EventArgs e)
        {

        }


        private void media_action()
        {

                DataTable ds1 = new DataTable();

                ds1 = DAL.Stream.FindCameras();

                List<myObject> url_list = (from row in ds1.AsEnumerable()
                    select new myObject
                        {   // add url here and to the database so it can be used with the PTZ
                            stream = row.Field<string>("stream"),
                            panel = row.Field<int>("panel"),
                            parameters = row.Field<string>("parameters"),
                            cam_url = row.Field<string>("cam_url")
                        }).ToList();




            string switchExpression = m_action.Text;
            switch (switchExpression)
            {
                case "record":
                    m_player1.Stop();
                    m_player2.Stop();
                    m_player3.Stop();
                    m_player4.Stop();

                    string[] filename = new string[url_list.Count];

                    string[] output = new string[url_list.Count];

                    
                    for (int i = 0; i < url_list.Count; i++)
                    {
                        filename[i] = String.Format("{0:yyyy-MM-dd-hh-mm-ss}_Camera" + (i + 1), DateTime.Now, Name);
                        if (i < (url_list.Count - 1))
                        {
                           // output[i] = @":sout=#duplicate{dst=file{dst=C:\Users\user1\Desktop\video\" + filename[i] + ".asf},dst=display} --udp-caching=750 --tcp-caching=750 --realrtsp-caching=750";    // --control netsync --netsync-master-ip 192.168.1.100
                            output[i] = @":sout=#duplicate{dst=file{dst=C:\Users\user1\Desktop\video\" + filename[i] + ".asf},dst=display}  --control netsync --netsync-master-ip 192.168.1.100";
                        }
                        else
                        {
                           // output[i] = @":sout=#duplicate{dst=file{dst=C:\Users\user1\Desktop\video\" + filename[i] + ".asf},dst=display} --udp-caching=750 --tcp-caching=750 --realrtsp-caching=750";  // --control netsync --netsync-master
                            output[i] = @":sout=#duplicate{dst=file{dst=C:\Users\user1\Desktop\video\" + filename[i] + ".asf},dst=display} --control netsync --netsync-master";
                        }
                    }
                    

                    /*
                    for (int i = 0; i < url_list.Count; i++)
                    {
                        filename[i] = String.Format("{0:yyyy-MM-dd-hh-mm-ss}_Camera" + (i + 1), DateTime.Now, Name);
                        
                        output[i] = @":sout=#duplicate{dst=file{dst=C:\Users\user1\Desktop\video\" + filename[i] + ".asf},dst=display} --udp-caching=750 --tcp-caching=750 --realrtsp-caching=750";  // --control netsync --netsync-master
                        
                    } */


                    for (int i = 0; i < url_list.Count; i++)
                    {
                        filename[i] = String.Format("{0:yyyy-MM-dd-hh-mm-ss}_Camera" + (i + 1), DateTime.Now, Name);

                        output[i] = @":sout=#duplicate{dst=file{dst=C:\Users\user1\Desktop\video\" + filename[i] + ".asf},dst=display}"; 

                    }

                    m_factory = new IMediaPlayerFactory[url_list.Count];

                    for (int i = 0; i < url_list.Count; i++) {
                        m_factory[i] = new MediaPlayerFactory(true);
                        m_media[i] = m_factory[i].CreateMedia<IMedia>(url_list[i].stream, output[i]);
                    }


                    break;

                case "view":

                    /*
                    string[] output1 = new string[url_list.Count];

                    for (int i = 0; i < url_list.Count; i++)
                    {                   
                        output1[i] = @":sout=#display";
                    }

                    m_factory = new IMediaPlayerFactory[url_list.Count];

                    for (int i = 0; i < url_list.Count; i++) {
                        m_factory[i] = new MediaPlayerFactory(true);
                        m_media[i] = m_factory[i].CreateMedia<IMedia>(url_list[i].stream, output1[i]);
                    } */

                    string[] output1 = new string[url_list.Count];

                    for (int i = 0; i < url_list.Count; i++)
                    {
                    /*    if (i < 1)
                        {
                           // output1[i] = @":sout=#display  --rt-priority --use-stream-immediate"; 
                            // --control netsync --netsync-master
                            output1[i] = @":sout=#display  --rt-offset=-2000";  
                        }
                        if (i > 2)
                        {

                          // output1[i] = @":sout=#display --rt-offset=-2000 --stream-filter timeshift";   
                          //--control netsync --netsync-master-ip 192.164.1.60
                            output1[i] = @":sout=#display  --rt-priority --use-stream-immediate";
                        }
                        else
                        {
                            output1[i] = @":sout=#display";   // --control netsync  --netsync-master-ip 192.164.1.60
                        }
                        */
                    }

                    m_factory = new IMediaPlayerFactory[url_list.Count];

                    for (int i = 0; i < url_list.Count; i++) {
                        m_factory[i] = new MediaPlayerFactory(true);
                        m_media[i] = m_factory[i].CreateMedia<IMedia>(url_list[i].stream, output1[i]);
                    }

                     
                    

                    break;

                case "playback":

                    //these are attempts to use video sync
                    string output2 = @":sout=#display --control netsync --netsync-master-ip 192.164.1.60";  // 
                    string output3 = @":sout=#display --control netsync --netsync-master";  // 
                    

                    // string output2 = @":sout=#display";

                    string common;
                    string commonfilename;
                    int place;
                    int place2;


                    // Begin building the common filename so that I can select all videos in a group.
                    place2 = textBox1.Text.IndexOf(".asf");


                    place = textBox1.Text.LastIndexOf('\\');

                    common = textBox1.Text.Substring(0, (place));

                    commonfilename = textBox1.Text.Substring((place + 1), ((place2 - 2) - place));

                    string[] dirs = Directory.GetFiles(common);

                    int t = 0;

                   // m_factory = new IMediaPlayerFactory[4];

                  /*  
                    foreach (string dir in dirs)
                    {
                        if (dir.Contains(commonfilename)) //contains(commonfilename) = true; 
                        {   // get rid of the url_list that came from copying this.  Change it to an array of strings 
                           
                            m_factory[t] = new MediaPlayerFactory(true);
                            if (t <= 2)
                            {
                                m_filemedia[t] = m_factory[t].CreateMedia<IMediaFromFile>(dir, output2);
                            }
                            else
                            {
                                m_filemedia[t] = m_factory[t].CreateMedia<IMediaFromFile>(dir, output3);
                            } 

                            t++;
                        }

                    } */


                    foreach (string dir in dirs)
                    {
                        if (dir.Contains(commonfilename)) //contains(commonfilename) = true; 
                        {   // get rid of the url_list that came from copying this.  Change it to an array of strings 

                            m_factory[t] = new MediaPlayerFactory(true);
                            if (t < 1)
                            {
                                m_filemedia[t] = m_factory[t].CreateMedia<IMediaFromFile>(dir, output2);
                            }
                            else
                            {
                                m_filemedia[t] = m_factory[t].CreateMedia<IMediaFromFile>(dir, output3);
                            }

                            t++;
                        }

                    }




                    /*
                    foreach (string dir in dirs)
                    {
                        if (dir.Contains(commonfilename)) //contains(commonfilename) = true; 
                        {   // get rid of the url_list that came from copying this.  Change it to an array of strings 

                            m_factory[t] = new MediaPlayerFactory(true);

                                m_filemedia[t] = m_factory[t].CreateMedia<IMediaFromFile>(dir, output2);

                            t++;
                        }

                    } */


                    break;
            }

            if ((m_action.Text == "record") | (m_action.Text == "view"))
            {
                m_player1.Open(m_media[0]);
                m_player2.Open(m_media[1]);
                m_player3.Open(m_media[2]);
                m_player4.Open(m_media[3]);



                //m_player4.Pause();

                //System.Threading.Thread.Sleep(1500);


                /*  Add code here to get rid of these lists...
                for (int i = 0; i < url_list.Count; i++)
                {
                   
                }
                 */

                m_media[0].Parse(true);
                m_media[1].Parse(true);
                m_media[2].Parse(true);
                m_media[3].Parse(true);


                m_player1.Play();
                m_player2.Play();
                m_player3.Play();
                m_player4.Play();

                m_player1.Pause();

                //button2.PerformClick();

                /*  Doesn't work.  Streams will pause for a second and then continue.
                m_player1.Pause();
                m_player2.Pause();
                m_player3.Pause();
                m_player4.Pause();
                */

              //  System.Threading.Thread.Sleep(1000);
                
                /*
                DateTime Tthen = DateTime.Now;
                do
                {
                    Application.DoEvents();
                } while (Tthen.AddSeconds(3) > DateTime.Now);
                 */
                

               // m_player1.Play();
            }
            else
            {
                //temporarily commenting out
                m_player5.Open(m_filemedia[0]);

               // if m_player6 not empty?
                m_player6.Open(m_filemedia[1]);
                m_player7.Open(m_filemedia[2]);
                m_player8.Open(m_filemedia[3]);

                /*  Add code here to get rid of these lists...
                for (int i = 0; i < url_list.Count; i++)
                {
                   
                }
                 */

                m_filemedia[0].Parse(true);
                m_filemedia[1].Parse(true);
                m_filemedia[2].Parse(true);
                m_filemedia[3].Parse(true);

                m_player5.Play();
                m_player6.Play();
                m_player7.Play();
                m_player8.Play();
                
            }
        }



        void Events_ParsedChanged(object sender, MediaParseChange e)
        {
            Console.WriteLine(e.Parsed);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            m_player1.Volume = trackBar2.Value;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            m_player1.Stop();
            m_player2.Stop();
            m_player3.Stop();
            m_player4.Stop();
            m_player5.Stop();
            m_player6.Stop();
            m_player7.Stop();
            m_player8.Stop();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (button2.Text  == "Pause")
            {
                button2.Text = "Continue";
            }
            else
            {
                button2.Text = "Pause";
            }
            

            m_player1.Pause();
            m_player2.Pause();
            m_player3.Pause();
            m_player4.Pause();
            m_player5.Pause();
            m_player6.Pause();
            m_player7.Pause();
            m_player8.Pause();

            // Here I was trying to allow the scrubber to work when the video was paused
            // UISync.Execute(() => InitControls());
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //errorProvider1.Clear();
        }

        private class UISync
        {
            private static ISynchronizeInvoke Sync;

            public static void Init(ISynchronizeInvoke sync)
            {
                Sync = sync;
            }

            public static void Execute(Action action)
            {
                Sync.BeginInvoke(action, null);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'oPENSIMDataSet.Camera' table. You can move, or remove it, as needed.
            this.cameraTableAdapter.Fill(this.oPENSIMDataSet.Camera);

        }



        private void button20_Click(object sender, EventArgs e)
        {

            m_action.Text = "view";
            media_action();


        }


        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            panel1.Width = (tempWidth / 2);
            panel2.Width = (tempWidth / 2);
            panel3.Width = (tempWidth / 2);
            panel4.Width = (tempWidth / 2);
            MessageBox.Show("User Resolution is " + tempHeight.ToString() + " X " + tempWidth.ToString());
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            // var loc = new Point(0, 600);
            // tableLayoutPanel2.Location = loc;
            ShowPartialScreen(tableLayoutPanel2);
            tableLayoutPanel1.Width = tempWidth;
            tableLayoutPanel1.Height = tempHeight;
        }


        private void button12_Click_1(object sender, EventArgs e)
        {
            ShowFullScreen(panel1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShowFullScreen(panel2);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ShowFullScreen(panel3);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ShowFullScreen(panel4);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            /* this.Hide(); */
            var ConfigurationForm = new ConfigurationForm();
            ConfigurationForm.Show();
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            UISync.Execute(() => InitControls());
            //MessageBox.Show(Tbar.Value.ToString());
            if (textBox1.Text.Trim().Length == 0)
            {
                MessageBox.Show("You must pick a video file using the Open button first.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {m_action.Text = "playback";
                    media_action(); 
            }

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
                    
            if (this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.FixedSingle)
            {

                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                button6.Text = "Reset Screen";
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                button6.Text = "Full Screen";
            }
        
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {   // Is m_player1 the correct player for viewing?
           // m_player1.Position = (float)trackBar1.Value / 100;
           // m_player2.Position = (float)trackBar1.Value / 100;
           // m_player3.Position = (float)trackBar1.Value / 100;
           // m_player4.Position = (float)trackBar1.Value / 100;
            m_player5.Position = (float)trackBar1.Value / 1000;
            m_player6.Position = (float)trackBar1.Value / 1000;
            m_player7.Position = (float)trackBar1.Value / 1000;
            m_player8.Position = (float)trackBar1.Value / 1000;
        }

        //I don't think that it begins this process the first time I run it.  Do I have to initialize it from the beginning?
        //Why does it only come here after the video has finished the first time?
        private void PlayTimerTick(object sender, System.EventArgs e)
        {
            //update the position based on mediaposition automatically
            //temporarily just assigning this to m_player5
            MediaPosition = m_player5.Position;

            MediaTimeSpan = m_player5.Time;
            if (MediaPosition == 0)
                return;
            if (pnlCtls.Visible)
            {
               // lblTime.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", MediaTimeSpan.Hours, MediaTimeSpan.Minutes, MediaTimeSpan.Seconds);
            }

            try
            {
                //only want to do this once
                if (!shownDuration)
                {
                    //MediaDuration = m_player.Media.Duration;
                    
                    //MediaDuration = m_media.Duration;
                    //lblDuration.Text = string.Format("( {0:D2}:{1:D2}:{2:D2} )", MediaDuration.Hours, MediaDuration.Minutes, MediaDuration.Seconds);
                    Tbar.Minimum = 0;
                    Tbar.Maximum = 1000;
                    Tbar.SmallChange = 10;
                    Tbar.LargeChange = 50;
                    shownDuration = true;

                }

                
                Tbar.SuspendChangeEvent = true;
                //changed the line below to make data types match.
                //Tbar.Value = Convert.ToInt16(MediaPosition * 1000);
                Tbar.Value = Convert.ToInt16(MediaPosition * 1000);
                Tbar.SuspendChangeEvent = false;
               


            }
            catch (Exception ex)
            {
            }

        }

        private void Tbar_ValueChanged(object sender, System.EventArgs e)
        {
            m_player5.Position = (float)Tbar.Value / 1000;
            m_player6.Position = (float)Tbar.Value / 1000;
            m_player7.Position = (float)Tbar.Value / 1000;
            m_player8.Position = (float)Tbar.Value / 1000;
           
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {

        }




        private void button3_Click_1(object sender, EventArgs e)
        {
            m_player5.Play();
        }

        private void Tbar_Scroll_1(object sender, EventArgs e)
        {
            m_player5.Position = (float)Tbar.Value / 1000;
            //MessageBox.Show(Tbar.Value.ToString());
        }

        private void PLayTimer_Tick_1(object sender, EventArgs e)
        {
            //update the position based on mediaposition automatically
            //temporarily just assigning this to m_player1
            MediaPosition = m_player5.Position;
            MediaTimeSpan = m_player5.Time;
            if (MediaPosition == 0)
                return;
            if (pnlCtls.Visible)
            {
                // lblTime.Text = string.Format("{0:D2}:{1:D2}:{2:D2}", MediaTimeSpan.Hours, MediaTimeSpan.Minutes, MediaTimeSpan.Seconds);
            }

            try
            {
                //only want to do this once
                if (!shownDuration)
                {
                    //MediaDuration = m_player.Media.Duration;

                    //MediaDuration = m_media.Duration;
                    //lblDuration.Text = string.Format("( {0:D2}:{1:D2}:{2:D2} )", MediaDuration.Hours, MediaDuration.Minutes, MediaDuration.Seconds);
                    Tbar.Minimum = 0;
                    Tbar.Maximum = 1000;
                    Tbar.SmallChange = 10;
                    Tbar.LargeChange = 50;
                    shownDuration = true;

                }


                Tbar.SuspendChangeEvent = true;
                //changed the line below to make data types match.
                //Tbar.Value = Convert.ToInt16(MediaPosition * 1000);
                Tbar.Value = Convert.ToInt16(MediaPosition * 1000);
                Tbar.SuspendChangeEvent = false;



            }
            catch (Exception ex)
            {


            }

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            m_action.Text = "record";
            media_action();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            m_player1.Pause();
            m_player5.Pause();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //string ptzurl;
            /*
       
            if (comboBox1.Text == "Camera 1")
            {
                // ptzurl = "http://192.168.1.50:88/cgi-bin/CGIProxy.fcgi?usr=pi&pwd=raspberry";
                ptzurl = "192.168.1.50:88";

            }
            else if (comboBox1.Text == "Camera 2")
            {

                //  ptzurl = "http://192.168.1.70:88/cgi-bin/CGIProxy.fcgi?usr=pi&pwd=raspberry";
                ptzurl = "192.168.1.70:88";

            }
            else if (comboBox1.Text == "Camera 3")
            {

                // ptzurl = "http://192.168.1.60:88/cgi-bin/CGIProxy.fcgi?usr=pi&pwd=raspberry";
                ptzurl = "192.168.1.60:88";
            }
            else
            {
                //ptzurl = "http://192.168.1.50:88/cgi-bin/CGIProxy.fcgi?usr=pi&pwd=raspberry";
                //ptzurl = "192.168.1.50:88";
                ptzurl = comboBox1.SelectedValue.ToString();
            }

            */

            ptzurl = comboBox1.SelectedValue.ToString();
  
        }

        //string txtHost = "192.168.1.80:88";
        //string txtHost = "192.168.1.90:88";
        //string txtHost = ptzurl;
        string txtUsername = "pi";
        string txtPassword = "raspberry";

        private void SendCommand(string commandUrl, string command, string optionalValue = "")
        {
            string url = "";
            //if (!optionalValue.Equals("")) url = String.Format(commandUrl, txtHost, txtUsername, txtPassword, command, optionalValue);
            if (!optionalValue.Equals("")) url = String.Format(commandUrl, ptzurl, txtUsername, txtPassword, command, optionalValue);
            //else url = String.Format(commandUrl, txtHost, txtUsername, txtPassword, command);
            else url = String.Format(commandUrl, ptzurl, txtUsername, txtPassword, command);
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

        private void trackBar2_Scroll_1(object sender, EventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.cameraTableAdapter.FillBy(this.oPENSIMDataSet.Camera);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.cameraTableAdapter.FillBy1(this.oPENSIMDataSet.Camera);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.cameraTableAdapter.FillBy1(this.oPENSIMDataSet.Camera);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.cameraTableAdapter.FillBy(this.oPENSIMDataSet.Camera);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                this.cameraTableAdapter.FillBy1(this.oPENSIMDataSet.Camera);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStripButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                this.cameraTableAdapter.FillBy(this.oPENSIMDataSet.Camera);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillByToolStripButton_Click_2(object sender, EventArgs e)
        {
            try
            {
                this.cameraTableAdapter.FillBy(this.oPENSIMDataSet.Camera);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }
    
        //there was an extra closing bracket here, I don't think it needed it.




        /*  Old Junk Below this line ............................................................................................................ */




        //public  MediaPlayerFactory m_factory1 { get; set; }
        //public  IDiskPlayer m_player1 { get; set; }}
    }
}
