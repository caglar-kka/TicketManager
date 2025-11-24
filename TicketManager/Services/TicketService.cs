using TicketManager.Models;

namespace TicketManager.Services
{
    internal class TicketService
    {
        // Hier merken wir uns das StorageService-Objekt
        private StorageService storageService;

        // Konstruktor mit 1 Parameter
        public TicketService(StorageService storageService)
        {
            this.storageService = storageService;
        }

        // Beispiel-Methode: aktuell nur Platzhalter
        public void ShowAllTickets(List<Ticket> tickets)
        {
            foreach (var ticket in tickets)
            {
                // Farben setzen je nach Status
                ticket.ApplyStatusColor();


                // Ticket anzeigen
                ticket.Print();
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public void ShowTicket(Ticket ticket)
        {
            // Farben setzen je nach Status
            ticket.ApplyStatusColor();

            // Ticket anzeigen
            ticket.Print();
            Console.ResetColor();
        }

        public void SearchTickets(List<Ticket> tickets)
        {
            Console.Write("Suche Ticket: ");
            string search = Console.ReadLine();

            int matchCount = 0; // zählt gefundene Tickets

            foreach (var ticket in tickets)
            {
                if (
                    ticket.Title.ToLower().Contains(search.ToLower()) ||
                    ticket.Description.ToLower().Contains(search.ToLower()) ||
                    ticket.Customer.CompanyName.ToLower().Contains(search.ToLower()) ||
                    ticket.Customer.ContactPerson.ToLower().Contains(search.ToLower()) ||
                    ticket.Customer.Address.City.ToLower().Contains(search.ToLower())
                )
                {
                    ShowTicket(ticket);
                    matchCount++;
                }
            }

            if (matchCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Kein passendes Ticket gefunden");
                Console.ResetColor();
            }
        }
        public void ShowFilteredTickets(List<Ticket> tickets, Func<Ticket, bool> filter)
        {
            foreach (var ticket in tickets)
            {
                if (filter(ticket))
                {
                    ShowTicket(ticket);
                }

            }
        }
        public void MarkTicketAsCompleted(List<Ticket> tickets)
        {
            Console.WriteLine("**************************************");
            foreach (var ticket in tickets) // Alle offenen Tickets anzeigen
            {
                if (ticket.Status != TicketStatus.Erledigt)
                {
                    Console.WriteLine($"ID: {ticket.Id} - {ticket.Title}");
                }
            }

            string input;

            // Benutzer nach Ticket-ID fragen
            while (true)
            {
                Console.Write("Bitte gib die Ticket-ID ein: ");
                input = Console.ReadLine();
                Console.WriteLine("**************************************");

                if (int.TryParse(input, out int result))
                {
                    Ticket ticket = tickets.Find(t => t.Id == result);

                    if (ticket == null)
                    {
                        Console.WriteLine("Kein Ticket mit dieser ID gefunden.");
                    }
                    else if (ticket.Status == TicketStatus.Erledigt)
                    {
                        Console.WriteLine("Dieses Ticket wurde bereits erledigt.");
                    }
                    else
                    {
                        ticket.Status = TicketStatus.Erledigt;
                        storageService.SaveTickets(tickets);
                        Console.WriteLine("Ticket wurde als erledigt markiert.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe! Bitte gib eine gültige Ticket-ID ein.");
                }
            }
        }
        public void EditTicket(List<Ticket> tickets)
        {
            Console.WriteLine("\n--- Bearbeiten: Liste aller Tickets ---");

            foreach (var t in tickets)
            {
                Console.WriteLine($"ID: {t.Id} | {t.Title} | Status: {t.Status}");
            }

            Console.WriteLine("----------------------------------------");
            Console.Write("Bitte gib die Ticket-ID ein, die du bearbeiten möchtest: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id))
            {
                Ticket ticket = tickets.Find(t => t.Id == id);

                if (ticket == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ticket mit dieser ID wurde nicht gefunden");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Ticket #{ticket.Id} wurde gefunden.");
                Console.ResetColor();

                Console.WriteLine("\nWas möchtest du bearbeiten?");
                Console.WriteLine("1 - Titel");
                Console.WriteLine("2 - Beschreibung");
                Console.WriteLine("3 - Status");
                Console.WriteLine("4 - Firmenname");
                Console.WriteLine("5 - Kontaktperson");
                Console.WriteLine("6 - Straße");
                Console.WriteLine("7 - PLZ");
                Console.WriteLine("8 - Stadt");
                Console.WriteLine("0 - Abbrechen");
                Console.Write("Auswahl: ");
                string selection = Console.ReadLine();

                switch (selection)
                {
                    case "1":
                        Console.Write("Neuer Titel: ");
                        ticket.Title = Console.ReadLine();
                        break;

                    case "2":
                        Console.Write("Neue Beschreibung: ");
                        ticket.Description = Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine("Neuen Status wählen:");
                        Console.WriteLine("1 - Offen");
                        Console.WriteLine("2 - In Bearbeitung");
                        Console.WriteLine("3 - Erledigt");
                        Console.Write("Auswahl: ");
                        string statusChoice = Console.ReadLine();

                        switch (statusChoice)
                        {
                            case "1":
                                ticket.Status = TicketStatus.Offen;
                                break;

                            case "2":
                                ticket.Status = TicketStatus.InBearbeitung;
                                break;

                            case "3":
                                ticket.Status = TicketStatus.Erledigt;
                                break;

                            default:
                                Console.WriteLine("Ungültige Auswahl. Status bleibt unverändert");
                                break;
                        }
                        break;

                    case "4":
                        Console.Write("Neuer Firmenname: ");
                        ticket.Customer.CompanyName = Console.ReadLine();
                        break;

                    case "5":
                        Console.Write("Neue Kontaktperson: ");
                        ticket.Customer.ContactPerson = Console.ReadLine();
                        break;

                    case "6":
                        Console.Write("Neue Straße: ");
                        ticket.Customer.Address.Street = Console.ReadLine();
                        break;

                    case "7":
                        Console.Write("Neue PLZ: ");
                        ticket.Customer.Address.PostalCode = Console.ReadLine();
                        break;

                    case "8":
                        Console.Write("Neue Stadt: ");
                        ticket.Customer.Address.City = Console.ReadLine();
                        break;

                    case "0":
                        Console.WriteLine("Bearbeitung abgebrochen");
                        return;

                    default:
                        Console.WriteLine("Ungültige Auswahl");
                        return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ticket wurde erfolgreich bearbeitet");
                storageService.SaveTickets(tickets);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ungültige Eingabe. Bitte gib eine gültige Zahl ein");
                Console.ResetColor();
            }
        }
        public void ShowTicketsSortedByPriority(List<Ticket> tickets)
        {
            var sorted = tickets.OrderByDescending(t => t.Priority).ToList();

            Console.WriteLine("\n--- Tickets nach Priorität (Hoch -> Niedrig) ---");

            foreach (var ticket in sorted)
            {
                ticket.Print();
            }
        }
        public void ShowTicketsByPriority(List<Ticket> tickets)
        {
            // Frage den Benutzer, welche Priorität er sehen will
            Console.Write("Welche Priorität anzeigen? (Hoch, Mittel, Niedrig): ");
            string eingabe = Console.ReadLine();

            // Versuche, die Eingabe in ein gültiges Enum-Wert umzuwandeln
            if (Enum.TryParse(eingabe, true, out PriorityLevel priorität))
            {
                // Nur die Tickets heraussuchen, die diese Priorität haben
                List<Ticket> gefilterteTickets = tickets
                    .Where(t => t.Priority == priorität)
                    .ToList();

                Console.WriteLine($"\nTickets mit Priorität: {priorität}\n");

                // Falls keine Tickets gefunden wurden
                if (gefilterteTickets.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Keine Tickets mit dieser Priorität gefunden.");
                    Console.ResetColor();
                    return;
                }

                // Zeige alle gefundenen Tickets an
                foreach (Ticket ticket in gefilterteTickets)
                {
                    ticket.Print();
                }
            }
            else
            {
                // Wenn die Eingabe ungültig war (z. B. Tippfehler)
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ungültige Eingabe! (Erlaubt: Hoch, Mittel, Niedrig)");
                Console.ResetColor();
            }
        }
        public void CreateNewTicket(List<Ticket> tickets)
        {
            // Eingabe Ticketdaten
            Console.Write("Titel des Tickets: ");
            string titel = Console.ReadLine();

            Console.Write("Beschreibung des Problems: ");
            string beschreibung = Console.ReadLine();

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

            Console.WriteLine("Priorität wählen:");
            Console.WriteLine("1 - Niedrig");
            Console.WriteLine("2 - Mittel");
            Console.WriteLine("3 - Hoch");
            Console.Write("Auswahl: ");
            string input = Console.ReadLine();

            PriorityLevel priority;

            switch (input)
            {
                case "1":
                    priority = PriorityLevel.Niedrig;
                    break;
                case "2":
                    priority = PriorityLevel.Mittel;
                    break;
                case "3":
                    priority = PriorityLevel.Hoch;
                    break;
                default:
                    Console.WriteLine("Ungültige Auswahl. Standard 'Mittel' wird verwendet");
                    priority = PriorityLevel.Mittel;
                    break;
            }

            // Erzeuge Address-Objekt
            Address address = new Address
            {
                Street = street,
                PostalCode = postalCode,
                City = city,
            };

            // Erzeuge Customer-Objekt
            Customer customer = new Customer
            {
                CompanyName = companyName,
                ContactPerson = contactPerson,
                Address = address
            };

            // Ticket erstellen und hinzufügen
            Ticket ticket = new Ticket
            {
                Title = titel,
                Description = beschreibung,
                Customer = customer,
                Priority = priority
            };

            // Prüfung mit IsValid
            if (!ticket.IsValid())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ticketdaten sind ungültig. Ticket wurde NICHT erstellt.");
                Console.ResetColor();
                return;
            }

            // Wenn gültig, zur Liste hinzufügen
            tickets.Add(ticket);
            storageService.SaveTickets(tickets);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ticket wurde erfolgreich erstellt!");
            Console.ResetColor();
        }
        public void ShowDashboard(List<Ticket> tickets)
        {
            int total = tickets.Count;
            int open = tickets.Count(t => t.Status == TicketStatus.Offen);
            int inProgress = tickets.Count(t => t.Status == TicketStatus.InBearbeitung);
            int done = tickets.Count(t => t.Status == TicketStatus.Erledigt);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[Gesamt: {total} | Offen: {open} | Bearbeitung: {inProgress} | Erledigt: {done}]");
            Console.ResetColor();
        }
        public void DeleteTicket(List<Ticket> tickets)
        {
            Console.WriteLine("\n--- Ticket löschen ---");

            // 1. Alle Tickets anzeigen (nur ID + Titel + Status)
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nVerfügbare Tickets:");
            Console.ResetColor();

            foreach (var t in tickets)
            {
                Console.WriteLine($"ID: {t.Id} | {t.Title} | Status: {t.Status} | Priorität: {t.Priority}");
            }

            Console.WriteLine();

            // 2. Benutzer nach ID fragen
            Console.Write("Welche Ticket-ID soll gelöscht werden? ");
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ungültige Eingabe. Bitte eine gültige ID eingeben.");
                Console.ResetColor();
                return;
            }

            // 3. Ticket suchen
            Ticket ticket = tickets.FirstOrDefault(t => t.Id == id);

            if (ticket == null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Kein Ticket mit dieser ID gefunden.");
                Console.ResetColor();
                return;
            }

            // 4. Sicherheitsabfrage
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Sicher löschen? Ticket #{ticket.Id} – \"{ticket.Title}\" (j/n)");
            Console.ResetColor();

            string confirm = Console.ReadLine().ToLower();

            if (confirm != "j")
            {
                Console.WriteLine("Löschen abgebrochen.");
                return;
            }

            // 5. Löschen
            tickets.Remove(ticket);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ticket wurde gelöscht.");
            Console.ResetColor();
        }
    }
}
