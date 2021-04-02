using Invoice_System.Main;
using Invoice_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Invoice_System.Search
{
    /// <summary>
    /// Contains all business logic for the Main Window
    /// </summary>
    public static class clsSearchLogic
    {
        /// <summary>
        /// Executes SQL on the database
        /// </summary>
        private static clsDataAccess db = new clsDataAccess();
        /// <summary>
        /// Builds SQL statements
        /// </summary>
        private static clsSearchSQL sql = new clsSearchSQL();


        /// <summary>
        /// Get the invoices in the database and show them in the listbox
        /// Updates respective combo boxes as well
        /// </summary>
        /// <param name="listbox">Invoices listbox</param>
        /// <param name="numberCombo">Invoice number combo box</param>
        /// <param name="costCombo">Invoice total cost combo box</param>
        public static void GetInvoices(ListBox listbox, ComboBox numberCombo, ComboBox costCombo)
        {
            try
            {
                List<clsInvoice> invoices = new List<clsInvoice>();
                List<Int32> invoiceNumbers = new List<Int32>();
                SortedSet<float> sortedInvoiceCosts = new SortedSet<float>();
                List<string> invoiceCosts = new List<string>();
                HashSet<DateTime> invoiceDates = new HashSet<DateTime>();

                int rows = 0;
                DataSet data = db.ExecuteSQLStatement(sql.SelectAll(), ref rows);

                // Add each invoice into a list
                foreach (DataRow r in data.Tables[0].Rows)
                {
                    int number = Int32.Parse(r[0].ToString());
                    DateTime date = DateTime.Parse(r[1].ToString());
                    float cost = float.Parse(r[2].ToString());

                    cost *= 100;
                    cost = (float)Math.Floor(cost);
                    cost /= 100;

                    invoices.Add(new clsInvoice(number, date, cost));
                    invoiceNumbers.Add(number);
                    sortedInvoiceCosts.Add(cost);
                }

                // Append $ to invoiceCosts
                foreach (float i in sortedInvoiceCosts)
                {
                    invoiceCosts.Add('$' + i.ToString());
                }

                listbox.ItemsSource = invoices;
                numberCombo.ItemsSource = invoiceNumbers;
                costCombo.ItemsSource = invoiceCosts;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all invoices with the matching cost
        /// </summary>
        /// <param name="listbox">Invoices listbox</param>
        /// <param name="totalCost">The cost parameter for query</param>
        public static void GetInvoicesByTotalCost(ListBox listbox, float totalCost)
        {
            try
            {
                List<clsInvoice> invoices = new List<clsInvoice>();

                int rows = 0;
                DataSet data = db.ExecuteSQLStatement(sql.SelectByTotalCost(totalCost), ref rows);

                // Add each invoice into a list
                foreach (DataRow r in data.Tables[0].Rows)
                {
                    int invoiceNumber = Int32.Parse(r[0].ToString());
                    DateTime invoiceDate = DateTime.Parse(r[1].ToString());
                    float cost = float.Parse(r[2].ToString());

                    cost *= 100;
                    cost = (float)Math.Floor(cost);
                    cost /= 100;

                    invoices.Add(new clsInvoice(invoiceNumber, invoiceDate, cost));
                }

                listbox.ItemsSource = invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all invoices with the matching date
        /// </summary>
        /// <param name="listbox">Invoices listbox</param>
        /// <param name="date">The date to query</param>
        public static void GetInvoicesByDate(ListBox listbox, DateTime date)
        {
            try
            {
                List<clsInvoice> invoices = new List<clsInvoice>();

                int rows = 0;
                DataSet data = db.ExecuteSQLStatement(sql.SelectByDate(date), ref rows);

                // Add each invoice into a list
                foreach (DataRow r in data.Tables[0].Rows)
                {
                    int invoiceNumber = Int32.Parse(r[0].ToString());
                    DateTime invoiceDate = DateTime.Parse(r[1].ToString());
                    float cost = float.Parse(r[2].ToString());

                    cost *= 100;
                    cost = (float)Math.Floor(cost);
                    cost /= 100;

                    invoices.Add(new clsInvoice(invoiceNumber, invoiceDate, cost));
                }

                listbox.ItemsSource = invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the invoice using the invoice number
        /// </summary>
        /// <param name="listbox">Invoices listbox</param>
        /// <param name="number">The number to query</param>
        public static void GetInvoiceByNumber(ListBox listbox, string number)
        {
            try
            {
                List<clsInvoice> invoices = new List<clsInvoice>();

                int rows = 0;
                DataSet data = db.ExecuteSQLStatement(sql.SelectByInvoiceNumber(number), ref rows);

                // Add each invoice into a list
                foreach (DataRow r in data.Tables[0].Rows)
                {
                    int invoiceNumber = Int32.Parse(r[0].ToString());
                    DateTime date = DateTime.Parse(r[1].ToString());
                    float cost = float.Parse(r[2].ToString());

                    cost *= 100;
                    cost = (float)Math.Floor(cost);
                    cost /= 100;

                    invoices.Add(new clsInvoice(invoiceNumber, date, cost));
                }

                listbox.ItemsSource = invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// A dynamic query for getting a list of invoices based on three parameters
        /// This is NOT called from the main window. Please use InitiateThiccQuery() instead
        /// </summary>
        /// <param name="listbox">Invoices listbox</param>
        /// <param name="invoiceNumber">The number for query</param>
        /// <param name="totalCost">The cost for query</param>
        /// <param name="date">The date for query</param>
        private static void GetInvoicesByNumberCostDate(ListBox listbox, string invoiceNumber, float totalCost, DateTime date)
        {
            try
            {
                List<clsInvoice> invoices = new List<clsInvoice>();

                int rows = 0;
                DataSet data = db.ExecuteSQLStatement(sql.SelectByNumberCostDate(invoiceNumber, totalCost, date), ref rows);

                // Add each invoice into a list
                foreach (DataRow r in data.Tables[0].Rows)
                {
                    int invoiceID = Int32.Parse(r[0].ToString());
                    DateTime invoiceDate = DateTime.Parse(r[1].ToString());
                    float cost = float.Parse(r[2].ToString());

                    cost *= 100;
                    cost = (float)Math.Floor(cost);
                    cost /= 100;

                    invoices.Add(new clsInvoice(invoiceID, invoiceDate, cost));
                }

                listbox.ItemsSource = invoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Entrypoint to getting invoices by number, cost, and date.
        /// </summary>
        /// <param name="invoiceListBox">Invoices listbox</param>
        /// <param name="numberComboBox">Invoice number combobox</param>
        /// <param name="costComboBox">Invoice cost combobox</param>
        /// <param name="invoiceDatePicker">Invoice date picker</param>
        public static void InitiateThiccQuery(ListBox invoiceListBox, ComboBox numberComboBox, ComboBox costComboBox, DatePicker invoiceDatePicker) {
            try
            {
                string invoiceNumber = "";
                float totalCost = -1;
                DateTime date = new DateTime(0);

                if (numberComboBox.SelectedItem != null)
                    invoiceNumber = numberComboBox.SelectedItem.ToString();
                if (costComboBox.SelectedItem != null)
                {
                    string value = costComboBox.SelectedItem.ToString();
                    totalCost = float.Parse(value.Split('$')[1]);
                }
                if (invoiceDatePicker.SelectedDate != null)
                {
                    date = (DateTime)invoiceDatePicker.SelectedDate;
                }

                if (!(invoiceNumber.Equals("") && totalCost == -1 && date == new DateTime(0)))
                    GetInvoicesByNumberCostDate(invoiceListBox, invoiceNumber, totalCost, date);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
