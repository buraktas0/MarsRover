using System;
using MarsRover.Core.Model;
using MarsRover.Core.Parser;

namespace MarsRover.App
{
    public class Plateau
    {
        public static Coordinate MinCoordinate { get; } = new Coordinate(0, 0);
        public static Coordinate MaxCoordinate { get; set; } = null;

        public static void DefineBorders(string coordinateMessage)
        {
            MaxCoordinate = CommunicationParser.ParseCoordinateMessage(coordinateMessage);
        }

        public static bool IsLandingPossible(RoverState state)
        {
            if (state.Position.X >= MinCoordinate.X && state.Position.X <= MaxCoordinate.X &&
                state.Position.Y >= MinCoordinate.Y && state.Position.Y <= MaxCoordinate.Y)
                return true;
            else return false;
        }

        public static bool IsMovementPossible(RoverState roverState)
        {
            switch (roverState.Direction)
            {
                case Direction.N:
                    if (roverState.Position.Y == MaxCoordinate.Y) return false;
                    break;
                case Direction.E:
                    if (roverState.Position.X == MaxCoordinate.X) return false;
                    break;
                case Direction.S:
                    if (roverState.Position.Y == MinCoordinate.Y) return false;
                    break;
                case Direction.W:
                    if (roverState.Position.X == MinCoordinate.X) return false;
                    break;
            }
            return true;
        }
    }
}
