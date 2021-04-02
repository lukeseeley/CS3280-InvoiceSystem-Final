using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_System.Items
{
    /// <summary>
    /// Contains all SQL statements for the Items Window
    /// </summary>
    class clsItemsSQL
    {
        #region Methods
        /// <summary>
        /// Method for returning the SQL string for a SelectAll Games request
        /// </summary>
        /// <returns>SQL query string</returns>
        public string SelectAll()
        {
            return "Select * FROM Games";
        }

        /// <summary>
        /// Method for returning the SQL string for adding a new game to the database
        /// </summary>
        /// <param name="title">The title of the new game</param>
        /// <param name="cost">The cost of the new game</param>
        /// <returns></returns>
        public string addGame(string title, float cost)
        {
            return "INSERT INTO Games (GameTitle, Cost) VALUES ('" + title + "', " + cost + ")";
        }

        /// <summary>
        /// Method for returning the SQL string for updating a game based on it's id
        /// </summary>
        /// <param name="id">The ID of the game to be updated</param>
        /// <param name="title">The new title of the game</param>
        /// <param name="cost">The new cost of the game</param>
        /// <returns>The SQL string for updating a game</returns>
        public string updateGame(int id, string title, float cost)
        {
            return "UPDATE Games SET GameTitle = '" + title + "', Cost = " + cost + " WHERE GameID = " + id;
        }

        /// <summary>
        /// Method for returning the SQL string for acquiring all Line items that have the referenced GameId
        /// </summary>
        /// <param name="id">The GameId being used for the query</param>
        /// <returns>The SQL string for all LineItems by gameId</returns>
        public string selectLineItemsByGameId(int id)
        {
            return "SELECT * FROM LineItems WHERE GameID = " + id;
        }

        public string deleteGame(int id)
        {
            return "DELETE FROM Games WHERE GameID = " + id;
        }

        
        #endregion

    }
}
