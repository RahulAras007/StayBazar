using MySql.Data.MySqlClient;
using StayBazar.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class CostCentreController : Controller
    {
        // GET: Admin/CostCentre
        public ActionResult InsertCostCentre()
        {
            
            return View();
        }
        public ActionResult DisplayCostCentre()
        {
            List<costcentre> model = new List<costcentre>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "Select * from offlinebooking_costcentre";
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            model.Add(new costcentre()
                            {
                                costcentreID = Convert.ToInt16(rdr["CostCentre_ID"]),
                                CostCentrecode = Convert.ToInt16(rdr["CostCenter_Codee"]),
                                percentage = Convert.ToInt16(rdr["Percentage"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult InsertCostCentre(costcentre model)
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "Insert into offlinebooking_costcentre(CostCenter_Codee,Percentage)Values(@CostCentrecode,@percentage)";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {

                    cmd.Connection = con;
                    con.Open();
                    cmd.Parameters.AddWithValue("@CostCentrecode", model.CostCentrecode);
                    cmd.Parameters.AddWithValue("@percentage", model.percentage);
                    model.costcentreID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();


                }

            }

            return View(model);
        }
    }
}