using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Npgsql;
using SISPostgres.Models;
using System.Diagnostics;

namespace SISPostgres.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Index([Bind] LoginViewModel ad)
        {

            string uid = ad.Username;
            string pass = ad.Password;
            string outpass;

            NpgsqlConnection con = new NpgsqlConnection("Host=localhost;Username=postgres;Password=superuser;Database=ContosoUniversityData");
        //    string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
             
            con.Open();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*) FROM userdata where email=@Email and password=@Password", con);

            cmd.Parameters.AddWithValue("Email", uid);
            cmd.Parameters.AddWithValue("Password", pass);


            Int64 Username = (Int64)cmd.ExecuteScalar();

            if (Username == 1)
            {
                var routeValue = new RouteValueDictionary
 (new { action = "Index", controller = "Home" });
                return RedirectToRoute(routeValue);

                TempData["msg"] = "You are welcome to Admin Section";
            }
            else
            { TempData["msg"] = "User id or Password is wrong.!"; }


            return View();
        }



       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}