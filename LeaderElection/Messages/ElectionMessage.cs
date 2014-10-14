using LeaderElection.Interfaces;
using LeaderElection.Models;

namespace LeaderElection.Messages
{
    /// <summary>
    ///     Sent to announce faster election
    /// </summary>
    public class ElectionMessage : IMessage
    {
        public ElectionMessage(BullyProcess bullyProcess)
        {
            BullyProcess = bullyProcess;
        }

        public BullyProcess BullyProcess { get; set; }
    }
}