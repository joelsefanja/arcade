﻿#pragma checksum "..\..\..\PauzeMenu.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4D313414F708A003258FC8FE6973E2C417B4D4CF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Arcade;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Arcade {
    
    
    /// <summary>
    /// PauzeMenu
    /// </summary>
    public partial class PauzeMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\PauzeMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas newcanvas;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\PauzeMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button VerderSpelen;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\PauzeMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Herstarten;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\PauzeMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Geluid;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\PauzeMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Hoofdmenu;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Arcade;component/pauzemenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PauzeMenu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.9.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.newcanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 27 "..\..\..\PauzeMenu.xaml"
            this.newcanvas.KeyDown += new System.Windows.Input.KeyEventHandler(this.OnKeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.VerderSpelen = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\PauzeMenu.xaml"
            this.VerderSpelen.Click += new System.Windows.RoutedEventHandler(this.VerderSpelenButtonClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Herstarten = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\PauzeMenu.xaml"
            this.Herstarten.Click += new System.Windows.RoutedEventHandler(this.HerstartenButtonClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Geluid = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\PauzeMenu.xaml"
            this.Geluid.Click += new System.Windows.RoutedEventHandler(this.GeluidButtonClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Hoofdmenu = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\..\PauzeMenu.xaml"
            this.Hoofdmenu.Click += new System.Windows.RoutedEventHandler(this.HoofmenuButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

