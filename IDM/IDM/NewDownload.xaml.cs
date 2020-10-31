using IDM.Classes;
using IDM.Model;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using static IDM.Classes.FileDownloader;

namespace IDM
{

    public partial class NewDownload : Window
    {
        private bool urlValid;
        private NumberFormatInfo numberFormat = NumberFormatInfo.InvariantInfo;
        private MainWindow mainWindow;

        #region Constructor
        public NewDownload(MainWindow mainWin)
        {
            InitializeComponent();
            mainWindow = mainWin;
            urlValid = false;

            if(!Properties.Settings.Default.Path.Equals("Empty") && Properties.Settings.Default.Path.Trim() != null)
            {
                tbPath.Text = Properties.Settings.Default.Path;
            }
            
        }
        #endregion

        #region SupportingFunctions
        private bool IsUsed(string Url)
        {
            int Flag = 0;
            foreach (FileDownloader d in Downloads.Instance.DownloadsList)
            {
                if (d.Url.ToString() == Url)
                {
                    Flag = 1;
                    break;
                }
            }
            if (Flag == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsURLValid(string Url)
        {
            //checks whether string is valid URL or not
            try
            {
                AppDomain domain = AppDomain.CurrentDomain;
                // Set a timeout interval of 2 seconds.
                domain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromSeconds(1000));
                Object timeout = domain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
                Console.WriteLine("Default regex match timeout: {0}",
                                   timeout == null ? "<null>" : timeout);
                string strRegex = @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$";
                Regex re = new Regex(strRegex, RegexOptions.IgnoreCase);
                if (re.IsMatch(Url))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Check URL", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private string GetFreeDiskSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    long freeSpace = drive.AvailableFreeSpace;
                    double mbFreeSpace = (double)freeSpace / Math.Pow(1024, 2);
                    double gbFreeSpace = mbFreeSpace / 1024D;

                    if (freeSpace < Math.Pow(1024, 3))
                    {
                        return mbFreeSpace.ToString("#.00", numberFormat) + " MB";
                    }
                    return gbFreeSpace.ToString("#.00", numberFormat) + " GB";
                }
            }
            return String.Empty;
        }
        #endregion

        #region ContextChangedHandler
        private void tbURL_Changed(object sender, TextChangedEventArgs e)
        {
            if(IsURLValid(tbURL.Text))
            {
                urlValid = true;
                tbSaveAs.Text = tbURL.Text.Substring(tbURL.Text.LastIndexOf("/") + 1);
                Console.WriteLine("Url is valid now");
            }
            else
            {
                urlValid = false;
                tbSaveAs.Text = String.Empty;
                Console.WriteLine("not valid");
            }
        }

        private void tbDownloadFolder_TextChanged(object sender, TextChangedEventArgs e)
        {
            string drive = String.Empty;
            if (tbPath.Text.Length > 3)
                drive = tbPath.Text.Remove(3);
            else
                drive = tbPath.Text;

            tbDiskspace.Text = "Free Disk Space: " + GetFreeDiskSpace(drive);
        }
        private void tbSaveAs_Changed(object sender, TextChangedEventArgs e)
        {
            if(!tbSaveAs.Text.Contains("."))
            {
                System.Windows.MessageBox.Show("Please Provide Extention to the file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region ButtonClickHandler
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbDialog = new FolderBrowserDialog();
            fbDialog.Description = "Choose Download Folder";
            fbDialog.ShowNewFolderButton = true;
            DialogResult result = fbDialog.ShowDialog();

            if (result.ToString().Equals("OK"))
            {
                string path = fbDialog.SelectedPath;
                if (path.EndsWith("\\") == false)
                    path += "\\";
                tbPath.Text = path;

                if (Properties.Settings.Default.Path.Equals("Empty") || Properties.Settings.Default.Path.Trim() == null)
                {
                    MessageBoxResult results = System.Windows.MessageBox.Show("Do You Want to make this path as Default Download Path? ", "Settings", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if(results == MessageBoxResult.Yes)
                    {
                        Properties.Settings.Default.Path = tbPath.Text;
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }
        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            if(urlValid)
            {
                if (tbSaveAs.Text.Length < 3)
                {
                    System.Windows.MessageBox.Show("The local file name is not valid.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    FileDownloader fileDownloader = new FileDownloader(tbURL.Text.Trim());
                    Uri temp = new Uri(tbURL.Text);
                    fileDownloader.FileName = temp.Segments.Last(); //tbSaveAs.Text.Trim();
                    fileDownloader.DownloadPath = tbPath.Text.Trim().ToString() + '\\' + tbSaveAs.Text.Trim().ToString();

                    if (!Directory.Exists(tbPath.Text))
                    {
                        Directory.CreateDirectory(tbPath.Text);
                    }

                    string filePath = tbPath.Text + fileDownloader.FileName;
                    string tempPath = filePath + ".tmp";
                    fileDownloader.UsersDirectory = tbPath.Text.Trim().ToString();

                    if (File.Exists(tempPath))
                    {
                        System.Windows.MessageBox.Show("There is already a download in progress at the specified path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (File.Exists(filePath))
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show("There is already a file with the same name, do you want to overwrite it? ", "Error" + filePath, MessageBoxButton.YesNo, MessageBoxImage.Warning);

                        if (result == MessageBoxResult.Yes)
                        {
                            File.Delete(filePath);
                        }
                        else
                            return;
                    }

                    fileDownloader.CheckRange();
                    if (fileDownloader.DownloadError)
                    {
                        return;
                    }

                    fileDownloader.AddedOn = DateTime.UtcNow;
                    fileDownloader.Status = FileDownloader.DownloadStatus.Initialized.ToString();
                    fileDownloader.OnPropertyChanged("Status");
                    fileDownloader.OnPropertyChanged("StatusString");
                    Downloads.Instance.DownloadsList.Add(fileDownloader);
                   
                    fileDownloader.StartDownload();

                    this.Close();

                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                System.Windows.MessageBox.Show("The URL is not a valid download link.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}