using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Betalien : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Gebruiker"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
            Betaling aBetaling = (Betaling)Session["Betaling"];
            if (aBetaling != null)
            {
                if (aBetaling.Geslaagd == false)
                { //Klant heeft nog niet betaald

                }
                else
                { //Betaling geslaagd en gaat naar profielpagina met bestellingenpanel
                    Session["profieltab"] = "bestellingen";
                    Response.Redirect("ProfilePage.aspx");
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
    protected void btnBetaald_Click(object sender, EventArgs e)
    {
        Betaling aBetaling = (Betaling)Session["Betaling"];
        Overeenkomst aOvereenkomst = new Overeenkomst();
        try
        {
            database.editOvereenkomst(aBetaling.BestellingID.ToString(), "1", "betaald");
            aOvereenkomst = database.readOvereenkomst(aBetaling.BestellingID.ToString());
            aBetaling.Geslaagd = true;
            Session["Betaling"] = aBetaling;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Klant aKlant = (Klant)Session["Gebruiker"];
        if (aKlant != null && aKlant.klantID == aOvereenkomst.klantID)
        {
            List<Winkelwagen> winkelwagenlijst = database.readWinkelwagen(aOvereenkomst.klantID);
            if (winkelwagenlijst != null)
            {
                foreach(Winkelwagen item in winkelwagenlijst)
                {
                    try
                    {
                        database.removeFromWinkelwagen(aKlant.klantID, item.oldproductID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        Session["Betaling"] = null;
        Session["profieltab"] = "bestellingen";
        Response.Redirect("ProfilePage.aspx");
    }
}