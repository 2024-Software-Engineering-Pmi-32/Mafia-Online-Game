using System;
using System.Windows.Forms;
using MafiaGameBLL.Services;
using MafiaGameDAL;
using MafiaGamePL.Helpers;

namespace MafiaGamePL
{
    public class CreateSessionForm : Form
    {
        private TextBox txtSessionName;
        private TextBox txtSessionPassword;
        private Button btnCreate;
        private Label lblMessage;

        public CreateSessionForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtSessionName = new TextBox { Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(200, 23) };
            this.txtSessionPassword = new TextBox { Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(200, 23), UseSystemPasswordChar = true };
            this.btnCreate = new Button { Text = "Create", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(75, 30) };
            this.lblMessage = new Label { Location = new System.Drawing.Point(20, 140), Size = new System.Drawing.Size(200, 30) };

            this.btnCreate.Click += BtnCreate_Click;

            this.ClientSize = new System.Drawing.Size(260, 200);
            this.Controls.Add(new Label { Text = "Session Name:", Location = new System.Drawing.Point(20, 0), Size = new System.Drawing.Size(100, 20) });
            this.Controls.Add(this.txtSessionName);
            this.Controls.Add(new Label { Text = "Password:", Location = new System.Drawing.Point(20, 40), Size = new System.Drawing.Size(100, 20) });
            this.Controls.Add(this.txtSessionPassword);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.lblMessage);
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            string name = txtSessionName.Text;
            string password = txtSessionPassword.Text;

            // Отримуємо ID поточного користувача
            int userId = CurrentUserContext.UserId;

            if (sessionService.CreateSession(name, password, userId))
            {
                lblMessage.Text = "Session created successfully!";
            }
            else
            {
                lblMessage.Text = "Session with this name already exists.";
            }
        }
    }
}