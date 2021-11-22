using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CovidBookingDashboard.Controllers
{
    public class ConsetFormController : Controller
    {
        string conString = "Data Source=CHECKUPSSERVER;Initial Catalog =ZidiDb; User Id=zidiadmin;Password=L90ns!@123";
        public static string DbConn = "Data Source=CHECKUPSSERVER;Initial Catalog =ZidiDb; User Id=zidiadmin;Password=L90ns!@123";
        // GET: ConsetForm
        public ActionResult index2()
        {
            return View();
        }
        public ActionResult Index(string CycleID)
        {
            //pdf generator
            HtmlToPdf converter = new HtmlToPdf();
            //// set converter options
            converter.Options.PdfPageSize = PdfPageSize.A4;

            // header settings
            converter.Options.DisplayHeader = true;
            converter.Header.DisplayOnFirstPage = true;
            converter.Header.DisplayOnOddPages = true;
            converter.Header.DisplayOnEvenPages = true;
            converter.Header.Height = 128;
            converter.Options.DisplayFooter = true;
            converter.Footer.DisplayOnFirstPage = true;
            converter.Footer.DisplayOnOddPages = true;
            converter.Footer.DisplayOnEvenPages = true;
            converter.Footer.Height = 128;

            string PatientName = "";
            string test = "";
            string date = DateTime.UtcNow.AddHours(3).ToString("dd-MM-yyyy");
            string time = DateTime.UtcNow.AddHours(3).ToString("HH:mm:ss"); ;
            string idNumber = "";
            string Nationality = "";
            string Phone = "";
            string email = "";
            string sampleColectionLocation = "";
            string insurance = "";
            string memberNo = "";
            string dob = "";
            string gender = "";


            using (SqlConnection con = new SqlConnection(DbConn))
            {
                
                using (SqlCommand cmd = new SqlCommand($@"select (PR.patient_first_name+' '+ pr.patient_middle_name +' '+ PR.patient_last_name) AS PatientName,
Pr.email Email, pr.patient_tel Phone, pr.dob, pr.gender,pr.id_no Idnumber,pr.nationality,
op.CollectionLocation,isNULL(op.Insurance,'') Insurance, ISNULL( op.MemberNumber,'') MemberNumber, op.TypeOfTest test from visit_cycle vc
inner join onlinepatients op on op.CycleId=vc.cycle_id
inner join patient_registration pr on pr.patient_id=vc.patient_id
where vc.cycle_id='{CycleID}'", con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        PatientName = rdr["PatientName"].ToString();
                        test = rdr["test"].ToString();
                        idNumber = rdr["Idnumber"].ToString();
                        Nationality = rdr["nationality"].ToString();
                        Phone = rdr["Phone"].ToString();
                        email = rdr["Email"].ToString();
                        sampleColectionLocation = rdr["CollectionLocation"].ToString();
                        insurance = rdr["Insurance"].ToString();
                        memberNo = rdr["MemberNumber"].ToString();
                        dob = rdr["dob"].ToString();
                    }

                }
            }
            string selectedTest = test == "3558" ? "Rapid Antigen Test" : "RT-PCR Test";
                string htmlString = $@"
<!DOCTYPE html>
<html>
<head>
    <link href='C:\consentForm\Res\style.css' rel='stylesheet'>
    <link href='C:\consentForm\Res\bootsrap.min.css' rel='stylesheet' type='text/css'>
    <link href='C:\consentForm\Res\Receipt.css' rel='stylesheet' type='text/css'>
    <link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css'>    
</head>
<body>
     <div class='container'>
        <div class='panel'>
            <div class='row'>
            </div><br />

<div class='row'>
<div class='float-left'>
<div style='margin-top: 10px;'>
<div class='row'>
<div class='col-md-12'style='text-align: left;'>
<h4 style='margin: 0 !important;'><FONT FACE='Trebuchet MS, serif'><b><U>INFORMED CONSENT fORM fOR COVID-19 TESTING</U><span style='color: red !important'>(Must be signed before service) </span></b></FONT></h4>
<P style='margin: 0 !important;font-weight:700 ;color: black;'>
Test requested</P>
<P style='margin-left: 20px !important;'>
{selectedTest} </P>
<P style='margin-left: 20px !important;'>
</div>
</div>
</div>
<div>
<div class='row'>
    <div class='col-md-8'>
        <P style='margin: 0 !important;font-weight:700 ;color: black;'>
Patient's Details <span style='color: red !important'>(*MoH REQUIRED fields)</span></P>
</div>
    <div class='col-md-3'>
        <div class='card' style='width:100%; height: 100%;'>
  <div class='card-body'>
    <center>
         <p class='card-text'>PLACE SAMPLE</p>
        <p class='card-text'>LABEL HERE</p>   
    </center>
  </div>
</div>
    </div>
    <div class='col-md-4'>
        <P style='margin: 0 !important;'>
Date : {date}</P>
</div>
    <div class='col-md-4 float-left'>
      <P style='font-weight:400 ;color: black;' class='float-left'>
Time: {time}</P>  
</div>
<div class='col-md-4'></div>

<div class='col-md-12'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-4 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Full Name <span style='color: red !important'>(as in ID/Passport)*</span></P></label>
 
    <div class='col-sm-8'>
      <input type='text' class='inp' id='label-name' value='{PatientName}'>
    </div>
 
  </div>
</div>
<div class='col-md-12'>
    <div class='row'>
        <div class='col-md-7'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-5 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>National ID/Passport No*</P></label>
 
    <div class='col-sm-7'>
      <input type='text' class='inp' id='label-name' value={idNumber}>
    </div>
 </div>
  </div>
       <div class='col-md-5'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-3 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Nationality*</P></label>
 
    <div class='col-sm-9'>
      <input type='text' class='inp' id='label-name' value='{Nationality}'>
    </div>
 </div>
  </div>
</div>
</div>
<div class='col-md-12'>
    <div class='row'>
        <div class='col-md-5'>
            <label class='form-check-label'>
   <P>
Date<span style='margin: 0 !important;color: black !important;font-weight:700 ;color: black;'>(dd/mm/yyyy):</span></P>
  </label>
  <div class='form-check-inline'>
  <label class='form-check-label'>
   <input type='text' class='inp1' id='label-name'>
  </label>
 <!--     <div class='form-group row'>
 
    <label for='label-name' class='col-md-5 col-form-label'><P style='margin: 0 !important;'>
Date<span style='color: black !important;font-weight:700 ;color: black;'>:</span></P></label>
 
    <div class='col-sm-7'>
      <input type='text' class='inp' id='label-name' value='{dob}'>
    </div>
 </div> -->
  </div>
</div>
        <div class='col-md-3'>
     <div class='form-group row'>
 
<div class='form-check-inline'>
  <label class='form-check-label'>
   SEX:
  </label>
</div>
<div class='form-check-inline'>
  <label class='form-check-label'>
    M <input type='checkbox' class='form-check-input' value=''>
  </label>
</div>
<div class='form-check-inline'>
  <label class='form-check-label'>
    F <input type='checkbox' class='form-check-input' value='' disabled>
  </label>
</div>
 </div>
  </div>
       <div class='col-md-4'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-4 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Temperature:</P></label>
 
    <div class='col-sm-8'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
</div>
</div>
<div class='col-md-12 '>
    <div class='form-group row'>
 
    <label for='label-name' class='col-md-5 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Mobile no*(WhatsApp Number preffered):</P></label>
 
    <div class='col-sm-7'>
      <input type='text' class='inp' id='label-name' value='{Phone}'>
    </div>
 </div>
</div>
<div class='col-md-12 '>
<div class='form-group row'>
 <div class='col-md-12'>
<div class='form-check-inline'>
  <label class='form-check-label'>
    <P style='margin: 0 !important;font-weight:600 ;color: black;'>Send results and TT certificate on WhatsApp?</P>
  </label>
</div>
<div class='form-check-inline'>
  <label class='form-check-label'>
    Yes <input type='checkbox' class='form-check-input' value=''>
  </label>
</div>
<div class='form-check-inline'>
  <label class='form-check-label'>
    No <input type='checkbox' class='form-check-input' value='' disabled>
  </label>
</div>
 </div>
</div>
</div>
<div class='col-md-12'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-3 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Email Address: </P></label>
 
    <div class='col-sm-9'>
      <input type='text' class='inp' id='label-name' value={email}>
    </div>
 
  </div>
</div>
<div class='col-md-12'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-sm-5 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Name of Employer(for corporate clients): </P></label>
 
    <div class='col-sm-7'>
      <input type='text' class='inp' id='label-name'>
    </div>
 
  </div>
</div>
<div class='col-md-12'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-sm-3 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Physical Location Address </P></label>
 
    <div class='col-sm-9'>
      <input type='text' class='inp' id='label-name'>
    </div>
 
  </div>
</div>
<div class='col-md-12'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-sm-3 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Location of samle collection </P></label>
 
    <div class='col-sm-9'>
      <input type='text' class='inp' id='label-name' value='{sampleColectionLocation}'>
    </div>
 
  </div>
</div>
<div class='col-md-12'>
     <div class='form-group row'>
        <div class='col-md-12'>
            <label class='form-check-label'>
   <P style='font-weight:600 ;color: black;'>I</P>
  </label>
  <div class='form-check-inline'>
  <label class='form-check-label'>
   <input type='text' class='inp1' id='label-name' value='{PatientName}'>
  </label>
</div>
           <label class='form-check-label'>
   <P style='font-weight:600 ;color: black;'> of sound mind do consent and understand the below:</P>
  </label>
        </div>
 </div>
  </div>
<div class='col-md-12'>
    <div class='row'>
<div class='col-md-12'>
<P style='font-weight:600 ;color: black;'>1. That payment of the cost of COVID-19 test shall be paid by:</P>
 <div class='col-md-12'>
 <div class='row'>
        <div class='col-md-6'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-3 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Myselft(patient)</P></label>
 
    <div class='col-sm-9'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
       <div class='col-md-6'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-3 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Parent(Name)</P></label>
 
    <div class='col-sm-9'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
</div>
</div>

         <div class='col-md-12'>
  <div class='row'>
        <div class='col-md-12'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-2 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Company(Name)</P></label>
 
    <div class='col-sm-10'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
</div>
</div>
         <div class='col-md-12'>
<div class='row'>
        <div class='col-md-6'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-3 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Insurance(Name)</P></label>
 
    <div class='col-sm-9'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
       <div class='col-md-6'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-4 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Membership No</P></label>
 
    <div class='col-sm-8'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
</div>
</div>
        </div>
        </div>
             
<div class='col-md-12'>
<P style='font-weight:600 ;color: black;'>2. For Minors or dependents ONLY— List the next of Kin/Guardian (ONLY)</P>
<div class='col-md-12'>
 <div class='row'>
        <div class='col-md-6'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-5 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Guardian's Name:</P></label>
 
    <div class='col-sm-7'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
       <div class='col-md-6'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-3 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Relationship:</P></label>
 
    <div class='col-sm-9'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
</div>
</div>

         <div class='col-md-12'>
  <div class='row'>
        <div class='col-md-12'>
     <div class='form-group row'>
 
    <label for='label-name' class='col-md-5 col-form-label'><P style='margin: 0 !important;font-weight:600 ;color: black;'>Gurdian's Phone(WhatsApp preferred)</P></label>
 
    <div class='col-sm-7'>
      <input type='text' class='inp' id='label-name'>
    </div>
 </div>
  </div>
</div>
</div>
</div>
</div>
</div>

</div>
</div>
</div>
             <div class='row'>
</div>
</div>
</div>
  <div class='container' style='page-break-before: always;'>
        <div class='panel'>
            <div class='row'>
            </div><br />

<div class='row'>
<div class='float-left'>
<div>
<div class='row'>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>3. That my decision to opt for the COVID-19 test 1s voluntary and without any coercion from anyone</P>
</div>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>4. That the novel coronavirus, COVID-19, has been declared a global pandemic by the World Health Organization (WHO) and that COVID-19 is extremely contagious and is believed to spread through person-to-person contact. As a result, health agencies recommend social distancing within the collection facilities.
</P>
</div>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>5. I recognize that all the staff at Checkups Medical Center have put in place reasonable preventative measures aimed at reducing the spread of COVID-19.</P>
</div>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>6. I hereby give my express permission to Checkups Medical Centre to proceed with the COVID-19 test</P>
</div>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>7. I understand  that Negatiive results do  not  preclude SARS-COV-2 infection </P>
</div>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>8. That if the employer caters for the cost of the test. the employer is entitled to be informed  of the patient's test results (whether positive or negative). It will be  the responsibility of the employer to
manage its employee post-COVID  test resullt.
</P>
</div>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>9. The Hospital shall only conduct COVID testing. Any patient management post COVID test report shall be the employer's responsibility.</P>
</div>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>10. I shall take the responsibility  to follow the measures (socla/  distancing, wearing mask in workplace, healthcare facility and public places maintaining hand hygiene) to reduce the spread of COVID-19 recommended by The Ministry of HeaIth (MOH) after the test.
</P>
</div>
<div class='col-md-12'style='text-align: left;'>
<P style='font-weight:600 ;color: black;'>11. Having read and being explained to me the above provisions; I voluntarily consent to be bound by  the above terms and shall not hold Checkups Medical Centre liable for any eventualities  pertaining to the release of results for COVID-19 tests and trestments.</P>
</div>
<div class='col-md-12' style='margin-top:-20px ;'>
            <label class='form-check-label'>
   <P style='margin: 0 !important;font-weight:600 ;color: black;'>Signed by person taking test:  </P>
  </label>
  <div class='form-check-inline'>
  <label class='form-check-label'>
   <input type='text' class='inp' id='label-name'>
  </label>
</div>
</div>
<div class='col-md-12'style='text-align: left; margin-top:5px ;'>
<P style='font-weight:600 ;color: black;'>Results Status (For Lab Offi cia l Use ONLY)</P>
<P style='font-weight:600 ;color: black;'>.Negative  &nbsp;&nbsp;&nbsp;    .Positive</P>
<P style='font-weight:800 ;color: black;'>Clinical/Lab Manager</P>
</div>
<div class='col-md-12'>
<div class='row'>
<div class='col-md-4'>
        <div class='float-left'>
                <div class='col-md-12'>
                    <table>
                        <tr>
                            <td><small>Name:&nbsp;&nbsp;</small></td>
                            <td style='width:100%'><small style='font-weight:bold; font-style:italic; color:black'>Robert Cheruiyot Ronoh</small></td>
                        </tr>
                    </table>
                </div>
            </div>
			        <div class='col-md-4'>
            <div class='row' style='color:black;'>
                <div class='col-md-12'>
                    <table>
                        <tr>
                            <td ><small>Signature:&nbsp;&nbsp;&nbsp;&nbsp;</small></td>
                            <td class='under' style='width:100%'><small style='font-weight:bold; font-style:italic; color:black'>&nbsp;&nbsp;&nbsp;<img src='C:/Certificate/Res/Idris.png'></small></td>
                        </tr>
                    </table>
                </div>
            </div><br />

        </div>

        </div>
        <div class='col-md-4' style='margin: 0 !important;'>
                       <div style='position: relative;text-align: center;'>
  <img src='C:/Certificate/Res/stamp.png'style='width:100%;'>
  <div style='position: absolute;top: 50%;left: 50%;transform: translate(-50%, -50%);'>{date}</div>
</div>
          </div>

       </div>

</div>
</div>
</div>

<div class='row'>
</div>
</div>
</div>
</body>
</html>


";


            PdfDocument doc = converter.ConvertHtmlString(htmlString);

            string headerImage = @"C:\Certificate\Res\header.png";
            PdfImageSection headerImg = new PdfImageSection(0, 0, 594, headerImage);
            converter.Header.Add(headerImg);

            string imgFile = @"C:\Certificate\Res\footer.png";
            PdfImageSection img = new PdfImageSection(0, 0, 594, imgFile);
            converter.Footer.Add(img);

            byte[] pdf = doc.Save();

            // close pdf document
            doc.Close();

            // return resulted pdf document
            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = PatientName+".pdf";
            return fileResult;
        }
        
    }
}