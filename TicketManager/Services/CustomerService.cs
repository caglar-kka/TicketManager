using TicketManager.Models;

namespace TicketManager.Services
{
    internal class CustomerService
    {
        private readonly CustomerStorageService storage;
        public CustomerService(CustomerStorageService storageService)
        {
            storage = storageService;
        }
        public void ShowAllCustomers(List<Customer> customers)
        {
            Console.WriteLine("\n--- Alle Kunden ---");

            if (customers.Count == 0)
            {
                Console.WriteLine("Es sind keine Kunden gespeichert.");
                return;
            }

            foreach (var c in customers)
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine($"Firma: {c.CompanyName}");
                Console.WriteLine($"Kontakt: {c.ContactPerson}");
                Console.WriteLine($"Adresse: {c.Address.Street}, {c.Address.PostalCode} {c.Address.City}");
            }
        }
        public void CreateCustomer(List<Customer> customers)
        {
            Console.WriteLine("\n--- Neuen Kunden anlegen ---");

            Console.Write("Firmenname: ");
            string companyName = Console.ReadLine();

            Console.Write("Kontaktperson: ");
            string contactPerson = Console.ReadLine();

            Console.Write("Straße: ");
            string street = Console.ReadLine();

            Console.Write("PLZ: ");
            string postalCode = Console.ReadLine();

            Console.Write("Stadt: ");
            string city = Console.ReadLine();

            // Adresse erstellen
            Address address = new Address
            {
                Street = street,
                PostalCode = postalCode,
                City = city
            };

            // Kunde erstellen
            Customer customer = new Customer
            {
                CompanyName = companyName,
                ContactPerson = contactPerson,
                Address = address
            };

            // Prüfen, ob Kunde gültig ist
            if (!customer.IsValid())
            {
                Console.WriteLine("Kundendaten ungültig! Kunde wurde NICHT gespeichert.");
                return;
            }

            // Kunde zur Liste hinzufügen
            customers.Add(customer);

            // Datei speichern
            storage.SaveCustomers(customers);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kunde wurde erfolgreich erstellt!");
            Console.ResetColor();
        }
        public void CreateNewCustomer(List<Customer> customers)
        {
            Console.WriteLine("\n--- Neuen Kunden anlegen ---");

            Console.Write("Firmenname: ");
            string firma = Console.ReadLine();

            Console.Write("Kontaktperson: ");
            string kontakt = Console.ReadLine();

            Console.Write("Straße: ");
            string strasse = Console.ReadLine();

            Console.Write("PLZ: ");
            string plz = Console.ReadLine();

            Console.Write("Stadt: ");
            string stadt = Console.ReadLine();

            Customer neuer = new Customer
            {
                CompanyName = firma,
                ContactPerson = kontakt,
                Address = new Address
                {
                    Street = strasse,
                    PostalCode = plz,
                    City = stadt
                }
            };

            customers.Add(neuer);
            storage.SaveCustomers(customers);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kunde wurde erfolgreich angelegt!");
            Console.ResetColor();
        }
        public void EditCustomer(List<Customer> customers)
        {
            Console.WriteLine("\n--- Kunden bearbeiten ---");

            if (customers.Count == 0)
            {
                Console.WriteLine("Keine Kunden vorhanden.");
                return;
            }

            // Kunden Nummern anzeigen
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {customers[i].CompanyName} ({customers[i].ContactPerson})");
            }

            Console.Write("\nNummer des Kunden eingeben: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 1 || index > customers.Count)
            {
                Console.WriteLine("Ungültige Eingabe.");
                return;
            }

            Customer customer = customers[index - 1];

            Console.WriteLine($"\nGefundener Kunde: {customer.CompanyName}");

            // Menü was geändert werden soll
            Console.WriteLine("Was möchtest du ändern?");
            Console.WriteLine("1 - Firmenname");
            Console.WriteLine("2 - Kontaktperson");
            Console.WriteLine("3 - Straße");
            Console.WriteLine("4 - PLZ");
            Console.WriteLine("5 - Stadt");
            Console.WriteLine("0 - Abbrechen");
            Console.Write("Auswahl: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Neuer Firmenname: ");
                    customer.CompanyName = Console.ReadLine();
                    break;

                case "2":
                    Console.Write("Neue Kontaktperson: ");
                    customer.ContactPerson = Console.ReadLine();
                    break;

                case "3":
                    Console.Write("Neue Straße: ");
                    customer.Address.Street = Console.ReadLine();
                    break;

                case "4":
                    Console.Write("Neue PLZ: ");
                    customer.Address.PostalCode = Console.ReadLine();
                    break;

                case "5":
                    Console.Write("Neue Stadt: ");
                    customer.Address.City = Console.ReadLine();
                    break;

                case "0":
                    Console.WriteLine("Bearbeitung abgebrochen.");
                    return;

                default:
                    Console.WriteLine("Ungültige Auswahl.");
                    return;
            }

            // Speichern
            storage.SaveCustomers(customers);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kunde wurde erfolgreich bearbeitet!");
            Console.ResetColor();
        }
        public void SearchCustomer(List<Customer> customers)
        {
            Console.WriteLine("\n--- Kunden suchen ---");

            Console.Write("Suchbegriff eingeben: ");
            string search = Console.ReadLine().ToLower();

            int foundCount = 0;

            foreach (var c in customers)
            {
                if (c.CompanyName.ToLower().Contains(search) ||
                    c.ContactPerson.ToLower().Contains(search) ||
                    c.Address.City.ToLower().Contains(search))
                {
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine($"Firma: {c.CompanyName}");
                    Console.WriteLine($"Kontakt: {c.ContactPerson}");
                    Console.WriteLine($"Adresse: {c.Address.Street}, {c.Address.PostalCode} {c.Address.City}");
                    foundCount++;
                }
            }

            if (foundCount == 0)
            {
                Console.WriteLine("Keine passenden Kunden gefunden.");
            }
        }
        public void ShowTicketsOfCustomer(List<Customer> customers, List<Ticket> tickets)
        {
            Console.WriteLine("\n--- Tickets eines Kunden anzeigen ---");

            if (customers.Count == 0)
            {
                Console.WriteLine("Keine Kunden vorhanden.");
                return;
            }

            // Kundenliste anzeigen
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {customers[i].CompanyName} ({customers[i].ContactPerson})");
            }

            Console.Write("\nNummer des Kunden eingeben: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 1 || index > customers.Count)
            {
                Console.WriteLine("Ungültige Eingabe.");
                return;
            }

            Customer selected = customers[index - 1];

            Console.WriteLine($"\nTickets für: {selected.CompanyName}\n");

            // Alle Tickets suchen, die diesen Kunden haben
            var customerTickets = tickets
                .Where(t => t.Customer.CompanyName.ToLower() == selected.CompanyName.ToLower())
                .ToList();

            if (customerTickets.Count == 0)
            {
                Console.WriteLine("Dieser Kunde hat keine Tickets.");
                return;
            }

            foreach (var t in customerTickets)
            {
                t.Print();
                Console.WriteLine();
            }
        }
        public void DeleteCustomer(List<Customer> customers)
        {
            Console.WriteLine("\n--- Kunden löschen ---");

            if (customers.Count == 0)
            {
                Console.WriteLine("Es gibt keine Kunden zum Löschen.");
                return;
            }

            // Kundenliste anzeigen
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {customers[i].CompanyName} ({customers[i].ContactPerson})");
            }

            Console.Write("\nNummer des zu löschenden Kunden: ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int index) || index < 1 || index > customers.Count)
            {
                Console.WriteLine("Ungültige Eingabe.");
                return;
            }

            // Sicherheitsabfrage
            Customer c = customers[index - 1];
            Console.WriteLine($"\nSoll der Kunde '{c.CompanyName}' wirklich gelöscht werden? (j/n)");
            string confirm = Console.ReadLine().ToLower();

            if (confirm != "j")
            {
                Console.WriteLine("Löschen abgebrochen.");
                return;
            }

            customers.RemoveAt(index - 1);
            storage.SaveCustomers(customers);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kunde erfolgreich gelöscht!");
            Console.ResetColor();
        }
        public void AssignTicketToCustomer(List<Customer> customers, List<Ticket> tickets, StorageService ticketStorage)
        {
            Console.WriteLine("\n--- Ticket einem Kunden zuordnen ---");

            if (customers.Count == 0)
            {
                Console.WriteLine("Keine Kunden vorhanden.");
                return;
            }

            if (tickets.Count == 0)
            {
                Console.WriteLine("Keine Tickets vorhanden.");
                return;
            }

            // 1. Tickets anzeigen
            Console.WriteLine("\nTickets:");
            foreach (var t in tickets)
            {
                Console.WriteLine($"ID: {t.Id} | {t.Title} | Kunde: {t.Customer.CompanyName}");
            }

            Console.Write("\nTicket-ID eingeben: ");
            string inputTicket = Console.ReadLine();

            if (!int.TryParse(inputTicket, out int ticketId))
            {
                Console.WriteLine("Ungültige Eingabe.");
                return;
            }

            Ticket ticket = tickets.FirstOrDefault(t => t.Id == ticketId);

            if (ticket == null)
            {
                Console.WriteLine("Ticket wurde nicht gefunden.");
                return;
            }

            // 2. Kunden anzeigen
            Console.WriteLine("\nKunden:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {customers[i].CompanyName} ({customers[i].ContactPerson})");
            }

            Console.Write("\nNummer des neuen Kunden: ");
            string inputCustomer = Console.ReadLine();

            if (!int.TryParse(inputCustomer, out int customerIndex) || customerIndex < 1 || customerIndex > customers.Count)
            {
                Console.WriteLine("Ungültige Eingabe.");
                return;
            }

            Customer selectedCustomer = customers[customerIndex - 1];

            // 3. Zuordnen
            ticket.Customer = selectedCustomer;

            // 4. Tickets speichern
            ticketStorage.SaveTickets(tickets);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ticket #{ticket.Id} wurde dem Kunden '{selectedCustomer.CompanyName}' zugeordnet.");
            Console.ResetColor();
        }
    }
}


