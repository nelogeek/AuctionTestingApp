using AuctionApp.Data.Tables;
using LinqToDB;

namespace AuctionApp.Data
{
    public class AuctionsDatabase : LinqToDB.Data.DataConnection
    {
        public AuctionsDatabase() : base()
        {
        }
        public AuctionsDatabase(string configuration) : base(configuration)
        {
        }

        public ITable<Auction> Auctions => this.GetTable<Auction>();
        public ITable<Lot> Lots => this.GetTable<Lot>();
        public ITable<Company> Companies => this.GetTable<Company>();
        public ITable<LotCompany> LotCompanies => this.GetTable<LotCompany>();
        public ITable<CompanyOwnership> CompanyOwnerships => this.GetTable<CompanyOwnership>();
    }
}
