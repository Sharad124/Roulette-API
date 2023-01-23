namespace Roulette_API.Models
{
    public class Bet
    {
        public int BetID { get; set; }
        public decimal BetAmount { get; set; }
        public string? BetItem { get; set; }
        public int BetTypeID { get; set; }
        public int UserID { get; set; }
        public decimal PotentialPayout { get; set; }
        public bool isActive { get; set; }
        
    }
}
