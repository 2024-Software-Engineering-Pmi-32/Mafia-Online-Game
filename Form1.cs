using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateFormElements();
        }

        private void CreateFormElements()
        {
            PictureBox pictureBox = new PictureBox
            {
                Size = new Size(530, 450), 
                Location = new Point(670, 100), 
                BorderStyle = BorderStyle.FixedSingle,
                Image = Image.FromFile("C:\\Users\\irase\\OneDrive\\Зображення\\Знімки екрана\\Знімок екрана 2024-10-27 105443.png"),
                SizeMode = PictureBoxSizeMode.StretchImage 
            }; 
        


            Label loginLabel = new Label
            {
                Text = "Login",
                Location = new Point(780, 595),
                AutoSize = true,
                Font = new Font("Tahoma", 18, FontStyle.Bold) 

            };
            TextBox loginTextBox = new TextBox
            {
                Location = new Point(940, 600),
                Width = 130, 
                Font = new Font("Tahoma", 10) 

            };

            Label passwordLabel = new Label
            {
                Text = "Password",
                Location = new Point(780, 625),
                AutoSize = true,
                Font = new Font("Tahoma", 18, FontStyle.Bold) 
            };
            TextBox passwordTextBox = new TextBox
            {
                Location = new Point(940, 630),
                Width = 130, 
                Font = new Font("Tahoma", 10), 
                PasswordChar = '*' 
            };

            Button signUpButton = new Button
            {
                Text = "Sign Up",
                Location = new Point(900, 700),
                AutoSize = true 
            };
            signUpButton.Click += new EventHandler(SignUpButton_Click);

            this.Controls.Add(pictureBox);
            this.Controls.Add(loginLabel);
            this.Controls.Add(loginTextBox);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(passwordTextBox);
            this.Controls.Add(signUpButton);

            this.Text = "Login Form";
            this.Size = new Size(1920, 1080);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sign Up Clicked");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


    }
}
