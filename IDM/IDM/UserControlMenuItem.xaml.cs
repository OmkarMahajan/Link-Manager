using IDM.Classes;
using IDM.Model;
using IDM.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xamarin.Forms;
using static IDM.Classes.FileDownloader;

namespace IDM
{
    public partial class UserControlMenuItem : UserControl
    {
        public MainWindow mainWindow;
        internal UserControlMenuItem(ItemMenu itemMenu,MainWindow main)
        {
            InitializeComponent();
            this.mainWindow = main;
            ExpanderMenu.Visibility = itemMenu.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = itemMenu.SubItems == null ? Visibility.Visible : Visibility.Collapsed;

            this.DataContext = itemMenu;
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int intselectedindex = ListViewMenu.SelectedIndex;

            var CurrentValue = ExpanderMenu.Header.ToString(); 

            if (CurrentValue.Equals("Tasks"))
            {
                switch (ListViewMenu.SelectedIndex)
                {
                    //Add New Download
                    case 0:
                        NewDownload newDownload = new NewDownload(mainWindow);
                        newDownload.ShowDialog();
                        break;
                    
                    //Add Batch Download
                    case 1:
                        BatchDownloads batchDownloads = new BatchDownloads(mainWindow);
                        batchDownloads.ShowDialog();
                        break;

                    //Exit
                    case 2:
                        mainWindow.Close();
                        break;
                }

            }
            if (CurrentValue.Equals("File"))
            {
                switch (ListViewMenu.SelectedIndex)
                {
                    //start Download
                    case 0:
                        if (mainWindow.DownloadsGrid.SelectedItems.Count > 0)
                        {
                            var selectedDownloads = mainWindow.DownloadsGrid.SelectedItems.Cast<FileDownloader>();

                            foreach (FileDownloader download in selectedDownloads)
                            {
                                download.Start();
                            }
                        }
                        break;

                    //Pause Download
                    case 1:
                        if (mainWindow.DownloadsGrid.SelectedItems.Count > 0)
                        {
                            var selectedDownloads = mainWindow.DownloadsGrid.SelectedItems.Cast<FileDownloader>();

                            foreach (FileDownloader download in selectedDownloads)
                            {
                                download.Pause();
                            }
                        }
                        break;
                    
                    //Remove File
                    case 2:
                        if (mainWindow.DownloadsGrid.Items.Count == 0)
                        {
                            MessageBoxResult results = System.Windows.MessageBox.Show("There is nothing to delete in the list", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        
                        if (mainWindow.DownloadsGrid.SelectedItems.Count > 0)
                        {
                            MessageBoxResult result = System.Windows.MessageBox.Show("Do You want delete data too?", "Delete", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                            var selectedDownloads = mainWindow.DownloadsGrid.SelectedItems.Cast<FileDownloader>();
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
                                else if(result == MessageBoxResult.No)
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

                            foreach(string file in DeleteFile)
                            {
                                while(true)
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

                            if(DeleteFile.Count > 1)
                            {
                                MessageBox.Show("Files Deleted Successfully", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else if(DeleteFile.Count == 1)
                            {
                                MessageBox.Show("File Deleted Successfully", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                //Do nothing
                            }
                        }
                        break;
                }

            }
            if (CurrentValue.Equals("Downloads"))
            {
                switch (ListViewMenu.SelectedIndex)
                {
                    //Resume All
                    case 0:
                        foreach (FileDownloader download in Downloads.Instance.DownloadsList)
                        {
                            download.Start();
                        }
                        break;

                   //Pause All
                    case 1:
                        foreach (FileDownloader download in Downloads.Instance.DownloadsList)
                        {
                            download.Pause();
                        }
                        break;

                    //Delete All Completed
                    case 2:
                        if(mainWindow.DownloadsGrid.Items.Count == 0)
                        {
                            MessageBoxResult result = System.Windows.MessageBox.Show("There is nothing to delete in the list", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if(mainWindow.DownloadsGrid.SelectedItems.Count > 0)
                        {
                            MessageBoxResult result = System.Windows.MessageBox.Show("Do You want delete data too?", "Delete", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                            var selectedDownloads = mainWindow.DownloadsGrid.SelectedItems.Cast<FileDownloader>();
                            var Delete = new List<FileDownloader>();
                            var DeleteFile = new List<string>();

                            foreach (FileDownloader download in selectedDownloads)
                            {
                                var filepath = download.DownloadPath;
                                if (result == MessageBoxResult.Yes && download.Status == DownloadStatus.Completed.ToString())
                                {
                                    DeleteFile.Add(filepath);
                                    Delete.Add(download);
                                }
                                else if (result == MessageBoxResult.No && download.Status == DownloadStatus.Completed.ToString())
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
                                        if (File.Exists(file))
                                        {
                                            File.Delete(file);
                                            break;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }
                            }

                            if (DeleteFile.Count > 1)
                            {
                                MessageBox.Show("Files Deleted Successfully", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else if (DeleteFile.Count == 1)
                            {
                                MessageBox.Show("File Deleted Successfully", "Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                //Do nothing
                            }
                        }

                        break;
                   
                   //Options or Settings
                    case 3:
                        SettingWindow settingWindow = new SettingWindow();
                        settingWindow.ShowDialog();
                        break;
                }
            }
            if (CurrentValue.Equals("About Us"))
            {
                switch (ListViewMenu.SelectedIndex)
                {
                    //About Link Manager
                    case 0:
                        About about = new About();
                        about.ShowDialog();
                        break;
                 
                }
            }

        }
    }
}
