using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoverSimulator
{
    class Program
    {

        static void Main(string[] args)
        {
            Field field = new Field(0,0);
            var uiOperation  = UIOperation.RequestInitialSetting;
            while (uiOperation == UIOperation.RequestInitialSetting)
            {
                uiOperation = RequestInitialSetting(ref field);
                while (uiOperation == UIOperation.RequestCommand)
                    uiOperation = RequestCommand(ref field);

            }
        }

        private static UIOperation RequestCommand(ref Field field)
        {
            Console.Write("Enter command string: ");
            string input = Console.ReadLine();
            if (input == "Q") return UIOperation.Quit;
            if (input == "R") return UIOperation.RequestInitialSetting;

            var _lastRoverGoodPosition = new RoverPosition(
                    field.RoverCommands.Position.X,
                    field.RoverCommands.Position.Y,
                    field.RoverCommands.Position.Orientation,
                    field.RoverCommands.Position.MovementUnits
                );

            var validCommands = field.RoverCommands.ProcessCommands(input);
            if (!validCommands)
            {
                Console.WriteLine(" --> FALSE: " + field.RoverCommands.Position.ToString() + " (Not Valid Command)");
                field.RoverCommands.Position = new RoverPosition(_lastRoverGoodPosition.X, _lastRoverGoodPosition.Y, _lastRoverGoodPosition.Orientation, _lastRoverGoodPosition.MovementUnits);
            }
            else if (field.RoverCommands.Position.X < 0 || field.RoverCommands.Position.X > field.Width
                || field.RoverCommands.Position.Y < 0 || field.RoverCommands.Position.Y > field.Height)
            {

                Console.WriteLine(" --> FALSE: " + field.RoverCommands.Position.ToString() + " (Rover outside boudaries, restaured to prevous position)");
                field.RoverCommands.Position = new RoverPosition(_lastRoverGoodPosition.X, _lastRoverGoodPosition.Y, _lastRoverGoodPosition.Orientation, _lastRoverGoodPosition.MovementUnits);
            }
            else
                Console.WriteLine(" --> TRUE: " + field.RoverCommands.Position.ToString());
            return UIOperation.RequestCommand;
        }

        private static UIOperation RequestInitialSetting(ref Field field)
        {
            Console.WriteLine("Enter \"Q\" = Quit         \"R\" = Restart");
            Console.Write("Enter the following values separated by comma (fieldWidth,fieldHeight, roverPostionX, roverPositionY, roverOrientation) Note that orientation must be one of the following values [N,E,S,W]: ");
            string input = Console.ReadLine();
            if (input == "Q") return UIOperation.Quit;

            int width;
            int height;
            int x;
            int y;
            string orientation;

            var inputArray = input.Split(',');
            if(inputArray.Length != 5)
            {
                Console.Write("  --> Error: You must enter the five values");
                return UIOperation.RequestInitialSetting;
            }


            bool isInteger = int.TryParse(inputArray[0], out width); ;
            if (!isInteger || width <= 0)
            {
                Console.Write("  --> Error: [fieldWidth] must be an integer greather than 0");
                return UIOperation.RequestInitialSetting;
            }

            isInteger = int.TryParse(inputArray[1], out height);
            if (!isInteger || height <= 0)
            {
                Console.Write("Error: [fieldHeight] must be an integer greather than 0.");
                return UIOperation.RequestInitialSetting;
            }
            isInteger = int.TryParse(inputArray[2], out x);
            if (!isInteger || x < 0)
            {
                Console.Write("Error: [roverPositionX] must be an integer greather than or equal to 0.");
                return UIOperation.RequestInitialSetting;
            }
            isInteger = int.TryParse(inputArray[3], out y);
            if (!isInteger || y < 0)
            {
                Console.Write("Error: [roverPositionY] must be an integer greather than or equel to 0.");
                return UIOperation.RequestInitialSetting;
            }

            orientation = inputArray[4];
            if (orientation.Length != 1 || !"NESW".Contains(orientation))
            {
                Console.Write("Error: roverOrientation must be one of the following letters [N,E,S,W].");
                return UIOperation.RequestInitialSetting;
            }
            field = new Field(width, height);
            field.RoverCommands = new RoverCommands(x, y, inputArray[4]);

            return UIOperation.RequestCommand;

        }

        enum UIOperation { 
            Quit, RequestInitialSetting, RequestCommand
        }
    }
}
