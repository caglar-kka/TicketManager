using TicketManager.Models;
using TicketManager.Services;

namespace TicketManager
{
    internal class Menu
    {
        private StorageService storageService;
        private TicketService ticketService;

        private CustomerService customerService;
        private CustomerStorageService customerStorage;
        private List<Customer> customers;

        public void Start(List<Ticket> tickets)
        {
            // Services erzeugen
            storageService = new StorageService();
            ticketService = new TicketService(storageService);

            customerStorage = new CustomerStorageService();
            customers = customerStorage.LoadCustomers();
            customerService = new CustomerService(customerStorage);

            while (true)
            {
                Console.WriteLine("\n--- Ticket-System ---");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1 - Neues Ticket");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("2 - Alle Tickets");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("3 - Suchen");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("4 - Offene Tickets");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("5 - Erledigt setzen");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("6 - Bearbeiten");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("7 - Nach Priorität");

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("8 - Priorität filtern");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("9 - Löschen");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n--- Kunden ---");
                Console.ResetColor();

                Console.WriteLine("10 - Alle Kunden anzeigen");
                Console.WriteLine("11 - Neuen Kunden anlegen");
                Console.WriteLine("12 - Kunden bearbeiten");
                Console.WriteLine("13 - Kunden löschen");
                Console.WriteLine("14 - Kunde suchen");
                Console.WriteLine("15 - Tickets dieses Kunden anzeigen");
                Console.WriteLine("16 - Ticket einem Kunden zuordnen");

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("0 - Beenden");

                Console.ResetColor();

                ticketService.ShowDashboard(tickets);
                Console.WriteLine();

                Console.Write("Auswahl: ");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": // Neues Ticket erstellen
                        ticketService.CreateNewTicket(tickets);
                        break;

                    case "2": // Alle Tickets anzeigen
                        ticketService.ShowAllTickets(tickets);
                        break;

                    case "3": // Ticket suchen
                        ticketService.SearchTickets(tickets);
                        break;

                    case "4": // Nur offene Tickets anzeigen
                        ticketService.ShowFilteredTickets(tickets, t => t.IsOpen);
                        break;

                    case "5": // Ticket als erledigt markieren
                        ticketService.MarkTicketAsCompleted(tickets);
                        break;

                    case "6": // Ticket bearbeiten
                        ticketService.EditTicket(tickets);
                        break;

                    case "7": // Tickets nach Priorität (Hoch -> Niedrig) anzeigen
                        ticketService.ShowTicketsSortedByPriority(tickets);
                        break;

                    case "8": // Nur Tickets mit bestimmter Priorität anzeigen
                        ticketService.ShowTicketsByPriority(tickets);
                        break;

                    case "9": // Ticket löschen
                        ticketService.DeleteTicket(tickets);
                        storageService.SaveTickets(tickets);
                        break;

                    case "10":
                        customerService.ShowAllCustomers(customers);
                        break;

                    case "11":
                        customerService.CreateNewCustomer(customers);
                        break;

                    case "12":
                        customerService.EditCustomer(customers);
                        break;

                    case "13":
                        customerService.DeleteCustomer(customers);
                        break;

                    case "14":
                        customerService.SearchCustomer(customers);
                        break;

                    case "15":
                        customerService.ShowTicketsOfCustomer(customers, tickets);
                        break;

                    case "16":
                        customerService.AssignTicketToCustomer(customers, tickets, storageService);
                        break;

                    case "0": // Programm beenden
                        Console.WriteLine("Programm wird beendet");
                        return;

                    default:
                        Console.WriteLine("Ungültige Eingabe");
                        break;
                }
            }
        }
    }
}