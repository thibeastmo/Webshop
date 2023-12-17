using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bevestiging : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Registratiegegevens"] != null)
        {
            Klant aKlant = (Klant)Session["Registratiegegevens"];
            Session["Registratiegegevens"] = null;
            string strToken = Request.QueryString["Name"];
            if (strToken == string.Empty || strToken == null)
            { //Bevestigingsmail net verstuurd
                Session["Registratiegegevens"] = aKlant;
                registratievoltooiendiv.Visible = true;
                registratiebevestigingdiv.Visible = false;
                registratieerrordiv.Visible = false;
            }
            else
            { //Gebruiker heeft de bevestigingsmail gebruikt
                if (strToken == (string)Session["Token"])
                { //Juiste token
                    registratievoltooiendiv.Visible = false;
                    registratiebevestigingdiv.Visible = true;
                    registratieerrordiv.Visible = false;
                    try
                    {
                        database.editKlant(aKlant.klantID, "1", "bevestigd");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    aKlant.bevestigd = "1";
                    Session["Gebruiker"] = aKlant;
                    FormsAuthentication.SetAuthCookie(aKlant.mail, false);
                }
                else
                { //Verkeerde token
                    registratievoltooiendiv.Visible = false;
                    registratiebevestigingdiv.Visible = false;
                    registratieerrordiv.Visible = true;
                }
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void btnNewMailResend_Click(object sender, EventArgs e)
    {
        if (Session["Registratiegegevens"] != null)
        { //De gebruiker heeft net geregistreerd
            if (txtMail.Text != string.Empty)
            { //Mail opnieuw versturen en aanpassen in database
                try
                {
                    Klant aKlant = (Klant)Session["Registratiegegevens"];
                    database.editKlant(aKlant.klantID, txtMail.Text ,"mail");
                    aKlant.mail = txtMail.Text;
                    string strToken = Methods.CreatePublicToken();
                    SendRegisterMail(aKlant, strToken);
                    registratievoltooiendiv.Visible = false;
                    registratiebevestigingdiv.Visible = true;
                    registratieerrordiv.Visible = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void btnMailResend_Click(object sender, EventArgs e)
    {
        if (Session["Registratiegegevens"] != null)
        { //De gebruiker heeft net geregistreerd
          //Mail opnieuw versturen
            try
            {
                Klant aKlant = (Klant)Session["Registratiegegevens"];
                string strToken = Methods.CreatePublicToken();
                SendRegisterMail(aKlant, strToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    private void SendRegisterMail(Klant newKlant, string strToken)
    {
        string strBody = "<h2 style='color:gray;font-size:3em;text-align:center" +
                         "font-weight:bold'>Bijna klaar met registreren!<h3>" +
                         "<p style='color:darkgray;font-size:1.2em'>Beste " + newKlant.voornaam + ",<br />" +
                         "Je bent bijna klaar met registreren. Je moet enkel nog op de knop onderaan deze mail klikken om " +
                         "de registratie af te ronden.<br />" +
                         "Ben jij dit niet? Dan mag je deze mail negeren.<br /></p>" +
                         "<br /><br /><br />" +
                         "<a href='Bevestiging.aspx?Name=" + strToken + "' style='padding:2%;background-color:dodgerblue;color:white;" +
                         "font-weight:bold;font-size:2em;margin-left:auto;margin-right:auto;text-decoration:none'>Bevestig</a>";
        try
        {
            Methods.Send(txtMail.Text, "thimo@fmgraphics.be", "Bevestigings e-mail", strBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}