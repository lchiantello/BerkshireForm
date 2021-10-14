using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerkshireForm.SpecialExceptions
{
    /*Custom exception class will be used by data access layer 
        Will allow all exceptions to be handled in the form, while differentiating handling*/
    class ReasonDataException : Exception
    {
        public ReasonDataException() : base() { }
        public ReasonDataException(string message) : base(message) { }
        public ReasonDataException(string message, Exception inner) : base(message, inner) { }
        public ReasonDataException(string message, string actionType, string inputText, Exception inner) : base(message, inner)
        {
            this.ActionType = actionType;
            this.InputText = inputText;
        }

        /*Custom properties allow better flexibility in displaying detailed exception info for developer
            without exposing underlying functionality to user*/

        public string ActionType { get; set; }
        public string InputText { get; set; }
    }
}
