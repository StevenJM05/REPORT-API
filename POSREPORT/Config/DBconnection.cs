using MySql.Data.MySqlClient;

namespace POSREPORT.Config
{
    public class DBconnection
    {
        private string _connectionstring;
        protected MySqlConnection _connection;

        public DBconnection() {
            _connectionstring = "Server=192.168.0.5;Database=pos;User ID=local;Password=ysklpepe123;";
        }

        public MySqlConnection GetConnection()
        {
            return _connection;
        }
        public void connect()
        {
            _connection = new MySqlConnection(_connectionstring);
            _connection.Open();
        }

        public void Close()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

    }


}
