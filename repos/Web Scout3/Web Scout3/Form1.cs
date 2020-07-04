using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Text.RegularExpressions;

namespace Web_Scout3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            { ScriptErrorsSuppressed = true; }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Work in progress. Finally adds tabs. This is browser mk3 written in C#. Hopefully it will do better");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        WebBrowser webTab = null;

        public bool ScriptErrorsSuppressed { get; }

        private void button4_Click(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Text = "New Tab";
            tabControl1.Controls.Add(tab);
            tabControl1.SelectTab(tabControl1.TabCount - 1);
            webTab = new WebBrowser() { ScriptErrorsSuppressed = true };
            webTab.Parent = tab;
            webTab.Dock = DockStyle.Fill;
            webTab.Navigate("https://www.google.com/");
            textBox1.Text = "https://www.google.com/";
            webTab.DocumentCompleted += WebTab_DocumentCompleted;

        }

        private void WebTab_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = webTab.DocumentTitle;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            WebBrowser web = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (web != null)
            {
                if (web.CanGoBack)
                    web.GoBack();
            }
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            Control control = (Control)sender;

            // Ensure the Form remains square (Height = Width).
            if (control.Size.Height != control.Size.Width)
            {
                control.Size = new Size(control.Size.Width, control.Size.Height);
            }
            this.textBox1.Width = this.Search.Location.X - this.Home.Location.X - 40;
            //Debug.Write(this.Width+", "+ this.Height+"\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Debug.Write("Form loaded");
            this.MinimumSize = new Size(512, 128);
            webBrowser1.Navigate("https://www.google.com/");
            tabControl1.SelectedTab.Text = webBrowser1.DocumentTitle;
            this.textBox1.MinimumSize = new Size(40, 20);
            this.textBox1.Width = this.Search.Location.X - this.Home.Location.X - 40;   
        }


        private void Search_Click(object sender, EventArgs e)
        {
            WebBrowser web = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            Regex urlchk = new Regex(@"\b(?:https?://|www\.)\S+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (web != null)
            {
                if (urlchk.IsMatch(textBox1.Text))
                    web.Navigate(textBox1.Text);
                else
                    web.Navigate("https://www.google.com/search?q=" + textBox1.Text + "&oq=");
                if (textBox1.Text.Contains("hajduk"))
                    web.Navigate("https://gnkdinamo.hr/");
            }
           Debug.WriteLine(urlchk.Match(textBox1.Text));
        }


        private void Home_Click(object sender, EventArgs e)
        {
            WebBrowser web = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (web != null)
                web.Navigate("https://www.google.com/");
        }

        private class WebBrowser_DocumentCompletedEventArgs
        {
        }

        private void Foreward_Click(object sender, EventArgs e)
        {
            WebBrowser web = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (web != null)
            {
                if (web.CanGoForward)
                    web.GoForward();
            }
        }

        internal class WebBrowser_DocumentCompleted
        {
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13)
            {
                return;
            }
            WebBrowser web = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (web != null)
            {
                Search_Click(sender,e);
                //web.Navigate(textBox1.Text);
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            WebBrowser web = tabControl1.SelectedTab.Controls[0] as WebBrowser;
            if (web != null)
            {
                web.Refresh();
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            TabPage current_tab = tabControl1.SelectedTab;
            tabControl1.TabPages.Remove(current_tab);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            WebBrowser web = tabControl1.SelectedTab.Controls[0] as WebBrowser;

                for (int i = tabControl1.TabPages.Count - 1; web!=null&&i >= 0; i--)
                {
                    if(tabControl1.TabPages[i]!=tabControl1.SelectedTab)
                        tabControl1.TabPages.Remove(tabControl1.TabPages[i]);
            }
        }
    }
}