using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProductPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["ProductID"] != null)
        {
            Product aProduct = new Product();
            List<int> tempProduct = (List<int>)Session["ProductID"];
            try
            {

                aProduct = database.readProductbyOldproductID(tempProduct[0].ToString());
            }
            catch
            {
            }
            if (aProduct != null)
            { //Er is een product gekozen en pagina mag geladen worden
                InitializeProductData(aProduct);
                if (Session["Gebruiker"] != null)
                { //Gebruiker ingelogd
                    btnFavoriet.Enabled = true;
                    btnInWinkelwagen.Enabled = true;
                    //Controleer of gebruiker het product bij favorieten heeft
                }
                else
                { //Gebruiker NIET ingelogd
                    btnFavoriet.Enabled = false;
                    btnInWinkelwagen.Enabled = false;
                }
                if (tempProduct.Count == 2)
                {
                    if (tempProduct[1] == 0)
                    {
                        try
                        {
                            database.editProduct(aProduct.productID.ToString(), (aProduct.aantalkeerbekeken + 1).ToString(), "aantalkeerbekeken");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        tempProduct[1] = 1;
                        Session["ProductID"] = tempProduct;
                    }
                }
                
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    private void InitializeProductData(Product aProduct)
    {
        if (aProduct != null)
        {
            lblProductnaam.Text = aProduct.productnaam;
            lblMerk.Text = aProduct.merk;
            lblPrijs.Text = "€ " + aProduct.prijs.ToString();
            if (aProduct.afbeelding == "0")
            {
                aProduct.afbeelding = "no_image_found.png";
            }
            imgproduct.ImageUrl = "images/producten/" + aProduct.afbeelding;
            if (aProduct.Beschrijving != string.Empty)
            {
                pproduct.InnerText = aProduct.Beschrijving;
            }
            else
            {
                pproduct.InnerText = string.Empty;
            }
            if (Session["Gebruiker"] != null)
            {
                Klant aKlant = (Klant)Session["Gebruiker"];
                List<Favorieten> favorietenlijst = database.readFavorieten(aKlant.klantID);
                bool containsitem = false;
                if (favorietenlijst != null)
                {
                    foreach (Favorieten afavoriet in favorietenlijst)
                    {
                        if (afavoriet.oldproductID == aProduct.oldproductID)
                        {
                            containsitem = true;
                        }
                    }
                }
                if (containsitem)
                {
                    btnFavoriet.Style.Add("background-image", "url('../images/favorite.png')");
                }
                else
                {
                    btnFavoriet.Style.Add("background-image", "url('../images/favorite_not.png')");
                }
            }
            btnFavoriet.ID = "btnFavoriet" + aProduct.oldproductID.ToString();
            btnInWinkelwagen.ID = "btnInWinkelwagen" + aProduct.oldproductID.ToString();
        }
        else
        {
            //Response.Redirect("Default.aspx");
        }
    }
    protected void AddToWinkelwagen(object sender, EventArgs e)
    {
        Klant aKlant = (Klant)Session["Gebruiker"];
        string[] entries = (sender as Button).ID.Split(new string[] { "btnInWinkelwagen" }, StringSplitOptions.None);
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
        string[] entries = (sender as Button).ID.Split(new string[] { "btnFavoriet" }, StringSplitOptions.None);
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
}