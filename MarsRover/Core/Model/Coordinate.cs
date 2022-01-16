using System;

namespace MarsRover.Core.Model
{
    public class Coordinate
    {
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

    }
}
