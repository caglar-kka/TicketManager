using TicketManager.Models;
using TicketManager.Services;

namespace TicketManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Tickets holen
            StorageService storage = new StorageService();
            Menu menu = new Menu();
            List<Ticket> tickets;

            if (!File.Exists("tickets.json"))
            {
                tickets = TicketList.GetTicketList();

                // und sofort einmal speichern
                storage.SaveTickets(tickets);
            }
            else
            {
                // Wenn Datei existiert dann gespeicherte Tickets nehmen
                tickets = storage.LoadTickets();
            }

            // Menü starten
            menu.Start(tickets);
        }
    }
}

