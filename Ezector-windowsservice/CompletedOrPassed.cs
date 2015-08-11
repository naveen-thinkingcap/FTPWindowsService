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
            //SqlCommand cmd= new SqlCommand("Select * from users",con);
            //cmd.ExecuteNonQuery();
            //DataTable dt= new DataTable();
            //dt = cmd.ExecuteNonQuery();
            string strSQL = "Select StudentRecord.StudentID,StudentRecord.CourseID,StudentRecord.Success,StudentRecord.DateCompleted "+
            ",(Select distinct(value) from Customfields where Customfields.fieldid='cb9d2390-0418-4ac5-8843-f687e9fc5549' and Customfields.objectid=StudentRecord.StudentID) as Username "+
            ",(Select distinct(value) from Customfields where Customfields.fieldid='4eeda208-3788-4f0a-b21b-199b913e3869' and Customfields.objectid=StudentRecord.CourseID) as QualificationID "+
            " from StudentRecord "+
            " where Success='Passed' or Success='Completed' and DateCompleted is not null and DateCompleted>=CONVERT(VARCHAR(10),'"+DateTime.UtcNow+"',121) and DateCompleted < CONVERT(VARCHAR(10),'"+DateTime.UtcNow.AddDays(1)+"',121)"  ;
            

            //string strSQL = "Select * from users";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Personal Number \t, QualificationID (Q) \t, Date \n");

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ////con.Open();
                    //// Metadatafield for username is fieldid='cb9d2390-0418-4ac5-8843-f687e9fc5549'
                    //// Metadatafield for sap code is fieldid='4eeda208-3788-4f0a-b21b-199b913e3869'
                    ////string personalnumber = "";
                    ////string qualitficationid = "";
                    ////string strSQL1 = "Select * from Customfields where (fieldid='cb9d2390-0418-4ac5-8843-f687e9fc5549' and objectid='" + dr["StudentID"] + "')or(fieldid='4eeda208-3788-4f0a-b21b-199b913e3869' and objectid='" + dr["CourseID"] + "')";
                    ////SqlDataAdapter dameta = new SqlDataAdapter(strSQL1, con);
                    ////DataSet dsmeta = new DataSet();
                    ////dameta.Fill(dsmeta);

                    ////if (dsmeta.Tables[0].Rows.Count > 0)
                    ////{

                    ////    for (int i = 0; i < dsmeta.Tables[0].Rows.Count; i++)
                    ////    {
                    ////        if (dsmeta.Tables[0].Rows[0]["FieldID"].ToString() == "cb9d2390-0418-4ac5-8843-f687e9fc5549")
                    ////        {
                    ////            personalnumber = dsmeta.Tables[0].Rows[0]["value"].ToString();
                    ////        }
                    ////        if (dsmeta.Tables[0].Rows[0]["FieldID"].ToString() == "4eeda208-3788-4f0a-b21b-199b913e3869")
                    ////        {
                    ////            qualitficationid = dsmeta.Tables[0].Rows[0]["value"].ToString();
                    ////        }
                    ////    }
                    ////        ////foreach (DataRow drval in dsmeta)
                    ////        ////{
                    ////        ////    if (drval["FieldID"].ToString() == "cb9d2390-0418-4ac5-8843-f687e9fc5549")
                    ////        ////    {
                    ////        ////        personalnumber = drval["value"].ToString();
                    ////        ////    }
                    ////        ////    if (drval["FieldID"].ToString() == "4eeda208-3788-4f0a-b21b-199b913e3869")
                    ////        ////    {
                    ////        ////        qualitficationid = drval["value"].ToString();
                    ////        ////    }
                    ////        ////}
                    ////}
                    ////con.Close();

                    //builder.AppendFormat(dr["UserID"].ToString() + "," + dr["FirstName"].ToString() +" "+dr["LastName"].ToString()+ "\n");
                    //builder.AppendFormat(dr["StudentID"].ToString() + "," + dr["CourseID"].ToString() + "," + dr["Success"].ToString() + "\n");
                    //builder.AppendFormat(personalnumber + "," + qualitficationid + "," + dr["DateCompleted"].ToString() + "\n");
                    builder.AppendFormat(dr["Username"].ToString() + "\t," + dr["QualificationID"] + "\t," + dr["DateCompleted"].ToString() + "\n");
                }
            }
            
            string filename=DateTime.UtcNow.Year.ToString()+DateTime.UtcNow.Month.ToString()+DateTime.UtcNow.Day.ToString()+"EZECTOR".ToUpper()+".csv";
            //StreamWriter write = new StreamWriter();
            System.IO.File.WriteAllText("C:\\Users\\Naveen\\Desktop\\Naveen\\"+filename, builder.ToString());
            //file.WriteAllText(builder.ToString());
        }
    }
}
