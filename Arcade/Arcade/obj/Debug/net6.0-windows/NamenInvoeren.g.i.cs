// Updated by XamlIntelliSenseFileGenerator 15-10-2022 15:51:56
#pragma checksum "..\..\..\NamenInvoeren.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D3C1E78EF1910D133F227FA13D8EF696D7B0EFD7"
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


namespace Arcade
{


    /// <summary>
    /// NamenInvoeren
    /// </summary>
    public partial class NamenInvoeren : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 38 "..\..\..\NamenInvoeren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inputPlayer1;

#line default
#line hidden


#line 39 "..\..\..\NamenInvoeren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox inputPlayer2;

#line default
#line hidden


#line 40 "..\..\..\NamenInvoeren.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button start;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Arcade;component/nameninvoeren.xaml", System.UriKind.Relative);

#line 1 "..\..\..\NamenInvoeren.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.inputPlayer1 = ((System.Windows.Controls.TextBox)(target));

#line 38 "..\..\..\NamenInvoeren.xaml"
                    this.inputPlayer1.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.inputPlayer1_TextChanged);

#line default
#line hidden

#line 38 "..\..\..\NamenInvoeren.xaml"
                    this.inputPlayer1.GotFocus += new System.Windows.RoutedEventHandler(this.OnFocus1);

#line default
#line hidden

#line 38 "..\..\..\NamenInvoeren.xaml"
                    this.inputPlayer1.LostFocus += new System.Windows.RoutedEventHandler(this.FocusLostInvoer1);

#line default
#line hidden
                    return;
                case 2:
                    this.inputPlayer2 = ((System.Windows.Controls.TextBox)(target));

#line 39 "..\..\..\NamenInvoeren.xaml"
                    this.inputPlayer2.GotFocus += new System.Windows.RoutedEventHandler(this.OnFocus2);

#line default
#line hidden

#line 39 "..\..\..\NamenInvoeren.xaml"
                    this.inputPlayer2.LostFocus += new System.Windows.RoutedEventHandler(this.FocusLostInvoer2);

#line default
#line hidden
                    return;
                case 3:
                    this.start = ((System.Windows.Controls.Button)(target));

#line 40 "..\..\..\NamenInvoeren.xaml"
                    this.start.Click += new System.Windows.RoutedEventHandler(this.startSpel);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Grid namenGrid;
    }
}

