using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice_System.Items
{
    /// <summary>
    /// This is a data class for storing the information of a Game Item
    /// </summary>
    public class Game
    {
        #region Attributes
        /// <summary>
        /// This is tied to the database id of the game
        /// </summary>
        public int GameID { get; set; }

        /// <summary>
        /// This is a string for storing the title of a game
        /// </summary>
        public string GameTitle { get; set; }

        /// <summary>
        /// This is a float representation of a games cost
        /// </summary>
        public float Cost { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Constructor Method for a Game object
        /// </summary>
        /// <param name="id">Database id of the game</param>
        /// <param name="title">The Title of the Game</param>
        /// <param name="cost">The float of the monetary cost</param>
        public Game(int id, string title, float cost)
        {
            GameID = id;
            GameTitle = title;
            Cost = cost;
        }

        public override string ToString()
        {
            return GameID + ": " + GameTitle;
        }

        #endregion
    }
}
