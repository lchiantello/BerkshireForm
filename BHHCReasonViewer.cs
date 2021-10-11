using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BerkshireForm
{
    public partial class BHHCReasonViewer : Form
    {
        /*BindingList provides change notifications*/
        BindingList<string> reasonList = new BindingList<string>();
        public BHHCReasonViewer()
        {
            InitializeComponent();
        }

        /*Assigning a binding list as the data source ensures that any change to the collection
            will be reflected in the listbox, removing the need to explicitly update the contents of the control*/
        private void BHHCReasonViewer_Load(object sender, EventArgs e)
        {
            lstReasons.DataSource = reasonList;
        }

        //private void InitializeReasonList()
        //{
        //    reasonList.AllowNew = true;
        //    reasonList.AllowRemove = true;
        //    reasonList.RaiseListChangedEvents = true;
        //    reasonList.AllowEdit = true;
        //}


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

                /*If the addition was canceled, or the user didn't input any text, don't update the list*/
                if (!string.IsNullOrEmpty(newReason))
                {
                    reasonList.Add(newReason);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                //string reason = lstReasons.SelectedItem.ToString();

                int reasonIndex;
                string reason;

                /*As long as there's at least 1 item in the list, there will be a selected index
                    If a selected item is deleted, listbox automatically selects an existing item*/
                if (reasonList.Count > 0)
                {
                    /*Get the location of selected item from listbox*/
                    reasonIndex = lstReasons.SelectedIndex;

                    /*Retrieving the item directly from the binding list facilitates potential modification
                        from a list of strings to a list of objects*/
                    reason = reasonList[reasonIndex];

                    /*Minimize scope of the input form 
                        * Allow disposal as soon as we grab the input text via the "Reason" attribute*/
                    using (InputDialog input = new InputDialog())
                    {
                        /*Populate the input textbox with the current text*/
                        input.Reason = reason;

                        /*Customize button text to improve user experience*/
                        input.Action = "Save";

                        /*Ensure that the item isn't modified if the user canceled the edit*/
                        if (input.ShowDialog() == DialogResult.OK)
                        {
                            reason = input.Reason;
                        }
                    }

                    /*If the user cleared the text, delete the item*/
                    if(!String.IsNullOrEmpty(reason))
                    {
                        reasonList[reasonIndex] = reason;
                    }
                    else
                    {
                        reasonList.RemoveAt(reasonIndex);
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
                int reasonIndex;

                /*As long as there's at least 1 item in the list, there will be a selected index
                    If a selected item is deleted, listbox automatically selects an existing item*/
                if (reasonList.Count > 0)
                {
                    reasonIndex = lstReasons.SelectedIndex;
                    reasonList.RemoveAt(reasonIndex);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
