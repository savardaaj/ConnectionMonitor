using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for ModelDatabaseHandler
/// </summary>
public class ModelDatabaseHandler
{
    public ModelDatabaseHandler()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void PopulateDataTable(Repeater rptCustomers)
    {
        try
        {
            string constr = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT LabName, IPAddress, LabID FROM Labs Order By LabName Asc", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        rptCustomers.DataSource = dt;
                        rptCustomers.DataBind();
                    }
                }
            }
        }
        catch(Exception e)
        {
            //TODO: handle exception
        }
    }
}