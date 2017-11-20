
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Script.Serialization;
using System.Reflection;


public partial class About : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public override void ProcessRequest(HttpContext context)
    {
        var request = context.Request;
        var requestBody = new StreamReader(request.InputStream, request.ContentEncoding).ReadToEnd();
        if (requestBody != "")
        {
            var jsonSerializer = new JavaScriptSerializer();
            var data = jsonSerializer.Deserialize<List<Connection>>(requestBody);
            Table connectionsTable = new Table();
            connectionsTable.ID = "tblConnections";
            foreach (Connection c in data)
            {
                TableRow row = new TableRow();

                //td's
                foreach (PropertyInfo prop in c.GetType().GetProperties())
                {
                    TableCell cell = new TableCell();
                    cell.ID = "td" + prop.GetValue(c, null) + Guid.NewGuid().ToString().Replace("-", "");
                    row.Cells.Add(cell);
                }

                row.ID = "trConnection" + Guid.NewGuid().ToString().Replace("-", "");
                connectionsTable.Rows.Add(row);
            }
            //divConnections.Controls.Add(connectionsTable);
        }
    }
}