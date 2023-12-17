using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.Url.AbsolutePath.Contains("Bevestiging.aspx"))
        //{
        //    footer.Style.Add("position", "absolute");
        //}
        btnimg.ImageUrl = "../images/logo_metnaam_original.png";
        if (Session["Gebruiker"] == null)
        {
            btnProfiel.Text = "Aanmelden";
            btnWinkelkar.Text = "0";
            //btnWinkelkar.Text = "     0";
        }
        else
        {
            Klant aKlant = (Klant)Session["Gebruiker"];
            btnProfiel.Text = aKlant.voornaam;
            string strCommand = "SELECT oldproductID FROM tblwinkelwagen WHERE klantID = @variabele;";
            List<string> products = database.readColumn(strCommand, aKlant.klantID);
            string strAmountOfProductsInWinkelwagen = Convert.ToString(products.Count());
            string output = string.Empty;
            //for (int intCounter = strAmountOfProductsInWinkelwagen.Length; intCounter <  6; intCounter++)
            //{
            //    output += " ";
            //}
            output += strAmountOfProductsInWinkelwagen;
            btnWinkelkar.Text = output;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            Page.MaintainScrollPositionOnPostBack = true;
        }
    }
    protected void btnimg_Click(object sender, EventArgs e)
    {
        Session["Preferences"] = null;
        Session["Producten"] = null;
        Response.Redirect("Default.aspx");
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Shop.aspx?Name=" + txtSearch.Text);
    }
    protected void btnProfiel_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoginPage.aspx");
    }
    protected void btnWinkelkar_Click(object sender, EventArgs e)
    {
        if (Session["Gebruiker"] != null)
        {
            Response.Redirect("WinkelwagenPage.aspx");
        }
    }
}
