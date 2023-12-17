using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    int intAantalMeestBekeken = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        CreateMeestBekeken(GetMeestBekeken());
        CreateCategorieen(GetCategorieen());
    }
    private List<Product> GetMeestBekeken()
    {
        return database.readRowsProducten(intAantalMeestBekeken, "aantalkeerbekeken");
    }
    private void CreateMeestBekeken(List<Product> meestBekekenProducten)
    {
        mbekekenproducten.Controls.Clear();
        foreach (Product product in meestBekekenProducten)
        {
            HtmlGenericControl newDiv = new HtmlGenericControl("duv");
            newDiv.Attributes.Add("class", "mbekeken");
            ImageButton newImg = new ImageButton();
            if (product.afbeelding != "0")
            {
                newImg.ImageUrl = product.afbeelding;
            }
            else
            {
                newImg.ImageUrl = "images/producten/no_image_found.png";
            }
            newImg.ID = "afbeelding" + product.oldproductID.ToString();
            newImg.Click += new ImageClickEventHandler(GoToProductPage);
            HtmlGenericControl newH2 = new HtmlGenericControl("h2");
            newH2.InnerHtml = product.prijs.ToString();
            HtmlGenericControl newSpan = new HtmlGenericControl("span");
            HtmlGenericControl newSpan1 = new HtmlGenericControl("span");
            newSpan1.InnerHtml = product.merk;
            Button newSpan2 = new Button();
            newSpan2.Text = product.productnaam;
            newSpan2.ID = "naam" + product.oldproductID.ToString();
            newSpan2.Click += new EventHandler(GoProductPage);

            newDiv.Controls.Add(newImg);
            newDiv.Controls.Add(newH2);
            newSpan.Controls.Add(newSpan1);
            newSpan.Controls.Add(newSpan2);
            newDiv.Controls.Add(newSpan);
            mbekekenproducten.Controls.Add(newDiv);
        }
    }
    private List<string> GetCategorieen()
    {
        string strCommand = "SELECT hoofdcategorie FROM tblproducten;";
        List<string> productIDList = database.readColumn(strCommand, "categorie");
        return productIDList.Distinct().ToList();

    }
    private void CreateCategorieen(List<string> categorieen)
    {
        foreach (string categorie in categorieen)
        {
            Button newButton = new Button();
            newButton.Attributes.Add("class", "categoriediv");
            newButton.Text = categorie;
            newButton.Click += new EventHandler(SendCategorie);
            hoofdcategorieen.Controls.Add(newButton);
        }
    }
    private void SendCategorie(object sender, EventArgs e)
    {
        string strTemp = (sender as Button).Text;
        Response.Redirect("shop.aspx?Name=" + strTemp);
    }
    private void GoProductPage(object sender, EventArgs e)
    {
        string[] entries = new string[5];
        try
        {
            entries = (sender as Control).ID.Split(new string[] { "naam" }, StringSplitOptions.None);
        }
        catch
        {
        }
        List<int> OldroductID = new List<int>();
        OldroductID.Add(Convert.ToInt32(entries[entries.Length - 1]));
        OldroductID.Add(0);
        Session["ProductID"] = OldroductID;
        Response.Redirect("ProductPage.aspx");
    }
    private void GoToProductPage(object sender, EventArgs e)
    {
        string[] entries = new string[5];
        try
        {
            entries = (sender as Control).ID.Split(new string[] { "afbeelding" }, StringSplitOptions.None);
        }
        catch
        {
        }
        List<int> OldroductID = new List<int>();
        OldroductID.Add(Convert.ToInt32(entries[entries.Length - 1]));
        OldroductID.Add(0);
        Session["ProductID"] = OldroductID;
        Response.Redirect("ProductPage.aspx");
    }
}