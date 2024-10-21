using Npgsql;
using System;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Host=localhost;Username=postgres;Password=1111;Database=MafiaData";

        try
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Connection successful!");
                
                FillDatabaseWithTestData(conn);

                DisplayUsers(conn);
                DisplayRoles(conn);
                DisplaySessions(conn);
                DisplayPlayers(conn);
                DisplayMessages(conn);

                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }
    }

    static void DisplayUsers(NpgsqlConnection conn)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM Users", conn))
        using (var reader = cmd.ExecuteReader())
        {
            Console.WriteLine("\nUsers:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Nickname: {reader["nickname"]}, Email: {reader["email"]}, IsBanned: {reader["isBanned"]}");
            }
        }
    }

    static void DisplayRoles(NpgsqlConnection conn)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM Roles", conn))
        using (var reader = cmd.ExecuteReader())
        {
            Console.WriteLine("\nRoles:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Name: {reader["name"]}, Description: {reader["description"]}");
            }
        }
    }

    static void DisplaySessions(NpgsqlConnection conn)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM Sessions", conn))
        using (var reader = cmd.ExecuteReader())
        {
            Console.WriteLine("\nSessions:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, Title: {reader["title"]}, Host: {reader["host"]}, IsPublic: {reader["isPublic"]}, Capacity: {reader["capacity"]}");
            }
        }
    }

    static void DisplayPlayers(NpgsqlConnection conn)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM Players", conn))
        using (var reader = cmd.ExecuteReader())
        {
            Console.WriteLine("\nPlayers:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, UserID: {reader["userId"]}, SessionID: {reader["sessionId"]}, Nickname: {reader["nickname"]}, IsAlive: {reader["isAlive"]}");
            }
        }
    }

    static void DisplayMessages(NpgsqlConnection conn)
    {
        using (var cmd = new NpgsqlCommand("SELECT * FROM Messages", conn))
        using (var reader = cmd.ExecuteReader())
        {
            Console.WriteLine("\nMessages:");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]}, SessionID: {reader["sessionId"]}, PlayerID: {reader["playerId"]}, Message: {reader["message"]}, Timestamp: {reader["timestamp"]}");
            }
        }
    }

    static void FillDatabaseWithTestData(NpgsqlConnection conn)
    {
        Random random = new Random();

        for (int i = 0; i < 30; i++)
        {
            string nickname = "User" + (i + 1);
            string email = $"user{i + 1}@example.com";
            string password = "password" + (i + 1);
            bool isBanned = random.Next(0, 2) == 0;

            using (var cmd = new NpgsqlCommand("INSERT INTO Users (nickname, email, password, isBanned) VALUES (@nickname, @email, @password, @isBanned)", conn))
            {
                cmd.Parameters.AddWithValue("nickname", nickname);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Parameters.AddWithValue("isBanned", isBanned);
                cmd.ExecuteNonQuery();
            }
        }

        string[] roleNames = { "Mafia", "Civilian", "Doctor", "Sheriff" };
        foreach (var roleName in roleNames)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO Roles (name, description) VALUES (@name, @description)", conn))
            {
                cmd.Parameters.AddWithValue("name", roleName);
                cmd.Parameters.AddWithValue("description", $"{roleName} description");
                cmd.ExecuteNonQuery();
            }
        }

        for (int i = 0; i < 10; i++)
        {
            string title = "Session " + (i + 1);
            string password = "sessionpassword" + (i + 1);
            bool isPublic = random.Next(0, 2) == 0;
            int capacity = random.Next(4, 10);

            using (var cmd = new NpgsqlCommand("INSERT INTO Sessions (host, title, password, isPublic, capacity) VALUES (@host, @title, @password, @isPublic, @capacity)", conn))
            {
                cmd.Parameters.AddWithValue("host", random.Next(1, 31));
                cmd.Parameters.AddWithValue("title", title);
                cmd.Parameters.AddWithValue("password", password);
                cmd.Parameters.AddWithValue("isPublic", isPublic);
                cmd.Parameters.AddWithValue("capacity", capacity);
                cmd.ExecuteNonQuery();
            }
        }

        for (int i = 1; i <= 30; i++)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO Players (userId, sessionId, nickname, roleId) VALUES (@userId, @sessionId, @nickname, @roleId)", conn))
            {
                cmd.Parameters.AddWithValue("userId", random.Next(1, 31));
                cmd.Parameters.AddWithValue("sessionId", random.Next(1, 11));
                cmd.Parameters.AddWithValue("nickname", "Player" + i);
                cmd.Parameters.AddWithValue("roleId", random.Next(1, 5));
                cmd.ExecuteNonQuery();
            }
        }

        for (int i = 1; i <= 50; i++)
        {
            using (var cmd = new NpgsqlCommand("INSERT INTO Messages (sessionId, playerId, message) VALUES (@sessionId, @playerId, @message)", conn))
            {
                cmd.Parameters.AddWithValue("sessionId", random.Next(1, 11));
                cmd.Parameters.AddWithValue("playerId", random.Next(1, 31));
                cmd.Parameters.AddWithValue("message", "Message " + i);
                cmd.ExecuteNonQuery();
            }
        }
    }
}