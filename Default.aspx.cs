using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;

public partial class _Default : Page
{
    List<Connection> incomingData = new List<Connection>();
    //public List<ConnectionData> connectionData = new List<ConnectionData>();
    public ArrayList connectionData = new ArrayList();
    string labID;
    string labName;
    string ipAddress;
    string str;

    public void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.BindRepeater();
        }
    }

    private void BindRepeater()
    {
        string constr = ConfigurationManager.ConnectionStrings["Appserver"].ConnectionString;
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

    public void ClearList(object sender, EventArgs e)
    {
        Global.Data.Clear();
    }
    
}