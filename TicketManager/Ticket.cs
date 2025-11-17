namespace TicketManager
{
    public enum TicketStatus
    {
        Offen,
        InBearbeitung,
        Erledigt
    }
    public enum PriorityLevel
    {
        Niedrig,
        Mittel,
        Hoch
    }

    internal class Ticket
    {
        private static int lastId = 0;
        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; }
        public Customer Customer { get; set; }
        public PriorityLevel Priority { get; set; }

        private TicketStatus status;
        public TicketStatus Status
        {
            get => status;

            set
            {
                status = value;

                if (status == TicketStatus.Erledigt)
                {
                    Priority = PriorityLevel.Niedrig;
                }
            }
        }

        public bool IsOpen
        {
            get
            {
                return Status != TicketStatus.Erledigt;
            }
        }

        public Ticket()
        {
            CreatedAt = DateTime.Now;
            Status = TicketStatus.Offen;
            Priority = PriorityLevel.Mittel;
            lastId++;
            Id = lastId;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title)
                && !string.IsNullOrWhiteSpace(Description)
                && Customer != null && Customer.IsValid();
        }
        public void ApplyStatusColor()
        {
            switch (Status)
            {
                case TicketStatus.Offen:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case TicketStatus.InBearbeitung:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case TicketStatus.Erledigt:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }

        public void Print()
        {
            ApplyStatusColor();

            Console.WriteLine($"Ticket #{Id}");
            Console.WriteLine($"Titel: {Title}");
            Console.WriteLine($"Beschreibung: {Description}");
            Console.WriteLine($"Firmenname: {Customer.CompanyName}");
            Console.WriteLine($"Kontaktperson: {Customer.ContactPerson}");
            Console.WriteLine($"Adresse: {Customer.Address.Street}, {Customer.Address.PostalCode}, {Customer.Address.City}");
            Console.WriteLine($"Status: {Status}");
            Console.Write("Priorität: ");
            PrintPriorityWithColor(Priority);
            Console.WriteLine($"Erstellt am: {CreatedAt}");
            Console.WriteLine("--------------------------------------------");

            Console.ResetColor();
        }
        public static void PrintPriorityWithColor(PriorityLevel prio)
        {
            switch (prio)
            {
                case PriorityLevel.Hoch:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case PriorityLevel.Mittel:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case PriorityLevel.Niedrig:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            Console.Write("\u25CF "); // ●

            Console.ResetColor();
            Console.WriteLine(prio);
        }
        public static ConsoleColor GetPriorityColor(PriorityLevel priority)
        {
            // Je nach Priorität die passende Farbe zurückgeben
            switch (priority)
            {
                case PriorityLevel.Hoch:
                    return ConsoleColor.Red;

                case PriorityLevel.Mittel:
                    return ConsoleColor.Yellow;

                case PriorityLevel.Niedrig:
                    return ConsoleColor.Green;

                default:
                    return ConsoleColor.Gray;
            }
        }
    }
}