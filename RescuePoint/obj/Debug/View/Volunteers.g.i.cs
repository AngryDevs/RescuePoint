﻿#pragma checksum "C:\Users\AJ\Documents\GitHub\RescuePoint\RescuePoint\View\Volunteers.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6F725FA583AE3BEDA9B20D0449BA1E9E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace RescuePoint.View {
    
    
    public partial class Page4 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Image imgEvac;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Image imgBarUp;
        
        internal System.Windows.Controls.Image imgBarDown;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Maps.Controls.Map MyMapControl;
        
        internal System.Windows.Controls.Image imgRoute;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/RescuePoint;component/View/Volunteers.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.imgEvac = ((System.Windows.Controls.Image)(this.FindName("imgEvac")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.imgBarUp = ((System.Windows.Controls.Image)(this.FindName("imgBarUp")));
            this.imgBarDown = ((System.Windows.Controls.Image)(this.FindName("imgBarDown")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.MyMapControl = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("MyMapControl")));
            this.imgRoute = ((System.Windows.Controls.Image)(this.FindName("imgRoute")));
        }
    }
}

