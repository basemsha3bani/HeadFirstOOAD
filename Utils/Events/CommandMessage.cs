using System;
using System.Collections.Generic;
using System.Text;

namespace Utils.Events
{
    public class CommandMessage
    {
        private string id;
        private string message;

        public CommandMessage(string id, string message)
        {
            this.id = id;
            this.message = message;
        }
    }
}
