using NUnit.Framework;
using MarsRover.Core.Parser;
using MarsRover.Core.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using MarsRover.App;
using System;

namespace MarsRoverTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [TestFixture]
        public class ParserTests
        {
            [Test]
            [TestCase("5 5")]
            public void ParseCoordinateMessageTest(string message)
            {
                var expected = new Coordinate(5, 5);
                var coordinate = CommunicationParser.ParseCoordinateMessage(message);
                Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(coordinate));
            }

            [Test]
            [TestCase("1 2 N")]
            public void ParseLandingMessageTest(string message)
            {
                var expected = new RoverState { Direction = Direction.N, Position = new Coordinate(1,2) };
                var state = CommunicationParser.ParseLandingMessage(message);
                Assert.AreEqual(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(state));
            }

            [Test]
            [TestCase("LMRM")]
            public void ParseMovementMessageTest(string message)
            {
                var expected = new List<ActionType> {
                    ActionType.TurnLeft,
                    ActionType.MoveForward,
                    ActionType.TurnRight,
                    ActionType.MoveForward
                };
                var actions = new List<ActionType>();
                foreach(var c in message)
                {
                    var action = CommunicationParser.ParseMovementMessage(c);
                    actions.Add(action);
                }                
                Assert.AreEqual(expected, actions);
            }


            [Test]
            [TestCase('T')]
            public void ParseMovementMessageTest(char message)
            {
                var action = CommunicationParser.ParseMovementMessage(message);
                Assert.AreEqual(ActionType.Wait, action);
            }
        }

        [TestFixture]
        public class MovementTests
        {
            [Test]
            [TestCase(3, 3, ExpectedResult = true)]
            [TestCase(6, 5, ExpectedResult = false)]
            public bool IsLandingPossibleTest(int x, int y)
            {
                Plateau.DefineBorders("5 5");
                bool success = Plateau.IsLandingPossible(new RoverState { Position = new Coordinate(x, y) });
                return success;
            }

            [Test]
            [TestCase(3, 3, Direction.N, ExpectedResult = true)]
            [TestCase(4, 5, Direction.W, ExpectedResult = true)]
            [TestCase(5, 2, Direction.E, ExpectedResult = false)]
            public bool IsMovementPossibleTest(int x, int y, Direction direction)
            {
                Plateau.DefineBorders("5 5");
                bool success = Plateau.IsMovementPossible(new RoverState { Position = new Coordinate(x, y), Direction = direction });
                return success;
            }

        }






    }
}
