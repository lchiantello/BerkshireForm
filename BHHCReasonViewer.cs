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
        //BindingList<Reason> reasonList = new BindingList<Reason>();
        //BindingList<string> reasonList = new BindingList<string>();

        /**/
        ReasonMaster reasonMaster = new ReasonMaster();

        public BHHCReasonViewer()
        {
            InitializeComponent();
        }

        /*Assigning a binding list as the data source ensures that any change to the collection
            will be reflected in the listbox, removing the need to explicitly update the contents of the control*/
        private void BHHCReasonViewer_Load(object sender, EventArgs e)
        {
            reasonMaster.TableSetup();
            LoadReasons();
            
        }

        //private void InitializeReasonList()
        //{
        //    reasonList.AllowNew = true;
        //    reasonList.AllowRemove = true;
        //    reasonList.RaiseListChangedEvents = true;
        //    reasonList.AllowEdit = true;
        //}

        private void LoadReasons()
        {
            reasonList = new BindingList<Reason>(reasonMaster.Get());
            lstReasons.DataSource = reasonList;
            lstReasons.DisplayMember = "ReasonText";
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
                        //input.Close();
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
                    //reasonList.Add(newReason);
                }

            }
            catch (ReasonDataException dex)
            {
                if (dex.InnerException != null)
                    Console.WriteLine("Inner exception: {0}", dex.InnerException);
                    
                MessageBox.Show($"Please retry: {dex.ActionType} for reason {dex.InputText}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                            //add logic to update in db
                            reason.ReasonText = reasonText;
                        }
                        else
                        {
                            //add call to delete from db
                            DeleteReason(reasonIndex);
                        }
                    }
                    
                    
                }
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    //reasonIndex = lstReasons.SelectedIndex;
                    ////delete from db
                    //reasonList.RemoveAt(reasonIndex);
                    DeleteReason(lstReasons.SelectedIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteReason(int reasonIndex)
        {
            try
            {
                Reason reason = reasonList[reasonIndex];

                var deletedRows = reasonMaster.Delete(reason.Id);
                
                if (deletedRows > 0)
                {
                    Console.WriteLine($"Deleted {deletedRows} for Id = {reason.Id}");
                    reasonList.RemoveAt(reasonIndex);
                }
                else
                {
                    Console.WriteLine($"Delete failed - No rows found for Id = {reason.Id} with ReasonText = {reason.ReasonText}, located at index {reasonIndex}");
                    MessageBox.Show($"Delete failed for reason: {reason.ReasonText}");

                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
