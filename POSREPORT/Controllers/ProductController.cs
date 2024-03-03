using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using POSREPORT.Config;
using POSREPORT.models;
using System.Text.Json;
using System.Text;

namespace POSREPORT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        public List<Product> Get()
        {
            DBconnection dBconnection = new DBconnection();
            dBconnection.connect();

            List<Product> products = new List<Product>();


            string query = "select * from products";

            MySqlCommand command = new MySqlCommand(query, dBconnection.GetConnection());
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["id"]);
                string code = Convert.ToString(reader["code"]);
                string name = Convert.ToString(reader["name"]);
                string description = Convert.ToString(reader["description"]);
                bool isService = Convert.ToBoolean(reader["is_service"]);
                int brandId = Convert.ToInt32(reader["brand_id"]);
                int categoryId = Convert.ToInt32(reader["category_id"]);
                int unitMeasureId = Convert.ToInt32(reader["unit_measure_id"]);
                bool isAvailable = Convert.ToBoolean(reader["is_available"]);
                string createdAt = Convert.ToString(reader["created_at"]);

                products.Add(new Product(id,code,name,description,isService,brandId,categoryId,unitMeasureId,isAvailable,createdAt));
            }

            return products;
        }

        [HttpPost]
        public IActionResult Create()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var jsonString = reader.ReadToEndAsync().Result;


                    using (JsonDocument document = JsonDocument.Parse(jsonString))
                    {
                       
                        JsonElement root = document.RootElement;
                        string name = root.GetProperty("name").GetString();
                        string code = root.GetProperty("code").GetString();
                        string description = root.GetProperty("description").GetString();
                        int isService = root.GetProperty("is_service").GetInt32();
                        int brandId = root.GetProperty("brand_id").GetInt32();
                        int categoryId = root.GetProperty("category_id").GetInt32();
                        int unitMeasureId = root.GetProperty("unit_measure_id").GetInt32();
                        int isAvailable = root.GetProperty("is_Available").GetInt32();
                        string createdAt = root.GetProperty("created_at").GetString();
                        string updateAt = root.GetProperty("update_at").GetString();

                        DBconnection dBconnection = new DBconnection();
                        dBconnection.connect();

                        string query = $"INSERT INTO products(name, code, description, is_service, brand_id, category_id, " +
                            $"unit_measure_id,is_available, created_at, updated_at) VALUES('{name}', '{code}','{description}', '{isService}', {brandId}, {categoryId}, {unitMeasureId}, '{isAvailable}', '{createdAt}', '{updateAt}')";
                        MySqlCommand command = new MySqlCommand(query, dBconnection.GetConnection());
                        command.ExecuteNonQuery();
                        return Ok("Product created successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing JSON: {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {
            try
            {

 

                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var jsonString = reader.ReadToEndAsync().Result;


                    using (JsonDocument document = JsonDocument.Parse(jsonString))
                    {

                        JsonElement root = document.RootElement;
                        string name = root.GetProperty("name").GetString();
                        string code = root.GetProperty("code").GetString();
                        string description = root.GetProperty("description").GetString();
                        int isService = root.GetProperty("is_service").GetInt32();
                        int brandId = root.GetProperty("brand_id").GetInt32();
                        int categoryId = root.GetProperty("category_id").GetInt32();
                        int unitMeasureId = root.GetProperty("unit_measure_id").GetInt32();
                        int isAvailable = root.GetProperty("is_Available").GetInt32();
                        string createdAt = root.GetProperty("created_at").GetString();
                        string updateAt = root.GetProperty("update_at").GetString();

                        DBconnection dBconnection = new DBconnection();
                        dBconnection.connect();

                        string query = $"UPDATE products SET " +
                        $"name = '{name}', " +
                        $"code = '{code}', " +
                        $"description = '{description}', " +
                        $"is_service = '{isService}', " +
                        $"brand_id = {brandId}, " +
                        $"category_id = {categoryId}, " +
                        $"unit_measure_id = {unitMeasureId}, " +
                        $"is_available = '{isAvailable}', " +
                        $"updated_at = '{updateAt}' " +
                        $"WHERE id = {id}";

                        MySqlCommand command = new MySqlCommand(query, dBconnection.GetConnection());
                        command.ExecuteNonQuery();
                        return Ok("Product update successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing JSON: {ex.Message} {ex.StackTrace}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                DBconnection dBconnection = new DBconnection();
                dBconnection.connect();
                string query = $"DELETE FROM products WHERE id = {id}";
                MySqlCommand command = new MySqlCommand(query, dBconnection.GetConnection());
                command.ExecuteNonQuery();

                return Ok("Se elimino correctamente");
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Ocurrio un error");
            }

        }

    }
    


}