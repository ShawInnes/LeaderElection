LeaderElection
==============

(From Wikipedia)

The bully algorithm is a method in distributed computing for dynamically electing a coordinator by process ID number. The process with the highest process ID number is selected as the coordinator.

Assumptions
-----------
- The system is synchronous and uses timeout for identifying process failure.
- Allows processes to crash during execution of algorithm.
- Message delivery between processes should be reliable.
- Prior information about other process id's must be known.

Message types
-------------
- Election Message: Sent to announce faster election
- Answer Message: Respond to the election message
- Coordinator message: Sent to announce the identity of the elected process

- Assumes that system is synchronous
- Uses timeout to detect process failure/crash
- Each processor knows which processor has the higher identifier number and communicates with that

When a process P determines that the current coordinator is down because of message timeouts or failure of the coordinator to initiate a handshake, it performs the following sequence of actions:

1. P broadcasts an election message (inquiry) to all other processes with higher process IDs, expecting an "I am alive" response from them if they are alive.
2. If P hears from no process with a higher process ID than it, it wins the election and broadcasts victory.
3. If P hears from a process with a higher ID, P waits a certain amount of time for any process with a higher ID to broadcast itself as the leader. If it does not receive this message in time, it re-broadcasts the election message.
4. If P gets an election message (inquiry) from another process with a lower ID it sends an "I am alive" message back and starts new elections.

Note that if P receives a victory message from a process with a lower ID number, it immediately initiates a new election. This is how the algorithm gets its name - a process with a higher ID number will bully a lower ID process out of the coordinator position as soon as it comes online. 
