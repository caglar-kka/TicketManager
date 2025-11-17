namespace TicketManager
{
    internal class Customer
    {
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public Address Address { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(CompanyName)
                && !string.IsNullOrWhiteSpace(ContactPerson)
                && Address != null && Address.IsValid();
        }
    }
}
