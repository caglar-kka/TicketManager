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

    }
}


