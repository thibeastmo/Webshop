using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProfilePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Gebruiker"] != null)
        {
            Response.Redirect("ProfilePage.aspx");
        }
    }
    #region Registreren
    protected void btnRegistreren_Click(object sender, EventArgs e)
    {
        if (CheckIfAllRegisterDataIsFilledIn())
        { //Alle gegevens zijn ingevuld
            string strCommand = "SELECT klantID FROM tblklanten WHERE mail = @variabele;";
            List<string> x = database.readColumn(strCommand, txtMail.Text);
            if (x.Count == 0)
            {
                ResetRegisterColors();
                lblErrorRegistreren.Text = string.Empty;
                //Controleer of wachtwoord1 = wachtwoord2
                if (CheckIfPasswordsAreTheSame())
                { //Wachtwoorden zijn hetzelfde
                  //Voeg klant toe aan database
                    Klant newKlant = new Klant();
                    newKlant.voornaam = txtVoornaam.Text;
                    newKlant.achternaam = txtAchternaam.Text;
                    newKlant.mail = txtMail.Text;
                    newKlant.telefoonnummer = txtTel.Text;
                    newKlant.wachtwoord = txtWachtwoord1.Text;
                    newKlant.bevestigd = "0";
                    try
                    {
                        database.addKlant(newKlant);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Session["Registratiegegevens"] = newKlant;
                    string strToken = Methods.CreatePublicToken();
                    Session["Token"] = strToken;
                    SendRegisterMail(newKlant, strToken);
                    Response.Redirect("Bevestiging.aspx");
                }
            }
            else
            {
                ResetRegisterColors();
                txtMail.BorderColor = Color.Pink;
                lblErrorRegistreren.Text = "Een account met deze mail bestaat al!";
            }
        }
    }
    private bool CheckIfAllRegisterDataIsFilledIn()
    {
        ResetRegisterColors();
        bool AllFilledIn = true;
        if (txtVoornaam.Text == string.Empty)
        {
            AllFilledIn = false;
            txtVoornaam.BorderColor = Color.Pink;
        }
        if (txtAchternaam.Text == string.Empty)
        {
            AllFilledIn = false;
            txtAchternaam.BorderColor = Color.Pink;
        }
        if (txtTel.Text == string.Empty)
        {
            AllFilledIn = false;
            txtTel.BorderColor = Color.Pink;
        }
        if (txtMail.Text == string.Empty)
        {
            AllFilledIn = false;
            txtMail.BorderColor = Color.Pink;
        }
        if (txtWachtwoord1.Text == string.Empty)
        {
            AllFilledIn = false;
            txtWachtwoord1.BorderColor = Color.Pink;
        }
        if (txtWachtwoord2.Text == string.Empty)
        {
            AllFilledIn = false;
            txtWachtwoord2.BorderColor = Color.Pink;
        }
        if (!AllFilledIn)
        {
            lblErrorRegistreren.Text = "Niet alle gegevens zijn ingevuld!";
        }

        return AllFilledIn;
    }
    private bool CheckIfPasswordsAreTheSame()
    {
        ResetRegisterColors();
        bool SamePasswords = true;
        if (txtWachtwoord1.Text != txtWachtwoord2.Text)
        { //Wachtwoorden verschillen
            SamePasswords = false;
            txtWachtwoord1.BorderColor = Color.Pink;
            txtWachtwoord2.BorderColor = Color.Pink;
            lblErrorRegistreren.Text = "De wachtwoorden komen niet overeen!";
        }
        return SamePasswords;
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
    private void ResetRegisterColors()
    {
        txtVoornaam.BorderColor = Color.GhostWhite;
        txtAchternaam.BorderColor = Color.GhostWhite;
        txtTel.BorderColor = Color.GhostWhite;
        txtMail.BorderColor = Color.GhostWhite;
        txtWachtwoord1.BorderColor = Color.GhostWhite;
        txtWachtwoord2.BorderColor = Color.GhostWhite;
        lblErrorRegistreren.Text = string.Empty;
    }
    #endregion
    #region Aanmelden
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        ResetAanmeldenColors();
        if (UserName.Text != string.Empty)
        {
            if (Password.Text != string.Empty)
            {
                string strCommand = "SELECT klantID FROM tblklanten WHERE mail = @variabele;";
                List<string> x = database.readColumn(strCommand, UserName.Text);
                string strKlantID = x[0];
                if (x.Count != 0)
                { //Mail bestaat
                  //Krijg wachtwoord waarde als eerste waarde van list<string>
                    strCommand = "SELECT wachtwoord FROM tblklanten WHERE mail = @variabele;";
                    x = database.readColumn(strCommand, UserName.Text);
                    if (x[0] == Password.Text)
                    { //Inloggen
                        Klant aKlant = database.readKlant(strKlantID);
                        Session["Gebruiker"] = aKlant;
                        FormsAuthentication.SetAuthCookie(aKlant.mail, false);
                        if (cbRememberMe.Checked)
                        {
                            // Set the new expiry date - to thirty days from now
                            DateTime expiryDate = DateTime.Now.AddDays(30);

                            // Create a new forms auth ticket
                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, UserName.Text, DateTime.Now, expiryDate, true, String.Empty);

                            // Encrypt the ticket
                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                            // Create a new authentication cookie - and set its expiration date
                            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            authenticationCookie.Expires = ticket.Expiration;

                            // Add the cookie to the response.
                            Response.Cookies.Add(authenticationCookie);
                        }
                        Response.Redirect("Default.aspx");
                    }
                    else
                    { //Fout wachtwoord
                        Password.BorderColor = Color.Pink;
                        lblErrorAanemlden.Text = "Verkeerde wachtwoord of mail. Probeer opnieuw!";
                    }
                }
                else
                { //Mail bestaat niet
                    UserName.BorderColor = Color.Pink;
                    lblErrorAanemlden.Text = "Deze mail is nog niet geregistreerd!";
                }
            }
            else
            {
                Password.BorderColor = Color.Pink;
                lblErrorAanemlden.Text = "Vul uw wachtwoord in!";
            }
        }
        else
        {
            UserName.BorderColor = Color.Pink;
            lblErrorAanemlden.Text = "Vul uw mail/gebruikersnaam in!";
        }
        
    }
    private void ResetAanmeldenColors()
    {
        UserName.BorderColor = Color.GhostWhite;
        Password.BorderColor = Color.GhostWhite;
        lblErrorAanemlden.Text = string.Empty;
    }
    #endregion
}