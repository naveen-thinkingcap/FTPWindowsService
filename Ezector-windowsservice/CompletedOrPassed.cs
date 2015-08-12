using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
namespace Ezector_windowsservice
{
    public class CompletedOrPassed
    {
        public  CompletedOrPassed()
        { 
        
        }

        public void ProcessCompletedOrPassed()
        {
            SqlConnection con = new SqlConnection("Data Source=tcp:yalfrekhyc.database.windows.net,1433;Initial Catalog=EZECTOR_LMS;User ID=thinkingcap@yalfrekhyc;Password=t!12Bang;Trusted_Connection=False;Encrypt=True;Max Pool Size=119;Connection Timeout=30");
            //SqlConnection con = new SqlConnection("Data Source=tcp:kvyj4yqx8f.database.windows.net,1433;Initial Catalog=MAGNUM_LMS_2015;User ID=thinkingcap@kvyj4yqx8f;Password=t!12Bang;Trusted_Connection=False;Encrypt=True;Max Pool Size=60;Connection Timeout=30");
            con.Open();
            //************************
            // Fetcching completed/passed course for StudentRecord
            string strSQL = "Select StudentRecord.StudentID,StudentRecord.CourseID,case when course.type='Learning Path' then StudentRecord.DateEnrolled else StudentRecord.DateStarted end as DateStarted,StudentRecord.LastDateAttempted,StudentRecord.Success,Convert(date,StudentRecord.DateCompleted) as DateCompleted,(Select distinct(value) from Customfields where Customfields.fieldid='cb9d2390-0418-4ac5-8843-f687e9fc5549' and Customfields.objectid=StudentRecord.StudentID) as Username ,(Select distinct(value) from Customfields where Customfields.fieldid='4eeda208-3788-4f0a-b21b-199b913e3869' and Customfields.objectid=StudentRecord.CourseID) as QualificationID " +
            "from StudentRecord "+
            "inner join Course on Course.Courseid=Studentrecord.courseid "+
            "where (StudentRecord.Success='Passed' or StudentRecord.Success='Completed') and StudentRecord.DateCompleted is not null "+
            "and Course.Programid='0581b6e4-89ef-4075-952a-65ed21dee34b' " +
            "and Convert(date,DateCompleted)=Convert(date,'"+DateTime.UtcNow.AddDays(-1)+"')" +
            //"and Convert(date,DateCompleted)=Convert(date,'" + DateTime.UtcNow + "')" +
            "order by datecompleted desc";

            SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
            
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Personal Number (P) \t, QualificationID (Q) \t, Start Date \n");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    builder.AppendFormat(dr["Username"].ToString() + "\t," + dr["QualificationID"] + "\t," + String.Format("{0:d.M.yyyy}", Convert.ToDateTime(dr["DateStarted"].ToString()))/* Convert.ToDateTime(dr["DateCompleted"].ToString()).ToString("d.mm.yyyy") */+ "\n");
                }
            }
            //************************

            //************************
            // Fetcching completed Activity for StudentActivity
            string strSQL1 = "Select StudentActivity.StudentID,StudentActivity.ActivityID,StudentActivity.CompletionDate,(Select distinct(value) from Customfields where Customfields.fieldid='cb9d2390-0418-4ac5-8843-f687e9fc5549' and Customfields.objectid=StudentActivity.StudentID) as Username ,(Select distinct(value) from Customfields where Customfields.fieldid='4eeda208-3788-4f0a-b21b-199b913e3869' and Customfields.objectid=StudentActivity.ActivityID) as QualificationID "+
            "from StudentActivity "+
            "inner join Activity on Activity.ActivityID=StudentActivity.ActivityID " +
            //"where Activity.ProgramID='06CDF85E-A6DF-44DA-BA0C-F58BEA5F7D44' " +
            "where Activity.ProgramID='0581b6e4-89ef-4075-952a-65ed21dee34b' " +
            "and StudentActivity.CompletionDate is not Null "+
            //"and Convert(date,StudentActivity.CompletionDate)=Convert(date,'" + DateTime.UtcNow.AddDays(-1) + "')" +
            "order by CompletionDate desc";
            con.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(strSQL1, con);

            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            con.Close();



            if (ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    builder.AppendFormat(dr1["Username"].ToString() + "\t," + dr1["QualificationID"] + "\t," + String.Format("{0:d.M.yyyy}", Convert.ToDateTime(dr1["CompletionDate"].ToString()))/* Convert.ToDateTime(dr["DateCompleted"].ToString()).ToString("d.mm.yyyy") */+ "\n");
                }
            }
            //************************

            string filename=DateTime.UtcNow.Year.ToString()+DateTime.UtcNow.Month.ToString()+DateTime.UtcNow.Day.ToString()+"EZECTOR".ToUpper()+".csv";
            
            //TODO where to save the file
            System.IO.File.WriteAllText("C:\\Users\\Naveen\\Desktop\\Naveen\\"+filename, builder.ToString());
            
        }
    }
}
