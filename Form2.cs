using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            CreateFormElements();
        }

        
        private void CreateFormElements()
        {
            PictureBox pictureBox2 = new PictureBox
            {
                Size = new Size(160, 160),
                Location = new Point(870, 135),
                BorderStyle = BorderStyle.None, 
                Image = Image.FromFile("C:\\Users\\irase\\OneDrive\\Зображення\\Знімки екрана\\Знімок екрана 2024-10-27 210826.png"),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            pictureBox2.Paint += (sender, e) =>
            {
                using (Pen blackPen = new Pen(Color.Black, 5))
                {
                    e.Graphics.DrawRectangle(blackPen, 0, 0, pictureBox2.Width - 1, pictureBox2.Height - 1);
                }
            };





            Button CreateRoom = new Button
            {
                Text = "Create room",
                Location = new Point(790, 400),
                Size = new Size(320, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat 
            };

            CreateRoom.FlatAppearance.BorderSize = 2; 
            CreateRoom.FlatAppearance.BorderColor = Color.Black; 
            CreateRoom.Click += new EventHandler(CreateRoom_Click);




            Button JoinRoom = new Button
            {
                Text = "Join to room",
                Location = new Point(790, 475),
                Size = new Size(320, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            JoinRoom.FlatAppearance.BorderSize = 2; 
            JoinRoom.FlatAppearance.BorderColor = Color.Black; 
            JoinRoom.Click += new EventHandler(JoinRoom_Click);


            Button Rules = new Button
            {
                Text = "Rules",
                Location = new Point(700, 600),
                Size = new Size(200, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            Rules.FlatAppearance.BorderSize = 2; 
            Rules.FlatAppearance.BorderColor = Color.Black; 
            Rules.Click += new EventHandler(Rules_Click);



            Button Setting = new Button
            {
                Text = "Setting",
                Location = new Point(1000, 600),
                Size = new Size(200, 50),
                Font = new Font("Tahoma", 18, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            Setting.FlatAppearance.BorderSize = 2; 
            Setting.FlatAppearance.BorderColor = Color.Black; 
            Setting.Click += new EventHandler(Setting_Click);



            this.Controls.Add(pictureBox2);
            this.Controls.Add(CreateRoom);
            this.Controls.Add(JoinRoom);
            this.Controls.Add(Rules);
            this.Controls.Add(Setting);


            this.Text = "Login Form";
            this.Size = new Size(1920, 1080);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void CreateRoom_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Create room Clicked");
        }

        private void JoinRoom_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("Join to room Clicked");
        }


        private void Rules_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Rules Clicked");
        }


        private void Setting_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Setting Clicked");
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {
        }


    }
}
