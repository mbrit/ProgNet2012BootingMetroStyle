using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace StreetFooClient
{
    public static class AppRuntime
    {
        // holds the logon token...
        internal static string LogonToken { get; private set; }

        // holds the system database that's used for system settings (not implemented
        // in this example)...
        private const string SystemDatabaseConnectionString = "StreetFoo-system.db";

        // holds the user database...
        private static string UserDatabaseConnectionString { get; set; }

        // called when a logon is successful...
        public static async void Logon(string username, string logonToken)
        {
            // set the logon token...
            LogonToken = logonToken;

            // set the user database...
            UserDatabaseConnectionString = string.Format("StreetFoo-user-{0}.db", username);

            // check the database tables - we block here (although it's not ideal, we could
            // work it through by adding a callback)...
            var conn = GetUserDatabase();
            await conn.CreateTableAsync<ReportItem>();
        }

        // gets the user's database...
        public static SQLiteAsyncConnection GetUserDatabase()
        {
            return new SQLiteAsyncConnection(UserDatabaseConnectionString);
        }

        public static SQLiteAsyncConnection GetSystemDatabase()
        {
            return new SQLiteAsyncConnection(SystemDatabaseConnectionString);
        }
    }
}
