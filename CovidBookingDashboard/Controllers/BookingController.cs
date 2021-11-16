using CovidBookingDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CovidBookingDashboard.Controllers
{
    public class BookingController : Controller
    {

        string conString = "Data Source=CHECKUPSSERVER;Initial Catalog =ZidiDb; User Id=zidiadmin;Password=L90ns!@123";
        public static string DbConn = "Data Source=CHECKUPSSERVER;Initial Catalog =ZidiDb; User Id=zidiadmin;Password=L90ns!@123";
        //string conString = @"Data Source=DESKTOP-TTKUQJC\SQLEXPRESS;Initial Catalog =ZidiDb;Integrated Security=True";
        //public static string DbConn = @"Data Source=DESKTOP-TTKUQJC\SQLEXPRESS;Initial Catalog =ZidiDb;Integrated Security=True";



        public void CertPDF(string CycleID)
        {
            string FilePath = @"E:\Certificates\OneDrive\CovidCerts\AllCerts\" + CycleID + ".pdf";
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
        }

        public JsonResult SendWhatsApp(string cycleId)
        {
            var responseMessage = new ResponseMessage();
            string strQry = "UPDATE  DocumentsLog SET IsWhatsappSent=2 where document_type='covid_cert' and cycle_id='" + cycleId + "'";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand command = new SqlCommand(strQry, con);
            int numResult = command.ExecuteNonQuery();
            con.Close();
            if (numResult > 0)
            {
                responseMessage.Status = "200";
                responseMessage.Message = "success";
                return Json(responseMessage, JsonRequestBehavior.AllowGet);
            }
            else
            {
                responseMessage.Status = "500";
                responseMessage.Message = "Something went wrong";
                return Json(responseMessage, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SendEmail(string cycleId)
        {
            var responseMessage = new ResponseMessage();
            string strQry = $"Update DocumentsLog set is_sent=1 where cycle_id='{cycleId}' and document_type='covid_cert'";
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand command = new SqlCommand(strQry, con);
            int numResult = command.ExecuteNonQuery();
            con.Close();
            if (numResult > 0)
            {
                responseMessage.Status = "200";
                responseMessage.Message = "success";
                return Json(responseMessage, JsonRequestBehavior.AllowGet);
            }
            else
            {
                responseMessage.Status = "500";
                responseMessage.Message = "Something went wrong";
                return Json(responseMessage, JsonRequestBehavior.AllowGet);
            }

        }

    }
}

//select vc.cycle_id, (PR.patient_first_name+' '+ pr.patient_middle_name +' '+ PR.patient_last_name) AS FullName, vc.cycle_created_time,
// IsWhatsappSent, test_type, gender, id_no, email, nationality, channel,
//                 pr.patient_tel as PhoneNumber
//                from visit_cycle VC
//                left JOIN DocumentsLog dl ON VC.cycle_id= dl.cycle_id
//                inner join patient_registration pr on pr.patient_id=vc.patient_id 
//				where document_type='covid_cert'
//				order by cycle_created_time desc