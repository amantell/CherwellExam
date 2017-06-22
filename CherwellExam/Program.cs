using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherwellExam
{
    class Program
    {
        static void Main(string[] args)
        {
            GridBuilder gridBuilder = new GridBuilder(60, 60);
            gridBuilder.Build();
            Console.WriteLine("Write the integer coordinates of the row and column you're looking for. E.g. 0, -10, 0, 0, 10, -10 (which corresponds to v1x, v1y, v2x, v2y, v3x, v3y)");
            string input = Console.ReadLine();
            List<string> rawCoordinates = input.Split(',').ToList();
            List<int> coordinates = new List<int>();
            int result = 0;
            int numberOfCoordinates = 6;

            try
            {
                if (rawCoordinates.Count != numberOfCoordinates) throw new Exception("You must submit six coordinates");
                foreach (string coordinate in rawCoordinates)
                {
                    if (int.TryParse(coordinate, out result))
                    {
                        coordinates.Add(result);

                    } else
                    {
                        throw new Exception("You typed a coordinate that is not an integer.");
                    }
                }
                string output = gridBuilder.GetTriangle(new Coordinates() { V1x = coordinates[0], V1y = coordinates[1], V2x = coordinates[2], V2y = coordinates[3], V3x = coordinates[4], V3y = coordinates[5] });
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    class Coordinates {
        public int V1x;
        public int V1y;
        public int V2x;
        public int V2y;
        public int V3x;
        public int V3y;
    }

    class GridBuilder
    {
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const int initX = 0;
        const int initY = 0;
        const int x = 10;
        const int y = 10;

        int _gridWidth;
        int _gridHeight;
        int _currentX;
        int _currentY;
        char _currentLetter;
        Dictionary<string, Coordinates> _triangles;

        public Dictionary<string, Coordinates> Triangles
        {
            get { return _triangles; }
            set { _triangles = value; }
        }
        public GridBuilder(int gridWidth, int gridHeight)
        {
            _gridWidth = gridWidth;
            _gridHeight = 0 - gridHeight;
            _currentX = initX;
            _currentY = initY;
            _triangles = new Dictionary<string, Coordinates>();
        }

        public void Build()
        {
            int columnCounter;
            Coordinates coordinates;

            for (int iCounter = 0; iCounter < alphabet.Length; iCounter++)
            {
                if (_currentY <= _gridHeight) break;

                columnCounter = 0;
                _currentLetter = alphabet[iCounter];
                _currentY -= y;

                while (_currentX < _gridWidth)
                {
                    columnCounter++;

                    coordinates = new Coordinates();
                    coordinates.V1x = _currentX;
                    coordinates.V1y = _currentY;

                    coordinates.V2x = _currentX;
                    coordinates.V2y = _currentY + y;

                    coordinates.V3x = _currentX + x;
                    coordinates.V3y = _currentY;

                    _triangles.Add(string.Format("{0}{1}", _currentLetter, columnCounter), coordinates);

                    columnCounter++;
                    _currentX += x;

                    coordinates = new Coordinates();
                    coordinates.V1x = _currentX;
                    coordinates.V1y = _currentY + y;

                    coordinates.V2x = _currentX - x;
                    coordinates.V2y = _currentY + y;

                    coordinates.V3x = _currentX;
                    coordinates.V3y = _currentY;

                    _triangles.Add(string.Format("{0}{1}", _currentLetter, columnCounter), coordinates);
                }
                _currentX = 0;
            }
        }

        public string GetTriangle(Coordinates coordinates)
        {
            foreach (KeyValuePair<string, Coordinates> keyValue in _triangles)
            {
                if (coordinates.V1x == keyValue.Value.V1x
                    && coordinates.V1y == keyValue.Value.V1y
                    && coordinates.V2x == keyValue.Value.V2x
                    && coordinates.V2y == keyValue.Value.V2y
                    && coordinates.V3x == keyValue.Value.V3x
                    && coordinates.V3y == keyValue.Value.V3y
                    )
                {
                    return keyValue.Key;
                }
            }
            return "The coordinates do not match a row and column.";
        }
    }
}
