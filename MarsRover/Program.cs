using System;
using System.Collections.Generic;
using MarsRover.App;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Rover> RoverSquad = new List<Rover>();
            
            Console.WriteLine(" - Mars Rover - ");

            Console.WriteLine("Enter upper-right coordinates:");
            Plateau.DefineBorders(Console.ReadLine());

            for (int i = 0; i < 2; i++)
            {
                try
                {
                    Console.WriteLine("Enter landing location for rover:");
                    Rover Rover = new Rover();
                    Rover.Land(Console.ReadLine());
                    RoverSquad.Add(Rover);
                    Console.WriteLine("Enter instructions on how to explore the plateau:");
                    Rover.Operate(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }                
            }
            
            Console.WriteLine(" - - - ");

            foreach(var Rover in RoverSquad)
            {
                Console.WriteLine(Rover.State.Position.X + " " + Rover.State.Position.Y + " " + Rover.State.Direction);
            }

            Console.ReadLine();
            
        }
    }
}
