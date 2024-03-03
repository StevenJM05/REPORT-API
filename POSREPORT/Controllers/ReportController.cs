using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using POSREPORT.Config;
using POSREPORT.models;
using System.Data.Common;

namespace POSREPORT.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        [HttpGet("customers/count")]
        public IActionResult countCostuners()
        {
            DBconnection dbconnection = new DBconnection();
            dbconnection.connect();
            int num = 0;
            string query = "SELECT COUNT(id) AS NumOfCostumers FROM customers";
            MySqlCommand command = new MySqlCommand(query, dbconnection.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                num = Convert.ToInt32(reader["NumOfCostumers"]);
            }

            var result = new { count = num };

            return Ok(result);
        }

        [HttpGet("products/count")]

        public IActionResult countProducts()
        {
            DBconnection dbconnection = new DBconnection();
            dbconnection.connect();
            int num = 0;
            string query = "SELECT COUNT(id) AS NumOfProducts FROM products";
            MySqlCommand command = new MySqlCommand(query, dbconnection.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["NumOfProducts"]);
            }

            var result = new { count = num };

            return Ok(result);
        }


        [HttpGet("sales/count")]

        public IActionResult CountSales() 
        {
            DBconnection dbconnection = new DBconnection();
            dbconnection.connect();
            int num = 0;
            string query = "SELECT COUNT(id) AS NumOfSales FROM sales";
            MySqlCommand command = new MySqlCommand(query, dbconnection.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["NumOfSales"]);
            }

            var result = new { count = num };

            return Ok(result);
        }

        [HttpGet("sales/count/range")]
        public IActionResult countSalesDate([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {

            DBconnection dbconnection = new DBconnection();
            dbconnection.connect();
            int num = 0;
            
            string query = $"SELECT COUNT(id) AS CountSalesDate FROM sales WHERE created_at BETWEEN '{fechaInicio.ToString("yyyy-MM-dd")}' AND '{fechaFin.ToString("yyyy-MM-dd")}'";
            MySqlCommand command = new MySqlCommand(query, dbconnection.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                num = Convert.ToInt32(reader["CountSalesDate"]);
            }

            var result = new { count = num };

            return Ok(result);
        }

        [HttpGet("sales/range")]

        public List<Sales> SalesDate([FromQuery] DateTime fechaInicio, [FromQuery] DateTime fechaFin)
        {
            DBconnection dbconnection = new DBconnection();
            dbconnection.connect();

            List<Sales> sales = new List<Sales>();
            string query = $"SELECT * FROM sales WHERE created_at BETWEEN '{fechaInicio.ToString("yyyy-MM-dd")}' AND '{fechaFin.ToString("yyyy-MM-dd")}'";
            MySqlCommand command = new MySqlCommand(query, dbconnection.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                int customerId = Convert.ToInt32(reader["customer_id"]);
                int paymentId = Convert.ToInt32(reader["payment_id"]);
                int userId = Convert.ToInt32(reader["user_id"]);
                int posId = Convert.ToInt32(reader["pos_id"]);

                sales.Add(new Sales(id,customerId, paymentId, userId, posId));
            }

            return sales;
        }

        [HttpGet("Customer/top")]

        public IActionResult customersTop()
        {
            DBconnection dbconnection = new DBconnection();
            dbconnection.connect();

            string query = "select c.id, c.name, c.code, COUNT(*) AS TotalCompras FROM customers c join sales s on c.id = s.customer_id group by c.id, c.name, c.code order by TotalCompras asc";
            MySqlCommand command = new MySqlCommand(query,dbconnection.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            
            int id = 0, TotalSales = 0;
            string name = "", code = "";

            while(reader.Read())
            {
                id = Convert.ToInt32(reader["id"]);
                name = Convert.ToString(reader["name"]);
                code = Convert.ToString(reader["code"]);
                TotalSales = Convert.ToInt32(reader["TotalCompras"]);
            }

            var result = new
            {
                id = id,
                nombre = name,
                code = code,
                TotalSales = TotalSales,
            };

            return Ok(result);

        }
    }
}
