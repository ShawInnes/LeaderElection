using LeaderElection.Interfaces;
using LeaderElection.Messages;
using LeaderElection.Models;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace LeaderElection.Tests
{
    [TestFixture]
    public class ElectionProcessorTests
    {
        [Test]
        public void ShouldBeCreatable()
        {
            var messageService = Substitute.For<IMessageService>();
            var process = Substitute.For<BullyProcess>();

            var sut = new ElectionProcessor(messageService, process);

            sut.ShouldBeOfType<ElectionProcessor>();
        }

        [Test(Description = "P broadcasts an election message (inquiry) to all other processes with higher process IDs")]
        //, expecting an "I am alive" response from them if they are alive.
        public void ShouldSendElectionMessageOnStart()
        {
            var messageService = Substitute.For<IMessageService>();
            var process = Substitute.For<BullyProcess>();

            var sut = new ElectionProcessor(messageService, process);

            sut.StartElection();

            messageService.Received().Send(Arg.Any<ElectionMessage>());
        }

        [Test(Description = "send an 'I am alive' response to an election message if the sender has a lower process ID")]
        public void ShouldSendAnswerMessageInResponseToElectionMessagesForLowerId()
        {
            var messageService = Substitute.For<IMessageService>();
            var process = new BullyProcess { Id = 100, Process = "<local>" };

            var sut = new ElectionProcessor(messageService, process);
            
            bool wasCalled = false;
            messageService.MessageReceived += (sender, args) => wasCalled = true;
            messageService.MessageReceived += Raise.EventWith(null, new MessageReceivedArgs(new ElectionMessage(new BullyProcess { Id = 10, Process = "<remote>" })));

            wasCalled.ShouldBe(true);

            messageService.Received().Send(Arg.Any<AnswerMessage>());
        }

        [Test(Description = "If P receives a victory message from a process with a lower ID number, it immediately initiates a new election.")]
        public void ShouldStartElectionIfReceivedCoordinationMessageWithLowerId()
        {
            var messageService = Substitute.For<IMessageService>();
            var process = new BullyProcess { Id = 100, Process = "<local>" };

            var sut = new ElectionProcessor(messageService, process);

            bool wasCalled = false;
            messageService.MessageReceived += (sender, args) => wasCalled = true;
            messageService.MessageReceived += Raise.EventWith(null, new MessageReceivedArgs(new CoordinatorMessage(new BullyProcess { Id = 10, Process = "<remote>" })));

            wasCalled.ShouldBe(true);
            
            messageService.Received().Send(Arg.Any<ElectionMessage>());
        }
    }
}