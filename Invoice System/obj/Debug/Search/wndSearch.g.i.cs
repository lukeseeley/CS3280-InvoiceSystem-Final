#pragma checksum "..\..\..\Search\wndSearch.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "77A08BE42B355CF39BD9915B55D99FCC17D7CAA9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Invoice_System.Search;
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


namespace Invoice_System.Search {
    
    
    /// <summary>
    /// wndSearch
    /// </summary>
    public partial class wndSearch : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox invoice_number_combobox;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker invoice_date_datepicker;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox total_cost_combobox;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clear_selection_button;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button select_invoice_button;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox invoices_listbox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Search\wndSearch.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button go_back_button;
        
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
            System.Uri resourceLocater = new System.Uri("/Invoice System;component/search/wndsearch.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Search\wndSearch.xaml"
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
            
            #line 8 "..\..\..\Search\wndSearch.xaml"
            ((Invoice_System.Search.wndSearch)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\Search\wndSearch.xaml"
            ((Invoice_System.Search.wndSearch)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.invoice_number_combobox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 11 "..\..\..\Search\wndSearch.xaml"
            this.invoice_number_combobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GeneralSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.invoice_date_datepicker = ((System.Windows.Controls.DatePicker)(target));
            
            #line 13 "..\..\..\Search\wndSearch.xaml"
            this.invoice_date_datepicker.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.GeneralSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.total_cost_combobox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 14 "..\..\..\Search\wndSearch.xaml"
            this.total_cost_combobox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GeneralSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.clear_selection_button = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Search\wndSearch.xaml"
            this.clear_selection_button.Click += new System.Windows.RoutedEventHandler(this.clear_selection_button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.select_invoice_button = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Search\wndSearch.xaml"
            this.select_invoice_button.Click += new System.Windows.RoutedEventHandler(this.select_invoice_button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.invoices_listbox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.go_back_button = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\Search\wndSearch.xaml"
            this.go_back_button.Click += new System.Windows.RoutedEventHandler(this.go_back_button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

