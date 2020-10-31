using IDM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace IDM.Classes
{
    public class FileDownloader : INotifyPropertyChanged
    {
        //interface 
        public event PropertyChangedEventHandler PropertyChanged;

        // File name
        public string FileName { get; set; }

        // URL of the file to download
        public Uri Url { get; set; }

        // File type (extension)
        public string FileType
        {
            get
            {
                return Url.ToString().Substring(Url.ToString().LastIndexOf('.') + 1).ToUpper();
            }
        }

        // Temporary file path
        public string TempDownloadPath { get; set; }

        // Downloaded file path
        public string DownloadPath { get; set; }

        // Local folder which contains the file
        public string DownloadFolder
        {
            get
            {
                return this.TempDownloadPath.Remove(TempDownloadPath.LastIndexOf("\\") + 1);
            }
        }

        //User Selected Directory
        public string UsersDirectory { get; set; }
        // Download status
        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
              
            }
        }

        public float Percent
        {
            get
            {
                return ((float)(Downloaded) / (float)FileSize) * 100f;
            }
        }

        public string PercentString
        {
            get
            {
                if (Percent < 0 || float.IsNaN(Percent))
                    return "0.0%";
                else if (Percent > 100)
                    return "100.0%";
                else
                    return String.Format(numberFormat, "{0:0.0}%", Percent);
            }
        }

        public float Progress
        {
            get
            {
                return Percent;
            }
        }

        // Server supports the Range header (resuming the download)
        public bool SupportsRange { get; set; }
        // There was an error during download
        public bool DownloadError { get; set; }
        // Temporary file was created
        public bool TempFileCreated { get; set; }
        // Status text in the DataGrid
        public string StatusText;
        public string StatusString
        {
            get
            {
                if (this.DownloadError)
                    return StatusText;
                else
                    return Status.ToString();
            }
            set
            {
                StatusText = value;
            }
        }
        public long Downloaded { get; set; }
        public string DownloadedSizeString
        {
            get
            {
              return  FormatSizeString(Downloaded);
            }
            set
            {
                Downloaded = long.Parse(value);
       
            }
        }

        public long FileSize { get; set; }
        public string FileSizeString
        {
            get
            {
                return FormatSizeString(FileSize);
            }
        }

        //To Update display every second
        public DateTime LastUpdateTime { get; set; }

        public DateTime AddedOn { get; set; }

        public string AddedOnString
        {
            get
            {
                string format = "dd.MM.yyyy. HH:mm:ss";
                return AddedOn.ToString(format);
            }
        }

        public DateTime CompletedOn { get; set; }
        public string CompletedOnString
        {
            get
            {
                if (CompletedOn != DateTime.MinValue)
                {
                    string format = "dd.MM.yyyy. HH:mm:ss";
                    return CompletedOn.ToString(format);
                }
                else
                    return String.Empty;
            }
        }

        public Thread thread;
        public double StartTime { get; set; }
        public double EndTime { get; set; }

        public int downloadSpeed;
        public string DownloadSpeed
        {
            get
            {
                if (this.Status == DownloadStatus.Downloading.ToString() && !this.DownloadError)
                {
                    return FormatSpeedString(downloadSpeed);
                }
                return String.Empty;
            }
        }

        public double downloadtime {get; set;}

        #region Constructor
        public FileDownloader(string url)
        {
            Url = new Uri(url, UriKind.Absolute);
            this.SupportsRange = false;
            this.DownloadError = false;
            this.FileName = "NewDownload";
            this.StatusText = String.Empty;
            this.Status = DownloadStatus.Initialized.ToString();
            this.AddedOn = DateTime.UtcNow;
            this.Downloaded = 1;
            this.downloadSpeed = 1;
        }

        #endregion

        #region PropertyChangeHandler
        internal void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region link Management
        public void StartDownload()
        {
            if ((this.Status == DownloadStatus.Initialized.ToString()) || Status == DownloadStatus.Queued.ToString() || this.Status == DownloadStatus.Paused.ToString() || this.DownloadError)
            {
                if (!this.SupportsRange)
                {
                    this.StatusString = "Error: Server does not support resume";
                    this.DownloadError = true;
                    //RaiseDownloadCompleted();
                    //return;
                }
            }
            this.DownloadError = false;

            thread = new Thread(DownloadFile);
            thread.IsBackground = true;
            thread.Start();
        }
        
        private void DownloadFile()
        {
            // to the caller. Initialize to 0 here.
            int bytesProcessed = (int)this.Downloaded;
            // Assign values to these objects here so that they can
            // be referenced in the finally block
            Stream remoteStream = null;
            Stream localStream = null;
            WebResponse response = null;
            //Change status of download
            this.Status = DownloadStatus.Downloading.ToString();
            OnPropertyChanged("Status");
            OnPropertyChanged("StatusString");
            // Use a try/catch/finally block as both the WebRequest and Stream
            // classes throw exceptions upon error
            try
            {
                WebRequest request = (HttpWebRequest)WebRequest.Create(this.Url);
                request.Method = "GET";

                if (request != null)
                {
                    // Set download starting point
                   
                    // Send the request to the server and retrieve the
                    // WebResponse object 
                    response = request.GetResponse();
                    if (response != null)
                    {
                        // Once the WebResponse object has been retrieved,
                        // get the stream object associated with the response's data
                        remoteStream = response.GetResponseStream();
                        // Create the local file
                        try
                        {
                            localStream = File.Create(this.DownloadPath);
                        }
                        catch (Exception)
                        {
                            //string illegal = "\"M<>\"\\a/ry/ h**ad:>> a\\/:*?\"<>| li*tt|le|| la\"mb.?";
                            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                            string url = Url.ToString();
                            foreach (char c in invalid)
                            {
                                this.FileName = this.FileName.Replace(c.ToString(), "");
                            }
                            OnPropertyChanged("FileName");
                            this.DownloadPath = this.UsersDirectory + @"/" + this.FileName.Trim();
                            OnPropertyChanged("DownloadPath");

                            Console.WriteLine(this.FileName);
                            localStream = File.Create(this.DownloadPath);
                            //Url.ToString().Substring(Url.ToString().LastIndexOf("?") + 1);
                        }


                        // Allocate a 1k buffer
                        byte[] buffer = new byte[1024];
                        int bytesRead;

                        this.StartTime = Environment.TickCount;
                        do
                        {
                            // Read data (up to 1k) from the stream
                            bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                            // Write the data to the local file
                            localStream.Write(buffer, 0, bytesRead);

                            // Increment total bytes processed
                            bytesProcessed += bytesRead;

                            // Stop the file if the download is paused or completed
                            if (this.Status != DownloadStatus.Downloading.ToString())
                            {
                                this.Status = DownloadStatus.Paused.ToString();
                                OnPropertyChanged("Status");
                                OnPropertyChanged("StatusString");
                                localStream.Close();
                                break;
                            }

                            this.downloadtime = Environment.TickCount;
                            this.Downloaded = bytesProcessed;
                            OnPropertyChanged("Downloaded");
                            OnPropertyChanged("DownloadedSizeString");
                            OnPropertyChanged("Percent");
                            OnPropertyChanged("PercentString");
                            OnPropertyChanged("Progress");
                            CalculateDownloadSpeed();
                        } while (bytesRead > 0);

                        if(this.Status == DownloadStatus.Downloading.ToString())
                        {
                            this.EndTime = Environment.TickCount;
                            this.Status = FileDownloader.DownloadStatus.Completed.ToString();
                            OnPropertyChanged("Status");
                            OnPropertyChanged("StatusString");
                            this.CompletedOn = DateTime.UtcNow;
                            OnPropertyChanged("CompletedOn");
                            OnPropertyChanged("CompletedOnString");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine("timed error");
            }
            finally
            {
                // Close the response and streams objects here to make sure they're closed even if an exception.
                if (response != null) response.Close();
                if (remoteStream != null) remoteStream.Close();
                if (localStream != null) localStream.Close();
                if (thread != null) thread.Abort();
            }

        }

        public void CheckRange()
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(this.Url);
                webRequest.Method = "HEAD";
                webRequest.Timeout = 5000;

                using (WebResponse response = webRequest.GetResponse())
                {
                    foreach (var header in response.Headers.AllKeys)
                    {
                        if (header.Equals("Accept-Ranges", StringComparison.OrdinalIgnoreCase))
                        {
                            this.SupportsRange = true;
                        }

                        Console.WriteLine(header);
                    }
                    Console.WriteLine(Url.Segments.Last());
                    this.FileSize = response.ContentLength;

                    if (this.FileSize <= 0)
                    {
                        System.Windows.MessageBox.Show("file not exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.DownloadError = true;
                    }
                    response.Close();
                }
                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        public void CalculateDownloadSpeed()
        {
            double secs = Math.Floor(this.downloadtime - this.StartTime);
            double secs2 = Math.Round(secs, 0);
            double kbsec = Math.Round((double) this.Downloaded / secs);
            //Console.WriteLine(kbsec);
            this.downloadSpeed = (int)secs2;
            OnPropertyChanged("downloadSpeed");
            OnPropertyChanged("DownloadSpeed");
        }


        public enum DownloadStatus
        {
            Initialized,

            Downloading,

            Pausing,

            Paused,

            Queued,

            Deleting,
            
            Deleted,

            Completed,

            Error
        }

        private static NumberFormatInfo numberFormat = NumberFormatInfo.InvariantInfo;

        public static string FormatSizeString(long byteSize)
        {
            double kiloByteSize = (double)byteSize / 1024D;
            double megaByteSize = kiloByteSize / 1024D;
            double gigaByteSize = megaByteSize / 1024D;

            if (byteSize < 1024)
                return String.Format(numberFormat, "{0} B", byteSize);
            else if (byteSize < 1048576)
                return String.Format(numberFormat, "{0:0.00} kB", kiloByteSize);
            else if (byteSize < 1073741824)
                return String.Format(numberFormat, "{0:0.00} MB", megaByteSize);
            else
                return String.Format(numberFormat, "{0:0.00} GB", gigaByteSize);
        }
        public static string FormatSpeedString(int speed)
        {
            float kbSpeed = (float)speed / 1024F;
            float mbSpeed = kbSpeed / 1024F;

            if (speed <= 0)
                return String.Empty;
            else if (speed < 1024)
                return speed.ToString() + " B/s";
            else if (speed < 1048576)
                return kbSpeed.ToString("#.00", numberFormat) + " kB/s";
            else
                return mbSpeed.ToString("#.00", numberFormat) + " MB/s";
        }

        #region download activity
        // Start or continue download
        public void Start()
        {
            Console.WriteLine("Status = "+ this.status.ToString()+"    "+ this.StatusString);
            if (this.Status == DownloadStatus.Initialized.ToString() || this.Status == DownloadStatus.Paused.ToString()
                || this.Status == DownloadStatus.Queued.ToString() || this.DownloadError)
            {
                if (!this.SupportsRange && this.Downloaded > 0)
                {
                    this.StatusString = "Error: Server does not support resume";
                    this.DownloadError = true;
                    //this.RaiseDownloadCompleted();
                    return;
                }

                this.DownloadError = false;
                this.Status = DownloadStatus.Initialized.ToString();
                OnPropertyChanged("Status");
                OnPropertyChanged("StatusString");

                // Start the download thread
                thread = new Thread(new ThreadStart(DownloadFile));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        // Pause download
        public void Pause()
        {
            //Console.WriteLine("Status = " + this.status.ToString() + "    " + this.StatusString);
            if (this.Status == DownloadStatus.Downloading.ToString())
            {
                this.Status = DownloadStatus.Paused.ToString();
                OnPropertyChanged("Status");
                OnPropertyChanged("StatusString");
            }
            if (this.Status == DownloadStatus.Queued.ToString())
            {
                this.Status = DownloadStatus.Paused.ToString();
                OnPropertyChanged("Status");
                OnPropertyChanged("StatusString");
            }
        }
        #endregion
    }

}
