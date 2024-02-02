using LinqToDB.Mapping;
using System.ComponentModel.Design;

namespace AuctionApp.Data.Tables
{
    [Table(Name = "Auctions")]
    public class Auction
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        [Column]
        public string Number { get; set; }
        [Column]
        public string CompetitionOrgan { get; set; }
        [Column]
        public DateTime? DeadLine { get; set; }
        [Column]
        public DateTime? FactDate { get; set; }
        [Column]
        public string Address { get; set; }
        [Column]
        public string AuctionType { get; set; }
        [Column]
        public string AuctionStatusStr { get; set; }
        [Column]
        public DateTime? LastDateChanged { get; set; }
        [Column]
        public DateTime? PublicationDate { get; set; }


    }
}
