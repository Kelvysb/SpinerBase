#pragma checksum "..\..\..\..\Layers\FrontEnd\uscConnection.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "DF7F876F850B29F71ED018A8AA6FB914FA4E940C"
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


namespace SpinerBase.Layers.FrontEnd
{


    /// <summary>
    /// uscConnection
    /// </summary>
    public partial class uscConnection : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/SpinerBase;component/layers/frontend/uscconnection.xaml", System.UriKind.Relative);

#line 1 "..\..\..\..\Layers\FrontEnd\uscConnection.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.lblTag = ((System.Windows.Controls.Label)(target));
                    return;
                case 2:
                    this.txtDescription = ((System.Windows.Controls.TextBox)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Label lblName;
        internal System.Windows.Controls.TextBlock lblDescription;
        internal System.Windows.Controls.StackPanel stkButtons;
        internal System.Windows.Controls.Button btnPlay;
        internal System.Windows.Controls.Button btnRemove;
        internal System.Windows.Controls.Label lblInstance;
        internal System.Windows.Controls.Label lblDatabase;
        internal System.Windows.Controls.Label lblUser;
        internal System.Windows.Controls.Label lblPassword;
        internal System.Windows.Controls.Label lblType;
        internal System.Windows.Controls.Button btnConfig;
        internal System.Windows.Controls.Label lblServer;
    }
}

