using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Invoice_System.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        #region Attributes
        /// <summary>
        /// This is the logic class for this window, which handles all business logic
        /// </summary>
        private clsItemsLogic logic;

        /// <summary>
        /// This is an attribute used to pass the items list in case item changes are made
        /// </summary>
        private ObservableCollection<Game> gameList;

        /// <summary>
        /// A boolean flag for determining if the user is currently creating a new game
        /// Used by Save button to determine how to handle event
        /// </summary>
        private bool isCreatingGame;
        #endregion

        #region Methods
        /// <summary>
        /// The Constructor method for the window which initializes initial UI state
        /// </summary>
        public wndItems()
        {
            InitializeComponent();
            logic = new clsItemsLogic();
            isCreatingGame = false;

            //Set UI to initial state
            resetUI();

            //Bind the ListBox
            lstGames.ItemsSource = logic.getGamesList();
            //This is 
            gameList = logic.getGamesList();
        }

        /// <summary>
        /// Event method for handling when the window is closed, so that it stays alive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wndItemScreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            //Clean up the UI as the window is closing
            resetUI();
            logic.buildGameList();
            e.Cancel = true;
        }

        /// <summary>
        /// Method for getting a reference to the observable collection for use by the Main window to render an items drop down
        /// </summary>
        /// <returns>Reference to an Observable collection of Game objects</returns>
        public ref ObservableCollection<Game> getGameList()
        {
            if(gameList == null)
            {
                gameList = logic.getGamesList();
            }

            return ref gameList;
        }

        /// <summary>
        /// A method used to reset things back to an initial state for the UI
        /// </summary>
        private void resetUI()
        {
            //Clear UI flags
            isCreatingGame = false;

            //Clear UI Data
            txtGameCost.Text = "";
            txtGameID.Text = "";
            txtGameTitle.Text = "";
            lblInputError.Content = "";

            //Set UI state
            btnCancelChanges.IsEnabled = false;
            btnSaveChanges.IsEnabled = false;
            btnDeleteGame.IsEnabled = false;
            txtGameID.IsEnabled = false;
            txtGameCost.IsEnabled = false;
            txtGameTitle.IsEnabled = false;

            btnAddGame.IsEnabled = true;
            lstGames.IsEnabled = true;

            //Clear selected list item, if set
            lstGames.SelectedIndex = -1;
        }

        /// <summary>
        /// Event for when the list box has a different item selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstGames.SelectedIndex == -1)
            {
                logic.setGame(-1);
                return; //As it is just the selection being set to default
            }
            
            string game = lstGames.SelectedItem.ToString();
            int id = Int32.Parse(game.Split(':')[0]);

            try
            {
                logic.setGame(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Warning! The program has had the following error:\n" + MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
                return;
            }

            //Update UI
            txtGameID.Text = logic.CurrentGame.GameID.ToString();
            txtGameTitle.Text = logic.CurrentGame.GameTitle;
            txtGameCost.Text = "$ " + logic.CurrentGame.Cost.ToString();

            //Enable UI
            btnCancelChanges.IsEnabled = true;
            btnSaveChanges.IsEnabled = true;
            btnDeleteGame.IsEnabled = true;
            txtGameID.IsEnabled = true;
            txtGameCost.IsEnabled = true;
            txtGameTitle.IsEnabled = true;
        }

        /// <summary>
        /// Even for the button to return control back to the main screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            //Clean up the UI as the window closes
            logic.buildGameList();
            resetUI();
        }

        /// <summary>
        /// This is an event handler for when the cancel button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelChanges_Click(object sender, RoutedEventArgs e)
        {
            resetUI();
        }


        /// <summary>
        /// Event for handling when the new game button is clicked, and for setting up the UI for handling adding a new game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddGame_Click(object sender, RoutedEventArgs e)
        {
            //Set flag
            isCreatingGame = true;

            //Now lock UI
            lstGames.IsEnabled = false;
            btnAddGame.IsEnabled = false;
            btnDeleteGame.IsEnabled = false;

            //Now setup UI for adding new game
            btnCancelChanges.IsEnabled = true;
            btnSaveChanges.IsEnabled = true;
            txtGameID.IsEnabled = true;
            txtGameCost.IsEnabled = true;
            txtGameTitle.IsEnabled = true;

            txtGameID.Text = "TBD";
            txtGameTitle.Text = "";
            txtGameCost.Text = "$ ";
        }

        /// <summary>
        /// This method strips out the "$" from the cost string (It's purely for appearances anyways), it activates when the cost box is focused on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGameCost_GotFocus(object sender, RoutedEventArgs e)
        {
            txtGameCost.Text = txtGameCost.Text.Replace('$', ' ').Trim();
        }

        /// <summary>
        /// This method filters the text for any non numeric or period characters, then adds back in the $ for appearances once focus has been lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGameCost_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = "";
            string oldText = txtGameCost.Text.Trim();
            Regex pattern = new Regex(@"[0-9.]");

            for (int i = 0; i < oldText.Length; i++)
            {
                if (pattern.IsMatch(oldText[i].ToString()))
                {
                    text += oldText[i];
                }
            }

            txtGameCost.Text = "$ " + text;
        }

        /// <summary>
        /// This method handles filtering the title once focus on the text box has been lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGameTitle_LostFocus(object sender, RoutedEventArgs e)
        {
            string text = "";
            string oldText = txtGameTitle.Text.Trim();
            Regex pattern = new Regex(@"[a-zA-Z0-9\s]");

            for (int i = 0; i < oldText.Length; i++)
            {
                if (pattern.IsMatch(oldText[i].ToString()))
                {
                    text += oldText[i];
                }
            }

            txtGameTitle.Text = text;
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string newName = txtGameTitle.Text;
            string rawCost = txtGameCost.Text.Replace('$', ' ').Trim();

            //Double check user inputs
            if (newName.Length < 1)
            {
                lblInputError.Content = "Invalid Name";
                return; //As user input is not valid
            }
            else if (rawCost.Length < 1) {
                lblInputError.Content = "Invalid Cost";
                return; //As user input was not valid
            }
            else
            {
                lblInputError.Content = "";
            }
            
            float cost = float.Parse(rawCost);

            //Then we are creating a new game in the database
            if(isCreatingGame)
            {
                logic.addGame(newName, cost);
            }
            else
            {
                logic.updateGame(newName, cost);
            }

            //Finally reset the UI
            resetUI();
        }

        private void btnDeleteGame_Click(object sender, RoutedEventArgs e)
        {
            if (logic.checkGameUsage())
            {
                MessageBox.Show("The game is currently in use by an invoice, you must remove this game from all invoices before deleting it.", "Game in use by Invoice", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            logic.deleteGame();
            resetUI();
        }

        #endregion

    }
}
