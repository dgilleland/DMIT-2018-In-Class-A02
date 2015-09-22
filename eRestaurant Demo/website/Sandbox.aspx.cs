using eRestaurant.Framework.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sandbox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            var controller = new TempController();
            var data = controller.ListMenuCategories();
            // Hook the data up to the GridView
            MenuCategoryGrid.DataSource = data;
            MenuCategoryGrid.DataBind();
        }
    }
}