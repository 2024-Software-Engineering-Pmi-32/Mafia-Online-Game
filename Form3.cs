using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CreateFormElements();
        }

        private void CreateFormElements()
        {
            PictureBox profilePictureBox = new PictureBox
            {
                Size = new Size(300, 300),
                Location = new Point(250, 100),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.CenterImage

            };


            Label signUpLabel = new Label
            {
                Text = "Sign up",
                Location = new Point(800, 50),
                Size = new Size(300, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            Button addPhotoButton = new Button
            {
                Text = "Add photo...",
                Location = new Point(250, 420),
                Size = new Size(300, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            addPhotoButton.FlatAppearance.BorderSize = 2; 
            addPhotoButton.FlatAppearance.BorderColor = Color.Black; 
            addPhotoButton.Click += new EventHandler(addPhotoButton_Click);

            Label nameLabel = new Label
            {
                Text = "Enter your name:",
                Location = new Point(250, 620),
                Size = new Size(300, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            TextBox nameTextBox = new TextBox
            {
                Size = new Size(300, 100),
                Location = new Point(560, 620),
                Font = new Font("Tahoma", 18, FontStyle.Bold), 
                BorderStyle = BorderStyle.FixedSingle
            };



            Label emailLabel = new Label
            {
                Text = "Enter your email:",
                Location = new Point(250, 720),
                Size = new Size(300, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            TextBox emailTextBox = new TextBox
            {
                Size = new Size(300, 100),
                Location = new Point(560, 720),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle
            };

            Label passwordLabel = new Label
            {
                Text = "Password:",
                Location = new Point(950, 150),
                Size = new Size(200, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            TextBox passwordTextBox = new TextBox
            {
                Size = new Size(300, 100),
                Location = new Point(1300, 145),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '*'
            };

            Label repeatPasswordLabel = new Label
            {
                Text = "Repeat password:",
                Location = new Point(950, 250),
                Size = new Size(300, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            TextBox repeatPasswordTextBox = new TextBox
            {
                Size = new Size(300, 100),
                Location = new Point(1300, 245),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '*'
            };

            Button doneButton = new Button
            {
                Text = "Done",
                Location = new Point(1450, 720),
                Size = new Size(150, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };

            doneButton.FlatAppearance.BorderSize = 2; 
            doneButton.FlatAppearance.BorderColor = Color.Black; 
            doneButton.Click += new EventHandler(doneButton_Click);


            this.Controls.Add(profilePictureBox);
            this.Controls.Add(addPhotoButton);
            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);
            this.Controls.Add(emailLabel);
            this.Controls.Add(emailTextBox);
            this.Controls.Add(passwordLabel);
            this.Controls.Add(passwordTextBox);
            this.Controls.Add(repeatPasswordLabel);
            this.Controls.Add(repeatPasswordTextBox);
            this.Controls.Add(signUpLabel);
            this.Controls.Add(doneButton);



            this.Text = "Form 3";
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
        }
                
        private void addPhotoButton_Click(object sender, EventArgs e)
        {
              MessageBox.Show("add Photo Clicked");
        }
        private void doneButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Done Clicked");
        }
    }
}






























