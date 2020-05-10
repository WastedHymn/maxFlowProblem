using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            hideGraphSettingsPanelAtStart();
        }
        private void hideGraphSettingsPanelAtStart()
        {
            graphSettingPanel.Visible = false;
        }

        private void hideGraphSettingsPanel()
        {
            if (graphSettingPanel.Visible == true)
            {
                graphSettingPanel.Visible = false;
            }
        }
        
        private void showGraphSettingsPanel(Panel cPanel)
        {
            if (cPanel.Visible == false)
            {
                cPanel.Visible = true;
            }
            else
            {
                cPanel.Visible = false;
            }
        }

        private Form activeForm = null;
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showGraphSettingsPanel(graphSettingPanel);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openChildForm(new Form3());
        }
    }
}
