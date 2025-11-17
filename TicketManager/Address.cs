namespace TicketManager
{
    internal class Address
    {
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Street)
                && !string.IsNullOrWhiteSpace(PostalCode)
                && PostalCode.All(char.IsDigit) && PostalCode.Length == 5
                && !string.IsNullOrWhiteSpace(City);
        }
    }
}
