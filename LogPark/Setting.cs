﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using LogPark.BLL;
using System.Threading;
using LogPark.DAL;
using Microsoft.VisualBasic;
using System.Drawing;

namespace LogPark
{
    public partial class Setting : Form
    {
     

        public Setting()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.Manual;
            Rectangle screen = Screen.FromPoint(Cursor.Position).WorkingArea;
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            Location = new Point(screen.Left + (screen.Width - w) / 2, screen.Top + (screen.Height - h) / 2);
            Size = new Size(w, h);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
          

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedItem.ToString() == "Albanian")
            {
                ChangeLanguage("sq-AL");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("sq");
                Properties.Settings.Default.Language = "sq-AL";
            }

            else
            {
                ChangeLanguage("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                Properties.Settings.Default.Language = "en";
            }
            Properties.Settings.Default.Save();
        }

        private void ChangeLanguage(string lang)
        {
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(Setting));
                CultureInfo cultureInfo = new CultureInfo(lang);

                doRecursiveLoading(this, cultureInfo, resources);
              
            }
        }

        private void doRecursiveLoading(Control parent, CultureInfo cultureInfo, ComponentResourceManager resources)
        {
            foreach (Control c in parent.Controls)
            {
                resources.ApplyResources(c, c.Name, cultureInfo);
                if (c.Controls.Count > 0)
                    doRecursiveLoading(c, cultureInfo, resources);
            }
        }


        private void Setting_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Add("English");
           comboBox2.Items.Add("Albanian");
            //  comboBox2.SelectedIndex = 0;
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);


        }



        public void button3_Click(object sender, EventArgs e)
        {

            string ServerName = textBox6.Text;
            string UserId = textBox1.Text;
            string Password = textBox3.Text;
            string DatabaseName = textBox2.Text;

            ConfigBLL configBLL = new ConfigBLL();
            configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string UserId = textBox1.Text;
            string Password = textBox3.Text;
            string DatabaseName = textBox2.Text;


            ConfigBLL configBLL = new ConfigBLL();
            configBLL.TestDatabase(UserId, Password, ServerName, DatabaseName);
           

        }

        private void button2_Click(object sender, EventArgs e)
        {

             string SaveLanguage = Properties.Settings.Default.Language;
             ChangeLanguage(SaveLanguage);
             Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);
             Properties.Settings.Default.Save();

            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
          
         

          


        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.Enabled = false;
        }
    }



}


   