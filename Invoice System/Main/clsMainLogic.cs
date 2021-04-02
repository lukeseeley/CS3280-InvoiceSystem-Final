using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Invoice_System.Models;
using Invoice_System.Items;


namespace Invoice_System.Main
{
    class clsMainLogic
    {
        private clsMainSQL sql;
        private clsDataAccess db;

        public clsMainLogic()
        {
            sql = new clsMainSQL();
            db = new clsDataAccess();
        }
        

        /// <summary>
        /// Deletes the INVOICE with accociated with the invoiceid
        /// </summary>
        /// <param name="invoiceId">lets the system know which invoice to delete</param>
        public void DeleteInvoice(string invoiceId)
        {
            try
            {
                string sSQL = sql.DeleteInvoiceItems(invoiceId);  //  deletes items related
                db.ExecuteNonQuery(sSQL); //  Executes SQL statement
                sSQL = sql.DeleteInvoice(invoiceId); //  delete invoice   
                db.ExecuteNonQuery(sSQL); //  Executes SQL statement
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns the list of all the INVOICES in the database
        /// </summary>
        /// <returns>returns a list of invoice class objects
        /// </returns>
        public List<clsInvoice> getInvoicesList()
        {
            try
            {
                string sSQL = sql.GetAllInvoices(); ;    //Holds an SQL statement
                int iRet = 0;   //Number of return values
                DataSet ds = new DataSet(); //Holds the return values 

                List<clsInvoice> invoiceList = new List<clsInvoice>();  // Used to hold list of invoices
                clsInvoice invoice;  //  creates invoice to be put in the list

                //Extract the data and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
                {
                    //  Store the fields
                    int invoiceId = Int32.Parse(ds.Tables[0].Rows[i]["InvoiceId"].ToString()); 
                    DateTime invoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["InvoiceDate"].ToString());
                    float totalCost = float.Parse(ds.Tables[0].Rows[i]["TotalCost"].ToString());

                    //  Create the invoice
                    invoice= new clsInvoice(invoiceId, invoiceDate, totalCost);

                    //  Put it in the list
                    invoiceList.Add(invoice);
                }

                //  Return List
                return invoiceList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }       
        }


        /// <summary>
        /// Returns a list of ITEMS under an
        /// invoiceID
        /// </summary>
        /// <param name="invoiceID">tdistiguishes desired results</param>
        /// <returns>Returns a list of game items</returns>
        public clsInvoice GetInvoiceByID(string invoiceID)
        {
            try
            {
                string sSQL = sql.GetInvoiceByID(invoiceID); ;    //  Holds an SQL statement
                int iRet = 0;   //  Number of return values
                DataSet ds = new DataSet();  //  Holds the return values

                //Extract the data and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                
                int invoiceId = Int32.Parse(ds.Tables[0].Rows[0]["InvoiceId"].ToString());
                DateTime invoiceDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["InvoiceDate"].ToString());
                float totalCost = float.Parse(ds.Tables[0].Rows[0]["TotalCost"].ToString());

                clsInvoice invoice = new clsInvoice(invoiceId, invoiceDate, totalCost);  //  Create invoice
                
                return invoice; //invoiceList;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of ITEMS under an
        /// invoiceID
        /// </summary>
        /// <param name="invoiceID">tdistiguishes desired results</param>
        /// <returns>Returns a list of game items</returns>
        public List<Game> PopLineItemsByID(string invoiceID)
        { 
            try
            {

                string sSQL = sql.GetGamesFromInvoiceID(invoiceID); ;    //  Holds an SQL statement
                int iRet = 0;   //  Number of return values
                DataSet ds = new DataSet(); //  Holds the return values
                List<Game> gameList = new List<Game>();  //  List to hold games
                Game game;  //  Instance of Game

                //Extract the data and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                for (int i = 0; i < iRet; i++)
                {
                    int gameId = Int32.Parse(ds.Tables[0].Rows[i]["GameId"].ToString());
                    string gameTitle = ds.Tables[0].Rows[i]["GameTitle"].ToString();
                    float cost = float.Parse(ds.Tables[0].Rows[i]["Cost"].ToString());

                    game = new Game(gameId, gameTitle, cost);  //  Create game

                    gameList.Add(game);  //  Add the game to the list
                }

                //  Return List
                return gameList;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Executes SQL Statement that adds
        /// a game to the invoice
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="gameID"></param>
        public void addItemToInvoice(string invoiceId, string gameID)
        {
            try
            {                
                string lineItemNumber = getLineItemNum(invoiceId);
                string sSQL = sql.AddLineItem(invoiceId, lineItemNumber, gameID);
                db.ExecuteNonQuery(sSQL); //  Executes SQL statement
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the highest line number for the invoice
        /// and calculates the newest one to be used
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public string getLineItemNum(string invoiceId)
        {
            try
            {
                string sSQL = sql.GetLineItem(invoiceId); ;    //  Holds an SQL statement
                int iRet = 0;   //  Number of return values
                DataSet ds = new DataSet();  //  Holds the return values

                //Extract the data and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                string highestLineNum = ds.Tables[0].Rows[0]["LineItemNum"].ToString();

                if(highestLineNum == "")
                {
                    highestLineNum = "0";
                }
                int increaseLineNum = Int32.Parse(highestLineNum) + 1;

                string newLineNum = increaseLineNum.ToString();

                return newLineNum;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Deletes a line item with the invoiceid and gameid
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="gameId"></param>
        public void DeleteLineItem(string invoiceId, string gameId)
        {
            try
            {
                string sSQL = sql.DeleteLineItem(invoiceId, gameId);
                db.ExecuteNonQuery(sSQL); //  Executes SQL statement
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates the invoice with the date and cost
        /// uses the invoiceId to locate the correct one
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceCost"></param>
        public void UpdateInvoice(string invoiceId, DateTime invoiceDate, float invoiceCost)
        {
            try
            {
                int iYear = invoiceDate.Year;
                int iMonth = invoiceDate.Month;
                int iDay = invoiceDate.Day;

                string sDate = iMonth + "/" + iDay + "/" + iYear;

                string sSQL = sql.EditInvoice(invoiceId, sDate, invoiceCost);
                db.ExecuteNonQuery(sSQL); 
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds a new invoice. A date and cost are passed in and the
        /// invoice id auto generated
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <param name="invoiceCost"></param>
        public void AddInvoice(DateTime invoiceDate, float invoiceCost)
        {
            try
            {
                int iYear = invoiceDate.Year;
                int iMonth = invoiceDate.Month;
                int iDay = invoiceDate.Day;

                string sDate = iMonth + "/" + iDay + "/" + iYear;

                string sSQL = sql.AddInvoice( sDate, invoiceCost);
                db.ExecuteNonQuery(sSQL);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This gets the newest invoice id number
        /// to be used in other functions
        /// </summary>
        /// <returns></returns>
        public string GetNewestInvoice()
        {
            try
            {
                string sSQL = sql.GetNewestInvoice(); ;    //  Holds an SQL statement
                int iRet = 0;   //  Number of return values
                DataSet ds = new DataSet();  //  Holds the return values

                //Extract the data and put them into the DataSet
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);

                string newestInvoice = ds.Tables[0].Rows[0]["InvoiceId"].ToString();

                return newestInvoice;

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }    
}
