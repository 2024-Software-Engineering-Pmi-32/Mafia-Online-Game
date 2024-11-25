using System;
using System.Linq; // Додано для використання LINQ
using System.Windows.Forms;
using MafiaGameBLL.Services;
using MafiaGameDAL;
using MafiaGamePL.Helpers;

namespace MafiaGamePL
{
    public partial class LoginForm : Form
    {
        private readonly UserService _userService;
        private readonly AppDbContext _context; // Поле для збереження контексту

        public LoginForm()
        {
            InitializeComponent();
            _context = new AppDbContext(); // Ініціалізація контексту
            _userService = new UserService(_context);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (_userService.Login(username, password))
            {
                // Отримання користувача за іменем
                var user = _context.Users.First(u => u.Name == username);
                CurrentUserContext.UserId = user.Id;

                MessageBox.Show("Login successful");
                var mainMenu = new MainMenuForm();
                mainMenu.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect username or password");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (_userService.Register(username, password))
            {
                MessageBox.Show("Registration successful");
            }
            else
            {
                MessageBox.Show("A user with this username already exists");
            }
        }

        private System.ComponentModel.IContainer components = null;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnRegister = new Button();

            this.lblUsername.Text = "Username:";
            this.lblUsername.Location = new System.Drawing.Point(20, 20);
            this.lblUsername.Size = new System.Drawing.Size(70, 20);

            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new System.Drawing.Point(20, 60);
            this.lblPassword.Size = new System.Drawing.Size(70, 20);

            this.txtUsername.Location = new System.Drawing.Point(100, 20);
            this.txtUsername.Size = new System.Drawing.Size(160, 23);

            this.txtPassword.Location = new System.Drawing.Point(100, 60);
            this.txtPassword.Size = new System.Drawing.Size(160, 23);
            this.txtPassword.UseSystemPasswordChar = true;

            this.btnLogin.Text = "Login";
            this.btnLogin.Location = new System.Drawing.Point(100, 100);
            this.btnLogin.Size = new System.Drawing.Size(75, 30);
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            this.btnRegister.Text = "Register";
            this.btnRegister.Location = new System.Drawing.Point(185, 100);
            this.btnRegister.Size = new System.Drawing.Size(75, 30);
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);

            this.ClientSize = new System.Drawing.Size(300, 150);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnRegister);
            this.Name = "LoginForm";
            this.Text = "Login";
        }
    }
}