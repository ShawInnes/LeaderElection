using System;

namespace LeaderElection.Interfaces
{
    public interface IMessageService
    {
        void Send(IMessage electionMessage);
        event EventHandler<MessageReceivedArgs> MessageReceived;
    }
}