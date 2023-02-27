using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestApi.Model;

namespace TestApi.Controllers
{
    public class HomeController : Controller
    {
		private readonly AppDbContext db;
		private readonly IConfiguration _configuration;
		public HomeController(AppDbContext db, IConfiguration configuration)
		{
			this.db = db;
			_configuration = configuration;
		}

		[HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("GetDataStoredConnectionString")]
        [HttpGet]
        public async Task<List<UserModel>> GetDataConnectionString() 
		{
			var listUserModel = new List<UserModel>();
			var userModel = new UserModel();
			SqlConnection objConn = new SqlConnection();
			SqlCommand objCmd = new SqlCommand();
			SqlDataAdapter dtAdapter = new SqlDataAdapter();

			DataSet ds = new DataSet();
			DataTable dt;
			String strSQL;

			//strConnString = "Server=localhost;UID=sa;PASSWORD=;database=mydatabase; " +
			//" Max Pool Size=400;Connect Timeout=600;";

			strSQL = "exec HelloWorld @option = 1";

			objConn.ConnectionString = _configuration.GetConnectionString("MbkDbConstr");
			objConn.Open();
			objCmd.Connection = objConn;
			objCmd.CommandText = strSQL;
			objCmd.CommandType = CommandType.Text;

			dtAdapter.SelectCommand = objCmd;

			dtAdapter.Fill(ds);
			dt = ds.Tables[0];
			for (int i = 0; i < dt.Rows.Count ; i++)
			{
				userModel = new UserModel();
				userModel.Username = dt.Rows[i]["Username"].ToString();
				userModel.Password = dt.Rows[i]["Password"].ToString();
				listUserModel.Add(userModel);
			}
			dtAdapter = null;
			objConn.Close();
			objConn = null;
			return listUserModel;
        }

		[Route("GetDataStoredEntity")]

		[HttpGet]
		public async Task<List<UserModel>> GetDataEntity()
		{
			var listUserModel = new List<UserModel>();


			return listUserModel;
		}

		[Route("Insert")]
		[HttpPost]
		public async Task<IActionResult> Index([FromForm] UserModel user)
		{
			if (user.Id != 0)
			{
				var localize = _configuration.GetConnectionString("Default");
				var a = new UserModel();
				db.User.Select(x => x.Password).FirstOrDefault();
				var edit = await db.User.FindAsync(user.Id);
				if (edit == null)
					return NotFound();
				edit.Username = user.Username;
				edit.Password = user.Password;
				edit.Email = user.Email;
				await db.SaveChangesAsync();
			}
			else
			{
				user.CreateDate = DateTime.Now;
				await db.User.AddAsync(user);
				await db.SaveChangesAsync();
			}
			return Ok(user);
		}
	}
}
