using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        private string term = "*";
        private bool first = true;
        private bool scan = false;
        public Form2 termform;
        private System.Media.SoundPlayer player;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label7.Text = "";
            if (first)
            {
                termform = new Form2();
                termform.Show();
                first = false;
                button1.Text = "Change Term";
            }
            else
            {
                if (termform == null)
                {
                    term = "*";
                    label4.Text = "";
                    termform = new Form2();
                    termform.Show();
                }
                else
                {
                    termform.Close();
                    termform = new Form2();
                    termform.Show();
                }
            }
        
        }
            
       

        private void Form1_Load(object sender, EventArgs e)
        {
            player = new System.Media.SoundPlayer(@"alarm.wav");
            button2.Visible = false;
            button3.Visible = false;
            timer1.Start();
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
                  if (termform != null && term == "*")
                {
                    term = termform.getTerm();
                    if (term != "*")
                    {
                        termform.Close();
                        label4.Text = setTerm(term);
                    termform = null;
                    }
                }
            
        }
        private string setTerm(string term)
        {
            if (term == "201908")
                return "Fall 2019";
            if (term == "202001")
                return "Spring 2020";
            if (term == "202005")
                return "Summer 2020";
            return "Error";
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            if(!button2.Visible)
                    button2.Visible = true;
        }

        private string getResult(string crn)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://usfonline.admin.usf.edu/pls/prod/bwckschd.p_disp_detail_sched?term_in=" + term + "&crn_in=" + crn);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    string data = readStream.ReadToEnd();
                    String[] lines = data.Split('\n');
                    response.Close();
                    readStream.Close();
                    int i = 0;
                    try
                    {
                        while (!lines[i].Contains("Seats")) 
                            i++;
                       
                        string seatsLine = lines[i + 3];
                        if (seatsLine[22] == '0' || seatsLine[22] == '-')
                            return "No Open Seats";
                        else
                            return "Open Seats!";
                    }
                    catch (Exception e)
                    {
                        return "Error";
                    }
                }
            }
            catch (Exception e) { }
            return "Eroor";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (term == "*")
                label7.Text = "Please Select a Term";
            else
            {
                button3.Visible = true;
                label7.Text = "";
                button2.Visible = false;
                scan = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Visible = false;
            if (!button2.Visible)
                button2.Visible = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            checkBox3.Visible = false;
            if (!button2.Visible)
                button2.Visible = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            checkBox4.Visible = false;
            if (!button2.Visible)
                button2.Visible = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label16.Text = trackBar1.Value.ToString();
            timer2.Interval = trackBar1.Value*1000;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (scan)
            {
                if (!checkBox1.Visible)
                {
                    label6.Text = getResult(textBox1.Text);
                }
                if (!checkBox2.Visible)
                {
                    label8.Text = getResult(textBox2.Text);
                }
                if (!checkBox3.Visible)
                {
                    label10.Text = getResult(textBox3.Text);
                }
                if (!checkBox4.Visible)
                {
                    label12.Text = getResult(textBox4.Text);
                }
                if (!checkBox5.Visible)
                {
                    label18.Text = getResult(textBox5.Text);
                }
                if (!checkBox6.Visible)
                {
                    label20.Text = getResult(textBox6.Text);
                }
                if (!checkBox7.Visible)
                {
                    label22.Text = getResult(textBox7.Text);
                }
                if (!checkBox8.Visible)
                {
                    label24.Text = getResult(textBox8.Text);
                }
                if (label6.Text == "Open Seats!" || label8.Text == "Open Seats!" || label10.Text == "Open Seats!" || label12.Text == "Open Seats!" || label24.Text == "Open Seats!" || label18.Text == "Open Seats!" || label20.Text == "Open Seats!" || label22.Text == "Open Seats!")
                {
                    player.Play();
                    scan = false;
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            scan = false;
            player.Stop();
            checkBox1.Checked = false;
            checkBox1.Visible = true;
            checkBox2.Checked = false;
            checkBox2.Visible = true;
            checkBox3.Checked = false;
            checkBox3.Visible = true;
            checkBox4.Checked = false;
            checkBox4.Visible = true;
            checkBox5.Checked = false;
            checkBox5.Visible = true;
            checkBox6.Checked = false;
            checkBox6.Visible = true;
            checkBox7.Checked = false;
            checkBox7.Visible = true;
            checkBox8.Checked = false;
            checkBox8.Visible = true;
            button2.Visible = false;
            button3.Visible = false;

        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            checkBox8.Visible = false;
            if (!button2.Visible)
                button2.Visible = true;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            checkBox7.Visible = false;
            if (!button2.Visible)
                button2.Visible = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            checkBox6.Visible = false;
            if (!button2.Visible)
                button2.Visible = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            checkBox5.Visible = false;
            if (!button2.Visible)
                button2.Visible = true;
        }
    }
}
