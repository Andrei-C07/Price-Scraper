using Microsoft.Data.Sqlite;

namespace PriceScraper;

public class DBContext
{
    public static void initialize()
    {
        using var connection = new SqliteConnection("Data Source=Gaz.db");
        connection.Open();

        string createStationsTableQuery = @"
            CREATE TABLE IF NOT EXISTS gas_stations (
                id INTEGER PRIMARY KEY,
                name TEXT,
                address TEXT,
                url TEXT
            );";

        string createStationPricesTableQuery = @"
            CREATE TABLE IF NOT EXISTS staion_prices (
                id INTEGER PRIMARY KEY,
                station_id INTEGER,
                price DECIMAL,
                FOREIGN KEY(station_id) REFERENCES gas_stations(id)
            );
        ";

        using var stationsCommand = connection.CreateCommand();
        stationsCommand.CommandText = createStationsTableQuery;
        stationsCommand.ExecuteNonQuery();

        using var pricesCommand = connection.CreateCommand();
        pricesCommand.CommandText = createStationPricesTableQuery;
        pricesCommand.ExecuteNonQuery();


    }
}