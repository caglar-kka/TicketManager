namespace TicketManager
{
    internal class TicketList
    {
        public static List<Ticket> GetTicketList()
        {
            return new List<Ticket>
            {
                new Ticket
                {
                    Title = "Rechner startet nicht",
                    Description = "Der PC von Herr Meier fährt nicht hoch.",
                    Status = TicketStatus.Offen,
                    Priority = PriorityLevel.Hoch,
                    Customer = new Customer
                    {
                        CompanyName = "IT Solutions GmbH",
                        ContactPerson = "Herr Meier",
                        Address = new Address
                        {
                            Street = "Technikweg 7",
                            PostalCode = "20095",
                            City = "Hamburg"
                        }
                    }
                },
                new Ticket
                {
                    Title = "Drucker offline",
                    Description = "Der Netzwerkdrucker im Lager reagiert nicht.",
                    Status = TicketStatus.Offen,
                    Priority = PriorityLevel.Hoch,
                    Customer = new Customer
                    {
                        CompanyName = "LogiTrans AG",
                        ContactPerson = "Frau Schneider",
                        Address = new Address
                        {
                            Street = "Industriestraße 15",
                            PostalCode = "90402",
                            City = "Nürnberg"
                        }
                    }
                },
                new Ticket
                {
                    Title = "E-Mail-Problem",
                    Description = "Frau Müller kann keine Mails mehr empfangen.",
                    Status = TicketStatus.InBearbeitung,
                    Priority = PriorityLevel.Mittel,
                    Customer = new Customer
                    {
                        CompanyName = "OfficeLine GmbH",
                        ContactPerson = "Frau Müller",
                        Address = new Address
                        {
                            Street = "Kanzleistraße 8",
                            PostalCode = "50667",
                            City = "Köln"
                        }
                    }
                },
                new Ticket
                {
                    Title = "Monitor flackert",
                    Description = "Der Bildschirm im Büro 3 flackert ständig.",
                    Status = TicketStatus.Erledigt,
                    Priority = PriorityLevel.Niedrig,
                    Customer = new Customer
                    {
                        CompanyName = "DesignPro Studio",
                        ContactPerson = "Herr Weber",
                        Address = new Address
                        {
                            Street = "Grafikerweg 3",
                            PostalCode = "80331",
                            City = "München"
                        }
                    }
                }
            };
        }
    }
}