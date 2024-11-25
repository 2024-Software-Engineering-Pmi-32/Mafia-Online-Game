using System;
using System.Windows.Forms;
using MafiaGameBLL.Services;
using MafiaGameDAL;

namespace MafiaGamePL
{
    public class MainMenuForm : Form
    {
        private Button btnCreateSession;
        private Button btnSearchSession;
        private Button btnExit;

        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnCreateSession = new Button();
            this.btnSearchSession = new Button();
            this.btnExit = new Button();

            this.btnCreateSession.Text = "Create Session";
            this.btnCreateSession.Location = new System.Drawing.Point(100, 50);
            this.btnCreateSession.Size = new System.Drawing.Size(120, 30);
            this.btnCreateSession.Click += new EventHandler(this.BtnCreateSession_Click);

            this.btnSearchSession.Text = "Search Session";
            this.btnSearchSession.Location = new System.Drawing.Point(100, 100);
            this.btnSearchSession.Size = new System.Drawing.Size(120, 30);
            this.btnSearchSession.Click += new EventHandler(this.BtnSearchSession_Click);

            this.btnExit.Text = "Exit";
            this.btnExit.Location = new System.Drawing.Point(100, 150);
            this.btnExit.Size = new System.Drawing.Size(120, 30);
            this.btnExit.Click += new EventHandler(this.BtnExit_Click);

            this.ClientSize = new System.Drawing.Size(320, 250);
            this.Controls.Add(this.btnCreateSession);
            this.Controls.Add(this.btnSearchSession);
            this.Controls.Add(this.btnExit);
            this.Text = "Main Menu";
        }

        private void BtnCreateSession_Click(object sender, EventArgs e)
        {
            var createSessionForm = new CreateSessionForm();
            createSessionForm.ShowDialog();
        }

        private void BtnSearchSession_Click(object sender, EventArgs e)
        {
            var searchSessionForm = new SearchSessionForm();
            searchSessionForm.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}