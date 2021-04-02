using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_System.Search
{
    /// <summary>
    /// Contains all SQL statements for the Search Window
    /// </summary>
    class clsSearchSQL
    {
        /// <summary>
        /// This SQL statement queries the entire Invoice table
        /// </summary>
        /// <returns>The SQL statement that retrieves all the rows in the Invoice table</returns>
        public string SelectAll()
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// This SQL statement queries the Invoice table by invoice number
        /// </summary>
        /// <param name="sInvoiceNumber">The invoice number or ID</param>
        /// <returns>The SQL statement that retrieves all invoices with a specific invoice number or ID</returns>
        public string SelectByInvoiceNumber(string sInvoiceNumber)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceID = " + sInvoiceNumber;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// This SQL ttatement queries the Invoice table by date
        /// </summary>
        /// <param name="dInvoiceDate">The creation date of the invoice</param>
        /// <returns>The SQL statement that retrieves all invoices for a specific date</returns>
        public string SelectByDate(DateTime dInvoiceDate)
        {
            try
            {
                int iYear = dInvoiceDate.Year;
                int iMonth = dInvoiceDate.Month;
                int iDay = dInvoiceDate.Day;

                string sDate = iMonth + "/" + iDay + "/" + iYear;

                string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + sDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        /// <summary>
        /// This SQL statement queries the invoice table by total cost
        /// </summary>
        /// <param name="fTotalCost">The total cost of the invoice</param>
        /// <returns>The SQL statement that retrieves all invoices with a specific total cost</returns>
        public string SelectByTotalCost(float fTotalCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + fTotalCost;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This SQL statement queries the invoice table by all fields
        /// </summary>
        /// <param name="sInvoiceNumber">Invoice number</param>
        /// <param name="fCost">Total cost of invoice</param>
        /// <param name="dInvoiceDate">Invoice date</param>
        /// <returns></returns>
        public string SelectByNumberCostDate(string sInvoiceNumber, float fCost, DateTime dInvoiceDate)
        {

            try
            {
                bool numberExists = false;
                bool costExists = false;
                bool dateExists = false;

                numberExists = sInvoiceNumber != "" ? true : false;
                costExists = fCost != -1 ? true : false;
                dateExists = dInvoiceDate != new DateTime(0) ? true : false;

                int iYear = 0;
                int iMonth = 0;
                int iDay = 0;
                string sDate = "";

                if (dateExists)
                {
                    iYear = dInvoiceDate.Year;
                    iMonth = dInvoiceDate.Month;
                    iDay = dInvoiceDate.Day;

                    sDate = iMonth + "/" + iDay + "/" + iYear;
                }

                string sSQL = "";
                if (numberExists && costExists && dateExists)
                    sSQL = "SELECT * FROM Invoices WHERE InvoiceID = " + sInvoiceNumber +
                        " AND InvoiceDate = #" + sDate + "# AND TotalCost = " + fCost;
                else if (numberExists && costExists)
                    sSQL = "SELECT * FROM Invoices WHERE InvoiceID = " + sInvoiceNumber +
                        " AND TotalCost = " + fCost;
                else if (numberExists && dateExists)
                    sSQL = "SELECT * FROM Invoices WHERE InvoiceID = " + sInvoiceNumber +
                        " AND InvoiceDate = #" + sDate + "#";
                else if (costExists && dateExists)
                    sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + sDate + "# " +
                        " AND TotalCost = " + fCost;
                else if (costExists)
                    sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + fCost;
                else if (numberExists)
                    sSQL = "SELECT * FROM Invoices WHERE InvoiceID = " + sInvoiceNumber;
                else if (dateExists)
                    sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + dInvoiceDate + "#";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
