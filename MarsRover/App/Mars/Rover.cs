using System;
using MarsRover.Core.Event;
using MarsRover.Core.Model;
using MarsRover.Core.Parser;

namespace MarsRover.App
{
    public class Rover
    {
        private ActionCommandProcessEvent actionCommandProcessEvent;

        public RoverState State { get; set; }

        public void Land(string landingMessage)
        {
            var state = CommunicationParser.ParseLandingMessage(landingMessage);

            if (Plateau.IsLandingPossible(state)) State = state;
            else throw new IndexOutOfRangeException("Landing point exceeds plateau boundaries!");

            actionCommandProcessEvent = new ActionCommandProcessEvent();
            actionCommandProcessEvent.OnActionCommandProcessed += DoAction;            
        }

        public void Operate(string movementMessage)
        {
            foreach (char command in movementMessage.ToUpper())
            {
                actionCommandProcessEvent.Process(command);
            }
        }

        private void DoAction(object sender, ActionType e)
        {
            if (e == ActionType.TurnLeft)
            {
                if (State.Direction == Direction.N) State.Direction = Direction.W;
                else State.Direction--;
            }
            else if (e == ActionType.TurnRight)
            {
                if (State.Direction == Direction.W) State.Direction = Direction.N;
                else State.Direction++;
            }
            else if (e == ActionType.MoveForward)
            {
                if (Plateau.IsMovementPossible(State))
                {
                    switch (State.Direction)
                    {
                        case Direction.N:
                            State.Position.Y++;
                            break;
                        case Direction.E:
                            State.Position.X++;
                            break;
                        case Direction.S:
                            State.Position.Y--;
                            break;
                        case Direction.W:
                            State.Position.X--;
                            break;
                        default:
                            break;
                    }
                }
                else throw new IndexOutOfRangeException("Reached plateau boundaries!");
            }
        }        
    }
}
