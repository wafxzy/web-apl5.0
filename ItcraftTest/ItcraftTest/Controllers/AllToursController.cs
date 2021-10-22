using ItcraftTest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ItcraftTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllToursController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AllToursController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select id_t, title, description_t,
                    stars,price,continent
                    ,photo1, photo2, photo3,photo4, photo5, photo
                    from dbo.travel
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TravelAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                    select id_t, title, description_t,
                    stars,price,continent
                    ,photo1, photo2, photo3,photo4, photo5, photo
                    from dbo.travel where  id_t=" + id + @"
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TravelAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Tours tr)
        {
            string query = @"
                    insert into dbo.travel 
                    (title, description_t,
                   stars, price,continent,photo1, photo2, photo3,photo4, photo5, photo )
                    values 
                    (
                    '" + tr.title + @"'
                    ,'" + tr.description_t + @"'
                   ,'" + tr.stars + @"' 
                     ,'" + tr.price + @"'
                    ,'" + tr.continent + @"'
                    ,'" + tr.Photo1 + @"'
                    ,'" + tr.Photo2 + @"'
                    ,'" + tr.Photo3 + @"'
                    ,'" + tr.Photo4 + @"'
                    ,'" + tr.Photo6 + @"'
                    ,'" + tr.Photo + @"'
                    )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TravelAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Tours tr)
        {
            string query = @"
                    update dbo.Travel set 
                    title = '" + tr.title + @"'
                    ,description_t = '" + tr.description_t + @"'
                    ,stars = '" + tr.stars + @"'
                     ,price = '" + tr.price + @"',
                      photo = '" + tr.Photo + @"',
                      photo1 = '" + tr.Photo1 + @"',
                      photo2 = '" + tr.Photo2 + @"',
                       photo3 = '" + tr.Photo3 + @"',
                        photo4 = '" + tr.Photo4 + @"',
                       photo5 = '" + tr.Photo6 + @"'
                    where id_t = '" + tr.id_t + @" '
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TravelAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.travel
                    where id_t = " + id + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TravelAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
