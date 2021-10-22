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
    public class BuyTourController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public BuyTourController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpPost("SendRequest")]
        public JsonResult Post(BuyTour tr)
        {
            string query = @"
                    insert into dbo.BuyTours 
                    (fullname, phonenumber, adress,people,tourname )
                    values 
                    (
                    '" + tr.Fullname + @"'
                    ,'" + tr.Phonenumber + @"'
                   ,'" + tr.Adress + @"' 
                     ,'" + tr.People + @"'
                    ,'" + tr.Tourname + @"'
                   
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
    }
}
