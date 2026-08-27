using Microsoft.Data.Sqlite;

namespace PriceScraper;

public class DBContext
{
    private const string ConnectionString = "Data Source=Gaz.db";

    public static void Initialize()
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        string createStationsTableQuery = @"
            CREATE TABLE IF NOT EXISTS gas_stations (
                id INTEGER PRIMARY KEY,
                name TEXT,
                address TEXT,
                url TEXT UNIQUE
            );";

        string createStationPricesTableQuery = @"
            CREATE TABLE IF NOT EXISTS station_prices (
                id INTEGER PRIMARY KEY,
                station_id INTEGER,
                price DECIMAL,
                scraped_at TEXT,
                FOREIGN KEY(station_id) REFERENCES gas_stations(id)
            );";

        using var stationsCommand = connection.CreateCommand();
        stationsCommand.CommandText = createStationsTableQuery;
        stationsCommand.ExecuteNonQuery();

        using var pricesCommand = connection.CreateCommand();
        pricesCommand.CommandText = createStationPricesTableQuery;
        pricesCommand.ExecuteNonQuery();
    }

    public static void SavePrice(Station station)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        using var insertStation = connection.CreateCommand();
        insertStation.CommandText = @"
            INSERT INTO gas_stations (name, url, address)
            VALUES ($name, $url, $address)
            ON CONFLICT(url) DO NOTHING;
        ";
        insertStation.Parameters.AddWithValue("$name", station.GetName(station));
        insertStation.Parameters.AddWithValue("$url", station.GetUrl(station));
        insertStation.Parameters.AddWithValue("$address", station.GetAddress(station));
        insertStation.ExecuteNonQuery();

        using var getId = connection.CreateCommand();
        getId.CommandText = "SELECT id FROM gas_stations WHERE url = $url;";
        getId.Parameters.AddWithValue("$url", station.GetUrl(station));
        long stationId = (long)getId.ExecuteScalar()!;

        using var insertPrice = connection.CreateCommand();
        insertPrice.CommandText = @"
            INSERT INTO station_prices (station_id, price, scraped_at)
            VALUES ($stationId, $price, $scrapedAt);
        ";
        insertPrice.Parameters.AddWithValue("$stationId", stationId);
        insertPrice.Parameters.AddWithValue("$price", station.GetPrice(station));
        insertPrice.Parameters.AddWithValue("$scrapedAt", DateTime.UtcNow.ToString("s"));
        insertPrice.ExecuteNonQuery();
    }
}