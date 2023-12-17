using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for Product
/// </summary>
public class Product
{
    public int productID { get; set; }
    public string productnaam { get; set; }
    public string merk { get; set; }
    public decimal prijs { get; set; }
    public string subcategorie { get; set; }
    public string geslacht { get; set; }
    public string afbeelding { get; set; }
    public int aantalkeerbekeken { get; set; }
    public string hoofdcategorie { get; set; }
    public string Beschrijving { get; set; }
    public int oldproductID { get; set; }
}