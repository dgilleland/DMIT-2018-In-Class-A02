using eRestaurant.Framework.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Staff_SplitBill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void SelectBill_Click(object sender, EventArgs e)
    {
        GetBill();
    }
    private void GetBill()
    {
        var controller = new WaiterController();
        var data = controller.GetBill(int.Parse(ActiveBills.SelectedValue));
        //BillToSplit.Value = data.BillID.ToString();

        // Set the original bill items
        OriginalBillItems.DataSource = data.Items;
        OriginalBillItems.DataBind();

        // empty out other bill
        NewBillItems.DataSource = null;
        NewBillItems.DataBind();
    }

    protected void BillItems_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        e.Cancel = true; // to prevent any other processing in the GridView's default Select handling

        GridView sendingGridView = sender as GridView; // notice the safe casting
        GridViewRow row = sendingGridView.Rows[e.NewSelectedIndex];

        // 1) get the info from the row
        var qtyLabel = row.FindControl("Quantity") as Label;
        //               <asp:Label ID="Quantity" .... />
        var nameLabel = row.FindControl("ItemName") as Label;
        var priceLabel = row.FindControl("Price") as Label;

        // temp output
        MessageLabel.Text = "I want to move " + qtyLabel.Text + " " + nameLabel.Text + " items onto the other bill (GridView) $" + priceLabel.Text + " each";

        // 2) move it to the other gridview

        // 3) take the row out of this list

        // 4) happy dance
    }
}