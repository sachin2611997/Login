using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class DropController : Controller
    {

        SqlConnection con = new SqlConnection("data source =.; initial catalog = Office; integrated security = True;");
        // GET: Drop
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(file.InputStream))
            {
                bytes = br.ReadBytes(file.ContentLength);
            }

            string path = Server.MapPath("~/App_Data/File/");   
            string filename = Path.GetFileName(file.FileName);
            string fullpath = Path.Combine(path, filename);
            string filePath = @"~/App_Data/File/" + filename;


            using (SqlConnection con = new SqlConnection("data source =.; initial catalog = Office; integrated security = True;"))
            {
                string query = "INSERT INTO Files VALUES (@Name, @Data,@Path)";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Name", (filename));
                    cmd.Parameters.AddWithValue("@Path", (filePath));

                    cmd.Parameters.AddWithValue("@Data", bytes);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            file.SaveAs(fullpath);
            return Content("Success");
        }

        public FileResult Download()
        {
            string path = Server.MapPath("~/App_Data/File/");
            string filename = Path.GetFileName("anil.jpg");
            string fullpath = Path.Combine(path, filename);
            return File(fullpath,"image/jpg","anil.jpg");

        }

    }
}