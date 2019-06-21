using MySql.Data.MySqlClient;

namespace TestTask
{
    class DBConnection
    {
        private static DBConnection instance = null;
        public MySqlConnection Connection { get; private set; }
        //Tworzmy singleton do łączenia się z naszą bazą danych
        public static DBConnection Instance
        {
            get
            {

                return instance ?? (instance = new DBConnection());
            }
        }
        private DBConnection()
        {
            MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                UserID = DB.DBInfo.user,
                Password = DB.DBInfo.password,
                Database = DB.DBInfo.database,
                Port = uint.Parse(DB.DBInfo.port),
                Server = DB.DBInfo.server
            };
            Connection = new MySqlConnection(connectionStringBuilder.ToString());
        }
    }
}
