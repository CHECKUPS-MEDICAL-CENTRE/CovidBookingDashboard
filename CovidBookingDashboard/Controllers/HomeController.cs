using CovidBookingDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CovidBookingDashboard.Controllers
{
    public class HomeController : Controller
    {
         string conString = "Data Source=CHECKUPSSERVER;Initial Catalog =ZidiDb; User Id=zidiadmin;Password=L90ns!@123";
        //string conString = @"Data Source=DESKTOP-TTKUQJC\SQLEXPRESS;Initial Catalog =ZidiDb;Integrated Security=True";
        public ActionResult Index()
        {
           // OnlinePatientModel patients = new OnlinePatientModel();
            List<OnlinePatientModel> patientslist = new List<OnlinePatientModel>();
            DataTable dtFiles = GetUsersRegistered();
            foreach (DataRow dr in dtFiles.Rows)
            {
                patientslist.Add(new OnlinePatientModel
                {
                    FirstName = @dr["FirstName"].ToString(),
                    Surname = @dr["Surname"].ToString(),
                    OtherNames = @dr["OtherNames"].ToString(),
                    Telephone = @dr["Telephone"].ToString(),
                    Email = @dr["Email"].ToString(),
                    IdNumber = @dr["IdNumber"].ToString(),
                    VisitDate = Convert.ToDateTime(@dr["VisitDate"]).ToString(),
                    VisitTime = Convert.ToDateTime(@dr["DateCreated"]).ToString(),
                    CollectionSlot = @dr["CollectionSlot"].ToString(),

                });
            }
            ViewBag.PatientsList = patientslist;
            return View();
        }

        public JsonResult getBookings()
        {
            // OnlinePatientModel patients = new OnlinePatientModel();
            List<OnlinePatientModel> patientslist = new List<OnlinePatientModel>();
            DataTable dtFiles = GetUsersRegistered();
            foreach (DataRow dr in dtFiles.Rows)
            {
                patientslist.Add(new OnlinePatientModel
                {
                    FirstName = @dr["FirstName"].ToString()+" " + @dr["OtherNames"].ToString() +" "+ @dr["Surname"].ToString(),
                    Surname = @dr["Surname"].ToString(),
                    OtherNames = @dr["OtherNames"].ToString(),
                    Telephone = @dr["Telephone"].ToString(),
                    Email = @dr["Email"].ToString(),
                    IdNumber = @dr["IdNumber"].ToString(),
                    VisitDate = Convert.ToDateTime(@dr["VisitDate"]).ToString("MM-dd-yyyy"),
                    VisitTime = Convert.ToDateTime(@dr["DateCreated"]).ToString("HH:mm:ss"),
                    CollectionSlot = @dr["CollectionSlot"].ToString(),
                    status = Convert.ToInt32(@dr["status"].ToString()),
                    IsHomeCollection = Convert.ToInt32(@dr["IsHomeCollection"].ToString()),
                    TypeOfTestDescription = dr["TypeOfTestDescription"].ToString(),
                    CollectionLocation = dr["CollectionLocation"].ToString(),
                    DateCreated = Convert.ToDateTime(@dr["DateCreated"]).ToString("MM-dd-yyyy"),
                    CycleId = Guid.Parse(dr["CycleId"].ToString()),
                });
            }

            return Json(patientslist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getBookingsByStatus(string sDate, string eDate, int status)
        {
            DateTime StartDate;
            DateTime EndDate;
            StartDate= string.IsNullOrEmpty(sDate) ? StartDate = DateTime.Today.AddDays(-2) : Convert.ToDateTime(sDate);
            EndDate = string.IsNullOrEmpty(eDate) ? EndDate = DateTime.Today : Convert.ToDateTime(eDate);
            // OnlinePatientModel patients = new OnlinePatientModel();
            List <OnlinePatientModel> patientslist = new List<OnlinePatientModel>();
            DataTable dtFiles = GetUsersRegisteredByDateRangeByStatus(StartDate, EndDate, status);
            foreach (DataRow dr in dtFiles.Rows)
            {
                
                patientslist.Add(new OnlinePatientModel
                {
                    FirstName = @dr["FirstName"].ToString()+ @dr["OtherNames"].ToString()+ @dr["Surname"].ToString(),
                    Surname = @dr["Surname"].ToString(),
                    OtherNames = @dr["OtherNames"].ToString(),
                    Telephone = @dr["Telephone"].ToString(),
                    Email = @dr["Email"].ToString(),
                    IdNumber = @dr["IdNumber"].ToString(),
                    VisitDate = Convert.ToDateTime(@dr["VisitDate"]).ToString("MM-dd-yyyy"),
                    VisitTime = Convert.ToDateTime(@dr["DateCreated"]).ToString("HH:mm:ss"),
                    CollectionSlot = @dr["CollectionSlot"].ToString(),
                    status= Convert.ToInt32(@dr["status"].ToString()),
                    IsHomeCollection= Convert.ToInt32(@dr["IsHomeCollection"].ToString()),
                    TypeOfTestDescription = dr["TypeOfTestDescription"].ToString(),
                    CollectionLocation = dr["CollectionLocation"].ToString(),
                    DateCreated = Convert.ToDateTime(@dr["DateCreated"]).ToString("MM-dd-yyyy"),
                    CycleId= Guid.Parse( dr["CycleId"].ToString()),

                });
            }

            return Json(patientslist, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Index2()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private DataTable GetUsersRegistered()
        {
            DataTable dtData = new DataTable();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand command = new SqlCommand(@"Select * from onlinepatients
    WHERE DateCreated >= dateadd(day, datediff(day, 1, GETDATE()), 0)
        AND DateCreated < dateadd(day, datediff(day, -1, GETDATE()), 0)  order by DateCreated desc", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dtData);
            con.Close();
            return dtData;
        }
        private DataTable GetUsersRegisteredByDateRange(DateTime startDate,DateTime endDate)
        {
            DataTable dtData = new DataTable();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand command = new SqlCommand($@"select * from onlinepatients 
where cast(DateCreated as Date) between  cast({startDate} as Date) and cast({endDate} as Date)
", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dtData);
            con.Close();
            return dtData;
        }
        private DataTable GetUsersRegisteredByDateRangeByStatus(DateTime startDate, DateTime endDate, int status)
        {
            DataTable dtData = new DataTable();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand command = new SqlCommand($@"select * from onlinepatients 
where cast(DateCreated as Date) between  cast({startDate} as Date) and cast({endDate} as Date) and status={1}
", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dtData);
            con.Close();
            return dtData;
        }
        private DataTable GetUsersRegisteredByDate(DateTime date)
        {
            DataTable dtData = new DataTable();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand command = new SqlCommand($@"select * from onlinepatients 
where cast(DateCreated as Date) = cast({date} as Date)", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            da.Fill(dtData);
            con.Close();
            return dtData;
        }



        public JsonResult UpdateStatus(int newstatus, Guid cycleId)
        {
            var responseMessage = new ResponseMessage();
            string strQry = "update onlinepatients set status=" +
                newstatus + " where cycleId='"+ cycleId + "'";
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
        //booking per day
        public JsonResult FetchBookingCount(int duration)
        {
            // OnlinePatientModel patients = new OnlinePatientModel();
            List<BookCountVM> patientcount = new List<BookCountVM>();
            //OnlinePatientModel patients = new OnlinePatientModel();
            DataTable dtFiles = GetBookingCount(duration);
            foreach (DataRow dr in dtFiles.Rows)
            {
                patientcount.Add(new BookCountVM
                {

                    DurationIdentifier = @dr["DurationIdentifier"].ToString(),
                    CountResult = Convert.ToInt32(@dr["CountResult"].ToString()),

                });
            }

            return Json(patientcount.FirstOrDefault(), JsonRequestBehavior.AllowGet);
        }
        private DataTable GetBookingCount(int duration)
        {
            string command = "";
            
            if (duration == 1)
            {
                command = @"Select count(*) as CountResult,'leo' as DurationIdentifier  from onlinepatients
    WHERE DateCreated >= dateadd(day, datediff(day, 0, GETDATE()), 0)";
            }
            else
            {
                command = @"Select count(*) as CountResult,'mwezi' as DurationIdentifier  from onlinepatients
    WHERE DateCreated >= dateadd(MONTH, datediff(MONTH, 0, GETDATE()), 0)
";
            }
            DataTable dtData = new DataTable();
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand commanded = new SqlCommand(command, con);
            SqlDataAdapter da = new SqlDataAdapter(commanded);
            da.Fill(dtData);
            con.Close();
            return dtData;
        }
        //total bookings
        //booking per collection location
        //individual or group

    }
}