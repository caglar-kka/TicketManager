using System.Text.Json;
using TicketManager.Models;

namespace TicketManager.Services
{
    internal class CustomerStorageService
    {
        private const string FileName = "customers.json";

        public void SaveCustomers(List<Customer> customers)
        {
            string json = JsonSerializer.Serialize(customers, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(FileName, json);
        }

        public List<Customer> LoadCustomers()
        {
            if (!File.Exists(FileName))
                return new List<Customer>();

            string json = File.ReadAllText(FileName);
            List<Customer> customers = JsonSerializer.Deserialize<List<Customer>>(json);

            if (customers == null)
            {
                return new List<Customer>();
            }
            else
            {
                return customers;
            }
        }
    }
}
