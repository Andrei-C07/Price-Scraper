#include <iostream>
#include <sqlite3.h>
#include <string>

float get_average(sqlite3* db) {
    //query
    std::string query = "SELECT AVG(price) AS average_price FROM station_prices";

    
    sqlite3_stmt* stmt;
    sqlite3_prepare_v2(db, query.c_str(), -1, &stmt, nullptr);

    sqlite3_step(stmt);

    float average = sqlite3_column_double(stmt, 0);

    sqlite3_finalize(stmt);

    return average;
}

int main() {
    sqlite3* db = nullptr;

    int result = sqlite3_open("Gaz.db", &db);

    if (result == SQLITE_OK) {
        std::cout << "Connected Successfully to Database\n";
    } else {
        std::cout << "Failed to connect: " << sqlite3_errmsg(db) << "\n";
    }

    std::cout << get_average(db) << " c/L\n";

    sqlite3_close(db);
    return 0;
}