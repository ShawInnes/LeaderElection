using LeaderElection.Interfaces;
using LeaderElection.Models;

namespace LeaderElection.Messages
{
    /// <summary>
    ///     Respond to the election message
    /// </summary>
    public class AnswerMessage : IMessage
    {
        public AnswerMessage(BullyProcess bullyProcess)
        {
            BullyProcess = bullyProcess;
        }

        public BullyProcess BullyProcess { get; set; }
    }
}