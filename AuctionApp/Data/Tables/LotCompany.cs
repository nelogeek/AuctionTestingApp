using LinqToDB.Mapping;

namespace AuctionApp.Data.Tables
{
    [Table(Name = "LotCompany")]
    public class LotCompany
    {
        [PrimaryKey, NotNull]
        public Guid Id { get; set; }
        [PrimaryKey, NotNull]
        public Guid LotId { get; set; }
        [PrimaryKey, NotNull]
        public Guid CompanyId { get; set; }

        [Association(ThisKey = nameof(LotId), OtherKey = nameof(Tables.Lot.Id))]
        public Lot Lot { get; set; }

        [Association(ThisKey = nameof(CompanyId), OtherKey = nameof(Tables.Company.Id))]
        public Company Company { get; set; }
    }
}
