Feature: Electing Leader

Scenario: The process with the highest ProcessId is elected leader
  Given there are process with the ProcessIds
    | Process | Id |
    | A       | 1  |
    | B       | 3  |
    | C       | 4  |
    | D       | 2  |
  Then Process "C" should be elected leader

  Given there are process with the ProcessIds
    | Process | Id |
    | A       | 1  |
  Then Process "A" should be elected leader

Scenario: The process does not receive any responses with higher ProcessIds
  Given The current process does not have a an elected leader
  When the process broadcasts an election message to all processes with higher ProcessIds
  Then the process expects an "I am alive" response from them if they are alive