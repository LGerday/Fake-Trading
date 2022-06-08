using System;

namespace MyWPFApp.Model
{
    public class SettingEventArgs : EventArgs
    {
        public int Action { get; set; }
        public string Data { get; set; }
        public SettingEventArgs(string path,int action)
        {
            Data = path;
            Action = action;
        }
    }
}
