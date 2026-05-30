using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace TaskManager_Sabitov.Classes.Database
{
    public class Config
    {
        public readonly static string ConnectionData =
              @"server=localhost;
              database=taskmanager;
              uid=root;
              pwd=1234";

        public static readonly MySqlServerVersion Version = new MySqlServerVersion(new Version(8,0,11));
    }
}
