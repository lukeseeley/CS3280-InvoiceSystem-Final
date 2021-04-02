using Invoice_System.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_System.Items
{
    /// <summary>
    /// Contains all business logic for the Items Window
    /// </summary>
    class clsItemsLogic
    {
        #region Attributes
        /// <summary>
        /// The SQL class which builds all SQL statements
        /// </summary>
        private clsItemsSQL sql;

        /// <summary>
        /// The database access class for handling database requests
        /// </summary>
        private clsDataAccess db;

        /// <summary>
        /// The collection which stores a list of games
        /// </summary>
        private ObservableCollection<Game> gameList;

        /// <summary>
        /// This stores the currently selected game by the UI
        /// </summary>
        private Game selectedGame;

        /// <summary>
        /// The public accessor for the UI to access the current game
        /// </summary>
        public Game CurrentGame { get { return selectedGame; } }

        #endregion

        #region Methods
        /// <summary>
        /// This is the constructor method for the business logic class for the Items Windows
        /// </summary>
        public clsItemsLogic()
        {
            sql = new clsItemsSQL();
            db = new clsDataAccess();
            gameList = new ObservableCollection<Game>();
            buildGameList();
        }

        /// <summary>
        /// This method simply passes along the game list from the SQL class by reference
        /// </summary>
        /// <returns>Returns a reference to the Observable GameList for the UI to use</returns>
        public ref ObservableCollection<Game> getGamesList()
        {
            return ref gameList;
        }

        /// <summary>
        /// This method builds the game list
        /// </summary>
        public void buildGameList()
        {
            try
            {
                gameList.Clear();
                string SQL = sql.SelectAll();
                int rows = 0;

                DataSet ds = db.ExecuteSQLStatement(SQL, ref rows);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    gameList.Add(new Game(Int32.Parse(row[0].ToString()), row[1].ToString(), float.Parse(row[2].ToString())));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Used to set the currently selected game, if -1 then it is unsetting the selection.
        /// </summary>
        /// <param name="id">The ID of the game being selected, or -1 to unselect</param>
        public void setGame(int id)
        {
            try
            {
                if(id == -1) //Unselect the game
                {
                    selectedGame = null;
                    return;
                }
                
                for (int i = 0; i < gameList.Count; i++)
                {
                    if(gameList[i].GameID == id)
                    {
                        selectedGame = gameList[i];
                        return;
                    }
                }

                throw new Exception("Requested GameID not found");
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// A method used to add a new game to the database
        /// </summary>
        /// <param name="name">The name for the new game</param>
        /// <param name="cost">The cost for the new game</param>
        public void addGame(string name, float cost)
        {
            try
            {
                db.ExecuteNonQuery(sql.addGame(name, cost));
                buildGameList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// A method used to update the data of an existing game
        /// </summary>
        /// <param name="name">The new name for the currently selected game</param>
        /// <param name="cost">The new cost for the currently selected game</param>
        public void updateGame(string name, float cost)
        {
            try
            {
                if (name == selectedGame.GameTitle && cost == selectedGame.Cost)
                {
                    return; //As no changes have actually been made
                }

                db.ExecuteNonQuery(sql.updateGame(selectedGame.GameID, name, cost));
                buildGameList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Used for checking if the currently selected game is in use in an Invoice
        /// </summary>
        /// <returns>True if it is in use, false if not</returns>
        public bool checkGameUsage()
        {
            try
            {
                int rows = -1;
                DataSet ds = db.ExecuteSQLStatement(sql.selectLineItemsByGameId(selectedGame.GameID), ref rows);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if(Int32.Parse(row[2].ToString()) == selectedGame.GameID)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public void deleteGame()
        {
            try
            {
                if (checkGameUsage()) //Double check that game was checked
                {
                    throw new Exception("Game is in use by an invoice, game cannot deleted.");
                }

                db.ExecuteNonQuery(sql.deleteGame(selectedGame.GameID));
                buildGameList();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }
}
