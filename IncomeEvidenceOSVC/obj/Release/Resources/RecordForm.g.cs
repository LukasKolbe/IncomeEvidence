﻿#pragma checksum "..\..\..\Resources\RecordForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A78ABF5101EC2FCE9B9E5F95BFC8E67D51370AFB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using IncomeEvidenceOSVC.Resources;
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


namespace IncomeEvidenceOSVC.Resources {
    
    
    /// <summary>
    /// RecordForm
    /// </summary>
    public partial class RecordForm : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 39 "..\..\..\Resources\RecordForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker Date;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Resources\RecordForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IncomeTbx;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Resources\RecordForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CostsTbx;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Resources\RecordForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NoteTbx;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\Resources\RecordForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox DocumentsList;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\Resources\RecordForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveNewRecordButton;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\Resources\RecordForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OverwriteRecordButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Money Control;component/resources/recordform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Resources\RecordForm.xaml"
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
            
            #line 9 "..\..\..\Resources\RecordForm.xaml"
            ((IncomeEvidenceOSVC.Resources.RecordForm)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 15 "..\..\..\Resources\RecordForm.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CommandBinding_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Date = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.IncomeTbx = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.CostsTbx = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.NoteTbx = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.DocumentsList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 9:
            
            #line 110 "..\..\..\Resources\RecordForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewDocument_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 115 "..\..\..\Resources\RecordForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowDocument_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 120 "..\..\..\Resources\RecordForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeleteDocument_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.SaveNewRecordButton = ((System.Windows.Controls.Button)(target));
            
            #line 127 "..\..\..\Resources\RecordForm.xaml"
            this.SaveNewRecordButton.Click += new System.Windows.RoutedEventHandler(this.SaveRecordButton_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.OverwriteRecordButton = ((System.Windows.Controls.Button)(target));
            
            #line 136 "..\..\..\Resources\RecordForm.xaml"
            this.OverwriteRecordButton.Click += new System.Windows.RoutedEventHandler(this.OverwriteRecordButton_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 144 "..\..\..\Resources\RecordForm.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 8:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 94 "..\..\..\Resources\RecordForm.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.ListBoxItem_MouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

