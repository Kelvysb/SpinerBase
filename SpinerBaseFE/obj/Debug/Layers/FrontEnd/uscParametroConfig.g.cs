﻿#pragma checksum "..\..\..\..\Layers\FrontEnd\uscParametroConfig.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7C101146FCF6C06DAB5F3D33A88A7D65E167C518"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using SpinerBase.Layers.FrontEnd;
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


namespace SpinerBase.Layers.FrontEnd {
    
    
    /// <summary>
    /// uscParameterConfig
    /// </summary>
    public partial class uscParameterConfig : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\Layers\FrontEnd\uscParametroConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTag;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Layers\FrontEnd\uscParametroConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtDescription;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Layers\FrontEnd\uscParametroConfig.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPy;
        
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
            System.Uri resourceLocater = new System.Uri("/SpinerBase;component/layers/frontend/uscparametroconfig.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Layers\FrontEnd\uscParametroConfig.xaml"
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
            this.lblTag = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.txtDescription = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\..\..\Layers\FrontEnd\uscParametroConfig.xaml"
            this.txtDescription.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtDescription_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnPy = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\Layers\FrontEnd\uscParametroConfig.xaml"
            this.btnPy.Click += new System.Windows.RoutedEventHandler(this.btnPy_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

