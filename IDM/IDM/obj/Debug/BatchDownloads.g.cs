﻿#pragma checksum "..\..\BatchDownloads.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9EFFBA5CBB8CEF64150A742BA2105AE21A414223FE03EFFB65BDEFA1C56ED212"
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
    /// BatchDownloads
    /// </summary>
    public partial class BatchDownloads : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\BatchDownloads.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer rtScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\BatchDownloads.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RichTextBox rtDownloads;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\BatchDownloads.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.FlowDocument fdDownloads;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\BatchDownloads.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPath;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\BatchDownloads.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbDiskspace;
        
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
            System.Uri resourceLocater = new System.Uri("/IDM;component/batchdownloads.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BatchDownloads.xaml"
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
            this.rtScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.rtDownloads = ((System.Windows.Controls.RichTextBox)(target));
            return;
            case 3:
            this.fdDownloads = ((System.Windows.Documents.FlowDocument)(target));
            return;
            case 4:
            this.tbPath = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\BatchDownloads.xaml"
            this.tbPath.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbPath_Changed);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 37 "..\..\BatchDownloads.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnBrowse_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbDiskspace = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            
            #line 47 "..\..\BatchDownloads.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnAddDownloads_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

