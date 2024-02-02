using LinqToDB;
using LinqToDB.Configuration;

public class ConnectionStringSettings : IConnectionStringSettings
{
    public string ConnectionString { get; set; }
    public string Name { get; set; }
    public string ProviderName { get; set; }
    public bool IsGlobal => false;
}

public class AuctionsDbSettings : ILinqToDBSettings
{
    public IEnumerable<IDataProviderSettings> DataProviders
        => Enumerable.Empty<IDataProviderSettings>();

    public string DefaultConfiguration => "Sqlite";
    public string DefaultDataProvider => "Sqlite";

    public IEnumerable<IConnectionStringSettings> ConnectionStrings
    {
        get
        {
            yield return
                new ConnectionStringSettings
                {
                    Name = "Auctions",
                    ProviderName = ProviderName.SQLite,
                    ConnectionString =
                        @"Data Source=.\\Auctions.sqlite"
                };
        }
    }
}