using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Security.Tokens;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ProfilePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (fuUploadproducten.HasFile)
        {
            futxt.InnerText = fuUploadproducten.FileName;
        }
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["Gebruiker"] != null)
        {
            Klant aKlant = (Klant)Session["Gebruiker"];
            if (aKlant.admin)
            {
                btnUploadtab.Visible = true;
                btnUploadtab.Enabled = true;
                btnOverzicht.Visible = true;
                btnOverzicht.Enabled = true;
            }
            else
            {
                btnUploadtab.Visible = false;
                btnUploadtab.Enabled = false;
                btnOverzicht.Visible = false;
                btnOverzicht.Enabled = false;
            }
            if (Session["profieltab"] != null)
            {
                string tab = (string)Session["profieltab"];
                switch (tab)
                {
                    case "gegevens":
                        pnlGegevens.Visible = true;
                        pnlBestellingen.Visible = false;
                        pnlFavorieten.Visible = false;
                        pnlUpload.Visible = false;
                        pnlOverzicht.Visible = false;
                        txtVoornaam.Enabled = false;
                        txtAchternaam.Enabled = false;
                        txtMail.Enabled = false;
                        txtTel.Enabled = false;
                        txtWachtwoord1.Enabled = false;
                        txtWachtwoord2.Visible = false;
                        txtWachtwoord2.Enabled = false;
                        cbToggle.Enabled = false;
                        InitializeGegevens();
                        break;
                    case "favorieten":
                        pnlGegevens.Visible = false;
                        pnlBestellingen.Visible = false;
                        pnlFavorieten.Visible = true;
                        pnlUpload.Visible = false;
                        pnlOverzicht.Visible = false;
                        txtVoornaam.Enabled = false;
                        txtAchternaam.Enabled = false;
                        txtMail.Enabled = false;
                        txtTel.Enabled = false;
                        txtWachtwoord1.Enabled = false;
                        txtWachtwoord2.Visible = false;
                        txtWachtwoord2.Enabled = false;
                        cbToggle.Enabled = false;
                        InitializeFavorieten();
                        break;
                    case "bestellingen":
                        pnlGegevens.Visible = false;
                        pnlBestellingen.Visible = true;
                        pnlFavorieten.Visible = false;
                        pnlUpload.Visible = false;
                        pnlOverzicht.Visible = false;
                        txtVoornaam.Enabled = false;
                        txtAchternaam.Enabled = false;
                        txtMail.Enabled = false;
                        txtTel.Enabled = false;
                        txtWachtwoord1.Enabled = false;
                        txtWachtwoord2.Visible = false;
                        txtWachtwoord2.Enabled = false;
                        cbToggle.Enabled = false;
                        InitializeBestellingen();
                        break;
                    case "upload":
                        pnlGegevens.Visible = false;
                        pnlBestellingen.Visible = false;
                        pnlFavorieten.Visible = false;
                        pnlUpload.Visible = true;
                        pnlOverzicht.Visible = false;
                        txtVoornaam.Enabled = false;
                        txtAchternaam.Enabled = false;
                        txtMail.Enabled = false;
                        txtTel.Enabled = false;
                        txtWachtwoord1.Enabled = false;
                        txtWachtwoord2.Visible = false;
                        txtWachtwoord2.Enabled = false;
                        cbToggle.Enabled = false;
                        break;
                    case "overzicht":
                        pnlGegevens.Visible = false;
                        pnlBestellingen.Visible = false;
                        pnlFavorieten.Visible = false;
                        pnlUpload.Visible = false;
                        pnlOverzicht.Visible = true;
                        txtVoornaam.Enabled = false;
                        txtAchternaam.Enabled = false;
                        txtMail.Enabled = false;
                        txtTel.Enabled = false;
                        txtWachtwoord1.Enabled = false;
                        txtWachtwoord2.Visible = false;
                        txtWachtwoord2.Enabled = false;
                        cbToggle.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                pnlGegevens.Visible = true;
                pnlBestellingen.Visible = false;
                pnlFavorieten.Visible = false;
                pnlUpload.Visible = false;
                pnlOverzicht.Visible = false;
                txtVoornaam.Enabled = false;
                txtAchternaam.Enabled = false;
                txtMail.Enabled = false;
                txtTel.Enabled = false;
                txtWachtwoord1.Enabled = false;
                txtWachtwoord2.Visible = false;
                txtWachtwoord2.Enabled = false;
                cbToggle.Enabled = false;
                InitializeGegevens();
            }
        }
        else
        {
            pnlGegevens.Visible = true;
            pnlBestellingen.Visible = false;
            pnlFavorieten.Visible = false;
            pnlOverzicht.Visible = false;
            txtVoornaam.Enabled = false;
            txtAchternaam.Enabled = false;
            txtMail.Enabled = false;
            txtTel.Enabled = false;
            txtWachtwoord1.Enabled = false;
            txtWachtwoord2.Visible = false;
            txtWachtwoord2.Enabled = false;
            cbToggle.Enabled = false;
            Response.Redirect("LoginPage.aspx");
        }
    }
    private void InitializeGegevens()
    {
        Klant aKlant = (Klant)Session["Gebruiker"];
        cbToggle.Enabled = true;
        pnlBestellingen.Visible = false;
        pnlFavorieten.Visible = false;
        pnlGegevens.Visible = true;
        txtVoornaam.Enabled = false;
        txtAchternaam.Enabled = false;
        txtMail.Enabled = false;
        txtTel.Enabled = false;
        txtWachtwoord1.Enabled = false;
        txtWachtwoord2.Visible = false;
        if (Page.IsPostBack || txtVoornaam.Text == string.Empty && txtAchternaam.Text == string.Empty && txtMail.Text == string.Empty && txtTel.Text == string.Empty && txtWachtwoord1.Text == string.Empty && txtWachtwoord2.Text == string.Empty)
        {
            txtVoornaam.Text = aKlant.voornaam;
            txtAchternaam.Text = aKlant.achternaam;
            txtTel.Text = aKlant.telefoonnummer;
            txtMail.Text = aKlant.mail;
            txtWachtwoord1.Text = aKlant.wachtwoord.Length.ToString() + " characters";
        }
    }
    private void InitializeFavorieten()
    {
        //fitems.Controls.Clear();
        if (Session["Gebruiker"] != null)
        {
            Klant aKlant = (Klant)Session["Gebruiker"];
            List<Favorieten> favorietenlijst = database.readFavorieten(aKlant.klantID);
            List<string> oldproductenIDlijst = new List<string>();
            if (favorietenlijst != null)
            {
                divNoFavorieten.Visible = false;
                foreach (Favorieten x in favorietenlijst)
                {
                    oldproductenIDlijst.Add(x.oldproductID.ToString());
                }
                if (oldproductenIDlijst != null)
                {
                    foreach (string oldproductID in oldproductenIDlijst)
                    {
                        try
                        {
                            Oldproduct aOldproduct = database.readOldproduct(oldproductID);
                            if (aOldproduct.productnaam != null)
                            {
                                createOldroductAsFavourite(aOldproduct);
                            }
                            else
                            {
                                database.removeFavoriet(aKlant.klantID, oldproductID);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
            else
            {
                divNoFavorieten.Visible = true;
                fitems.Visible = false;
            }
        }
    }
    private void createOldroductAsFavourite(Oldproduct aOldproduct)
    {
        HtmlGenericControl mainDiv = new HtmlGenericControl("div");
        mainDiv.Attributes.Add("class", "itemdiv");

        if (aOldproduct.afbeelding == "0")
        {
            aOldproduct.afbeelding = "no_image_found.png";
        }
        ImageButton newImage = new ImageButton();
        newImage.ImageUrl = "images/producten/" + aOldproduct.afbeelding;
        newImage.ID = "imgProductX" + Convert.ToString(aOldproduct.oldproductID);
        newImage.Click += new ImageClickEventHandler(GoToProductPage);

        HtmlGenericControl newH4 = new HtmlGenericControl("h4");
        newH4.InnerHtml = aOldproduct.merk;

        Button newSpan1 = new Button();
        newSpan1.CssClass = "productnaam";
        newSpan1.Text = aOldproduct.productnaam;
        newSpan1.ID = "naamProductX" + Convert.ToString(aOldproduct.oldproductID);
        newSpan1.Click += new EventHandler(GoToProductPage);

        HtmlGenericControl newSpan2 = new HtmlGenericControl("span");
        newSpan2.Attributes.Add("class", "prijs");
        newSpan2.InnerHtml = aOldproduct.prijs.ToString();

        HtmlGenericControl newDiv = new HtmlGenericControl("div");

        Button newButton1 = new Button();
        newButton1.ID = "winkelkar" + Convert.ToString(aOldproduct.oldproductID);
        newButton1.Enabled = true;
        newButton1.Click += new EventHandler(AddToWinkelwagen);
        newButton1.CssClass = "btnwinkelwagen";

        Button newButton2 = new Button();
        newButton2.ID = "favoriet" + Convert.ToString(aOldproduct.oldproductID);
        newButton2.Style.Add("background-image", "images/favorite.png");
        newButton2.CssClass = "btnfavoriet";
        newButton2.Click += new EventHandler(AddToFavorieten);

        newDiv.Controls.Add(newButton1);
        newDiv.Controls.Add(newButton2);


        mainDiv.Controls.Add(newImage);
        mainDiv.Controls.Add(newH4);
        mainDiv.Controls.Add(newSpan1);
        mainDiv.Controls.Add(newSpan2);
        mainDiv.Controls.Add(newDiv);

        //Voeg het toe aan de pagina
        fitems.Controls.Add(mainDiv);
    }
    private void createOvereenkomst(Overeenkomst aOvereenkomst, int intCounter)
    {
        HtmlGenericControl mainDiv = new HtmlGenericControl("div");
        mainDiv.Attributes.Add("class", "bestellingdiv");

        HtmlGenericControl infodiv = new HtmlGenericControl("div");
        infodiv.Attributes.Add("class", "binfodiv");

        Label lblDatum = new Label();
        lblDatum.Text = aOvereenkomst.datum;
        lblDatum.ID = "lblDatum" + aOvereenkomst.bestellingID.ToString();
        Label lblBetaald = new Label();
        if (aOvereenkomst.betaald == 0)
        {
            lblBetaald.Text = "Niet betaald";
        }
        else
        {
            lblBetaald.Text = string.Empty;
        }
        lblBetaald.ID = "lblBetaald" + aOvereenkomst.bestellingID.ToString();
        Label lblBezorgd = new Label();
        if (aOvereenkomst.bezorgd == 0)
        {
            lblBezorgd.Text = "Niet bezorgd";
        }
        else
        {
            lblBezorgd.Text = "Bezorgd";
        }
        lblBezorgd.ID = "lblBezorgd" + aOvereenkomst.bestellingID.ToString();
        Label lblOvereenkomstNummer = new Label();
        lblOvereenkomstNummer.Text = "BestellingID: " + aOvereenkomst.bestellingID.ToString();
        lblOvereenkomstNummer.ID = "lblOvereenkomstnummer" + aOvereenkomst.bestellingID.ToString();
        Label lblAantalProducten = new Label();
        lblAantalProducten.Text = "Producten: " + aOvereenkomst.Bestellingproducten.Count.ToString();
        lblAantalProducten.ID = "lblAantalproducten" + aOvereenkomst.bestellingID.ToString();

        if (aOvereenkomst.betaald == 1)
        {
            infodiv.Controls.Add(lblBetaald);
            infodiv.Controls.Add(lblDatum);
        }
        else
        {
            infodiv.Controls.Add(lblDatum);
            infodiv.Controls.Add(lblBetaald);
        }
        infodiv.Controls.Add(lblBezorgd);
        infodiv.Controls.Add(lblOvereenkomstNummer);
        infodiv.Controls.Add(lblAantalProducten);

        mainDiv.Controls.Add(infodiv);

        HtmlGenericControl bitemdiv = new HtmlGenericControl("div");
        bitemdiv.Attributes.Add("id", "bitemlistdiv" + aOvereenkomst.bestellingID.ToString());
        bitemdiv.Attributes.Add("class", "bitemlistdiv");

        foreach (Bestelling aBestelling in aOvereenkomst.Bestellingproducten)
        {
            try
            {
                Oldproduct aOldproduct = database.readOldproduct(aBestelling.oldproductID.ToString());

                HtmlButton itemdiv = new HtmlButton();
                itemdiv.ServerClick += new EventHandler(GoProductPage);
                itemdiv.Attributes.Add("class", "bitemdiv");
                itemdiv.ID = intCounter.ToString() + "itemdiv_" + aOldproduct.oldproductID.ToString();

                HtmlGenericControl newImage = new HtmlGenericControl("img");
                if (aOldproduct.afbeelding == "0")
                {
                    aOldproduct.afbeelding = "no_image_found.png";
                }
                newImage.Attributes.Add("src", "images/producten/" + aOldproduct.afbeelding);

                Label lblproduct = new Label();
                lblproduct.Text = aOldproduct.merk + " " + aOldproduct.productnaam;

                Label lblamount = new Label();
                lblamount.Text = "Aantal: " + aBestelling.hoeveel.ToString();

                itemdiv.Controls.Add(newImage);
                itemdiv.Controls.Add(lblproduct);
                itemdiv.Controls.Add(lblamount);

                bitemdiv.Controls.Add(itemdiv);
            }
            catch
            {

            }
            
        }

        mainDiv.Controls.Add(bitemdiv);
        mainbestellingdiv.Controls.Add(mainDiv);
    }
    private void GoProductPage(object sender, EventArgs e)
    {
        string[] entries = new string[5];
        try
        {
            entries = (sender as Control).ID.Split(new string[] { "_" }, StringSplitOptions.None);
        }
        catch
        {
        }
        List<int> ProductID = new List<int>();
        ProductID.Add(Convert.ToInt32(entries[entries.Length - 1]));
        ProductID.Add(0);
        Session["ProductID"] = ProductID;
        Response.Redirect("ProductPage.aspx");
    }
    private void GoToProductPage(object sender, EventArgs e)
    {
        string[] entries = new string[5];
        try
        {
            entries = (sender as Control).ID.Split(new string[] { "ProductX" }, StringSplitOptions.None);
        }
        catch
        {
        }
        int ProductID = Convert.ToInt32(entries[entries.Length - 1]);
        Session["ProductID"] = ProductID;
        Response.Redirect("ProductPage.aspx");
    }
    protected void AddToWinkelwagen(object sender, EventArgs e)
    {
        Klant aKlant = (Klant)Session["Gebruiker"];
        string[] entries = (sender as Button).ID.Split(new string[] { "winkelkar" }, StringSplitOptions.None);
        string oldproductID = entries[1];
        string strCommand = "SELECT hoeveel FROM tblwinkelwagen WHERE oldproductID = @variabele AND klantID = '" + aKlant.klantID + "';";
        List<string> x = database.readColumn(strCommand, oldproductID);
        if (x.Count == 0)
        {
            try
            {
                database.addWinkelwagen(aKlant.klantID, oldproductID, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        { //Doe +1 bij hoeveel van dit product
            int intHoeveel = Convert.ToInt32(x[0]);
            intHoeveel++;
            try
            {
                database.editWinkelwagen(aKlant.klantID, oldproductID, intHoeveel.ToString(), "hoeveel");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        Response.Redirect(Request.RawUrl);
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
        Response.Redirect(Request.RawUrl);
    }
    private void InitializeBestellingen()
    {
        if (Session["Gebruiker"] != null)
        {
            Klant aKlant = (Klant)Session["Gebruiker"];
            string strCommand = "SELECT bestellingID FROM tblovereenkomst WHERE klantID = @variabele";
            List<string> bestellingIDlijst = new List<string>();
            try
            {
                bestellingIDlijst = database.readColumn(strCommand, aKlant.klantID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (bestellingIDlijst != null)
            {
                mainbestellingdiv.Controls.Clear();
                mainbestellingdiv.Visible = true;
                nobestellingdiv.Visible = false;
                List<Overeenkomst> Overeenkomstlijst = new List<Overeenkomst>();
                foreach (string id in bestellingIDlijst)
                {
                    Overeenkomstlijst.Add(database.readOvereenkomst(id));
                }
                int intCounter = 0;
                foreach (Overeenkomst overeenkomst in Overeenkomstlijst)
                {
                    createOvereenkomst(overeenkomst, intCounter);
                    intCounter++;
                }
            }
            else
            {
                mainbestellingdiv.Visible = false;
                nobestellingdiv.Visible = true;
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    protected void btnClick(object sender, EventArgs e)
    {
        Button x = (sender as Button);
        if (x.ID == "btnGegevens")
        {
            Session["profieltab"] = "gegevens";
            Response.Redirect(Request.RawUrl);
        }
        else if (x.ID == "btnFavorieten")
        {
            Session["profieltab"] = "favorieten";
            Response.Redirect(Request.RawUrl);
        }
        else if (x.ID == "btnWinkelwagen")
        {
            Session["profieltab"] = null;
            Response.Redirect("WinkelwagenPage.aspx");
        }
        else if (x.ID == "btnBestellingen")
        {
            Session["profieltab"] = "bestellingen";
            Response.Redirect(Request.RawUrl);
        }
        else if (x.ID == "btnNaarSite")
        {
            Session["profieltab"] = null;
            Response.Redirect("Default.aspx");
        }
        else if (x.ID == "btnLogUit")
        {
            Session["Gebruiker"] = null;
            Session["profieltab"] = null;
            FormsAuthentication.SignOut();
            Response.Redirect("LoginPage.aspx");
        }
        else if (x.ID == "btnUploadtab")
        {
            Session["profieltab"] = "upload";
            Response.Redirect(Request.RawUrl);
        }
        else if (x.ID == "btnOverzicht")
        {
            Session["profieltab"] = "overzicht";
            Response.Redirect(Request.RawUrl);
        }
    }
    protected void cbToggle_CheckedChanged(object sender, EventArgs e)
    {
        if ((sender as CheckBox).Checked)
        {
            txtVoornaam.Enabled = true;
            txtAchternaam.Enabled = true;
            txtMail.Enabled = true;
            txtTel.Enabled = true;
            txtWachtwoord1.Enabled = true;
            txtWachtwoord2.Visible = true;
            txtWachtwoord2.Enabled = true;
        }
        else
        {
            txtVoornaam.Enabled = false;
            txtAchternaam.Enabled = false;
            txtMail.Enabled = false;
            txtTel.Enabled = false;
            txtWachtwoord1.Enabled = false;
            txtWachtwoord2.Visible = false;
            txtWachtwoord2.Enabled = false;
            txtVoornaam.Text = string.Empty;
            txtAchternaam.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtWachtwoord1.Text = string.Empty;
            txtWachtwoord2.Text = string.Empty;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblErrorGegegevens.Text = string.Empty;
        lblErrorGegegevens.ForeColor = Color.Black;
        txtVoornaam.BorderColor = Color.GhostWhite;
        txtAchternaam.BorderColor = Color.GhostWhite;
        txtMail.BorderColor = Color.GhostWhite;
        txtTel.BorderColor = Color.GhostWhite;
        txtWachtwoord1.BorderColor = Color.GhostWhite;
        txtWachtwoord2.BorderColor = Color.GhostWhite;
        bool AnythingChanged = false;
        bool ChangedSuccess = true;
        if (Session["Gebruiker"] != null)
        {
            Klant aKlant = (Klant)Session["Gebruiker"];
            if (txtVoornaam.Text != string.Empty && txtVoornaam.Text != aKlant.voornaam)
            {
                try
                {
                    AnythingChanged = true;
                    database.editKlant(aKlant.klantID, txtVoornaam.Text, "voornaam");
                    aKlant.voornaam = txtVoornaam.Text;
                }
                catch (Exception ex)
                {
                    ChangedSuccess = false;
                    txtVoornaam.BorderColor = Color.Pink;
                    Console.WriteLine(ex.Message);
                }
            }
            if (txtAchternaam.Text != string.Empty && txtAchternaam.Text != aKlant.achternaam)
            {
                try
                {
                    AnythingChanged = true;
                    database.editKlant(aKlant.klantID, txtAchternaam.Text, "achternaam");
                    aKlant.achternaam = txtAchternaam.Text;
                }
                catch (Exception ex)
                {
                    ChangedSuccess = false;
                    txtAchternaam.BorderColor = Color.Pink;
                    Console.WriteLine(ex.Message);
                }
            }
            if (txtMail.Text != string.Empty && txtMail.Text != aKlant.mail)
            {
                try
                {
                    AnythingChanged = true;
                    database.editKlant(aKlant.klantID, txtMail.Text, "mail");
                    aKlant.mail = txtMail.Text;
                }
                catch (Exception ex)
                {
                    ChangedSuccess = false;
                    txtMail.BorderColor = Color.Pink;
                    Console.WriteLine(ex.Message);
                }
            }
            if (txtTel.Text != string.Empty && txtTel.Text != aKlant.telefoonnummer)
            {
                try
                {
                    AnythingChanged = true;
                    database.editKlant(aKlant.klantID, txtTel.Text, "telefoonnummer");
                    aKlant.telefoonnummer = txtTel.Text;
                }
                catch (Exception ex)
                {
                    ChangedSuccess = false;
                    txtTel.BorderColor = Color.Pink;
                    Console.WriteLine(ex.Message);
                }
            }
            if (txtWachtwoord2.Text != string.Empty)
            {
                if (txtWachtwoord1.Text == txtWachtwoord2.Text)
                {
                    if (txtWachtwoord1.Text != aKlant.wachtwoord)
                    { //Pas wachtwoord aan
                        try
                        {
                            AnythingChanged = true;
                            database.editKlant(aKlant.klantID, txtWachtwoord1.Text, "wachtwoord");
                            aKlant.wachtwoord = txtWachtwoord1.Text;
                        }
                        catch (Exception ex)
                        {
                            ChangedSuccess = false;
                            txtWachtwoord1.BorderColor = Color.Pink;
                            txtWachtwoord2.BorderColor = Color.Pink;
                            Console.WriteLine(ex.Message);
                        }
                    }
                    else
                    {
                        txtWachtwoord1.BorderColor = Color.Pink;
                        txtWachtwoord2.BorderColor = Color.Pink;
                        lblErrorGegegevens.ForeColor = Color.OrangeRed;
                        lblErrorGegegevens.Text = "Het wachtwoord is dezelfde als het originele!";
                    }
                }
                else
                {
                    txtWachtwoord1.BorderColor = Color.Pink;
                    txtWachtwoord2.BorderColor = Color.Pink;
                    lblErrorGegegevens.ForeColor = Color.OrangeRed;
                    lblErrorGegegevens.Text = "De wachtwoorden komen niet overeen!";
                }
            }
            if (AnythingChanged)
            {
                if (ChangedSuccess)
                {
                    lblErrorGegegevens.ForeColor = Color.Green;
                    lblErrorGegegevens.Text = "Uw ingevulde gegevens zijn correct aangepast.";
                }
                else
                {
                    lblErrorGegegevens.ForeColor = Color.Pink;
                    lblErrorGegegevens.Text += "Uw ingevulde gegevens zijn correct aangepast." + Environment.NewLine + lblErrorGegegevens.Text;
                }
                Session["Gebruiker"] = aKlant;
            }
            else
            {
                lblErrorGegegevens.Text = "Niets is aangepast.";
                lblErrorGegegevens.ForeColor = Color.Black;
            }
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string strPath = HttpContext.Current.Server.MapPath("csv/newproductcsv.csv");
        //Verwijder oud nieuwe csv bestand indien het er is
        bool FileStillThere = true;
        if (File.Exists(strPath))
        {
            FileStillThere = true;
            try
            {
                File.Delete(strPath);
                FileStillThere = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                FileStillThere = true;
            }
        }
        else
        {
            FileStillThere = false;
        }
        bool saved = false;
        //Slaag nieuwe csv bestand op de juist plek met de juiste naam op
        if (!FileStillThere)
        {
            fuUploadproducten.PostedFile.SaveAs(strPath);
            saved = true;
            lblUpload.Text = string.Empty;
            lblUpload.ForeColor = Color.Black;
        }
        else
        {
            lblUpload.Text += "Het csv bestand kon niet verwijderd worden.";
            lblUpload.ForeColor = Color.OrangeRed;
        }
        if (fuUploadproducten.HasFile)
        { //fileupload heeft een document

            //Stel splitwaarde in
            string strSplitValue = ";";
            if (txtSplitValue.Text != string.Empty)
            {
                strSplitValue = txtSplitValue.Text;
            }
            fuUploadproducten.BorderColor = Color.Transparent;

            //Initialiseer alle producten van het csv bestand
            List<Product> newproductlijst = new List<Product>();
            try
            {
                newproductlijst = database.csv_getAllProducts(strSplitValue, strPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (newproductlijst != null)
            {
                if (newproductlijst.Count != 0)
                {
                    //Check of er geen dubbele producten zijn ingevuld
                    bool NoneTheSame = true;
                    int intCounter = 0;
                    foreach(Product aProduct in newproductlijst)
                    {
                        int intCounterb = 0;
                        foreach(Product bProduct in newproductlijst)
                        {
                            if (intCounter != intCounterb)
                            {
                                if (aProduct.productnaam == bProduct.productnaam && aProduct.merk == bProduct.merk && aProduct.prijs == bProduct.prijs && aProduct.geslacht == bProduct.geslacht && aProduct.hoofdcategorie == bProduct.hoofdcategorie && aProduct.subcategorie == bProduct.subcategorie && aProduct.Beschrijving == bProduct.Beschrijving)
                                {
                                    NoneTheSame = false;
                                }
                            }
                            intCounterb++;
                        }
                        intCounter++;
                    }
                    if (NoneTheSame)
                    {
                        if (newproductlijst[0].productnaam != string.Empty)
                        {
                            //Initialiseer zowel volledige als current lijst 
                            List<Oldproduct> Oldproductlijst = database.readOldproductList();
                            List<Product> productlijst = database.readProductlist();

                            //Voeg producten die in current lijst zitten toe aan volledige lijst ==> overbodig want dat wordt al gedaan wanneer het csv bestand toegevoegd wordt
                            #region 
                            //if (productlijst != null)
                            //{ //Er is een current lijst
                            //    foreach (Product aProduct in productlijst)
                            //    {
                            //        bool Containsitem = false;
                            //        if (Oldproductlijst != null)
                            //        { //Volledige lijst is niet leeg
                            //            foreach (Oldproduct aOldproduct in Oldproductlijst)
                            //            {
                            //                if (aOldproduct.productnaam == aProduct.productnaam && aOldproduct.merk == aProduct.merk)
                            //                { //De volledige lijst bevat deze item van de nieuwe lijst al
                            //                    Containsitem = true;
                            //                }
                            //            }
                            //        }
                            //        if (!Containsitem)
                            //        { //Indien de volledige lijst deze item nog niet bevat voeg die dan toe
                            //            Oldproduct newOldproduct = new Oldproduct();
                            //            newOldproduct.productnaam = aProduct.productnaam;
                            //            newOldproduct.merk = aProduct.merk;
                            //            newOldproduct.prijs = aProduct.prijs;
                            //            newOldproduct.hoofdcategorie = aProduct.hoofdcategorie;
                            //            newOldproduct.subcategorie = aProduct.subcategorie;
                            //            newOldproduct.geslacht = aProduct.geslacht;
                            //            newOldproduct.aantalkeerbekeken = aProduct.aantalkeerbekeken;
                            //            newOldproduct.Beschrijving = aProduct.Beschrijving;
                            //            newOldproduct.afbeelding = aProduct.afbeelding;
                            //            database.addOldproduct(newOldproduct);
                            //        }
                            //    }
                            //}
                            #endregion


                            //Maak productendatabase leeg
                            //Makkelijke methode
                            string strCommand = "DELETE FROM tblproducten;";
                            bool tblproductencleared = false;
                            try
                            {
                                database.ExecuteCommand(strCommand);
                                tblproductencleared = true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            if (!tblproductencleared)
                            {//Moeilijke methode ==> overbodig  --> wordt uitgevoerd indien de eerste methode niet werkte
                                strCommand = "SELECT productID FROM tblproducten";
                                List<string> productIDlijst = database.readColumn(strCommand, null);
                                foreach (string id in productIDlijst)
                                {
                                    database.removeProduct(id);
                                }
                            }

                            //Reset de ID's
                            strCommand = "ALTER TABLE tblproducten DROP COLUMN productID, DROP PRIMARY KEY;";
                            database.ExecuteCommand(strCommand);
                            strCommand = "ALTER TABLE tblproducten ADD COLUMN productID INT NOT NULL AUTO_INCREMENT FIRST, ADD PRIMARY KEY(productID);";
                            database.ExecuteCommand(strCommand);

                            //voeg producten van csv bestand toe indien het opgeslagen is
                            if (saved)
                            { //Opgeslagen bestand en ready to execute stuff
                                foreach (Product newProduct in newproductlijst)
                                {
                                    //Voeg toe aan tbloldproducten indien het er nog niet in is en initialiseer het oldproductID voor het product in tblproducten
                                    bool Containsitem = false;
                                    if (Oldproductlijst != null)
                                    {
                                        foreach (Oldproduct aOldproduct in Oldproductlijst)
                                        {
                                            if (aOldproduct.productnaam == newProduct.productnaam && aOldproduct.merk == newProduct.merk && aOldproduct.prijs == newProduct.prijs && aOldproduct.geslacht == newProduct.geslacht)
                                            {
                                                Containsitem = true;
                                                newProduct.oldproductID = aOldproduct.oldproductID;

                                                //Maak volledige csv lijst up-to-date
                                                if (newProduct.prijs != aOldproduct.prijs)
                                                {
                                                    database.editOldproduct(aOldproduct.oldproductID.ToString(), newProduct.prijs.ToString().Replace(",", "."), "prijs");
                                                }
                                                if (newProduct.hoofdcategorie != aOldproduct.hoofdcategorie)
                                                {
                                                    database.editOldproduct(aOldproduct.oldproductID.ToString(), newProduct.hoofdcategorie, "hoofdcategorie");
                                                }
                                                if (newProduct.subcategorie != aOldproduct.subcategorie)
                                                {
                                                    database.editOldproduct(aOldproduct.oldproductID.ToString(), newProduct.subcategorie, "subcategorie");
                                                }
                                                if (newProduct.geslacht != aOldproduct.geslacht)
                                                {
                                                    database.editOldproduct(aOldproduct.oldproductID.ToString(), newProduct.geslacht, "geslacht");
                                                }
                                                if (newProduct.aantalkeerbekeken != aOldproduct.aantalkeerbekeken)
                                                {
                                                    database.editOldproduct(aOldproduct.oldproductID.ToString(), newProduct.aantalkeerbekeken.ToString(), "aantalkeerbekeken");
                                                }
                                                if (newProduct.Beschrijving != aOldproduct.Beschrijving)
                                                {
                                                    database.editOldproduct(aOldproduct.oldproductID.ToString(), newProduct.Beschrijving, "beschrijving");
                                                }
                                                if (newProduct.afbeelding != aOldproduct.afbeelding)
                                                {
                                                    database.editOldproduct(aOldproduct.oldproductID.ToString(), newProduct.afbeelding, "afbeelding");
                                                }
                                            }
                                        }
                                    }
                                    if (!Containsitem)
                                    {
                                        Oldproduct newOldproduct = new Oldproduct();
                                        newOldproduct.productnaam = newProduct.productnaam;
                                        newOldproduct.merk = newProduct.merk;
                                        newOldproduct.prijs = newProduct.prijs;
                                        newOldproduct.hoofdcategorie = newProduct.hoofdcategorie;
                                        newOldproduct.subcategorie = newProduct.subcategorie;
                                        newOldproduct.geslacht = newProduct.geslacht;
                                        newOldproduct.aantalkeerbekeken = newProduct.aantalkeerbekeken;
                                        newOldproduct.Beschrijving = newProduct.Beschrijving;
                                        newOldproduct.afbeelding = newProduct.afbeelding;
                                        long oldproductID = database.addOldproduct(newOldproduct);
                                        newProduct.oldproductID = Convert.ToInt32(oldproductID);
                                    }

                                    //Voeg toe aan tblproducten
                                    try
                                    {
                                        database.addProduct(newProduct);
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }
                                }
                            }
                            else
                            {
                                lblUpload.Text += "Het csv bestand kon niet opgeslagen worden.";
                                lblUpload.ForeColor = Color.OrangeRed;
                            }
                        }
                        else
                        {
                            lblUpload.Text += "De eerste rij van het csv bestand moet een product zijn!";
                            lblUpload.ForeColor = Color.OrangeRed;
                        }
                    }
                    else
                    {
                        lblUpload.Text += "Er zijn producten die meermaals voorkomen. Verwijdere deze vooraleer je uploadt.";
                        lblUpload.ForeColor = Color.OrangeRed;
                    }
                }
                else
                {
                    lblUpload.Text += "Er waren geen producten in het csv bestand";
                    lblUpload.ForeColor = Color.OrangeRed;
                }
            }
            else
            {
                lblUpload.Text += "Er waren geen producten in het csv bestand";
                lblUpload.ForeColor = Color.OrangeRed;
            }
        }
        else
        {
            fuUploadproducten.BorderColor = Color.OrangeRed;
        }
    }
    protected void btnGenerateOldProductCSV_Click(object sender, EventArgs e)
    {
        string strSplitValue = ";";
        if (txtSplitValue.Text != string.Empty)
        {
            strSplitValue = txtSplitValue.Text;
        }
        lblUpload.Text = string.Empty;
        lblUpload.ForeColor = Color.OrangeRed;
        string strCSVpath = Methods.createCSV("oldproduct", strSplitValue);
        if (strCSVpath != string.Empty)
        {
            //Response.TransmitFile(database.strFilePath);
            Response.ContentType = "Application/csv";
            Response.AppendHeader("Content-Disposition", "attachment; filename=oldproduct.csv");
            Response.TransmitFile(strCSVpath);
            Response.End();
        }
        else
        {
            lblUpload.Text += "Het csv bestand kon niet gemaakt worden.";
            lblUpload.ForeColor = Color.OrangeRed;
        }
    }
    protected void btnGenerateProductCSV_Click(object sender, EventArgs e)
    {
        string strSplitValue = ";";
        if (txtSplitValue.Text != string.Empty)
        {
            strSplitValue = txtSplitValue.Text;
        }
        lblUpload.Text = string.Empty;
        lblUpload.ForeColor = Color.OrangeRed;

        string strCSVpath = Methods.createCSV("product", strSplitValue);
        if (strCSVpath != string.Empty)
        {
            //Response.TransmitFile(database.strFilePath);
            Response.ContentType = "Application/csv";
            Response.AppendHeader("Content-Disposition", "attachment; filename=product.csv");
            Response.TransmitFile(strCSVpath);
            Response.End();
        }
        else
        {
            lblUpload.Text += "Het csv bestand kon niet gemaakt worden.";
            lblUpload.ForeColor = Color.OrangeRed;
        }
    }
    protected void btnGenereerBestellingen_Click(object sender, EventArgs e)
    {
        string strSplitValue = ";";
        lblOverzichtError.Text = string.Empty;
        lblOverzichtError.ForeColor = Color.OrangeRed;
        string strCSVpath = Methods.createCSV("bestellingen", strSplitValue);
        if (strCSVpath != string.Empty)
        {
            //Response.TransmitFile(database.strFilePath);
            Response.ContentType = "Application/csv";
            Response.AppendHeader("Content-Disposition", "attachment; filename=bestellingencsv.csv");
            Response.TransmitFile(strCSVpath);
            Response.End();
        }
        else
        {
            lblOverzichtError.Text += "Het csv bestand kon niet gemaakt worden.";
            lblOverzichtError.ForeColor = Color.OrangeRed;
        }
    }
    protected void btnGenereerProductenBestellingen_Click(object sender, EventArgs e)
    {
        string strSplitValue = ";";
        lblOverzichtError.Text = string.Empty;
        lblOverzichtError.ForeColor = Color.OrangeRed;
        string strCSVpath = Methods.createCSV("productenbestellingen", strSplitValue);
        if (strCSVpath != string.Empty)
        {
            //Response.TransmitFile(database.strFilePath);
            Response.ContentType = "Application/csv";
            Response.AppendHeader("Content-Disposition", "attachment; filename=productenbestellingencsv.csv");
            Response.TransmitFile(strCSVpath);
            Response.End();
        }
        else
        {
            lblOverzichtError.Text += "Het csv bestand kon niet gemaakt worden.";
            lblOverzichtError.ForeColor = Color.OrangeRed;
        }
    }
    protected void btnKlantIDInput_Click(object sender, EventArgs e)
    {
        bool isInt = false;
        int intKlantID = 0;
        try
        {
            intKlantID = Convert.ToInt32(txtKlantIDInput.Text);
            isInt = true;
        }
        catch
        {
            isInt = false;
        }
        if (isInt)
        {
            lblOverzichtError.Text = string.Empty;
            Klant aKlant = new Klant();
            try
            {
                aKlant = database.readKlant(intKlantID.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (aKlant.klantID != null)
            {
                lblOverzichtVoornaam.Text = aKlant.voornaam;
                lblOverzichtAchternaam.Text = aKlant.achternaam;
                lblOverzichtKlantID.Text = aKlant.klantID;
                lblOverzichtMail.Text = aKlant.mail;
                lblOverzichtTelefoonnummer.Text = aKlant.telefoonnummer;
                if (aKlant.bevestigd == "1")
                {
                    lblOverzichtBevestigd.Text = "Bevestigd";
                }
                else
                {
                    lblOverzichtBevestigd.Text = "Niet bevestigd";
                }
                if (aKlant.admin)
                {
                    lblOverzichtAdmin.Text = "Admin";
                }
                else
                {
                    lblOverzichtAdmin.Text = "Bezoeker";
                }
            }
            else
            {
                lblOverzichtError.Text = "Geen klant gevonden met deze klantID.";
            }
        }
        else
        {
            lblOverzichtError.Text = "Een klantID is een getal > 0.";
        }
    }
}