using System;
using System.Linq;
using System.Windows.Forms;
using MafiaGameBLL.Services;
using MafiaGameDAL;
using MafiaGamePL.Helpers;

namespace MafiaGamePL
{
    public class GameTableForm : Form
    {
        private ListBox lstPlayers;
        private Button btnReady;
        private Button btnLeave;
        private Label lblCountdown;

        private readonly int sessionId;
        private readonly int currentUserId;
        private System.Windows.Forms.Timer countdownTimer;
        private int countdown = 10;

        public GameTableForm(int sessionId)
        {
            this.sessionId = sessionId;
            this.currentUserId = CurrentUserContext.UserId;

            InitializeComponent();
            LoadPlayers();
        }

        private void InitializeComponent()
        {
            this.lstPlayers = new ListBox { Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(300, 200) };
            this.btnReady = new Button { Text = "Ready", Location = new System.Drawing.Point(20, 240), Size = new System.Drawing.Size(75, 30) };
            this.btnLeave = new Button { Text = "Leave", Location = new System.Drawing.Point(120, 240), Size = new System.Drawing.Size(75, 30) };
            this.lblCountdown = new Label { Location = new System.Drawing.Point(20, 280), Size = new System.Drawing.Size(300, 30), Text = "" };

            this.btnReady.Click += BtnReady_Click;
            this.btnLeave.Click += BtnLeave_Click;
            this.FormClosing += GameTableForm_FormClosing;

            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(new Label { Text = "Players in Session:", Location = new System.Drawing.Point(20, 0), Size = new System.Drawing.Size(120, 20) });
            this.Controls.Add(this.lstPlayers);
            this.Controls.Add(this.btnReady);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.lblCountdown);

            LoadPlayers();
        }

        private void LoadPlayers()
        {
            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            // Отримуємо список гравців для цієї сесії
            var players = sessionService.GetPlayersInSession(sessionId);

            lstPlayers.Items.Clear();
            foreach (var player in players)
            {
                lstPlayers.Items.Add(player.Name); // Виводимо ім'я гравця або іншу інформацію
            }
        }

        private void BtnReady_Click(object sender, EventArgs e)
        {
            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            var isReady = btnReady.Text == "Ready";
            sessionService.SetPlayerReadyStatus(sessionId, currentUserId, isReady);

            btnReady.Text = isReady ? "Cancel" : "Ready";

            LoadPlayers();

            // Якщо "Cancel", то зупиняємо відлік
            if (btnReady.Text == "Ready")
            {
                StopCountdown();
            }

            CheckAllPlayersReady();
        }


        private void BtnLeave_Click(object sender, EventArgs e)
        {
            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            bool success = sessionService.LeaveSession(sessionId, currentUserId);

            if (success)
            {
                MessageBox.Show("You have left the session.", "Leave", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error while leaving the session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GameTableForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            // Перевірка, чи є користувач у сесії
            if (!sessionService.IsPlayerInSession(sessionId, currentUserId))
            {
                MessageBox.Show("You are not in the session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = sessionService.LeaveSession(sessionId, currentUserId);

            if (result)
            {
                MessageBox.Show("You have left the session.", "Exit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error while leaving the session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckAllPlayersReady()
        {
            var context = new AppDbContext();
            var sessionService = new SessionService(context);

            var allReady = sessionService.AreAllPlayersReady(sessionId);

            if (allReady)
            {
                StartCountdown();
            }
            else
            {
                StopCountdown();
            }
        }

        private void StartCountdown()
        {
            countdown = 10;
            lblCountdown.Text = $"Game starts in: {countdown}";

            countdownTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();
        }

        private void StopCountdown()
        {
            if (countdownTimer != null)
            {
                countdownTimer.Stop();
                countdownTimer.Dispose();
                countdownTimer = null;
                lblCountdown.Text = "";
            }
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            countdown--;

            if (countdown < 0)
            {
                countdownTimer.Stop();
                MessageBox.Show("Game started!", "Game Start", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var gamePlayForm = new GamePlayForm(sessionId);
                gamePlayForm.Show();
                this.Hide();
            }
            else
            {
                lblCountdown.Text = $"Game starts in: {countdown}";
            }
        }
    }
}