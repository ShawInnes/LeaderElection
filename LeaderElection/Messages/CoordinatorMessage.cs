using LeaderElection.Interfaces;
using LeaderElection.Models;

namespace LeaderElection.Messages
{
    /// <summary>
    ///     Sent to announce the identity of the elected process
    /// </summary>
    public class CoordinatorMessage : IMessage
    {
        public CoordinatorMessage(BullyProcess bullyProcess)
        {
            BullyProcess = bullyProcess;
        }

        public BullyProcess BullyProcess { get; set; }
    }
}