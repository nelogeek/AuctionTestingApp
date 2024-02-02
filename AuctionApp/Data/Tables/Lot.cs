using LinqToDB.Mapping;

namespace AuctionApp.Data.Tables
{
    [Table(Name = "Lots")]
    public class Lot
    {
        [PrimaryKey, NotNull]
        public Guid Id { get; set; }
        [Column, NotNull]
        public Guid AuctionId { get; set; }
        [Column]
        public string ShortName { get; set; }
        [Column]
        public string Placement { get; set; }
        [Column]
        public double? Area { get; set; }
        [Column]
        public int? UseagePeriod { get; set; }
        [Column]
        public string FullName { get; set; }
        [Column]
        public string Comment { get; set; }
        [Column]
        public double? Deposit { get; set; }
        [Column]
        public Guid? CompanyWinnerId { get; set; }
        [Column]
        public double? TotalPrice { get; set; }
        [Column]
        public string AuctionResults { get; set; }
        [Column]
        public string LotNum { get; set; }
        [Column]
        public string Resource { get; set; }
        [Column]
        public string Currency { get; set; }
        [Column]
        public double? Payment { get; set; }
        [Column]
        public double? StartPrice { get; set; }

        [Association(ThisKey = nameof(Id), OtherKey = nameof(LotCompany.LotId))]
        public IList<Company> Companies { get; set; }

    }
}