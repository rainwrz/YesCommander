﻿#pragma checksum "..\..\..\CustomControls\FollowerRow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7C73CE9EC5A65D8BEB3359A5FDD3789F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace YesCommander.CustomControls {
    
    
    /// <summary>
    /// FollowerRow
    /// </summary>
    public partial class FollowerRow : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 32 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox isFavorit;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textName;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textRace;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textClass;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textLevel;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textItemLevel;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel abilities;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel possibleAblities;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel traits;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\CustomControls\FollowerRow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textIsFrozen;
        
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
            System.Uri resourceLocater = new System.Uri("/YesCommander;component/customcontrols/followerrow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\CustomControls\FollowerRow.xaml"
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
            this.isFavorit = ((System.Windows.Controls.CheckBox)(target));
            
            #line 32 "..\..\..\CustomControls\FollowerRow.xaml"
            this.isFavorit.Checked += new System.Windows.RoutedEventHandler(this.isFavorit_Checked);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\CustomControls\FollowerRow.xaml"
            this.isFavorit.Unchecked += new System.Windows.RoutedEventHandler(this.isFavorit_Unchecked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textName = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.textRace = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.textClass = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.textLevel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.textItemLevel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.abilities = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            this.possibleAblities = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.traits = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.textIsFrozen = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

