using System;

namespace MyWPFApp.Model;

[Serializable]
public class TradeVault
{
    public string Action { get; set; }
    public string Vault { get; set; }
    public double Coin { get; set; }
    public DateTime Date { get; set; }

    public TradeVault()
    {
        Action = "";
        Vault = "";
        Coin = 0;
        Date = DateTime.MinValue;
    }
    public TradeVault(string vault,double coin,DateTime date,string action)
    {
        Vault = vault;
        Coin = coin;
        Date = date;
        Action = action;
    }

}