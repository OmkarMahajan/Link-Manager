using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace IDM
{
    /// <summary>
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            tbPath.Text = Properties.Settings.Default.Path;
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
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show("Are You Sure Want to Save Current Settings?", "Settings",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Properties.Settings.Default.Path = tbPath.Text;
                Properties.Settings.Default.Save();
            }
            this.Close();
        }
    }
}
