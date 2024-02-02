using LinqToDB.Mapping;

namespace AuctionApp.Data.Tables
{
    [Table(Name = "CompanyOwnership")]
    public class CompanyOwnership
    {
        [PrimaryKey, NotNull]
        public Guid Id { get; set; }

        [Column, NotNull]
        public string Name { get; set; }
    }
}
