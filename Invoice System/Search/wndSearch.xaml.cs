using Invoice_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Invoice_System.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// The selected invoice which is shared with wndMain.XAML
        /// Property is used to get the selected invoice.
        /// </summary>
        public int SelectedInvoiceID { get; set; }

        /// <summary>
        /// Window is loaded
        /// </summary>
        public wndSearch()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an exception\n" + MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Select invoice button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_invoice_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (invoices_listbox.SelectedItem != null)
                {
                    this.SelectedInvoiceID = ((clsInvoice)invoices_listbox.SelectedItem).iInvoiceNumber;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an exception\n" + MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// The go back button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void go_back_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an exception\n" + MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Overrides the window closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an exception\n" + MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Clear selection button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_selection_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                invoice_number_combobox.SelectedItem = null;
                invoice_date_datepicker.SelectedDate = null;
                total_cost_combobox.SelectedItem = null;

                clsSearchLogic.GetInvoices(invoices_listbox, invoice_number_combobox, total_cost_combobox);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an exception\n" + MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Runs when the window is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                clsSearchLogic.GetInvoices(invoices_listbox, invoice_number_combobox, total_cost_combobox);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an exception\n" + MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// If any of the selections change, this will be called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GeneralSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsSearchLogic.InitiateThiccQuery(invoices_listbox, invoice_number_combobox, total_cost_combobox ,invoice_date_datepicker);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an exception\n" + MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
