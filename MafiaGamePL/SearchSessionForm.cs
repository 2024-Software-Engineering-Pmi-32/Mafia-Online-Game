using System;
using System.Linq;
using System.Windows.Forms;
using MafiaGameBLL.Services;
using MafiaGameDAL;
using MafiaGameDAL.Models;
using MafiaGamePL.Helpers;

namespace MafiaGamePL
{
    public class SearchSessionForm : Form
    {
        private ListBox lstSessions;
        private TextBox txtSessionPassword;
        private Button btnJoin;
        private Button btnDelete;
        private Label lblMessage;

        public SearchSessionForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lstSessions = new ListBox { Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(240, 120) };
            this.txtSessionPassword = new TextBox { Location = new System.Drawing.Point(20, 150), Size = new System.Drawing.Size(240, 23), UseSystemPasswordChar = true };
            this.btnJoin = new Button { Text = "Join", Location = new System.Drawing.Point(20, 190), Size = new System.Drawing.Size(75, 30) };
            this.btnDelete = new Button { Text = "Delete", Location = new System.Drawing.Point(120, 190), Size = new System.Drawing.Size(75, 30) };
            this.lblMessage = new Label { Location = new System.Drawing.Point(20, 230), Size = new System.Drawing.Size(240, 30) };

            this.btnJoin.Click += BtnJoin_Click;
            this.btnDelete.Click += BtnDelete_Click;

            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(new Label { Text = "Available Sessions:", Location = new System.Drawing.Point(20, 0), Size = new System.Drawing.Size(120, 20) });
            this.Controls.Add(this.lstSessions);
            this.Controls.Add(new Label { Text = "Enter Password:", Location = new System.Drawing.Point(20, 130), Size = new System.Drawing.Size(120, 20) });
            this.Controls.Add(this.txtSessionPassword);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnDelete);

            LoadSessions();
        }

        private void LoadSessions()
        {
            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            var sessions = sessionService.GetSessions();
            lstSessions.Items.Clear();
            foreach (var session in sessions)
            {
                lstSessions.Items.Add(session.Name);
            }
        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            if (lstSessions.SelectedItem == null)
            {
                lblMessage.Text = "Please select a session.";
                return;
            }

            var selectedSessionName = lstSessions.SelectedItem.ToString();
            var password = txtSessionPassword.Text;

            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            var session = context.Sessions.FirstOrDefault(s => s.Name == selectedSessionName);
            var userId = CurrentUserContext.UserId;  // Отримуємо userId поточного користувача

            // Передаємо sessionName, password та userId
            if (sessionService.JoinSession(selectedSessionName, password, userId))
            {
                lblMessage.Text = "Successfully joined the session!";
                MessageBox.Show($"You have joined the session: {selectedSessionName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var gameTableForm = new GameTableForm(session.Id);
                gameTableForm.Show();
                this.Close();
            }
            else
            {
                lblMessage.Text = "Invalid password or session.";
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lstSessions.SelectedItem == null)
            {
                lblMessage.Text = "Please select a session.";
                return;
            }

            var selectedSessionName = lstSessions.SelectedItem.ToString();
            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            var session = context.Sessions.FirstOrDefault(s => s.Name == selectedSessionName);

            if (session == null)
            {
                lblMessage.Text = "Session not found.";
                return;
            }

            // Отримання ID поточного користувача через CurrentUserContext
            var userId = CurrentUserContext.UserId;

            if (sessionService.DeleteSession(session.Id, userId))
            {
                lblMessage.Text = "Session deleted successfully!";
                LoadSessions(); // Оновити список сесій
            }
            else
            {
                lblMessage.Text = "You are not allowed to delete this session.";
            }
        }
    }
}