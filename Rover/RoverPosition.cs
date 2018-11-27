using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverSimulator
{
    public class RoverPosition
    {
        string[] _orientations = new string[] { "N", "E", "S", "W" };
        public int X{ get; set; }
        public int Y{ get; set; }
        int _orientationIdx;
        int _movementUnits;
        int _movementUnitsX;
        int _movementUnitsY;

        public int MovementUnits { get { return _movementUnits; } }
        public RoverPosition(int x, int y, string orientation, int movementUnits)
        {
            if(!_orientations.Contains(orientation))
                throw new Exception("Not Valid orientation");

            X = x;
            Y = y;
            _movementUnits = movementUnits;
            _orientationIdx = Array.IndexOf(_orientations, orientation);
            ResetMovementUnits();
        }

        public string Orientation
        {
            get{ return _orientations[_orientationIdx]; }
        }
        private void ResetMovementUnits()
        {
            switch(_orientationIdx)
            {
                case 1:
                    _movementUnitsX = _movementUnits;
                    _movementUnitsY = 0;
                    break;
                case 2:
                    _movementUnitsX = 0;
                    _movementUnitsY = -_movementUnits;
                    break;
                case 3:
                    _movementUnitsX = -_movementUnits;
                    _movementUnitsY = 0;
                    break;
                default:
                    _movementUnitsX = 0;
                    _movementUnitsY = _movementUnits;
                    break;
            }
        }


        public void RotateRight()
        {
            _orientationIdx = (_orientationIdx == _orientations.Length - 1 ? 0 : _orientationIdx + 1);
            ResetMovementUnits();
        }
        public void RotateLeft()
        {
            _orientationIdx = (_orientationIdx == 0 ? _orientations.Length - 1 : _orientationIdx - 1);
            ResetMovementUnits();
        }
        public void Advance()
        {
            X += _movementUnitsX;
            Y += _movementUnitsY;
        }

        public override string ToString()
        {
            return "X: " + X.ToString() + ", Y: " + Y.ToString() + ", Orientation: " + Orientation;
        }
    }
}
