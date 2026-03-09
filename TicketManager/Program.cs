using TicketManager.Models;
using TicketManager.Services;

namespace TicketManager
{
    public class Program
    {
        static void Main()
        {
            // Umlaute sauber anzeigen
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Tickets holen
            StorageService storage = new();
            Menu menu = new();
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

            // User-Services
            UserStorageService userStorage = new();
            UserService userService = new(userStorage);
            List<User> users = userStorage.LoadUsers();

            // Vormenü vor dem eigentlichen Ticket-Menü
            while (true)
            {
                Console.WriteLine("\n--- Benutzerbereich ---");
                Console.WriteLine("1 - User anlegen");
                Console.WriteLine("2 - Login");
                Console.WriteLine("3 - User löschen");
                Console.WriteLine("0 - Beenden");
                Console.Write("Auswahl: ");

                string input = Console.ReadLine() ?? string.Empty;

                switch (input)
                {
                    case "1":
                        userService.CreateNewUser(users);
                        break;

                    case "2":
                        bool loginErfolgreich = userService.Login(users);

                        if (loginErfolgreich)
                        {
                            menu.Start(tickets);
                        }
                        break;

                    case "3":
                        userService.DeleteUser(users);
                        break;

                    case "0":
                        Console.WriteLine("Programm wird beendet.");
                        return;

                    default:
                        Console.WriteLine("Ungültige Eingabe.");
                        break;
                }
            }
        }
    }
}

