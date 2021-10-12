using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerkshireForm.SpecialExceptions
{
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

        public string ActionType { get; set; }
        public string InputText { get; set; }
    }
}
