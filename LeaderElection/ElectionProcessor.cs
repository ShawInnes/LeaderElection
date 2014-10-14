using System.Collections.Generic;
using System.Linq;
using LeaderElection.Interfaces;
using LeaderElection.Messages;
using LeaderElection.Models;

namespace LeaderElection
{
    public class ElectionProcessor
    {
        private readonly IMessageService messageService;
        private readonly BullyProcess process;
        private readonly List<BullyProcess> processes = new List<BullyProcess>();

        public ElectionProcessor(IMessageService messageService, BullyProcess process)
        {
            this.messageService = messageService;
            this.process = process;

            processes.Add(process);

            messageService.MessageReceived += messageService_MessageReceived;
        }

        private void messageService_MessageReceived(object sender, MessageReceivedArgs e)
        {
            if (e.Message is CoordinatorMessage)
            {
                var message = e.Message as CoordinatorMessage;

                if (message.BullyProcess.Id < process.Id)
                    StartElection();
            }
            else if (e.Message is ElectionMessage)
            {
                var message = e.Message as ElectionMessage;

                if (message.BullyProcess.Id < process.Id)
                    SendAnswer();
            }
        }

        public BullyProcess GetLeader()
        {
            return processes.SingleOrDefault(p => p.Id == processes.Max(q => q.Id));
        }

        public void StartElection()
        {
            messageService.Send(new ElectionMessage(this.process));
        }

        public void SendAnswer()
        {
            messageService.Send(new AnswerMessage(this.process));
        }
    }
}