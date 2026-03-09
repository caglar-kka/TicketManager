using System.Text.Json;
using TicketManager.Models;

namespace TicketManager.Services
{
    internal class UserStorageService
    {
        private const string Filename = "users.json";
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };
        public void SaveUsers(List<User> users)
        {
            string json = JsonSerializer.Serialize(users, JsonOptions);

            File.WriteAllText(Filename, json);

        }
        public List<User> LoadUsers()
        {
            // Wenn Datei nicht existiert leere Liste zurückgeben
            if (!File.Exists(Filename))
            {
                return new();
            }

            // Datei einlesen
            string jsonText = File.ReadAllText(Filename);

            // JSON in Liste umwandeln
            List<User>? loadedUsers = JsonSerializer.Deserialize<List<User>>(jsonText);

            return loadedUsers ?? new();
        }
    }
}
