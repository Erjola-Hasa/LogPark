using LogPark.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogPark
{
    public partial class SettingSupervizor : Form
    {
        public SettingSupervizor()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Normal;
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            Resolution objFormResizer = new Resolution();
            objFormResizer.ResizeForm(this, screenHeight, screenWidth);

            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

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

        private void tabPage1_Click(object sender, EventArgs e)
        {
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

        }

        private void SettingSupervizor_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Add("English");
            comboBox2.Items.Add("Albanian");
            //  comboBox2.SelectedIndex = 0;
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SaveLanguage = Properties.Settings.Default.Language;
            ChangeLanguage(SaveLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SaveLanguage);

            LanguageService languageService = new LanguageService();
            // int Price = Convert.ToInt32(textBox5.Text);
            int Price = Convert.ToInt32(textBox5.Text);
            languageService.UpdatePrice(Price);
            this.Hide();
            DashboardSupervizor dshs = new DashboardSupervizor();
            dshs.ShowDialog();


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

        private void button3_Click(object sender, EventArgs e)
        {
            string ServerName = textBox6.Text;
            string UserId = textBox1.Text;
            string Password = textBox3.Text;
            string DatabaseName = textBox2.Text;

            ConfigBLL configBLL = new ConfigBLL();
            configBLL.ConnectToDatabase(UserId, Password, ServerName, DatabaseName);
        }
    }
}

