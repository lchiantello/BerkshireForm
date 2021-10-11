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
    /*Winforms doesn't offer an input dialog, so we need to create one*/
    public partial class InputDialog : Form
    {
        //private string _reason;
        public InputDialog()
        {
            InitializeComponent();
        }

        /*Save the input to an attribute to facilitate passing it 
         * between the input dialog and the main form*/
        public string Reason
        {
            get { return txtReason.Text;  }
            set { txtReason.Text = value; }
        }

        /*Allow customization of the button text to improve reusability*/
        public string Action
        {
            get { return btnAdd.Text; }
            set { btnAdd.Text = value; }
        }
        
        /*Close dialog and skip main form's subsequent button logic*/
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*Lets the main form know that it should process the input
            Input does not need to be passed - the main form 
            can just access the relevant attribute, which supports extensibility*/
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
