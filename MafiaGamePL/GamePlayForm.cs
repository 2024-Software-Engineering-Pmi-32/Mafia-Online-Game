using MafiaGameDAL;

public partial class GamePlayForm : Form
{
    private List<Player> players;
    private Dictionary<int, string> playerRoles;
    private int mafiaId;
    private int doctorId;
    private int victimId;
    private int countdown = 30;
    private System.Windows.Forms.Timer countdownTimer;
    private readonly int sessionId;

    private Label lblPhase;
    private Label lblCountdown;
    private ListBox lstPlayers;

    public GamePlayForm(int sessionId)
    {
        this.sessionId = sessionId;
        InitializeComponent();
        LoadPlayers();
    }

    // Ініціалізація компонентів форми
    private void InitializeComponent()
    {
        // Створення елементів форми
        this.lblPhase = new Label();
        this.lblCountdown = new Label();
        this.lstPlayers = new ListBox();

        // Налаштування властивостей lblPhase
        this.lblPhase.Location = new Point(10, 10);
        this.lblPhase.Size = new Size(200, 30);
        this.lblPhase.Text = "Phase: ";

        // Налаштування властивостей lblCountdown
        this.lblCountdown.Location = new Point(10, 40);
        this.lblCountdown.Size = new Size(200, 30);
        this.lblCountdown.Text = "Time remaining: ";

        // Налаштування властивостей lstPlayers
        this.lstPlayers.Location = new Point(10, 70);
        this.lstPlayers.Size = new Size(200, 150);

        // Додавання елементів на форму
        this.Controls.Add(this.lblPhase);
        this.Controls.Add(this.lblCountdown);
        this.Controls.Add(this.lstPlayers);

        // Налаштування властивостей форми
        this.Text = "Mafia Game";
        this.Size = new Size(800, 600);
    }

    // Завантаження гравців поточної сесії з бази даних
    private void LoadPlayers()
    {
        using (var context = new AppDbContext()) // Get the database context
        {
            // Get the players from the current session
            var sessionPlayers = context.SessionPlayers
                .Where(sp => sp.SessionId == sessionId)  // Filtering by SessionId
                .Select(sp => sp.UserId)                 // Selecting the UserId (foreign key)
                .ToList();

            // Now query the User table to get the details for each player
            var users = context.Users
                .Where(u => sessionPlayers.Contains(u.Id)) // Filter users by the selected UserIds
                .ToList();

            // Map the retrieved users to Player objects
            players = users
                .Select(user => new Player { Id = user.Id, Name = user.Name, Role = user.Role })
                .ToList();

            // Clear and populate the ListBox with player names
            lstPlayers.Items.Clear();
            foreach (var player in players)
            {
                lstPlayers.Items.Add(player.Name);
            }
        }

        // Assign roles and start the game phase
        AssignRoles();
        StartNightPhase();
    }

    // Призначення ролей гравцям
    private void AssignRoles()
    {
        var roles = new List<string> { "Mafia", "Doctor", "Citizen", "Citizen", "Citizen" }; // Призначаємо ролі
        playerRoles = new Dictionary<int, string>();

        Random rand = new Random();
        foreach (var player in players)
        {
            int index = rand.Next(roles.Count);
            var role = roles[index];
            roles.RemoveAt(index); // Видаляємо роль з пулу

            playerRoles[player.Id] = role;
        }

        mafiaId = players.FirstOrDefault(p => playerRoles[p.Id] == "Mafia")?.Id ?? -1;
        doctorId = players.FirstOrDefault(p => playerRoles[p.Id] == "Doctor")?.Id ?? -1;
    }

    // Запуск фази ночі
    private void StartNightPhase()
    {
        lblPhase.Text = "Night Phase";
        victimId = -1;
        countdown = 30;
        StartCountdown();
    }

    // Таймер для зворотного відліку
    private void StartCountdown()
    {
        countdownTimer = new System.Windows.Forms.Timer { Interval = 1000 };
        countdownTimer.Tick += CountdownTimer_Tick;
        countdownTimer.Start();
    }

    private void CountdownTimer_Tick(object sender, EventArgs e)
    {
        countdown--;
        lblCountdown.Text = $"Time remaining: {countdown}";

        if (countdown <= 0)
        {
            countdownTimer.Stop();
            OnNightEnd();
        }
    }

    // Завершення фази ночі
    private void OnNightEnd()
    {
        // Логіка ночі
        if (mafiaId != -1 && victimId == -1)
        {
            victimId = players.FirstOrDefault(p => p.Id != mafiaId)?.Id ?? -1;
            lblPhase.Text = "Day Phase";
            StartDayPhase();
        }
    }

    // Запуск фази дня
    private void StartDayPhase()
    {
        countdown = 30;
        StartCountdown();
    }

    // Завершення фази дня
    private void OnDayEnd()
    {
        // Голосування
        var votedPlayer = GetVotedPlayer();
        if (votedPlayer != null)
        {
            players.Remove(votedPlayer);
            lstPlayers.Items.Remove(votedPlayer.Name);
        }

        CheckGameEnd();
    }

    // Отримання гравця, за якого проголосували більшість
    private Player GetVotedPlayer()
    {
        // Тут логіка для голосування, поки просто вибираємо випадкового гравця
        return players.FirstOrDefault();
    }

    // Перевірка, чи закінчилась гра
    private void CheckGameEnd()
    {
        var mafia = players.FirstOrDefault(p => playerRoles[p.Id] == "Mafia");
        var citizens = players.Where(p => playerRoles[p.Id] != "Mafia").ToList();

        if (mafia == null || citizens.Count == 1)
        {
            // Перемога або програш
            lblPhase.Text = mafia == null ? "Citizens Win!" : "Mafia Wins!";
            MessageBox.Show(mafia == null ? "Citizens Win!" : "Mafia Wins!", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}

// Клас для гравця
public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}