using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Geslaagd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Betaling"] != null)
        {
            Betaling aBetaling = (Betaling)Session["Betaling"];
            if (aBetaling.Geslaagd)
            {
                betalinggeslaagddiv.Visible = true;
                betalingmisluktdiv.Visible = false;
            }
            else
            {
                betalingmisluktdiv.Visible = true;
                betalinggeslaagddiv.Visible = false;
            }
        }
        else
        {
            betalinggeslaagddiv.Visible = true;
            betalingmisluktdiv.Visible = false;
            //Response.Redirect("Default.aspx");
        }
    }
}