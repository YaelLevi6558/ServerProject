namespace Server.Models
{
    public class PurchaseDetails
    {
        public int PurchaseId { get; set; }
        public string GiftName { get; set; }
        public string? UserName { get; set; }
        public string UserFirstName { get; set; } = null!;
        public string UserLastName { get; set; } = null!;
        public string UserPhone { get; set; }
        public string UserEmail { get; set; } = null!;
        public int NumberOfTickets { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TicketCost { get; set; }

    }

}
