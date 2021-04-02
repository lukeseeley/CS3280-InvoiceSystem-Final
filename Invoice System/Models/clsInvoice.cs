using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_System.Models
{
    /// <summary>
    /// This represents an invoice retrieved from the database
    /// </summary>
    public class clsInvoice
    {
        /// <summary>
        /// The invoice number or ID
        /// </summary>
        public int iInvoiceNumber { get; set; }
        /// <summary>
        /// The date the invoice was created
        /// </summary>
        public DateTime dInvoiceDate { get; set; } 
        /// <summary>
        /// The total cost of the invoice
        /// </summary>
        public float fTotalCost { get; set; } 

        /// <summary>
        /// Constructs an clsInvoice object
        /// </summary>
        /// <param name="iInvoiceNumber">Invoice number or ID</param>
        /// <param name="dInvoiceDate"> Creation date of Invoice</param>
        /// <param name="fTotalCost"> Total cost of Invoice</param>
        public clsInvoice(int iInvoiceNumber, DateTime dInvoiceDate, float fTotalCost)  
        {
            this.iInvoiceNumber = iInvoiceNumber;
            this.dInvoiceDate = dInvoiceDate;
            this.fTotalCost = fTotalCost;
        }

        /// <summary>
        /// Overrides the ToString() method
        /// </summary>
        /// <returns>A string representation of an Invoice</returns>
        public override string ToString()
        {
            string sInvoice = "Invoice ID: " + this.iInvoiceNumber + "\nDate: " + this.dInvoiceDate.ToShortDateString() + "\nTotal Cost: $" + this.fTotalCost;

            return sInvoice;
        }
    }
}
