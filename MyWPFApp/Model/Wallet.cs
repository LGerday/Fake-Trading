using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Xml.Serialization;
using Excel = Microsoft.Office.Interop.Excel;

namespace MyWPFApp.Model
{
    [Serializable]
    public class Wallet  : INotifyPropertyChanged
    {

        private double _amount;
        public ObservableCollection<TradeSpot> TradeSpots { get; set; }

        public ObservableCollection<TradeSpot> TradesSpotsHistory { get; set; }
        public ObservableCollection<TradeFuture> TradeFuture { get; set; }
        public ObservableCollection<TradeFuture> TradesFutureHistory { get; set; }

        public Wallet()
        {
            TradeFuture = new ObservableCollection<TradeFuture>();
            TradeSpots = new ObservableCollection<TradeSpot>();
            TradesSpotsHistory = new ObservableCollection<TradeSpot>();
            TradesFutureHistory = new ObservableCollection<TradeFuture>();
        }
        public Wallet(float amount)
        {
            Amount = amount;
            TradeSpots = new ObservableCollection<TradeSpot>();
            TradesSpotsHistory = new ObservableCollection<TradeSpot>();
            TradeSpots.Add(new TradeSpot("Bitcoin", 0, 0, 0,"Wallet"));
            TradeSpots.Add(new TradeSpot("Ethereum", 0, 0, 0, "Wallet"));
            TradeSpots.Add(new TradeSpot("BNB", 0, 0, 0, "Wallet"));
            TradeSpots.Add(new TradeSpot("Solana", 0, 0, 0, "Wallet"));
            TradeSpots.Add(new TradeSpot("XRP", 0, 0, 0, "Wallet"));
            TradeSpots.Add(new TradeSpot("Avax", 0, 0, 0, "Wallet"));
            TradeSpots.Add(new TradeSpot("Fantom", 0, 0, 0, "Wallet"));
            TradeFuture = new ObservableCollection<TradeFuture>();
            TradesFutureHistory = new ObservableCollection<TradeFuture>();
        }
        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                PropertyChangedEventHandler("TotalAmount");
                PropertyChangedEventHandler();
            }
        }

        public double TotalAmount => Amount + GetAllValueCrypto() + GetAllValueFuture();


        public void ExportSpotHistory(string path)
        {
            if(TradesSpotsHistory.Count == 0) return;
            Excel.Application? myApp = null; 
            Excel.Workbook? myBook = null;
            Excel.Worksheet? mySheet = null;
            myApp = new Excel.Application
            {
                Visible = false
            };
            myBook = myApp.Workbooks.Add();
            mySheet = (Excel.Worksheet)myBook.Sheets[1]; // Première feuille
            mySheet.Cells.ClearContents();
            mySheet.Cells[1, 1] = "Action";
            mySheet.Cells[1, 2] = "Type";
            mySheet.Cells[1, 3] = "Date";
            mySheet.Cells[1, 4] = "Entry Price";
            mySheet.Cells[1, 5] = "Shares";
            mySheet.Cells[1, 6] = "Amount";
            int i = 2;
            foreach (TradeSpot trade in TradesSpotsHistory)
            {
                mySheet.Cells[i, 1] = trade.Action;
                mySheet.Cells[i, 2] = trade.Type;
                mySheet.Cells[i, 3] = trade.ActionDate.ToString();
                mySheet.Cells[i, 4] = trade.EntryPrice;
                mySheet.Cells[i, 5] = trade.Shares;
                mySheet.Cells[i, 6] = trade.Amount;
                i++;
            }
            string filename = @"\SpotHistory"+ TradesSpotsHistory[0].ActionDate.Month+"-"+TradesSpotsHistory[0].ActionDate.Year;
            string test = path + filename;
            try
            {
                myApp.DisplayAlerts = false; // Cache les messages envoyés par excel
                myBook.SaveAs(test); // Enregistrement du fichier
                myApp.DisplayAlerts = true;
                myBook.Close(); // Fermeture de l'application
            }
            catch (Exception e)
            {
                MessageBox.Show("Save file error : " + e.Message);
            }
            finally
            {
                myApp.Quit(); // Fermeture de Excel
            }
            TradesSpotsHistory.Clear();

        }
        public void RefreshAllPnlSpot(ObservableCollection<Cryptocurrency> crypto)
        {
            int i = 0;
            foreach (var trade in TradeSpots)
            {
                trade.Pnl = (trade.Shares * crypto[i].Price) - (trade.Shares * trade.EntryPrice);
                i += 1;
            }
        }

        public double GetAllValueFuture()
        {
            double amt = 0;
            foreach (var trade in TradeFuture)
            {
                amt += trade.Amount;
            }

            return amt;
        }
        public double GetAllValueCrypto()
        {
            double amt = 0;
            foreach (var trade in TradeSpots)
            {
                amt += trade.Amount + trade.Pnl;
            }
            return amt;
        }
        public void Serializable(string filename)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(Wallet));
            using (Stream fStream = new FileStream(filename,
                       FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, this);
            }
        }

        public void Deserializable(string filename)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(Wallet));
            using (Stream fStream = File.OpenRead(filename))
            {
                Wallet tempWallet = (Wallet)xmlFormat.Deserialize(fStream);
                if(tempWallet != null)
                {
                    this.Amount = tempWallet.Amount;
                    TradeSpots = tempWallet.TradeSpots;
                    TradesSpotsHistory = tempWallet.TradesSpotsHistory;
                    TradeFuture = tempWallet.TradeFuture;
                    TradesFutureHistory = tempWallet.TradesFutureHistory;
                }
            }
        }
        public void SellTradeSpot(TradeSpot trade, int crypto,string filename)
        {
            trade.EntryPrice = TradeSpots[crypto].EntryPrice;
            Amount += (trade.Shares * trade.ExitPrice);
            TradeSpots[crypto].Shares -= trade.Shares;
            TradeSpots[crypto].Amount -= trade.Shares * TradeSpots[crypto].EntryPrice;
            if (TradeSpots[crypto].Shares == 0)
                TradeSpots[crypto].EntryPrice = 0;
            trade.Pnl = (trade.Shares * trade.ExitPrice) - (trade.Shares * TradeSpots[crypto].EntryPrice);
            trade.ActionDate = DateTime.Now;
            TradesSpotsHistory.Add(trade);
            PropertyChangedEventHandler("TotalAmount");
        }
        public void AddTradeSpot(TradeSpot trade,int crypto,string filename)
        {
            trade.Amount = trade.Shares;
            trade.Shares /= trade.EntryPrice;
            Amount -= trade.Amount;
            trade.ActionDate = DateTime.Now;
            TradesSpotsHistory.Add(trade);
            if (TradeSpots[crypto].EntryPrice == 0)
            {
                TradeSpots[crypto].EntryPrice = trade.EntryPrice;
                TradeSpots[crypto].Amount = trade.Amount;
                TradeSpots[crypto].Shares = trade.Shares;
            }
            else
            {
                TradeSpots[crypto].EntryPrice = ((TradeSpots[crypto].EntryPrice * TradeSpots[crypto].Shares) + (trade.EntryPrice * trade.Shares)) / (TradeSpots[crypto].Shares + trade.Shares);
                TradeSpots[crypto].Amount = (TradeSpots[crypto].Shares+trade.Shares) * TradeSpots[crypto].EntryPrice;
                TradeSpots[crypto].Shares += trade.Shares;
            }
            PropertyChangedEventHandler("TotalAmount");
        }
        public void AddTradeFuture(TradeFuture trade,string filename)
        {
            Amount = _amount - trade.Amount;
            TradeFuture.Add(trade);
            TradesFutureHistory.Add(trade);
            PropertyChangedEventHandler("TotalAmount");
        }

        public void SellTradeFuture(TradeFuture trade,int pourcent,string filename,double exitPrice)
        {

            int nb = TradeFuture.IndexOf(trade);
            TradeFuture temp = new TradeFuture();
            temp.Leverage = trade.Leverage;
            temp.ExitPrice = exitPrice;
            temp.EntryPrice = trade.EntryPrice;

            if (nb != -1)
            {
                if(trade.Type == "Long")
                {
                    if (pourcent == 100)
                    {
                        TradeFuture.Remove(trade);
                        trade.ExitPrice = exitPrice;
                        trade.Pnl = trade.TotalAmount * (trade.ExitPrice - trade.EntryPrice);
                        Amount += (trade.Pnl + trade.Amount);
                        TradesFutureHistory.Add(trade);
                    }
                    else
                    {
                        temp.Amount = trade.Amount / 100 * pourcent;
                        temp.Pnl = temp.TotalAmount * (temp.ExitPrice - temp.EntryPrice);
                        TradeFuture[nb].Amount -= temp.Amount;
                        TradeFuture[nb].TotalAmount = TradeFuture[nb].Amount * TradeFuture[nb].Leverage;
                        temp.TotalAmount = temp.Amount * temp.Leverage;
                        Amount += (temp.Pnl + temp.Amount);
                        TradesFutureHistory.Add(temp);
                    }
                }
                else
                {
                    temp.Amount = trade.Amount / 100 * pourcent;
                    if (pourcent == 100)
                    {
                        TradeFuture.Remove(trade);
                        trade.ExitPrice = exitPrice;
                        trade.Pnl = trade.TotalAmount * (trade.EntryPrice - trade.ExitPrice);
                        Amount += (trade.Pnl+trade.Amount);
                        TradesFutureHistory.Add(trade);
                    }
                    else
                    {
                        TradeFuture[nb].Amount -= temp.Amount;
                        TradeFuture[nb].TotalAmount = TradeFuture[nb].Amount * TradeFuture[nb].Leverage;
                        temp.TotalAmount = temp.Amount * temp.Leverage;
                        temp.Pnl = temp.TotalAmount * (temp.EntryPrice - temp.ExitPrice);
                        Amount += (temp.Pnl + temp.Amount);
                        TradesFutureHistory.Add(temp);
                    }
                }
            }
            PropertyChangedEventHandler("TotalAmount");
        }

        public void DeleteWallet()
        {
            TradeFuture.Clear();
            foreach(TradeSpot trade in TradeSpots)
            {
                trade.Shares = 0;
                trade.ExitPrice = 0;
                trade.EntryPrice = 0;
                trade.Amount = 0;
            }
            TradesFutureHistory.Clear();
            TradesSpotsHistory.Clear();
            Amount = 50000;
            PropertyChangedEventHandler("TotalAmount");
        }
        private void PropertyChangedEventHandler([CallerMemberName] string? propertyname = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
