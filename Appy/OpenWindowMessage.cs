using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appy
{
    public class OpenWindowMessage : ValueChangedMessage<bool>
    {
        public OpenWindowMessage(bool value) : base(value)
        {
        }
    }
    public class MyMessage : ValueChangedMessage<string>
    {
        public MyMessage(string value) : base(value)
        {
        }
    }

}
