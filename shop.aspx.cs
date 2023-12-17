using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Shop : System.Web.UI.Page
{
    //Prijsvolgordes:
    // 0 = stijgend
    // 1 = dalend
    // 2 = meest bezocht
    int intProductsPerPage = 12;
    int intTotalProducts = 0;
    string MinPrijs;
    string MaxPrijs;
    bool MinPriceChanged;
    bool MaxPriceChanged;
    protected void Page_Load(object sender, EventArgs e)
    {
        string strUrlName = Request.QueryString["Name"];
        if (Page.IsPostBack)
        { //Page reloads
            if (Session["previousprices"] != null)
            {
                List<decimal> previousprijzen = (List<decimal>)Session["previousprices"];
                string templp = previousprijzen[0].ToString();
                string temphp = previousprijzen[1].ToString();
                Preference tempPreference = (Preference)Session["Preferences"];
                if (templp != txtLaagstePrijs.Text)
                { //Laagste prijs is veranderd door gebruiker
                    if (txtLaagstePrijs.Text == string.Empty)
                    {
                        txtLaagstePrijs.Text = "0";
                    }
                    MinPriceChanged = true;
                    tempPreference.minpSetByUser = true;
                    string[] entries = txtLaagstePrijs.Text.Split(',');
                    string[] entries2 = txtHoogstePrijs.Text.Split(',');
                    decimal intLaagsteprijs = Convert.ToDecimal(entries[0]);
                    decimal intHoogsteprijs = Convert.ToDecimal(entries2[0]);
                    if (intLaagsteprijs > intHoogsteprijs)
                    {
                        tempPreference.Minimumprijs = intHoogsteprijs;
                        tempPreference.maxpSetByUser = true;
                        tempPreference.Maximumprijs = intHoogsteprijs;
                    }
                    else
                    {
                        tempPreference.Minimumprijs = Convert.ToDecimal(entries[0]);
                    }
                }
                else
                {
                    MinPriceChanged = false;
                }
                if (temphp != txtHoogstePrijs.Text)
                { //Hoogste prijs is veranderd door gebruiker
                    if (txtHoogstePrijs.Text == string.Empty)
                    {
                        txtHoogstePrijs.Text = txtLaagstePrijs.Text;
                    }
                    MaxPriceChanged = true;
                    tempPreference.maxpSetByUser = true;
                    string[] entries = txtLaagstePrijs.Text.Split(',');
                    string[] entries2 = txtHoogstePrijs.Text.Split(',');
                    decimal intLaagsteprijs = Convert.ToDecimal(entries[0]);
                    decimal intHoogsteprijs = Convert.ToDecimal(entries2[0]);
                    if (intLaagsteprijs > intHoogsteprijs)
                    {
                        tempPreference.Maximumprijs = intLaagsteprijs;
                        tempPreference.minpSetByUser = true;
                        tempPreference.Minimumprijs = intLaagsteprijs;
                    }
                    else
                    {
                        tempPreference.Maximumprijs = Convert.ToDecimal(entries2[0]);
                    }
                }
                else
                {
                    MaxPriceChanged = false;
                }
                Session["Preferences"] = tempPreference;
            }
            GetMinMaxPrices();
            SetMinMaxPrices();
            if (!MinPriceChanged && !MaxPriceChanged)
            {
                UpdateVoorkeuren();
                SetSortValue();
                List<Product> ProductList = InitializeProducts(false);
                AddProductsToPage(ProductList);
                loadPagesnav(intTotalProducts);
            }
        }
        else
        { //Page does not reload
            itemlijst.Controls.Clear();
            Session["Pagina"] = 1;
            Preference newPreference = new Preference();
            newPreference.Prijsvolgorde = 2;
            newPreference.Minimumprijs = 0.00m;
            newPreference.Maximumprijs = 99999.99m;
            newPreference.minpSetByUser = false;
            newPreference.maxpSetByUser = false;
            newPreference.QueryString = strUrlName;
            Session["Preferences"] = newPreference;
            rbMeestBezocht.Checked = true;
            List<Product> ProductList = InitializeProducts(true);
            AddProductsToPage(ProductList);
            AddVoorkeurenToPage();
            GetMinMaxPrices();
            SetMinMaxPrices();
            loadPagesnav(intTotalProducts);
        }
        List<decimal> previousprices = new List<decimal>();
        previousprices.Add(Convert.ToDecimal(MinPrijs));
        previousprices.Add(Convert.ToDecimal(MaxPrijs));
        Session["previousprices"] = previousprices;
        Preference currentPreference = (Preference)Session["Preferences"];
        if (currentPreference.QueryString == string.Empty)
        {
            currentPreference.QueryString = string.Empty;
            Response.Redirect(Request.RawUrl.Replace(Request.Url.Query, ""));
        }
        Session["Preferences"] = currentPreference;
    }
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        { //Page reloads
            itemlijst.Controls.Clear();
            AddVoorkeurenToPage();
            //if (Session["previousprices"] != null)
            //{
            //    List<int> previousprices = (List<int>)Session["previousprices"];
            //    if (previousprices[0].ToString() != txtLaagstePrijs.Text)
            //    { //Laagste prijs is veranderd door gebruiker
            //        MinPriceChanged = true;
            //    }
            //    else
            //    {
            //        MinPriceChanged = false;
            //    }
            //    if (previousprices[1].ToString() != txtHoogstePrijs.Text)
            //    { //Hoogste prijs is veranderd door gebruiker
            //        MaxPriceChanged = true;
            //    }
            //    else
            //    {
            //        MaxPriceChanged = false;
            //    }
            //}
        }
        else
        { //Page does not reload
            
        }
    }
    private void ReloadPage()
    {
        UpdateVoorkeuren();
        SetSortValue();
        List<Product> ProductList = InitializeProducts(false);
        AddProductsToPage(ProductList);
        loadPagesnav(intTotalProducts);
    }
    private void AddVoorkeurenToPage()
    {
        Voorkeur aVoorkeur = (Voorkeur)Session["Voorkeuren"];
        Preference tempPreferenes = (Preference)Session["Preferences"];
        if (aVoorkeur != null)
        {
            if (aVoorkeur.merk.Count > 1)
            {
                HtmlGenericControl newH4 = new HtmlGenericControl("h4");
                newH4.InnerHtml = "Merken:";
                merken.Controls.Add(newH4);
                int intCounter = 0;
                foreach (string merk in aVoorkeur.merk)
                {
                    intCounter++;
                    HtmlGenericControl newLabel = new HtmlGenericControl("label");
                    newLabel.Attributes.Add("class", "voorkeursoort");
                    newLabel.ID = "merklabel" + intCounter.ToString();
                    CheckBox newcb = new CheckBox();
                    newcb.AutoPostBack = true;
                    newcb.ID = "merk" + intCounter.ToString();
                    //newcb.CheckedChanged += new EventHandler(EditPreference);
                    newcb.Text = merk;
                    if (tempPreferenes.Merken.Contains(merk))
                    {
                        newcb.Checked = true;
                    }
                    HtmlGenericControl newSpan = new HtmlGenericControl("span");
                    newSpan.Attributes.Add("class", "checkmark");
                    newLabel.Controls.Add(newcb);
                    newLabel.Controls.Add(newSpan);
                    merken.Controls.Add(newLabel);
                }
            }
            else
            {
                // merken.Visible = false;
            }
            if (aVoorkeur.geslacht.Count > 1)
            {
                HtmlGenericControl newH4 = new HtmlGenericControl("h4");
                newH4.InnerHtml = "Geslacht:";
                Geslachten.Controls.Add(newH4);
                int intCounter = 0;
                foreach (string geslacht in aVoorkeur.geslacht)
                {
                    intCounter++;
                    HtmlGenericControl newLabel = new HtmlGenericControl("label");
                    newLabel.Attributes.Add("class", "voorkeursoort");
                    newLabel.ID = "geslachtlabel" + intCounter.ToString();
                    CheckBox newcb = new CheckBox();
                    newcb.AutoPostBack = true;
                    newcb.ID = "geslacht" + intCounter.ToString();
                    //newcb.CheckedChanged += new EventHandler(EditPreference);
                    newcb.Text = geslacht;
                    string strTempGeslacht = string.Empty;
                    if (geslacht == "Vrouwen")
                    {
                        strTempGeslacht = "V";
                    }
                    else if (geslacht == "Mannen")
                    {
                        strTempGeslacht = "M";
                    }
                    else if (geslacht == "Kinderen")
                    {
                        strTempGeslacht = "K";
                    }
                    else
                    {
                        strTempGeslacht = "O";
                    }
                    if (tempPreferenes.Geslachten.Contains(strTempGeslacht))
                    {
                        newcb.Checked = true;
                    }
                    HtmlGenericControl newSpan = new HtmlGenericControl("span");
                    newSpan.Attributes.Add("class", "checkmark");
                    newLabel.Controls.Add(newcb);
                    newLabel.Controls.Add(newSpan);
                    Geslachten.Controls.Add(newLabel);
                }
            }
            else
            {
                Geslachten.Visible = false;
            }
            if (aVoorkeur.categorie.Count > 1)
            {
                HtmlGenericControl newH4 = new HtmlGenericControl("h4");
                newH4.InnerHtml = "Categorieën:";
                Categorieen.Controls.Add(newH4);
                int intCounter = 0;
                foreach (string categorie in aVoorkeur.categorie)
                {
                    intCounter++;
                    HtmlGenericControl newLabel = new HtmlGenericControl("label");
                    newLabel.Attributes.Add("class", "voorkeursoort");
                    newLabel.ID = "categorielabel" + intCounter.ToString();
                    CheckBox newcb = new CheckBox();
                    newcb.AutoPostBack = true;
                    newcb.ID = "categorie" + intCounter.ToString();
                    //newcb.CheckedChanged += new EventHandler(EditPreference);
                    newcb.Text = categorie;
                    if (tempPreferenes.Subcategorieen.Contains(categorie))
                    {
                        newcb.Checked = true;
                    }
                    HtmlGenericControl newSpan = new HtmlGenericControl("span");
                    newSpan.Attributes.Add("class", "checkmark");
                    newLabel.Controls.Add(newcb);
                    newLabel.Controls.Add(newSpan);
                    Categorieen.Controls.Add(newLabel);
                }
            }
            else
            {
                Categorieen.Visible = false;
            }
        }
        else
        {
            Geslachten.Visible = false;
            Categorieen.Visible = false;
            // merken.Visible = false;
        }
    }
    private void AddProductsToPage(List<Product> ProductList)
    {
        itemlijst.Controls.Clear();
        Klant aKlant = new Klant();
        bool Ingelogd = false;
        List<Favorieten> favorieten = new List<Favorieten>();
        if (Session["Gebruiker"] != null)
        {
            Ingelogd = true;
            aKlant = (Klant)Session["Gebruiker"];
            favorieten = GetAllFavorietenFromUser(aKlant.klantID);
        }
        int intHoeveelste = Convert.ToInt32(Session["Pagina"]) - 1;
        for (int intCounterP = 0; intCounterP < intProductsPerPage; intCounterP++)
        {
            if (ProductList.Count <= intCounterP + (intProductsPerPage * intHoeveelste))
            {
                break;
            }
            createProduct(ProductList[intCounterP + (intProductsPerPage * intHoeveelste)], Ingelogd, favorieten);
        }
    }
    private List<Product> InitializeProducts(bool InitializeNewProducts)
    {
        intTotalProducts = 0;
        List<Product> ProductList = new List<Product>();
        if (InitializeNewProducts)
        {
            Voorkeur Voorkeuren = new Voorkeur();
            string strCommand = "SELECT productID FROM tblproducten;";
            List<string> productIDList = database.readColumn(strCommand, "productID");
            foreach(string id in productIDList)
            {
                Product aProduct = database.readProduct(id);
                if (CheckAllPreferences(aProduct))
                {
                    string strTempGeslacht = string.Empty;
                    if (aProduct.geslacht == "V")
                    {
                        strTempGeslacht = "Vrouwen";
                    }
                    else if (aProduct.geslacht == "M")
                    {
                        strTempGeslacht = "Mannen";
                    }
                    else if (aProduct.geslacht == "K")
                    {
                        strTempGeslacht = "Kinderen";
                    }
                    else
                    {
                        strTempGeslacht = "Onzijdig";
                    }
                    if (!Voorkeuren.merk.Contains(aProduct.merk))
                    {
                        Voorkeuren.merk.Add(aProduct.merk);
                    }
                    if (!Voorkeuren.geslacht.Contains(strTempGeslacht))
                    {
                        
                        Voorkeuren.geslacht.Add(strTempGeslacht);
                    }
                    if (!Voorkeuren.categorie.Contains(aProduct.subcategorie))
                    {
                        Voorkeuren.categorie.Add(aProduct.subcategorie);
                    }
                    ProductList.Add(aProduct);
                    intTotalProducts++;
                }
            }
            Session["Voorkeuren"] = Voorkeuren;
            Session["OriginalProducts"] = ProductList;
        }
        else
        {
            List<Product> tempProductList = (List<Product>)Session["OriginalProducts"];
            foreach(Product aProduct in tempProductList)
            {
                if (CheckAllPreferences(aProduct))
                {
                    ProductList.Add(aProduct);
                    intTotalProducts++;
                }
            }
        }

        if (ProductList.Count == 0)
        {
            divNoItems.Visible = true;
        }
        else
        {
            divNoItems.Visible = false;
        }

        Preference tempPreference = (Preference)Session["Preferences"];
        if (tempPreference.Prijsvolgorde == 2)
        {
            //Sorteer op aantalbekeken dalend
            ProductList.Sort((p1, p2) => p1.aantalkeerbekeken.CompareTo(p2.aantalkeerbekeken));
            ProductList.Reverse();
        }
        if (tempPreference.Prijsvolgorde == 1)
        {
            //Sorteer op dalend
            ProductList.Sort((p1, p2) => p1.prijs.CompareTo(p2.prijs));
            ProductList.Reverse();
        }
        if (tempPreference.Prijsvolgorde == 0)
        {
            //Sorteer op stijgend
            ProductList.Sort((p1, p2) => p1.prijs.CompareTo(p2.prijs));
        }

        Session["CurrentProducts"] = ProductList;
        return ProductList;
    }
    private void SetSortValue()
    {
        Preference tempPreference = (Preference)Session["Preferences"];
        if (rbStijgend.Checked && tempPreference.Prijsvolgorde != 0)
        {
            tempPreference.Prijsvolgorde = 0;
            rbDalend.Checked = false;
            rbMeestBezocht.Checked = false;
            rbStijgend.Checked = true;
        }
        else if (rbDalend.Checked && tempPreference.Prijsvolgorde != 1)
        {
            tempPreference.Prijsvolgorde = 1;
            rbMeestBezocht.Checked = false;
            rbStijgend.Checked = false;
            rbDalend.Checked = true;
        }
        else if (rbMeestBezocht.Checked && tempPreference.Prijsvolgorde != 2)
        {
            tempPreference.Prijsvolgorde = 2;
            rbStijgend.Checked = false;
            rbDalend.Checked = false;
            rbMeestBezocht.Checked = true;
        }
        Session["Preferences"] = tempPreference;
    }
    private void CheckIfMinMaxPriceIsSetByUser()
    {
        txtLaagstePrijs.Text = txtLaagstePrijs.Text.Replace(".", ",");
        txtHoogstePrijs.Text = txtHoogstePrijs.Text.Replace(".", ",");
        if (!txtLaagstePrijs.Text.Contains(","))
        {
            txtLaagstePrijs.Text = txtLaagstePrijs.Text + ",00";
        }
        if (!txtHoogstePrijs.Text.Contains(","))
        {
            txtHoogstePrijs.Text = txtHoogstePrijs.Text + ",00";
        }
        Preference tempPreference = (Preference)Session["Preferences"];
        Voorkeur tempVoorkeur = (Voorkeur)Session["Voorkeuren"];
        List<string> previousprices = (List<string>)Session["PreviousPrices"];
        if (txtLaagstePrijs.Text != previousprices[0])
        {
            tempPreference.minpSetByUser = true;
            tempPreference.Minimumprijs = Convert.ToDecimal(txtLaagstePrijs.Text);
        }
        if (txtHoogstePrijs.Text != previousprices[1])
        {
            tempPreference.maxpSetByUser = true;
            tempPreference.Maximumprijs = Convert.ToDecimal(txtHoogstePrijs.Text);
        }
        Session["Preferences"] = tempPreference;
    }
    private void UpdateVoorkeuren()
    {
        List<string> MerkenLijst = new List<string>();
        List<string> SubcategorieLijst = new List<string>();
        List<string> GeslachtenLijst = new List<string>();
        Preference tempPreference = (Preference)Session["Preferences"];
        Preference originalPreference = tempPreference;
        Voorkeur aVoorkeur = (Voorkeur)Session["Voorkeuren"];
        int intCounter = 1;
        foreach(Control x in merken.Controls)
        {
            if (x.ID == "merklabel" + intCounter.ToString())
            {
                foreach (Control y in x.Controls)
                {
                    if (y.GetType() == typeof(CheckBox))
                    {
                        if ((y as CheckBox).Checked)
                        {
                            MerkenLijst.Add((y as CheckBox).Text);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                intCounter++;
            }
        }
        intCounter = 1;
        foreach(Control x in Categorieen.Controls)
        {
            if (x.ID == "categorielabel" + intCounter.ToString())
            {
                foreach (Control y in x.Controls)
                {
                    if (y.GetType() == typeof(CheckBox))
                    {
                        if ((y as CheckBox).Checked)
                        {
                            SubcategorieLijst.Add((y as CheckBox).Text);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                intCounter++;
            }
        }
        intCounter = 1;
        foreach(Control x in Geslachten.Controls)
        {
            if (x.ID == "geslachtlabel" + intCounter.ToString())
            {
                foreach (Control y in x.Controls)
                {
                    if (y.GetType() == typeof(CheckBox))
                    {
                        if ((y as CheckBox).Checked)
                        {
                            string strTempGeslacht = string.Empty;
                            if ((y as CheckBox).Text == "Vrouwen")
                            {
                                strTempGeslacht = "V";
                            }
                            else if ((y as CheckBox).Text == "Mannen")
                            {
                                strTempGeslacht = "M";
                            }
                            else if ((y as CheckBox).Text == "Kinderen")
                            {
                                strTempGeslacht = "K";
                            }
                            else
                            {
                                strTempGeslacht = "O";
                            }
                            GeslachtenLijst.Add(strTempGeslacht);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                intCounter++;
            }
        }
        tempPreference.Merken = MerkenLijst;
        tempPreference.Geslachten = GeslachtenLijst;
        tempPreference.Subcategorieen = SubcategorieLijst;
        if (originalPreference != tempPreference)
        {
            Session["Pagina"] = 0;
        }
        Session["Preferences"] = tempPreference;
    }
    private bool CheckAllPreferences(Product aProduct)
    {
        Preference tempPreference = (Preference)Session["Preferences"];
        bool GoedMerk = false;
        bool GoedHoefdcategorie = false;
        bool GoedSubcategorie = false;
        bool GoedGeslacht = false;
        bool GoedProductnaam = false;
        bool PrijsHogerDanMinimum = false;
        bool PrijsLagerDanMaximum = false;
        bool noPreference = true;
        if (tempPreference.Merken.Count > 0)
        {
            noPreference = false;
            foreach (string x in tempPreference.Merken)
            {
                if (tempPreference.QueryString != "" && tempPreference.QueryString != null)
                {
                    if (CheckIfStringContainsAString(x.ToLower(), tempPreference.QueryString.ToLower()))
                    {
                        GoedMerk = true;
                        break;
                    }
                }
                if (CheckIfStringContainsAString(aProduct.merk.ToLower(), x.ToLower()))
                {
                    GoedMerk = true;
                    break;
                }
                if (x == tempPreference.Merken[tempPreference.Merken.Count - 1])
                {
                    GoedMerk = false;
                }
            }
        }
        if (tempPreference.Geslachten.Count > 0)
        {
            noPreference = false;
            foreach (string x in tempPreference.Geslachten)
            {
                if (tempPreference.QueryString != "" && tempPreference.QueryString != null)
                {
                    if (CheckIfStringContainsAString(x.ToLower(), tempPreference.QueryString.ToLower()))
                    {
                        GoedGeslacht = true;
                        break;
                    }
                }
                if (CheckIfStringContainsAString(aProduct.geslacht.ToLower(), x.ToLower()))
                {
                    GoedGeslacht = true;
                    break;
                }
                if (x == tempPreference.Geslachten[tempPreference.Geslachten.Count - 1])
                {
                    GoedGeslacht = false;
                }
            }
        }
        if (tempPreference.Subcategorieen.Count > 0)
        {
            noPreference = false;
            foreach (string x in tempPreference.Subcategorieen)
            {
                if (tempPreference.QueryString != "" && tempPreference.QueryString != null)
                {
                    if (CheckIfStringContainsAString(x.ToLower(), tempPreference.QueryString.ToLower()))
                    {
                        GoedSubcategorie = true;
                        break;
                    }
                }
                if (CheckIfStringContainsAString(aProduct.subcategorie.ToLower(), x.ToLower()))
                {
                    GoedSubcategorie = true;
                    break;
                }
                if (x == tempPreference.Subcategorieen[tempPreference.Subcategorieen.Count - 1])
                {
                    GoedSubcategorie = false;
                }
            }
        }
        if (tempPreference.Hoofdcategorie != "" && tempPreference.Hoofdcategorie != null)
        {
            noPreference = false;
            if (tempPreference.QueryString != "" && tempPreference.QueryString != null)
            {
                if (CheckIfStringContainsAString(aProduct.hoofdcategorie.ToLower(), tempPreference.QueryString.ToLower()))
                {
                    GoedHoefdcategorie = true;
                }
                if (CheckIfStringContainsAString(aProduct.hoofdcategorie.ToLower(), tempPreference.Hoofdcategorie.ToLower()))
                {
                    GoedHoefdcategorie = true;
                }
            }
        }
        if (tempPreference.Minimumprijs <= aProduct.prijs || !tempPreference.minpSetByUser)
        {
            PrijsHogerDanMinimum = true;
        }
        if (tempPreference.Maximumprijs >= aProduct.prijs || !tempPreference.maxpSetByUser)
        {
            PrijsLagerDanMaximum = true;
        }

        if (noPreference)
        {
            if (tempPreference.QueryString != "" && tempPreference.QueryString != null)
            {
                if (CheckIfStringContainsAString(aProduct.hoofdcategorie.ToLower(), tempPreference.QueryString.ToLower()))
                {
                    GoedHoefdcategorie = true;
                }
                if (CheckIfStringContainsAString(aProduct.subcategorie.ToLower(), tempPreference.QueryString.ToLower()))
                {
                    GoedSubcategorie = true;
                }
                if (CheckIfStringContainsAString(aProduct.productnaam.ToLower(), tempPreference.QueryString.ToLower()))
                {
                    GoedProductnaam = true;
                }
                if (CheckIfStringContainsAString(aProduct.merk.ToLower(), tempPreference.QueryString.ToLower()))
                {
                    GoedMerk = true;
                }
                if (CheckIfStringContainsAString(aProduct.geslacht.ToLower(), tempPreference.QueryString.ToLower()))
                {
                    GoedGeslacht = true;
                }
            }
            else
            {
                GoedHoefdcategorie = true;
                GoedSubcategorie = true;
                GoedProductnaam = true;
                GoedMerk = true;
                GoedGeslacht = true;
            }
        }
        else
        {
            if (tempPreference.Merken.Count == 0)
            {
                GoedMerk = true;
            }
            if (tempPreference.Geslachten.Count == 0)
            {
                GoedGeslacht = true;
            }
            if (tempPreference.Subcategorieen.Count == 0)
            {
                GoedSubcategorie = true;
            }
            if (tempPreference.Hoofdcategorie == null || tempPreference.Hoofdcategorie == "")
            {
                GoedHoefdcategorie = true;
            }
        }

        //Return of het product bij de lijst mag of niet
        if (!noPreference)
        {
            if (GoedMerk && GoedGeslacht && GoedSubcategorie && GoedHoefdcategorie && PrijsHogerDanMinimum && PrijsLagerDanMaximum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (tempPreference.QueryString != "" && tempPreference.QueryString != null)
            {
                if (GoedHoefdcategorie || GoedGeslacht || GoedMerk || GoedProductnaam || GoedSubcategorie)
                {
                    if (PrijsHogerDanMinimum && PrijsLagerDanMaximum)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
    private bool CheckIfStringContainsAString(string strBig, string strStringwithin)
    {
        string[] entries = strBig.Split(new[] { strStringwithin }, StringSplitOptions.None);
        if (entries.Length > 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void btnNextPage_Click(object sender, EventArgs e)
    {
        int intPageNumber = Convert.ToInt32(Session["Pagina"]);
        int intAantalPaginas = Convert.ToInt32(Session["AantalPaginas"]);
        if (intPageNumber < intAantalPaginas)
        {
            intPageNumber++;
        }
        loadPage(intPageNumber);
    }
    protected void btnPreviousPage_Click(object sender, EventArgs e)
    {
        int intPageNumber = Convert.ToInt32(Session["Pagina"]);
        if (intPageNumber > 1)
        {
            intPageNumber--;
        }
        loadPage(intPageNumber);
    }
    private void createProduct(Product aProduct, bool Ingelogd, List<Favorieten> favorieten)
    {
        List<string> dafavorieten = new List<string>();
        if (favorieten != null)
        {
            foreach (Favorieten f in favorieten)
            {
                dafavorieten.Add(f.oldproductID.ToString());
            }
        }
        HtmlGenericControl mainDiv = new HtmlGenericControl("div");
        mainDiv.Attributes.Add("class", "itemdiv");

        if (aProduct.afbeelding == "0")
        {
            aProduct.afbeelding = "no_image_found.png";
        }
        ImageButton newImage = new ImageButton();
        newImage.ImageUrl = "images/producten/" + aProduct.afbeelding;
        newImage.ID = "imgProductX" + Convert.ToString(aProduct.oldproductID);
        newImage.Click += new ImageClickEventHandler(GoToProductPage);

        HtmlGenericControl newH4 = new HtmlGenericControl("h4");
        newH4.InnerHtml = aProduct.merk;

        Button newSpan1 = new Button();
        newSpan1.CssClass = "productnaam";
        newSpan1.Text = aProduct.productnaam;
        newSpan1.ID = "naamProductX" + Convert.ToString(aProduct.oldproductID);
        newSpan1.Click += new EventHandler(GoToProductPage);

        HtmlGenericControl newSpan2 = new HtmlGenericControl("span");
        newSpan2.Attributes.Add("class", "prijs");
        newSpan2.InnerHtml = aProduct.prijs.ToString();

        HtmlGenericControl newDiv = new HtmlGenericControl("div");

        Button newButton1 = new Button();
        newButton1.ID = "winkelkar" + Convert.ToString(aProduct.oldproductID);
        if (Ingelogd)
        {
            newButton1.Enabled = true;
            newButton1.Click += new EventHandler(AddToWinkelwagen);
        }
        else
        {
            newButton1.Enabled = false;
        }
        newButton1.CssClass = "btnwinkelwagen";

        Button newButton2 = new Button();
        newButton2.ID = "favoriet" + Convert.ToString(aProduct.oldproductID);
        if (Ingelogd)
        {
            newButton2.Enabled = true;
            newButton2.Click += new EventHandler(AddToFavorieten);
            if (dafavorieten.Contains(aProduct.oldproductID.ToString()))
            {
                newButton2.Style.Add("background-image", "images/favorite.png");
            }
            else
            {
                newButton2.Style.Add("background-image", "images/favorite_not.png");
            }
        }
        else
        {
            newButton2.Enabled = false;
            newButton2.Style.Add("background-image", "images/favorite_not.png");
        }
        newButton2.CssClass = "btnfavoriet";

        newDiv.Controls.Add(newButton1);
        newDiv.Controls.Add(newButton2);


        mainDiv.Controls.Add(newImage);
        mainDiv.Controls.Add(newH4);
        mainDiv.Controls.Add(newSpan1);
        mainDiv.Controls.Add(newSpan2);
        mainDiv.Controls.Add(newDiv);

        //Voeg het toe aan de pagina
        itemlijst.Controls.Add(mainDiv);
    }
    private void loadPagesnav(int intAmountOfProducts)
    {
        int intAantalProducten = intAmountOfProducts;

        //Stel de paginas in
        int intAmountOfPages = 1;
        int intTempAmount = intAantalProducten;
        while (intTempAmount > intProductsPerPage)
        {
            intAmountOfPages++;
            intTempAmount = intTempAmount - intProductsPerPage;
        }
        addPages(intAmountOfPages);
        Session["AantalPaginas"] = intAmountOfPages;
    }
    private void addPages(int intAmountOfPages)
    {
        currentPage.Text = Session["Pagina"].ToString();
        totalPages.Text = intAmountOfPages.ToString();
    }
    private void loadPage(int intNumber)
    {
        Session["Pagina"] = intNumber;
        currentPage.Text = intNumber.ToString();
        totalPages.Text = Convert.ToString(Session["AantalPaginas"]);

        List<Product> ProductList = InitializeProducts(false);
        AddProductsToPage(ProductList);
        //Laad de pagina met deze paginannummer in
        //Session["Producten"] = loadProducts();
        //UpdateMinMaxPrices();
    }
    private void GetMinMaxPrices()
    {
        string strHoogstePrijs = string.Empty;
        string strLaagstePrijs = string.Empty;
        List<decimal> prijzen = new List<decimal>();
        int inttemptotaalproducten = 0;
        foreach (Product product in (List<Product>)Session["OriginalProducts"])
        {
            prijzen.Add(Convert.ToDecimal(product.prijs));
            inttemptotaalproducten++;
        }

        //tempvalues
        decimal laagstep;
        decimal hoogstep;
        if (prijzen.Count != 0)
        {
            laagstep = prijzen[0];
            hoogstep = prijzen[prijzen.Count - 1];
        }
        else
        {
            laagstep = 0;
            hoogstep = 9999999;
        }

        if (prijzen.Count != 0)
        {
            prijzen.Sort();
            if (!MaxPriceChanged)
            {
                string[] entries = Convert.ToString(prijzen[prijzen.Count - 1]).Split(',');
                strHoogstePrijs = entries[0];
            }
            else
            {
                strHoogstePrijs = txtHoogstePrijs.Text;
            }
            if (!MinPriceChanged)
            {
                string[] entries = Convert.ToString(prijzen[0]).Split(',');
                strLaagstePrijs = entries[0];
            }
            else
            {
                strLaagstePrijs = txtLaagstePrijs.Text;
            }
            prijzen.Clear();
        }
        else
        {
            strLaagstePrijs = "0";
            strHoogstePrijs = "99999";
        }
        Voorkeur tempVoorkeur = (Voorkeur)Session["Voorkeuren"];
        Preference tempPreference = (Preference)Session["Preferences"];
        if (!tempPreference.minpSetByUser)
        { //minimumprijs niet aangepast door gebruiker
            tempVoorkeur.Minimumprijs = Convert.ToDecimal(strLaagstePrijs);
            MinPrijs = strLaagstePrijs;
        }
        else
        {
            tempVoorkeur.Minimumprijs = tempPreference.Minimumprijs;
            MinPrijs = Convert.ToString(tempPreference.Minimumprijs);
        }
        if (!tempPreference.maxpSetByUser)
        { //maximumprijs niet aangepast door gebruiker
            MaxPrijs = strHoogstePrijs;
            tempVoorkeur.Maximumprijs = Convert.ToDecimal(strHoogstePrijs);
        }
        else
        {
            tempVoorkeur.Maximumprijs = tempPreference.Maximumprijs;
            MaxPrijs = Convert.ToString(tempPreference.Maximumprijs);
        }
        Session["Voorkeuren"] = tempVoorkeur;
    }
    private void SetMinMaxPrices()
    {
        Voorkeur aVoorkeur = (Voorkeur)Session["Voorkeuren"];
        Preference tempPreferenes = (Preference)Session["Preferences"];
        if (tempPreferenes.maxpSetByUser)
        {
            txtHoogstePrijs.Text = Convert.ToString(tempPreferenes.Maximumprijs);
        }
        else
        {
            if (tempPreferenes.minpSetByUser)
            {
                if (tempPreferenes.Minimumprijs > aVoorkeur.Maximumprijs)
                {
                    aVoorkeur.Maximumprijs = tempPreferenes.Minimumprijs;
                    txtHoogstePrijs.Text = aVoorkeur.Maximumprijs.ToString();
                }
            }
            else
            {
                txtHoogstePrijs.Text = aVoorkeur.Maximumprijs.ToString();
            }
        }
        if (tempPreferenes.minpSetByUser)
        {
            txtLaagstePrijs.Text = Convert.ToString(tempPreferenes.Minimumprijs);
        }
        else
        {
            if (tempPreferenes.maxpSetByUser)
            {
                if (tempPreferenes.Maximumprijs < aVoorkeur.Minimumprijs)
                {
                    aVoorkeur.Minimumprijs = tempPreferenes.Maximumprijs;
                    txtLaagstePrijs.Text = aVoorkeur.Minimumprijs.ToString();
                }
            }
            else
            {
                txtLaagstePrijs.Text = aVoorkeur.Minimumprijs.ToString();
            }
        }
        Session["Voorkeuren"] = aVoorkeur;
    }
    protected void btnSetLaagstePrijs_Click(object sender, EventArgs e)
    {
        Preference tempPreference = (Preference)Session["Preferences"];
        tempPreference.minpSetByUser = true;
        tempPreference.Minimumprijs = Convert.ToDecimal(txtLaagstePrijs.Text);
        Session["Preferences"] = tempPreference;
        ReloadPage();
    }
    protected void btnSetHoogstePrijs_Click(object sender, EventArgs e)
    {
        Preference tempPreference = (Preference)Session["Preferences"];
        tempPreference.maxpSetByUser = true;
        tempPreference.Maximumprijs = Convert.ToDecimal(txtHoogstePrijs.Text);
        Session["Preferences"] = tempPreference;
        ReloadPage();
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
        List<int> ProductID = new List<int>();
        ProductID.Add(Convert.ToInt32(entries[entries.Length - 1]));
        ProductID.Add(0);
        Session["ProductID"] = ProductID;
        Response.Redirect("ProductPage.aspx");
    }
    private List<Favorieten> GetAllFavorietenFromUser(string strKlantID)
    {
        return database.readFavorieten(strKlantID);
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

        //ReloadPage();
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

        ReloadPage();
        //Response.Redirect(Request.RawUrl);
    }
    //private Control GetControlThatCausedPostBack(Page page)
    //{
    //    //initialize a control and set it to null
    //    Control ctrl = null;

    //    //get the event target name and find the control
    //    string ctrlName = page.Request.Params.Get("__EVENTTARGET");
    //    if (!String.IsNullOrEmpty(ctrlName))
    //        ctrl = page.FindControl(ctrlName);

    //    //return the control to the calling method
    //    return ctrl;
    //}
}