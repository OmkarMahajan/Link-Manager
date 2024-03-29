﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5FB7CCE2749E932F959E430D9542A8DAC8530CFC5998F9F727D99611E9088B39"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using IDM;
using IDM.Classes;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace IDM {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 91 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Menu;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer dgScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DownloadsGrid;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem cmStart;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem cmPause;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem cmDelete;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem cmOpenFile;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem cmOpenDownloadFolder;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem StartAllDownloads;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem cmPauseAll;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem cmCopyURLtoClipboard;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tcFileName;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tcSize;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tcDownloaded;
        
        #line default
        #line hidden
        
        
        #line 141 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tcPercent;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn tcProgress;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tcSpeed;
        
        #line default
        #line hidden
        
        
        #line 150 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tcStatus;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tcAddedOn;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn tcCompletedOn;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl Bottom_Tabs;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer propertiesScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid propertiesGrid;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn DGPropertyName;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn DGPropertyValue;
        
        #line default
        #line hidden
        
        
        #line 179 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pnlBottomMenu;
        
        #line default
        #line hidden
        
        
        #line 181 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBottomMenuHide;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBottomMenuShow;
        
        #line default
        #line hidden
        
        
        #line 196 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b1;
        
        #line default
        #line hidden
        
        
        #line 203 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b6;
        
        #line default
        #line hidden
        
        
        #line 210 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b2;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b3;
        
        #line default
        #line hidden
        
        
        #line 224 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b4;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b5;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button b;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/IDM;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 18 "..\..\MainWindow.xaml"
            ((IDM.MainWindow)(target)).ContentRendered += new System.EventHandler(this.Window_ContentRendered1);
            
            #line default
            #line hidden
            
            #line 22 "..\..\MainWindow.xaml"
            ((IDM.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.MainWindow_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 69 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnAboutUs_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Menu = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.dgScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 5:
            this.DownloadsGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 111 "..\..\MainWindow.xaml"
            this.DownloadsGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.downloadsGrid_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 111 "..\..\MainWindow.xaml"
            this.DownloadsGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.downloadGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cmStart = ((System.Windows.Controls.MenuItem)(target));
            
            #line 115 "..\..\MainWindow.xaml"
            this.cmStart.Click += new System.Windows.RoutedEventHandler(this.btnStart_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.cmPause = ((System.Windows.Controls.MenuItem)(target));
            
            #line 116 "..\..\MainWindow.xaml"
            this.cmPause.Click += new System.Windows.RoutedEventHandler(this.btnPause_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cmDelete = ((System.Windows.Controls.MenuItem)(target));
            
            #line 118 "..\..\MainWindow.xaml"
            this.cmDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cmOpenFile = ((System.Windows.Controls.MenuItem)(target));
            
            #line 120 "..\..\MainWindow.xaml"
            this.cmOpenFile.Click += new System.Windows.RoutedEventHandler(this.cmOpenFile_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.cmOpenDownloadFolder = ((System.Windows.Controls.MenuItem)(target));
            
            #line 121 "..\..\MainWindow.xaml"
            this.cmOpenDownloadFolder.Click += new System.Windows.RoutedEventHandler(this.cmOpenDownloadFolder_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.StartAllDownloads = ((System.Windows.Controls.MenuItem)(target));
            
            #line 123 "..\..\MainWindow.xaml"
            this.StartAllDownloads.Click += new System.Windows.RoutedEventHandler(this.cmStartAll_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.cmPauseAll = ((System.Windows.Controls.MenuItem)(target));
            
            #line 124 "..\..\MainWindow.xaml"
            this.cmPauseAll.Click += new System.Windows.RoutedEventHandler(this.cmPauseAll_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.cmCopyURLtoClipboard = ((System.Windows.Controls.MenuItem)(target));
            
            #line 127 "..\..\MainWindow.xaml"
            this.cmCopyURLtoClipboard.Click += new System.Windows.RoutedEventHandler(this.cmCopyURLtoClipboard_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.tcFileName = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 15:
            this.tcSize = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 16:
            this.tcDownloaded = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 17:
            this.tcPercent = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 18:
            this.tcProgress = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 19:
            this.tcSpeed = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 20:
            this.tcStatus = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 21:
            this.tcAddedOn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 22:
            this.tcCompletedOn = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 23:
            this.Bottom_Tabs = ((System.Windows.Controls.TabControl)(target));
            return;
            case 24:
            this.propertiesScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 25:
            this.propertiesGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 26:
            this.DGPropertyName = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 27:
            this.DGPropertyValue = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 28:
            this.pnlBottomMenu = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 29:
            this.btnBottomMenuHide = ((System.Windows.Controls.Button)(target));
            
            #line 181 "..\..\MainWindow.xaml"
            this.btnBottomMenuHide.Click += new System.Windows.RoutedEventHandler(this.btnBottomMenuHide_Click);
            
            #line default
            #line hidden
            return;
            case 30:
            this.btnBottomMenuShow = ((System.Windows.Controls.Button)(target));
            
            #line 185 "..\..\MainWindow.xaml"
            this.btnBottomMenuShow.Click += new System.Windows.RoutedEventHandler(this.btnBottomMenuShow_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            this.b1 = ((System.Windows.Controls.Button)(target));
            
            #line 196 "..\..\MainWindow.xaml"
            this.b1.Click += new System.Windows.RoutedEventHandler(this.btnAddURL_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            this.b6 = ((System.Windows.Controls.Button)(target));
            
            #line 203 "..\..\MainWindow.xaml"
            this.b6.Click += new System.Windows.RoutedEventHandler(this.btnbatchDownload_Click);
            
            #line default
            #line hidden
            return;
            case 33:
            this.b2 = ((System.Windows.Controls.Button)(target));
            
            #line 210 "..\..\MainWindow.xaml"
            this.b2.Click += new System.Windows.RoutedEventHandler(this.btnStart_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            this.b3 = ((System.Windows.Controls.Button)(target));
            
            #line 217 "..\..\MainWindow.xaml"
            this.b3.Click += new System.Windows.RoutedEventHandler(this.btnPause_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            this.b4 = ((System.Windows.Controls.Button)(target));
            
            #line 224 "..\..\MainWindow.xaml"
            this.b4.Click += new System.Windows.RoutedEventHandler(this.cmPauseAll_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            this.b5 = ((System.Windows.Controls.Button)(target));
            
            #line 231 "..\..\MainWindow.xaml"
            this.b5.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 37:
            this.b = ((System.Windows.Controls.Button)(target));
            
            #line 238 "..\..\MainWindow.xaml"
            this.b.Click += new System.Windows.RoutedEventHandler(this.btnSettings_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

