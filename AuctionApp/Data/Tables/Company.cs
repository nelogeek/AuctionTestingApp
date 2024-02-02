using LinqToDB.Mapping;

namespace AuctionApp.Data.Tables
{
    [Table(Name = "Companies")]
    public class Company
    {
        [PrimaryKey, NotNull]
		public Guid Id { get; set; }
        [Column]
		public string RegNum { get; set; }
        [Column]
		public string Inn { get; set; }
        [Column]
		public string Kpp { get; set; }
        [Column]
		public string Ogrn { get; set; }
        [Column]
		public string Location { get; set; }
        [Column]
		public string Phone { get; set; }
        [Column]
		public string CompanyName { get; set; }

        [Column]
        public Guid OwnershipId { get; set; }

        public string Ownership { get; set; }

		[Association(ThisKey = nameof(Id), OtherKey = nameof(LotCompany.CompanyId))]
		public IList<LotCompany> LotCompanies { get; set; }
	}
}
