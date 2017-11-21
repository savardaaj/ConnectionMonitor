using System;
using System.Web.UI;

public partial class _Default : Page { 

    public void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.BindRepeater();
        }
    }

    private void BindRepeater()
    {
        ModelDatabaseHandler mdbh = new ModelDatabaseHandler();
        mdbh.PopulateDataTable(rptCustomers);
        
    }

    public void ClearList(object sender, EventArgs e)
    {
        Global.Data.Clear();
    }
    
}