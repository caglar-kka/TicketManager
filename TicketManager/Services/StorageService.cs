using System.Text.Json;
using TicketManager.Models;

namespace TicketManager.Services
{
    internal class StorageService
    {
        public void SaveTickets(List<Ticket> tickets)
        {
            string json = JsonSerializer.Serialize(tickets,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                }
            );

            File.WriteAllText("tickets.json", json);
        }

        public List<Ticket> LoadTickets()
        {
            // Wenn Datei nicht existiert leere Liste zurückgeben
            if (!File.Exists("tickets.json"))
            {
                return new List<Ticket>();
            }

            // Datei einlesen
            string jsonText = File.ReadAllText("tickets.json");

            // JSON in Liste umwandeln
            List<Ticket> loadedTickets = JsonSerializer.Deserialize<List<Ticket>>(jsonText);

            // Falls JSON fehlerhaft war
            if (loadedTickets == null)
            {
                loadedTickets = new List<Ticket>();
            }

            return loadedTickets;
        }
    }
}
