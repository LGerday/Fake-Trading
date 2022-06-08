using System;

namespace MyWPFApp.Model
{
    public class PathEventArgs : EventArgs
    {
        public string Path { get; set; }
        public PathEventArgs(string path)
        {
            Path = path;
        }
    }
}
