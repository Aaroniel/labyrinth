using SFML.Graphics;
using SFML.Window;

namespace Labyrinth
{
    class Program
    {
        private static readonly uint Width = 1200;
        private static readonly uint Height = 900;
        private static readonly int Rows = (int)Height / Cell.Size;
        private static readonly int Columns = (int)Width / Cell.Size;
        private static RenderWindow window;
        private static List<Cell> gridCells;
        private static Cell currentCell;
        private static Stack<Cell> cellStack = new Stack<Cell>();
        private static Random random = new Random();

        static void Main()
        {
            InitializeWindow();
            InitializeGrid();
            MainLoop();
        }

        private static void InitializeWindow()
        {
            window = new RenderWindow(new VideoMode(Width, Height), "Labyrinth");
            window.Closed += (sender, e) => window.Close();
            window.SetFramerateLimit(144);
        }

        private static void InitializeGrid()
        {
            gridCells = new List<Cell>();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    gridCells.Add(new Cell(j, i));
                }
            }
            currentCell = gridCells[0];
            currentCell.IsVisited = true;
        }

        private static void MainLoop()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                window.Clear(new Color(48, 78, 84));

                GenerateMazeStep();

                DrawGrid();
                window.Display();
            }
        }

        private static void GenerateMazeStep()
        {
            Cell nextCell = GetUnvisitedNeighbour(currentCell);
            if (nextCell != null)
            {
                nextCell.IsVisited = true;
                RemoveWalls(currentCell, nextCell);
                cellStack.Push(currentCell);
                currentCell = nextCell;
            }
            else if (cellStack.Count > 0)
            {
                currentCell = cellStack.Pop();
            }
        }

        private static Cell GetUnvisitedNeighbour(Cell cell)
        {
            List<Cell> neighbours = new List<Cell>();

            AddNeighbour(cell.X, cell.Y - 1, neighbours); // Top
            AddNeighbour(cell.X, cell.Y + 1, neighbours); // Bottom
            AddNeighbour(cell.X - 1, cell.Y, neighbours); // Left
            AddNeighbour(cell.X + 1, cell.Y, neighbours); // Right

            if (neighbours.Count > 0)
            {
                return neighbours[random.Next(neighbours.Count)];
            }
            return null;
        }

        private static void AddNeighbour(int x, int y, List<Cell> neighbours)
        {
            Cell neighbour = GetCell(x, y);
            if (neighbour != null && !neighbour.IsVisited)
            {
                neighbours.Add(neighbour);
            }
        }

        private static Cell GetCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Columns || y >= Rows)
            {
                return null;
            }
            return gridCells[x + y * Columns];
        }

        private static void RemoveWalls(Cell current, Cell next)
        {
            int dx = current.X - next.X;
            int dy = current.Y - next.Y;

            if (dx == 1)
            {
                current.Walls["left"] = false;
                next.Walls["right"] = false;
            }
            else if (dx == -1)
            {
                current.Walls["right"] = false;
                next.Walls["left"] = false;
            }
            else if (dy == 1)
            {
                current.Walls["top"] = false;
                next.Walls["bottom"] = false;
            }
            else if (dy == -1)
            {
                current.Walls["bottom"] = false;
                next.Walls["top"] = false;
            }
        }

        private static void DrawGrid()
        {
            foreach (Cell cell in gridCells)
            {
                cell.Draw(window);
            }
            currentCell.Draw(window, new Color(139, 69, 19));
        }
    }
}
