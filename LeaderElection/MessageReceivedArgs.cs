using System;
using LeaderElection.Interfaces;

namespace LeaderElection
{
    public class MessageReceivedArgs : EventArgs
    {
        public MessageReceivedArgs(IMessage message)
        {
            Message = message;
        }

        public IMessage Message { get; set; }
    }
}