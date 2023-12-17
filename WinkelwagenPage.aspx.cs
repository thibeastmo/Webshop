using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WinkelwagenPage : System.Web.UI.Page
{
    decimal TotalPrice = 0;
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["Gebruiker"] != null)
        {
            Klant aKlant = (Klant)Session["Gebruiker"];
            List<Winkelwagen> winkelwagenlijst = database.readWinkelwagen(aKlant.klantID);
            List<Favorieten> favorieten = database.readFavorieten(aKlant.klantID);
            if (winkelwagenlijst != null)
            {
                pnlNoInWinkelwagen.Visible = false;
                pnlWinkelwagen.Visible = true;
                foreach(Winkelwagen winkelwagenproduct in winkelwagenlijst)
                {
                    bool ContainsItem = false;
                    if (favorieten != null)
                    {
                        foreach (Favorieten fav in favorieten)
                        {
                            if (fav.oldproductID.ToString() == winkelwagenproduct.oldproductID)
                            {
                                ContainsItem = true;
                            }
                        }
                    }
                    createWinkelwagenProduct(winkelwagenproduct, ContainsItem);
                }
                lblTotaal.Text = "€" + TotalPrice.ToString();
            }
            else
            {
                pnlNoInWinkelwagen.Visible = true;
                pnlWinkelwagen.Visible = false;
            }
        }
        else
        {
            //Response.Redirect("Default.aspx");
            Response.Redirect("LoginPage.aspx");
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    } //leeg
    private void createWinkelwagenProduct(Winkelwagen winkelwagenproduct, bool FavContainsItem)
    {
        Oldproduct aOldproduct = database.readOldproduct(winkelwagenproduct.oldproductID);
        if (aOldproduct.productnaam != null)
        {
            HtmlGenericControl mainDiv = new HtmlGenericControl("div");
            mainDiv.Attributes.Add("class", "wp");

            HtmlGenericControl picture = new HtmlGenericControl("img");
            if (aOldproduct.afbeelding == "0")
            {
                aOldproduct.afbeelding = "no_image_found.png";
            }
            picture.Attributes.Add("src", "/images/producten/" + aOldproduct.afbeelding);

            HtmlGenericControl naam = new HtmlGenericControl("span");
            naam.InnerText = aOldproduct.merk + Environment.NewLine + aOldproduct.productnaam;

            TextBox txthoeveel = new TextBox();
            txthoeveel.Attributes.Add("type", "number");
            txthoeveel.Text = winkelwagenproduct.hoeveel.ToString();
            txthoeveel.ID = "amount" + aOldproduct.oldproductID;
            txthoeveel.TextChanged += new EventHandler(txthoeveel_OnTextChanged);
            txthoeveel.AutoPostBack = true;

            HtmlGenericControl prijsperstuk = new HtmlGenericControl("span");
            prijsperstuk.InnerText = aOldproduct.prijs.ToString();
            HtmlGenericControl prijsmaalhoeveel = new HtmlGenericControl("span");
            prijsmaalhoeveel.InnerText = (aOldproduct.prijs * winkelwagenproduct.hoeveel).ToString();
            TotalPrice += (winkelwagenproduct.hoeveel * aOldproduct.prijs);

            Button verwijder = new Button();
            verwijder.ID = "verwijder" + aOldproduct.oldproductID.ToString();
            verwijder.CssClass = "dispose";
            verwijder.Click += new EventHandler(RemoveFromWinkelwagen);

            Button favoriet = new Button();
            favoriet.ID = "favoriet" + aOldproduct.oldproductID.ToString();
            favoriet.CssClass = "fav";
            favoriet.Click += new EventHandler(AddToFavorieten);
            if (FavContainsItem)
            {
                favoriet.Style.Add("background-image", "images/favorite.png");
            }
            else
            {
                favoriet.Style.Add("background-image", "images/favorite_not.png");
            }

            mainDiv.Controls.Add(picture);
            mainDiv.Controls.Add(naam);
            mainDiv.Controls.Add(txthoeveel);
            mainDiv.Controls.Add(prijsperstuk);
            mainDiv.Controls.Add(prijsmaalhoeveel);
            mainDiv.Controls.Add(verwijder);
            mainDiv.Controls.Add(favoriet);
            itemlist.Controls.Add(mainDiv);
        }
        else
        {
            database.removeFromWinkelwagen(winkelwagenproduct.klantID, winkelwagenproduct.oldproductID);
        }
    }
    protected void AddToFavorieten(object sender, EventArgs e)
    {
        string[] entries = (sender as Button).ID.Split(new string[] { "favoriet" }, StringSplitOptions.None);
        string oldproductID = entries[1];
        Klant aKlant = (Klant)Session["Gebruiker"];
        string strCommand = "SELECT klantID FROM tblfavorieten WHERE oldproductID = @variabele AND klantID = '" + aKlant.klantID + "';";
        List<string> x = database.readColumn(strCommand, oldproductID);
        if (x.Count == 0)
        {
            try
            {
                database.addFavoriet(aKlant.klantID, oldproductID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        { //Verwijder van favorieten
            try
            {
                database.removeFavoriet(aKlant.klantID, oldproductID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        ReloadPage();
        //Response.Redirect(Request.RawUrl);
    }
    protected void RemoveFromWinkelwagen(object sender, EventArgs e)
    {
        string[] entries = (sender as Button).ID.Split(new string[] { "verwijder" }, StringSplitOptions.None);
        string productID = entries[1];
        Klant aKlant = (Klant)Session["Gebruiker"];
        try
        {
            database.removeFromWinkelwagen(aKlant.klantID, productID);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Response.Redirect(Request.RawUrl);
    }
    protected void txthoeveel_OnTextChanged(object sender, EventArgs e)
    {
        Klant aKlant = (Klant)Session["Gebruiker"];
        string[] entries = (sender as TextBox).ID.Split(new string[] { "amount" }, StringSplitOptions.None);
        string productID = entries[1];
        try
        {
            database.editWinkelwagen(aKlant.klantID, productID, (sender as TextBox).Text, "hoeveel");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Response.Redirect(Request.RawUrl);
    }
    protected void btnGoToFavorieten_Click(object sender, EventArgs e)
    {
        Session["profieltab"] = "favorieten";
        Response.Redirect("ProfilePage.aspx");
    }
    protected void btnBetalen_Click(object sender, EventArgs e)
    {
        Klant aKlant = (Klant)Session["Gebruiker"];
        if (aKlant != null)
        {
            List<Winkelwagen> winkelwagenlijst = database.readWinkelwagen(aKlant.klantID);
            if (winkelwagenlijst != null)
            {
                //Controleer of er ooit een bestelling heeft plaatsgevonden maar dat nog niet betaald is
                //Als er ooit een bestelling is geweest dat niet betaald is dan verwijder je die en maak je een nieuwe aan
                //Verwijder dan zowel de overeenkomst als de producten in tblbestelling
                string strCommand = "SELECT bestellingID FROM tblovereenkomst WHERE klantID = @variabele;";
                List<string> bestellingIDLijstVanKlant = database.readColumn(strCommand, aKlant.klantID);
                if (bestellingIDLijstVanKlant != null)
                { //Klant heeft ooit al eens besteld of proberen te bestellen
                    foreach(string bestellingID in bestellingIDLijstVanKlant)
                    {
                        Overeenkomst aOvereenkomst = new Overeenkomst();
                        try
                        {
                            aOvereenkomst = database.readOvereenkomst(bestellingID);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        if (aOvereenkomst != null)
                        {
                            if (aOvereenkomst.betaald == 0)
                            {
                                try
                                {
                                    database.removeOvereenkomst(bestellingID);
                                    database.removeBestelling(bestellingID);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }

                string strDatum = DateTime.Now.ToString();
                DateTime dDate;
                if (DateTime.TryParse(strDatum, out dDate))
                {
                    strDatum = dDate.ToString("dd/MM/yyyy hh:mm").Replace("-", "/");
                }
                int intBestellingID = Methods.CreateBestelling(winkelwagenlijst, strDatum, false, false);
                Betaling aBetaling = new Betaling();
                aBetaling.BestellingID = intBestellingID;
                Session["Betaling"] = aBetaling;
                Response.Redirect("Betalen.aspx");
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("LoginPage.aspx");
        }
    }
    private void ReloadPage()
    {
        TotalPrice = 0;
        itemlist.Controls.Clear();
        if (Session["Gebruiker"] != null)
        {
            Klant aKlant = (Klant)Session["Gebruiker"];
            List<Winkelwagen> winkelwagenlijst = database.readWinkelwagen(aKlant.klantID);
            List<Favorieten> favorieten = database.readFavorieten(aKlant.klantID);
            if (winkelwagenlijst != null)
            {
                pnlNoInWinkelwagen.Visible = false;
                pnlWinkelwagen.Visible = true;
                foreach (Winkelwagen winkelwagenproduct in winkelwagenlijst)
                {
                    bool ContainsItem = false;
                    if (favorieten != null)
                    {
                        foreach (Favorieten fav in favorieten)
                        {
                            if (fav.oldproductID.ToString() == winkelwagenproduct.oldproductID)
                            {
                                ContainsItem = true;
                            }
                        }
                    }
                    createWinkelwagenProduct(winkelwagenproduct, ContainsItem);
                }
                lblTotaal.Text = "€" + TotalPrice.ToString();
            }
            else
            {
                pnlNoInWinkelwagen.Visible = true;
                pnlWinkelwagen.Visible = false;
            }
        }
        else
        {
            //Response.Redirect("Default.aspx");
            Response.Redirect("LoginPage.aspx");
        }
    }
}