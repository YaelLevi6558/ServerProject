namespace Server.Models
{
    public class GiftConnection
    {
        public int GiftId { get; set; }

        public string GiftName { get; set; } = null!;
        public string CategoryName { get; set; } = null;
        public string DonorName { get; set; } = null;
        public decimal TicketCost { get; set; }

        public string? ImageUrl { get; set; }

        public virtual Category? Category { get; set; } = null!;

        public virtual Donor? Donor { get; set; } = null!;
    }
}
