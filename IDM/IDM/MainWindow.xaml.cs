using IDM.Classes;
using IDM.Model;
using IDM.ViewModel;
using MaterialDesignThemes.Wpf;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Xamarin.Forms.PlatformConfiguration;
using static IDM.Classes.FileDownloader;

namespace IDM
{
 
    public partial class MainWindow : Window
    {
        static List<SubItem> menuTask;
        private List<string> propertyNames;
        private List<string> propertyValues;
        private List<PropertyModel> propertiesList;
        public MainWindow()
        {
            InitializeComponent();

            menuTask = new List<SubItem>();
            menuTask.Add(new SubItem("Add new Download"));
            menuTask.Add(new SubItem("Add batch download"));
            menuTask.Add(new SubItem("Exit"));
            var item1 = new ItemMenu("Tasks", menuTask, PackIconKind.CalendarTask);


            var menuFile = new List<SubItem>();
            menuFile.Add(new SubItem("Resume"));
            menuFile.Add(new SubItem("Stop Download"));
            menuFile.Add(new SubItem("Remove"));
            var item2 = new ItemMenu("File", menuFile, PackIconKind.FileCloud);

            var menuDownloads = new List<SubItem>();
            menuDownloads.Add(new SubItem("Resume All"));
            menuDownloads.Add(new SubItem("Pause All"));
            menuDownloads.Add(new SubItem("Delete All Completed"));
            menuDownloads.Add(new SubItem("Options"));
            var item3 = new ItemMenu("Downloads", menuDownloads, PackIconKind.DownloadNetwork);

            var menuHelp = new List<SubItem>();
            menuHelp.Add(new SubItem("About Link Manager"));
            var item4 = new ItemMenu("About Us", menuHelp, PackIconKind.HelpNetwork);

            //Menu.Children.Add(new UserControlMenuItem(item0));
            Menu.Children.Add(new UserControlMenuItem(item1,this));
            Menu.Children.Add(new UserControlMenuItem(item2,this));
            Menu.Children.Add(new UserControlMenuItem(item3,this));
            Menu.Children.Add(new UserControlMenuItem(item4,this));

            LoadDownloads();

            DownloadsGrid.ItemsSource = Downloads.Instance.DownloadsList;
            Downloads.Instance.DownloadsList.CollectionChanged += new NotifyCollectionChangedEventHandler(DownloadsList_CollectionChanged);

            propertyNames = new List<string>();
            propertyNames.Add("URL");
            propertyNames.Add("Supports Resume");
            propertyNames.Add("File Type");
            propertyNames.Add("Download Folder");

            propertyValues = new List<string>();
            propertiesList = new List<PropertyModel>();
            SetEmptyProperties();
        }

        private void DownloadsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(Downloads.Instance.DownloadsList.Count>0)
            {
                Console.WriteLine("sdsldksd");
            }
        }

        private void btnBottomMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideBottomMenu", btnBottomMenuHide, btnBottomMenuShow, pnlBottomMenu);
        }

        private void btnBottomMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowBottomMenu", btnBottomMenuHide, btnBottomMenuShow, pnlBottomMenu);
            
        }

        private void ShowHideMenu(string Storyboard1, System.Windows.Controls.Button btnHide, System.Windows.Controls.Button btnShow, StackPanel pnl)
        {
           Storyboard sb = FindResource(Storyboard1) as Storyboard;
            sb.Begin(pnl);

            if (Storyboard1.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (Storyboard1.Contains("Hide"))
            {
                btnHide.Visibility = System.Windows.Visibility.Hidden;
                btnShow.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void SetEmptyProperties()
        {
            if (propertiesList.Count > 0)
                propertiesList.Clear();

            List<string> propertyNames = new List<string>();
            propertyNames.Add("URL");
            propertyNames.Add("Supports Resume");
            propertyNames.Add("File Type");
            propertyNames.Add("Download Folder");

            for (int i = 0; i < 4; i++)
            {
                propertiesList.Add(new PropertyModel(propertyNames[i], String.Empty));
            }
            propertiesGrid.ItemsSource = null;
            propertiesGrid.ItemsSource = propertiesList;
        }

        private void Window_ContentRendered1(object sender, EventArgs e)
        {

        }

        private void LoadDownloads()
        {
            try
            {
                if (File.Exists("Downloads.xml"))
                {
                    // Load downloads from XML file
                    XElement downloads = XElement.Load("Downloads.xml");
                    if (downloads.HasElements)
                    {
                        IEnumerable<XElement> downloadsList =
                            from el in downloads.Elements()
                            select el;
                        foreach(XElement download in downloadsList)
                        {
                            // Create filedownloader object based on XML data
                            FileDownloader downloader = new FileDownloader(download.Element("url").Value);

                            downloader.FileName = download.Element("file_name").Value;
                            downloader.Url = new Uri(download.Element("url").Value);
                            
                            downloader.FileSize = Convert.ToInt64(download.Element("file_size").Value);
                            downloader.Downloaded = Convert.ToInt64(download.Element("downloaded_size").Value);

                            Downloads.Instance.DownloadsList.Add(downloader);

                            if (download.Element("status").Value == "Completed")
                            {
                                downloader.Status = DownloadStatus.Completed.ToString();
                            }
                            else
                            {
                                downloader.Status = DownloadStatus.Paused.ToString();
                            }

                            downloader.Status = download.Element("status").Value.ToString();
                            downloader.AddedOn = DateTime.Parse(download.Element("added_on").Value);
                            downloader.CompletedOn = DateTime.Parse(download.Element("completed_on").Value);
                            downloader.SupportsRange = Boolean.Parse(download.Element("supports_resume").Value);
                            downloader.DownloadError = Boolean.Parse(download.Element("has_error").Value);
                            downloader.DownloadPath = download.Element("DownloadFolder").Value;
                            downloader.UsersDirectory = download.Element("User_Directory").Value; 
                        }

                        // Create empty XML file
                        XElement root = new XElement("downloads");
                        XDocument xd = new XDocument();
                        xd.Add(root);
                        xd.Save("Downloads.xml");
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("There was an error while loading the downloads list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            string message = "Are you sure you want to exit the application?";
            MessageBoxResult result = System.Windows.MessageBox.Show(message, "Link Manager", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
                return;
            }
            else
            {
                SaveDownloads();
            }
        }

        private void SaveDownloads()
        {
            if (Downloads.Instance.TotalDownloads > 0)
            {
                // Pause downloads
                PauseAllDownloads();

                XElement root = new XElement("downloads");

                foreach (FileDownloader download in Downloads.Instance.DownloadsList)
                {
                    XElement xdl = new XElement("download",
                                        new XElement("file_name", download.FileName),
                                        new XElement("file_size", download.FileSize),
                                        new XElement("file_size_string", download.FileSizeString),
                                        new XElement("downloaded_size", download.Downloaded),
                                        new XElement("size", download.DownloadedSizeString),
                                        new XElement("percent", download.Percent.ToString()),
                                        new XElement("percent_string", download.PercentString.ToString()),
                                        new XElement("progress", download.Progress.ToString()),
                                        new XElement("speed", download.DownloadSpeed.ToString()),
                                        new XElement("status", download.Status.ToString()),
                                        new XElement("added_on", download.AddedOn.ToString()),
                                        new XElement("completed_on", download.CompletedOn.ToString()),

                                        new XElement("url", download.Url),
                                        new XElement("supports_resume", download.SupportsRange.ToString()),
                                        new XElement("has_error", download.DownloadError.ToString()),
                                        new XElement("DownloadFolder", download.DownloadPath.ToString()),
                                        new XElement("User_Directory", download.UsersDirectory.ToString())
                                        );
                    root.Add(xdl);

                    Console.WriteLine(download.DownloadPath);
                }
                XDocument xd = new XDocument();
                xd.Add(root);
                // Save downloads to XML file
                xd.Save("Downloads.xml");
            }
        }

        private void PauseAllDownloads()
        {
            if (DownloadsGrid.Items.Count > 0)
            {
                foreach (FileDownloader download in Downloads.Instance.DownloadsList)
                {
                    download.Pause();
                }
            }
        }

        private void StartAllDownload()
        {
            if (DownloadsGrid.Items.Count > 0)
            {
                foreach (FileDownloader download in Downloads.Instance.DownloadsList)
                {
                    download.Start();
                }
            }
        }

        private void downloadsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DownloadsGrid.SelectedItems.Count == 1)
            {

                var download = (FileDownloader)DownloadsGrid.SelectedItem;

                if (propertyValues.Count > 0)
                    propertyValues.Clear();

                propertyValues.Add(download.Url.ToString());
                string resumeSupported = "No";
                if (download.SupportsRange)
                    resumeSupported = "Yes";
                propertyValues.Add(resumeSupported);
                propertyValues.Add(download.FileType);
                propertyValues.Add(download.DownloadPath);

                if (propertiesList.Count > 0)
                    propertiesList.Clear();

                for (int i = 0; i < 4; i++)
                {
                    propertiesList.Add(new PropertyModel(propertyNames[i], propertyValues[i]));
                }
                propertiesGrid.ItemsSource = null;
                propertiesGrid.ItemsSource = propertiesList;
            }
            else
            {
                SetEmptyProperties();
            }
        }


        #region ClickHandler
        private void btnAddURL_Click(object sender, RoutedEventArgs e)
        {
            NewDownload newDownload = new NewDownload(this);
            newDownload.ShowDialog();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (DownloadsGrid.SelectedItems.Count > 0)
            {
                var selectedDownloads = DownloadsGrid.SelectedItems.Cast<FileDownloader>();

                foreach (FileDownloader download in selectedDownloads)
                {
                    Console.WriteLine(download.Status);
                    if (download.Status == DownloadStatus.Paused.ToString() || download.DownloadError)
                    {
                        download.Start();
                    }
                }
            }
        }
        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (DownloadsGrid.SelectedItems.Count > 0)
            {
                var selectedDownloads = DownloadsGrid.SelectedItems.Cast<FileDownloader>();

                foreach (FileDownloader download in selectedDownloads)
                {
                    download.Pause();
                }
            }
        }

        private void btnbatchDownload_Click(object sender, RoutedEventArgs e)
        {
            BatchDownloads batchDownloads = new BatchDownloads(this);
            batchDownloads.ShowDialog();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DownloadsGrid.Items.Count == 0)
            {
                MessageBoxResult results = System.Windows.MessageBox.Show("There is nothing to delete in the list", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DownloadsGrid.SelectedItems.Count > 0)
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Do You want delete data too?", "Delete", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                if (DownloadsGrid.SelectedItems.Count > 0)
                {
                    var selectedDownloads = DownloadsGrid.SelectedItems.Cast<FileDownloader>();
                    var Delete = new List<FileDownloader>();
                    var DeleteFile = new List<string>();

                    foreach (FileDownloader download in selectedDownloads)
                    {
                        var filepath = download.DownloadPath;
                        if (result == MessageBoxResult.Yes)
                        {
                            DeleteFile.Add(filepath);
                            Delete.Add(download);
                        }
                        else if (result == MessageBoxResult.No)
                        {
                            Delete.Add(download);
                        }
                        else
                        {
                            //Do nothing
                        }
                    }

                    foreach (FileDownloader download in Delete)
                    {
                        download.Pause();
                        Downloads.Instance.DownloadsList.Remove(download);
                    }

                    foreach (string file in DeleteFile)
                    {
                        while (true)
                        {
                            try
                            {
                                File.Delete(file);
                                break;
                            }
                            catch
                            {
                                continue;
                            }
                        }
                    }

                    if (DeleteFile.Count > 1)
                    {
                        System.Windows.MessageBox.Show("Files Deleted Successfully", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (DeleteFile.Count == 1)
                    {
                        System.Windows.MessageBox.Show("File Deleted Successfully", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        //Do nothing
                    }
                }
            }
                
        }
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow settingWindow = new SettingWindow();
            settingWindow.ShowDialog();
        }
        private void downloadGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DownloadsGrid.SelectedItems.Count > 0)
            {
                var selectedDownloads = (FileDownloader)DownloadsGrid.SelectedItem;
                try
                {
                    Process.Start(selectedDownloads.DownloadPath);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Error while opening the file", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
        }

        private void cmOpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (DownloadsGrid.SelectedItems.Count > 0)
            {
                var selectedDownloads = (FileDownloader)DownloadsGrid.SelectedItem;
                try
                {
                    Process.Start(selectedDownloads.DownloadPath);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Error while opening the file", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
        }
        private void cmOpenDownloadFolder_Click(object sender, RoutedEventArgs e)
        {
            if (DownloadsGrid.SelectedItems.Count > 0)
            {
                var selectedDownloads = (FileDownloader)DownloadsGrid.SelectedItem;
                try
                {
                    Process.Start(selectedDownloads.UsersDirectory);
                }
                catch
                {
                    System.Windows.MessageBox.Show("Error while opening the file", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
        }
        private void cmStartAll_Click(object sender, RoutedEventArgs e)
        {
            StartAllDownload();
        }
        private void cmPauseAll_Click(object sender, RoutedEventArgs e)
        {
            PauseAllDownloads();
        }
        private void btnAboutUs_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
        private void cmCopyURLtoClipboard_Click(object sender, RoutedEventArgs e)
        {
            if (DownloadsGrid.SelectedItems.Count > 0)
            {
                var selectedDownloads = (FileDownloader)DownloadsGrid.SelectedItem;
                System.Windows.Clipboard.SetText(selectedDownloads.Url.ToString());
            }
                
        }
        #endregion

        
    }
}
