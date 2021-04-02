using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_System.Main
{
    /// <summary>
    /// Contains all SQL statements for the Main Window
    /// </summary>
    class clsMainSQL
    {
        #region getters
        /// <summary>
        /// Returns a SQL statement to retrieve all
        /// the Invoices in database
        /// from Invoices table
        /// </summary>
        /// <returns>string</returns>
        public string GetAllInvoices()
        {
            string sSQL = "SELECT InvoiceId, InvoiceDate, TotalCost " +
                "FROM Invoices";
            return sSQL;

        }

        /// <summary>
        /// Returns a SQL statement to retrieve an invoice
        /// </summary>
        /// <param name="invoiceID">used to find invoice</param>
        /// <returns>string</returns>
        public string GetInvoiceByID(string invoiceID)
        {
            string sSQL = "SELECT InvoiceId, InvoiceDate, TotalCost " +
                "FROM Invoices " +
                "WHERE InvoiceId = " + invoiceID;
            return sSQL;
        }

        /// <summary>
        /// Returns a SQL statement to retrieve all
        /// the line items that are under an invoice
        /// </summary>
        /// <param name="invoiceID">the ID to </param>
        /// <returns>string</returns>
        public string GetLineItemsByID(string invoiceID)
        {
            string sSQL = "SELECT InvoiceID, LineItemNumber, GameID " +
                "FROM LineItems " +
                "WHERE InvoiceID = " + invoiceID;
            return sSQL;
        }

        /// <summary>
        /// Returns a SQL statement to retrieve all games
        /// that are on an invoice
        /// </summary>
        /// <param name="invoiceID">used to retrieve games</param>
        /// <returns></returns>
        public string GetGamesFromInvoiceID(string invoiceID)
        {
            string sSQL = "SELECT Games.GameID, Games.GameTitle, Games.Cost " +
                "FROM Games " +
                "INNER JOIN LineItems " +
                "ON Games.GameID = LineItems.GameID " +
                "WHERE LineItems.InvoiceID = " + invoiceID;
            return sSQL;
        }

        /// <summary>
        /// This gets the highest line item for 
        /// the invoice id
        /// </summary>
        /// <param name="invoiceId">used to get the line items</param>
        /// <returns>returns the highest line item num</returns>
        public string GetLineItem(string invoiceId)
        {
            string sSQL = "SELECT MAX(LineItemNum) AS LineItemNum " +
                "FROM LineItems " +
                "WHERE InvoiceId = " + invoiceId;
            return sSQL;
        }

        public string GetNewestInvoice()
        {
            string sSQL = "SELECT MAX(InvoiceId) AS InvoiceId " +
                "FROM Invoices";
            return sSQL;
        }

        #endregion

        #region modyfiers
        /// <summary>
        /// Returns a SQL statement that will create a new instance
        /// of the the invoice table with a date and costs
        /// </summary>
        /// <param name="invoiceID">The invoice number</param>
        /// <param name="date">New Date</param>
        /// <param name="cost">New Cost</param>
        /// <returns>string</returns>
        public string AddInvoice(string date, float cost)
        {
            string sSQL = "INSERT INTO Invoices(InvoiceDate, TotalCost) " +
                "VALUES(#" + date + "#, " + cost + ")";
            return sSQL;
        }

        /// <summary>
        /// Returns a SQL statement that creates Line Items
        /// with the use 
        /// </summary>
        /// <param name="invoiceID">Used to connect to the Invoices table</param>
        /// <param name="lineItemNum">Used to display place in Invoices table</param>
        /// <param name="gameID">Used to connect to the items table</param>
        /// <returns></returns>
        public string AddLineItem(string invoiceID, string lineItemNum, string gameID)
        {
            string sSQL = "INSERT INTO LineItems(InvoiceID, LineItemNum, GameID) " +
                "VALUES(" + invoiceID + ", " + lineItemNum + ", " + gameID + ")";
            return sSQL;

        }

        /// <summary>
        /// Returns a SQL statement to edit
        /// the date and cost on an invoice 
        /// from choosing the invoice
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <param name="date"></param>
        /// <param name="cost"></param>
        /// <returns>string</returns>
        public string EditInvoice(string invoiceID, string date, float cost)
        {
            string sSQL = "UPDATE Invoices " +
                "SET InvoiceDate = #" + date + "#, TotalCost = " + cost + " "  +
                "WHERE InvoiceID = " + invoiceID;
            return sSQL;
        }

        /// <summary>
        /// Returns a SQL statement to delete
        /// all Line Items related connected to
        /// a specific invoice
        /// </summary>
        /// <param name="invoiceNum">ID of the invoice</param>
        /// <returns>string</returns>
        public string DeleteInvoiceItems(string invoiceNum)
        {
            string sSQL = "DELETE FROM LineItems " +
                "WHERE InvoiceID = " + invoiceNum;
            return sSQL;

        }

        /// <summary>
        /// Returns a SQL statement to delete the 
        /// Invoice with a specific 
        /// </summary>
        /// <param name="invoiceNum">ID of the invoice</param>
        /// <returns>string</returns>
        public string DeleteInvoice(string invoiceNum)
        {
            string sSQL = "DELETE FROM Invoices WHERE InvoiceId = " + "11";
            return sSQL;

        }

       
        /// <summary>
        /// Deletes a specific line item using the
        /// invoice Id and the game Id
        /// </summary>s
        /// <param name="invoiceId"></param>
        /// <param name="gameId"></param>
        /// <returns>returns string to delete a line item</returns>
        public string DeleteLineItem(string invoiceId, string gameId)
        {
            string sSQL = "DELETE FROM LineItems " +
                "WHERE InvoiceID = " + invoiceId + " " +
                "AND GameID = " + gameId;
            return sSQL;
        }

        #endregion    
        
    }
}
