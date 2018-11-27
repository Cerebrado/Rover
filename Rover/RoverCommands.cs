using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverSimulator
{
    public class RoverCommands
    {
        Dictionary<char, CommandDelegate> _commands;

        delegate void CommandDelegate();

        public RoverPosition Position { get; set; }
        public RoverCommands(int initialXCoord, int initialYCoord, string orientation )
        {
            Position = new RoverPosition(initialXCoord, initialYCoord, orientation,1);
            _commands = new Dictionary<char, CommandDelegate>();
            _commands.Add('R', this.RotateRight);
            _commands.Add('L', this.RotateLeft);
            _commands.Add('A', this.Advance);
        }

        private void Advance()
        {
            Position.Advance();
        }

        private void RotateLeft()
        {
            Position.RotateLeft();
        }

        private void RotateRight()
        {
            Position.RotateRight();
        }

        public bool ProcessCommands(string commands)
        {
            if (!ValidateCommands(commands))
                return false;

            foreach (var c in commands)  
                _commands[c]();
            
            return true;
        }

        private bool ValidateCommands(string commands)
        {
            var validCommands = _commands.Keys.ToList();
            foreach (var c in commands)
            {
                if (!validCommands.Contains(c))
                    return false;
            }
            return true;
        }

    }
}
