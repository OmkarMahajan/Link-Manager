using IDM.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static IDM.Model.Downloads;
using System.Globalization;
using IDM.Classes;
using static IDM.Classes.FileDownloader;

namespace IDM.Model
{
    public class Downloads
    {
        private static Downloads instance = new Downloads();

        public static Downloads Instance
        {
            get
            {
                return instance;
            }
        }

        internal ObservableCollection<FileDownloader> DownloadsList = new ObservableCollection<FileDownloader>();
        internal ObservableCollection<PropertyModel> propertiesList = new ObservableCollection<PropertyModel>();
        public int ActiveDownloads
        {
            get
            {
                int active = 0;
                foreach (FileDownloader d in DownloadsList)
                {
                    if (!d.DownloadError)
                        if (d.Status == DownloadStatus.Downloading.ToString())
                            active++;
                }
                return active;
            }
        }

        public int TotalDownloads
        {
            get
            {
                int count = 0;
                foreach (FileDownloader d in DownloadsList)
                {
                    count++;
                }
                return count;
            }
        }
    }

}
