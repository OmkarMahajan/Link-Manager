using IDM.Classes;
using IDM.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static IDM.Classes.FileDownloader;

namespace IDM
{
    /// <summary>
    /// Interaction logic for BatchDownloads.xaml
    /// </summary>
    public partial class BatchDownloads : Window
    {
        MainWindow MainWindow;
        public BatchDownloads(MainWindow main)
        {
            InitializeComponent();
            this.MainWindow = main;
            if (!Properties.Settings.Default.Path.Equals("Empty") && Properties.Settings.Default.Path.Trim() != null)
            {
                tbPath.Text = Properties.Settings.Default.Path;
            }
        }

        private void btnAddDownloads_Click(object sender, RoutedEventArgs e)
        {
            TextRange downlodLinks = new TextRange(rtDownloads.Document.ContentStart, rtDownloads.Document.ContentEnd);
            //System.Windows.MessageBox.Show(downlodLinks.Text.GetType().ToString());
            String pattern = @"\s";
            String[] elements = System.Text.RegularExpressions.Regex.Split(downlodLinks.Text.ToString(), pattern);
            if(tbPath.Text==null && elements== null & tbPath.Text.Trim()==null)
            {
                System.Windows.MessageBox.Show("Fields Should not be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                foreach (var element in elements)
                {
                    if(IsURLValid(element))
                    {
                        try
                        {
                            FileDownloader fileDownloader = new FileDownloader(element.ToString().Trim());
                            Uri temp = new Uri(element);
                            fileDownloader.FileName = temp.Segments.Last();
                            Console.WriteLine(fileDownloader.FileName);
                            fileDownloader.UsersDirectory = tbPath.Text.Trim();
                            fileDownloader.DownloadPath = tbPath.Text.Trim().ToString() + "\\" + fileDownloader.FileName.Trim().ToString();
                            Console.WriteLine(fileDownloader.DownloadPath);

                            if (!Directory.Exists(tbPath.Text))
                            {
                                Directory.CreateDirectory(tbPath.Text);
                            }

                            string filePath = tbPath.Text + fileDownloader.FileName;
                            string tempPath = filePath + ".tmp";

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

                            //fileDownloader.TempDownloadPath = tempPath;
                            fileDownloader.AddedOn = DateTime.UtcNow;
                            fileDownloader.Status = FileDownloader.DownloadStatus.Initialized.ToString();
                            fileDownloader.OnPropertyChanged("Status");
                            fileDownloader.OnPropertyChanged("StatusString");

                      
                            Downloads.Instance.DownloadsList.Add(fileDownloader);
                            fileDownloader.StartDownload();

                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error from this"); 
                            System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                }
        }

        private NumberFormatInfo numberFormat = NumberFormatInfo.InvariantInfo;

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

        private void tbPath_Changed(object sender, TextChangedEventArgs e)
        {
            string drive = String.Empty;
            if (tbPath.Text.Length > 3 )
                drive = tbPath.Text.Remove(3);
            else
                drive = tbPath.Text;

            tbDiskspace.Text = "Free Disk Space: " + GetFreeDiskSpace(drive);
        }

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
                    if (results == MessageBoxResult.Yes)
                    {
                        Properties.Settings.Default.Path = tbPath.Text;
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }

        private bool IsURLValid(string Url)
        {
            //checks whether string is valid URL or not
            try
            {
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
    }
}
