using eRestaurant.BLL;
using eRestaurant.Entities.DTOs;
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
        MessageUserControl.TryRun(GetBill);
    }
    private void GetBill()
    {
        var controller = new WaiterController();
        var data = controller.GetBill(int.Parse(ActiveBills.SelectedValue));
        BillToSplit.Value = data.BillID.ToString();

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

        GridView sendingGridView = sender as GridView;
        GridView receivingGridView;
        if (sendingGridView == OriginalBillItems)
            receivingGridView = NewBillItems;
        else
            receivingGridView = OriginalBillItems;


        GridViewRow row = sendingGridView.Rows[e.NewSelectedIndex];

        // 1) get the info from the column
        OrderItem item = GetOrderItem(row);

        // 2) move it to the other gridview
        List<OrderItem> newItems = GetRowsFrom(receivingGridView);
        newItems.Add(item);
        receivingGridView.DataSource = newItems;
        receivingGridView.DataBind();
        
        // 3) take it out of this list
        List<OrderItem> myItems = GetRowsFrom(sendingGridView);
        myItems.RemoveAt(e.NewSelectedIndex);
        sendingGridView.DataSource = myItems;
        sendingGridView.DataBind();

        // 4) happy dance
    }

    private List<OrderItem> GetRowsFrom(GridView theGridView)
    {
        List<OrderItem> result = new List<OrderItem>();
        foreach(GridViewRow row in theGridView.Rows)
        {
            var data = GetOrderItem(row);
            result.Add(data);
        }
        return result;
    }

    private OrderItem GetOrderItem(GridViewRow row)
    {
        var qtyLabel = row.FindControl("Quantity") as Label;
        var nameLabel = row.FindControl("ItemName") as Label;
        var priceLabel = row.FindControl("Price") as Label;
        var result = new OrderItem()
        {
            Quantity = int.Parse(qtyLabel.Text),
            ItemName = nameLabel.Text,
            Price = decimal.Parse(priceLabel.Text)
        };
        return result;
    }

    protected void SplitBill_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(SplitTheBill);
    }

    private void SplitTheBill()
    {
        var originalItems = GetRowsFrom(OriginalBillItems);
        var newItems = GetRowsFrom(NewBillItems);
        int billId = int.Parse(BillToSplit.Value);

        WaiterController controller = new WaiterController();
        controller.SplitBill(billId, originalItems, newItems);
    }
}