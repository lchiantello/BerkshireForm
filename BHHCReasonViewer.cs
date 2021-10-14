using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BerkshireForm.Models;
using BerkshireForm.SpecialExceptions;
using BerkshireForm.Database.DataAccess;

namespace BerkshireForm
{
    public partial class BHHCReasonViewer : Form
    {
        /*BindingList provides change notifications*/
        BindingList<Reason> reasonList;

        /**/
        ReasonMaster reasonMaster = ReasonMaster.GetReasonMaster();

        public BHHCReasonViewer()
        {
            InitializeComponent();
        }

        
        private void BHHCReasonViewer_Load(object sender, EventArgs e)
        {
            try
            {
                reasonMaster.TableSetup();
                LoadReasons();
            }
            catch (ReasonDataException dex)
            {
                /*Print detailed exception data to the console, but don't expose it to the user for security reasons*/
                Console.WriteLine($"Error: {dex.ActionType} failed for table {dex.InputText}");
                /*Default ToString for the exception will include all salient details, including inner exception info*/
                Console.WriteLine(dex);
                /*Plain english is better for user experience and security*/
                MessageBox.Show($"{dex.Message}");
            }
            catch (Exception ex)
            {
                /*Print detailed exception data to the console, but don't expose it to the user for security reasons*/
                Console.WriteLine(ex);
                /*Plain english is better for user experience and security*/
                MessageBox.Show("Unable to load application");
            }

        }

        private void LoadReasons()
        {
            try
            {
                /*Assigning a binding list as the data source ensures that any change to the collection
                will be reflected in the listbox, removing the need to explicitly update the contents of the control*/
                reasonList = new BindingList<Reason>(reasonMaster.Get());
                lstReasons.DataSource = reasonList;
                lstReasons.DisplayMember = "ReasonText";
            }
            catch(ReasonDataException dex)
            {
                /*Print detailed exception data to the console, but don't expose it to the user for security reasons*/
                Console.WriteLine($"Error: {dex.ActionType} failed for table {dex.InputText}");
                /*Default ToString for the exception will include all salient details, including inner exception info*/
                Console.WriteLine(dex);
                /*Plain english is better for user experience and security*/
                MessageBox.Show(dex.Message);
            }
            catch(Exception ex)
            {
                /*Print detailed exception data to the console, but don't expose it to the user for security reasons*/
                Console.WriteLine(ex);
                /*Plain english is better for user experience and security*/
                MessageBox.Show("Unable to load saved reasons");
            }
                        
        }

        private void btnAddReason_onClick(object sender, EventArgs e)
        {
            try
            {
                /*Assign to empty string to facilitate minimization of logic in input form scope*/
                string newReason = "";

                /*Minimize scope of the input form 
                 * Allow disposal as soon as we grab the input text via the "Reason" attribute*/
                using (InputDialog input = new InputDialog())
                {
                    /*Ensures that input isn't processed if user chose to cancel the addition*/
                    if (input.ShowDialog() == DialogResult.OK)
                    {
                        
                        newReason = input.Reason;
                    }                   
                }

                /*If the addition was canceled, or the user didn't input any text, don't update the list or db*/
                if (!string.IsNullOrEmpty(newReason))
                {
                    var reasonId = reasonMaster.Add(newReason);
                    reasonList.Add(new Reason
                    {
                        Id = reasonId,
                        ReasonText = newReason
                    });
                }

            }
            catch (ReasonDataException dex)
            {

                Console.WriteLine($"Error: {dex.ActionType} for new input: {dex.InputText} failed");
                Console.WriteLine(dex);
                MessageBox.Show($"{dex.Message} {dex.InputText}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Reason Add Error");
            }
            
        }

        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //string reason = lstReasons.SelectedItem.ToString();

                int reasonIndex;
                Reason reason;
                

                /*As long as there's at least 1 item in the list, there will be a selected index
                    If a selected item is deleted, listbox automatically selects an existing item*/
                if (reasonList.Count > 0)
                {
                    /*Get the location of selected item from listbox*/
                    reasonIndex = lstReasons.SelectedIndex;

                    /*Retrieving the item directly from the binding list facilitates potential modification
                        from a list of strings to a list of objects*/
                    reason = reasonList[reasonIndex];
                    

                    if (reason != null)
                    {

                        string reasonText = reason.ReasonText;
                        /*Minimize scope of the input form 
                            * Allow disposal as soon as we grab the input text via the "Reason" attribute*/
                        using (InputDialog input = new InputDialog())
                        {
                            /*Populate the input textbox with the current text*/
                            input.Reason = reasonText;

                            /*Customize button text to improve user experience*/
                            input.Action = "Save";

                            /*Ensure that the item isn't modified if the user canceled the edit*/
                            if (input.ShowDialog() == DialogResult.OK)
                            {
                                reasonText = input.Reason;
                            }
                        }

                        /*If the user cleared the text, delete the item*/
                        if (!String.IsNullOrEmpty(reasonText))
                        {
                            var success = reasonMaster.Update(reason.Id, reasonText);

                            /*Only update the bindng list text if the update to the DB was successful, to keep the data and UI in sync*/
                            if(success)
                            {
                                reason.ReasonText = reasonText;
                                Console.WriteLine($"Successfully updated reasonText for reason with Id = {reason.Id}");
                            }
                            else
                            {
                                Console.WriteLine($"Update failed for Id = {reason.Id} from ReasonText = {reason.ReasonText} to {reasonText}, located at index {reasonIndex}");
                                MessageBox.Show($"Update from {reason.ReasonText} to {reasonText} was unsuccessful");
                            }                            
                        }
                        else
                        {
                            DeleteReason(reasonIndex);
                        }
                    }                                        
                }                               
            }
            catch (ReasonDataException dex)
            {
                Console.WriteLine($"Error: {dex.ActionType} to new input: {dex.InputText} failed");
                Console.WriteLine(dex);
                MessageBox.Show($"{dex.Message} {dex.InputText}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Unable update reason");
            }
        }

        /*Remove selected listbox item by removing corresponding item from binding list*/
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                /*As long as there's at least 1 item in the list, there will be a selected index
                    If a selected item is deleted, listbox automatically selects an existing item*/
                if (reasonList.Count > 0)
                {
                    DeleteReason(lstReasons.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Unable delete reason");
            }
        }

        /*Shared by delete button and edit button (if the user clears the input textbox while editing)*/
        private void DeleteReason(int reasonIndex)
        {
            try
            {
                Reason reason = reasonList[reasonIndex];

                var success = reasonMaster.Delete(reason.Id);

                /*Only update the bindng list if the deletion from the DB was successful, to keep the data and UI in sync*/
                if (success)
                {
                    Console.WriteLine($"Successfully deleted reason with Id = {reason.Id}");
                    reasonList.RemoveAt(reasonIndex);
                }
                else
                {
                    Console.WriteLine($"Delete failed - No rows found for Id = {reason.Id} with ReasonText = {reason.ReasonText}, located at index {reasonIndex}");
                    MessageBox.Show($"Unable to delete reason: {reason.ReasonText}");

                }
                
            }
            catch (ReasonDataException dex)
            {
                Console.WriteLine($"Error: {dex.ActionType} for {dex.InputText}, located at index {reasonIndex} failed");
                Console.WriteLine(dex);
                MessageBox.Show($"{dex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("Unable delete reason");
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
