#include <iostream>
#include <sqlite3.h>
#include <string>

float get_average() {
    std::cout << "Inside get_average func\n";
    return 0;
}

int main() {
    sqlite3* db = nullptr;

    int result = sqlite3_open("Gaz.db", &db);

    if (result == SQLITE_OK) {
        std::cout << "Connected Successfully to Database\n";
    } else {
        std::cout << "Failed to connect: " << sqlite3_errmsg(db) << "\n";
    }

    std::cout << get_average() << "\n";

    sqlite3_close(db);
    return 0;
}