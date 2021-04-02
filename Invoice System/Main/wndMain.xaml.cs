using System;
using System.Collections.Generic;
using System.Linq;
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
using Invoice_System.Search;
using Invoice_System.Items;
using Invoice_System.Models;
using System.Collections;
using System.Collections.ObjectModel;


namespace Invoice_System.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {

        #region Attributes

        /// <summary>
        /// This is the window to open up the items window later
        /// </summary>
        private wndItems wItemsWindow;

        /// <summary>
        /// This the window to open up the search window later
        /// </summary>
        private wndSearch wSearchWindow;

        /// <summary>
        /// This is the itemsList that is recieved from the Item's page
        /// </summary>
        private ObservableCollection<Game> itemsList;

        /// <summary>
        /// This is the logic class for this window, which handles all business logic
        /// </summary>
        private clsMainLogic clsLogic;


        #endregion


        public wndMain()
        {
            InitializeComponent();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;  //  Sets shutdown mode


            wItemsWindow = new wndItems();  //  Items Window
            itemsList = wItemsWindow.getGameList();  //  Gets the game list

            wSearchWindow = new wndSearch();  //  Search Winow

            clsLogic = new clsMainLogic();  //  Logic class

            lstInvoices.ItemsSource = clsLogic.getInvoicesList();  //  Loads the invoice list
            
        }

        #region NavigateToOtherPages
        /// <summary>
        /// Navigates to the search page
        /// Accepts the ID selected on the search
        /// page and loads the right hand side with it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblNavigateToSearch_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            wSearchWindow.ShowDialog();

            int iInvoiceID = wSearchWindow.SelectedInvoiceID;

            // Makes sure that an invoice was actually selected in wndSearch
            if (iInvoiceID != 0)
            {
                //  Load invoice items
                lstInvoiceItems.ItemsSource = clsLogic.PopLineItemsByID(iInvoiceID.ToString());

                //  creates invoice object and loads values in
                clsInvoice invoice = clsLogic.GetInvoiceByID(iInvoiceID.ToString());
                lblInvoiceNumber.Content = invoice.iInvoiceNumber;
                datePickerEdit.SelectedDate = DateTime.Parse(invoice.dInvoiceDate.ToString());
                lblTotalCost.Content =  invoice.fTotalCost;

                //  Get list of items on the invoice
                List<Game> currentItems = clsLogic.PopLineItemsByID(invoice.iInvoiceNumber.ToString());
                //  Populates current Items
                foreach (Game g in currentItems)
                {
                    cmboCurrentItemList_EditInvoice.Items.Add(g);
                }

                //  Populates available games
                foreach (Game g in itemsList)
                {
                    cmboItemList_EditInvoice.Items.Add(g);
                }

                //  Makes canvas visible
                canvasRightSide.Visibility = Visibility.Visible;
                disableEditing();
            }

            this.Show();
        }

        /// <summary>
        /// Navigates to the items page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LblNavigateToItems_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            wItemsWindow.ShowDialog();
            itemsList = wItemsWindow.getGameList();  //  Updates games list
            canvasRightSide.Visibility = Visibility.Hidden;
            this.Show();
        }
        #endregion

        #region Edit

        /// <summary>
        /// Enables the edit and delete buttons when a change is made
        /// Gets selected Id and uses it to populate the 
        /// right hand side with the invoice information 
        /// and list of items 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LstInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            //  Hides/Shows correct panels
            canvasRightSide.Visibility = Visibility.Visible;
            canvasRightSideAdd.Visibility = Visibility.Hidden;

            //  Gets invoice number to do operations
            string invoiceNum = GetSelectedInvoiceId();

            //  Get list of items on the invoice
            List<Game> currentItems = clsLogic.PopLineItemsByID(invoiceNum);

            //  Populates the list
            lstInvoiceItems.ItemsSource = clsLogic.PopLineItemsByID(invoiceNum);

            //  Populates current Items
            foreach (Game g in currentItems)
            {
                cmboCurrentItemList_EditInvoice.Items.Add(g);
            }

            //  Populates available games
            foreach (Game g in itemsList)
            {
                cmboItemList_EditInvoice.Items.Add(g);
            }

            //  Gets invoice and populates form
            clsInvoice invoice = clsLogic.GetInvoiceByID(invoiceNum);
            lblInvoiceNumber.Content = invoice.iInvoiceNumber;
            datePickerEdit.SelectedDate = DateTime.Parse(invoice.dInvoiceDate.ToString());
            lblTotalCost.Content = invoice.fTotalCost;

            disableEditing();
        }

        /// <summary>
        /// Adds the item to the invoice 
        /// and then refreshes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddItemToInvoice_EditInvoice_Click(object sender, RoutedEventArgs e)
        {   if(cmboItemList_EditInvoice.SelectedIndex != -1)
            {
                //  Get the game id and invoice id
                string selectedGame = cmboItemList_EditInvoice.SelectedItem.ToString().Split(':')[0];
                string invoiceId = lblInvoiceNumber.Content.ToString();

                //  Adds the game to the invoice
                clsLogic.addItemToInvoice(invoiceId, selectedGame);

                //  Updates Invoice (cost and date)
                UpdateInvoice();

                //  resets
                cmboItemList_EditInvoice.SelectedIndex = -1;
                UpdateEditCanvas();
            }     
            
        }

        /// <summary>
        /// Remove item from the invoice
        /// And update the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemoveItemToInvoice_EditInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (cmboItemList_EditInvoice.SelectedIndex != -1)
            {
                //  Get the game id and invoice id
                string selectedGame = cmboCurrentItemList_EditInvoice.SelectedItem.ToString().Split(':')[0];
                string invoiceId = lblInvoiceNumber.Content.ToString();

                //  Remove the game from the invoice
                clsLogic.DeleteLineItem(invoiceId, selectedGame);

                //  Resets 
                cmboItemList_EditInvoice.SelectedIndex = -1;
                UpdateEditCanvas();

                //  Updates Invoice (cost and date)
                UpdateInvoice();
            }
        }

        /// <summary>
        /// Update the date, and cost
        /// fields for the invoice
        /// </summary>
        private void UpdateInvoice()
        {
            if(lblTotalCost.Content.ToString() != "TBD")
            {
                //  Get all the labels from the edit form
                string invoiceId = lblInvoiceNumber.Content.ToString();
                DateTime invoiceDate = (DateTime)datePickerEdit.SelectedDate;
                float invoiceCost = float.Parse(lblTotalCost.Content.ToString().Trim(new char[] { '$' }));

                //  Update invoice info
                clsLogic.UpdateInvoice(invoiceId, invoiceDate, invoiceCost);

                lstInvoices.ItemsSource = clsLogic.getInvoicesList();  //  Reloads the invoice list
            }
            
        }

        /// <summary>
        /// Updates/Refreshes all the form details
        /// </summary>
        private void UpdateEditCanvas()
        {
            //  Gets an updated list of games
            List<Game> currentItems = clsLogic.PopLineItemsByID(lblInvoiceNumber.Content.ToString());

            //  Updates the game list
            lstInvoiceItems.ItemsSource = currentItems;

            //  Clears and refreshes the current items list
            cmboCurrentItemList_EditInvoice.Items.Clear();
            foreach (Game g in currentItems)
            {
                cmboCurrentItemList_EditInvoice.Items.Add(g);
            }

            //  Calculates the cost depending on 
            //  the items in the list and updates it
            float totalCost = 0;
            foreach (Game g in currentItems)
            {
                totalCost += g.Cost;
            }
            lblTotalCost.Content =  totalCost;
        }

        private void LblInvoiceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInvoice();
        }

        /// <summary>
        /// enables the editing features in the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEditInvoice_Click(object sender, RoutedEventArgs e)
        {
            datePickerEdit.IsEnabled = true;
            cmboItemList_EditInvoice.IsEnabled = true;
            btnAddItemToInvoice_EditInvoice.IsEnabled = true;
            cmboCurrentItemList_EditInvoice.IsEnabled = true;
            btnRemoveItemToInvoice_EditInvoice.IsEnabled = true;
        }

        /// <summary>
        /// Disable editing values
        /// </summary>
        private void disableEditing()
        {
            datePickerEdit.IsEnabled = false;
            cmboItemList_EditInvoice.IsEnabled = false;
            btnAddItemToInvoice_EditInvoice.IsEnabled = false;
            cmboCurrentItemList_EditInvoice.IsEnabled = false;
            btnRemoveItemToInvoice_EditInvoice.IsEnabled = false;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Gets the selected ID then deletes the
        /// items accociated with the ID
        /// then deletes the invoice associated with the ID
        /// Refreshes the invoice list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteInvoice_Click(object sender, RoutedEventArgs e)
        {
            string invoiceId = lblInvoiceNumber.Content.ToString();  //  Get ID of selected

            clsLogic.DeleteInvoice(invoiceId); //  Delete Invoice
            
            lstInvoices.ItemsSource = clsLogic.getInvoicesList();  //  Reloads the invoice list

            canvasRightSide.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Add
        /// <summary>
        /// Happens when the user clicks button to add an invoice
        /// Sets the labels and clears the Listbox
        /// disables the add and edit buttons 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddInvoice_Click(object sender, RoutedEventArgs e)
        {
            canvasRightSide.Visibility = Visibility.Hidden;
            canvasRightSideAdd.Visibility = Visibility.Visible;

            lblInvoiceNumber.Content = "TBD";
            
            lblTotalCost.Content = "TBD";

            lstInvoiceItems.ClearValue(ItemsControl.ItemsSourceProperty);

            foreach (Game g in itemsList)
            {
                cmboItemList.Items.Add(g);
            }            
            
            btnAddInvoice.IsEnabled = false;
            lstInvoiceItemsAdd.IsEnabled = false;
        }



        /// <summary>
        /// Adds an item to the invoice
        /// Takes selection from drop down 
        /// and adds it to the list item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddItemToInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (cmboItemList.SelectedIndex != -1)
            {
                //  Gets the item from the combo box and 
                //  adds it to the list of items
                Object selectedGame = cmboItemList.SelectedItem;
                lstInvoiceItemsAdd.Items.Add(selectedGame);
                foreach (Object g in lstInvoiceItemsAdd.Items)
                {
                    cmboCurrentItemList.Items.Add(g);
                }

                //  Calculates the cost depending on 
                //  the items in the list and updates it
                float totalCost = 0;
                foreach (Game g in lstInvoiceItemsAdd.Items)
                {
                    totalCost += g.Cost;
                }
                lblTotalCostAdd.Content = totalCost;

                //  resets combo box
                cmboItemList.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Removes a specific item from the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemoveItemFromInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (cmboCurrentItemList.SelectedIndex != -1) { 
                Game gameToRemove = (Game)cmboCurrentItemList.SelectedItem;
                lstInvoiceItemsAdd.Items.Remove(gameToRemove);
                cmboCurrentItemList.SelectedIndex = -1;
                cmboCurrentItemList.Items.Clear();
                foreach (Object g in lstInvoiceItemsAdd.Items)
                {
                    cmboCurrentItemList.Items.Add(g);
                }

                //  Calculates the cost depending on 
                //  the items in the list and updates it
                float totalCost = 0;
                foreach (Game g in lstInvoiceItemsAdd.Items)
                {
                    totalCost += g.Cost;
                }
                lblTotalCostAdd.Content = totalCost;
            }
        }

        /// <summary>
        /// When the user cancels a new invoice
        /// Resets the right canvas and exits out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            btnAddInvoice.IsEnabled = true;
            lstInvoiceItemsAdd.IsEnabled = false;

            canvasRightSideAdd.Visibility = Visibility.Hidden;

            resetAddInvoiceCanvas();

        }

        /// <summary>
        /// When the user adds a new invoice
        /// adds the invoice to the database and 
        /// resets the add invoice canvas and 
        /// closes it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdd_AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            //  check if the form has a date and at least one item
            if(datePickerAdd.SelectedDate != null && lstInvoiceItemsAdd.Items.Count != 0)
            {
                //  add invoice
                DateTime invoiceDate = (DateTime)datePickerAdd.SelectedDate;
                float invoiceCost = float.Parse(lblTotalCostAdd.Content.ToString());
                clsLogic.AddInvoice(invoiceDate, invoiceCost);

                //  gets newest invoice to add items to
                string invoiceId = clsLogic.GetNewestInvoice();
                //  add invoice items
                foreach (Game g in lstInvoiceItemsAdd.Items)
                {
                    string gameId = g.GameID.ToString();
                    clsLogic.addItemToInvoice(invoiceId, gameId);
                }

                lstInvoices.ItemsSource = clsLogic.getInvoicesList();  //  Reloads the invoice list

                resetAddInvoiceCanvas();

                canvasRightSideAdd.Visibility = Visibility.Hidden;

            }
            resetAddInvoiceCanvas();
        }

        /// <summary>
        /// Resets the Add invoice canvas
        /// for the next use. Clears out combo
        /// boxes and hides it
        /// </summary>
        private void resetAddInvoiceCanvas()
        {
            //  clears items from the items list to add
            cmboItemList.Items.Clear();
            //  clears items from the items list of exisiting
            cmboCurrentItemList.Items.Clear();
            //  clears items from the invoices items list
            lstInvoiceItemsAdd.Items.Clear();
            // hides the canvas
            canvasRightSideAdd.Visibility = Visibility.Hidden;
        }


        #endregion

        #region Support
        /// <summary>
        /// Gets the ID of the invoice selected
        /// from the listBox of invoices. Used as support
        /// for other functions
        /// </summary>
        /// <returns>invoice ID</returns>
        private string GetSelectedInvoiceId()
        {
            if(lstInvoices.SelectedItem != null)
            {
                string invoiceId;

                invoiceId = lstInvoices.SelectedItem.ToString();  //  Gets the invoice string
                invoiceId = invoiceId.Split(' ', '\n')[2];  //  Extracts only the number

                return invoiceId;  //  return ID
            }
            return "1";
            
        }


        #endregion

        
    }  
}
