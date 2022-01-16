using System;
using MarsRover.Core.Model;

namespace MarsRover.Core.Parser
{    
    public class CommunicationParser
    {
        public static Coordinate ParseCoordinateMessage(string message)
        {
            string[] points = message.ToUpper().Split(' ');
            return new Coordinate(Convert.ToInt32(points[0]), Convert.ToInt32(points[1]));
        }
        public static RoverState ParseLandingMessage(string message)
        {
            string[] points = message.ToUpper().Split(' ');
            return new RoverState
            {
                Position = new Coordinate(Convert.ToInt32(points[0]), Convert.ToInt32(points[1])),
                Direction = Enum.Parse<Direction>(points[2])
            };
        }
        public static ActionType ParseMovementMessage(char message)
        {
            if (message == 'L')
            {
                return ActionType.TurnLeft;
            }
            else if (message == 'R')
            {
                return ActionType.TurnRight;
            }
            else if (message == 'M')
            {
                return ActionType.MoveForward;
            }
            return ActionType.Wait;
        }
    }
}
