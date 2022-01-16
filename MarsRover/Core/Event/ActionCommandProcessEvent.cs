using System;
using MarsRover.Core.Model;
using MarsRover.Core.Parser;

namespace MarsRover.Core.Event
{
    public class ActionCommandProcessEvent
    {
        public event EventHandler<ActionType> OnActionCommandProcessed;

        public void Process(char command)
        {
            var action = CommunicationParser.ParseMovementMessage(command);

            OnActionCommandProcessed(this, action);
        }
    }
}