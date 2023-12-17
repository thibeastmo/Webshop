using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Overeenkomst
/// </summary>
public class Overeenkomst
{
    public int bestellingID { get; set; }
    public string klantID { get; set; }
    public string datum { get; set; }
    public int betaald { get; set; }
    public int bezorgd { get; set; }
    public List<Bestelling> Bestellingproducten { get; set; }
    public Overeenkomst()
    {
        Bestellingproducten = new List<Bestelling>();
    }
}