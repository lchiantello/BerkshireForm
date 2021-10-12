using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BerkshireForm.Models
{
    /*Dapper contrib data annotations allow use of CRUD operations in ReasonMaster */
    [Table("Reason")]

    /*Implement INotifyPropertyChanged to notify the bound Listbox of changes
     * to the Reason text without resetting the binding*/
    class Reason : INotifyPropertyChanged
    {
        private int _id = 0;
        private string _reasonText = String.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        /*Setters for properties will call this method to raise the property changed event */
        /*CallerMemberName will sub in the name of the property raising the event*/
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [Key]
        public int Id 
        { 
            get
            {
                return this._id;
            }
            set
            {
                if(value != this._id)
                {
                    this._id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ReasonText 
        { 
            get
            {
                return this._reasonText;
            }
            set
            {
                if (value != this._reasonText)
                {
                    this._reasonText = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
