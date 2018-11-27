using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverSimulator
{
    public class Field
    {
        public int Width { get; set; }
        public RoverCommands RoverCommands { get; internal set; }
        public int Height { get; set; }
        public Field(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
