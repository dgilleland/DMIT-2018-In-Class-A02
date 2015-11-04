using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Staff_FrontDesk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void MockLastBillingDateTime_Click(object sender, EventArgs e)
    {
        var controller = new eRestaurant.Framework.BLL.TempController();
        var info = controller.GetLastBillDateTime();
        // formatting date for use in an <input type="date"> HTML5 control
        SearchDate.Text = info.ToString("yyyy-MM-dd");

        // formatting time for use in an <input type="time"> HTML5 control
        SearchTime.Text = info.ToString("HH:mm:ss"); // HH is 24 hour clock, hh is 12 hour clock
    }
}