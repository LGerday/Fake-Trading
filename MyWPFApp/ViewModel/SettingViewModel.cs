using MyWPFApp.Model;

namespace MyWPFApp.ViewModel
{
    public class SettingViewModel
    {
        public SettingViewModel()
        {

        }
        public void SettingChangeWallet(string value)
        {
            SettingEvent(this, new SettingEventArgs(value, 1));
        }
        public void SettingSavePathChange(string path)
        {
            SettingEvent?.Invoke(this,new SettingEventArgs(path, 2));
        }
        public void DeleteAllData()
        {
            SettingEvent?.Invoke(this, new SettingEventArgs("", 3));
        }
        public void ExportTradeSpot()
        {
            SettingEvent?.Invoke(this, new SettingEventArgs("", 4));
        }
        public event ChangeSetting SettingEvent;
    }
    public delegate void ChangeSetting(object sender, SettingEventArgs e);
}
