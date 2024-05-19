using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Labyrinth
{
    public class Cell
    {
        public static int Size { get; } = 20;

        public int X { get; }
        public int Y { get; }
        public bool IsVisited { get; set; }
        public Dictionary<string, bool> Walls { get; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            IsVisited = false;
            Walls = new Dictionary<string, bool>
            {
                { "top", true },
                { "left", true },
                { "right", true },
                { "bottom", true }
            };
        }

        public void Draw(RenderTarget window)
        {
            DrawCell(window);
            DrawWalls(window);
        }

        public void Draw(RenderTarget window, Color color)
        {
            RectangleShape cell = new RectangleShape(new Vector2f(Size, Size))
            {
                FillColor = color,
                Position = new Vector2f(X * Size, Y * Size)
            };
            window.Draw(cell);
        }

        private void DrawCell(RenderTarget window)
        {
            RectangleShape cell = new RectangleShape(new Vector2f(Size, Size))
            {
                FillColor = IsVisited ? Color.Black : new Color(24, 39, 42),
                Position = new Vector2f(X * Size, Y * Size)
            };
            window.Draw(cell);
        }

        private void DrawWalls(RenderTarget window)
        {
            RectangleShape wall = new RectangleShape { FillColor = Color.Red };

            if (Walls["top"])
            {
                wall.Size = new Vector2f(Size, 1);
                wall.Position = new Vector2f(X * Size, Y * Size);
                wall.Rotation = 0;
                window.Draw(wall);
            }
            if (Walls["bottom"])
            {
                wall.Size = new Vector2f(Size, 1);
                wall.Position = new Vector2f(X * Size, (Y + 1) * Size);
                wall.Rotation = 0;
                window.Draw(wall);
            }
            if (Walls["right"])
            {
                wall.Size = new Vector2f(Size, 1);
                wall.Position = new Vector2f((X + 1) * Size, Y * Size);
                wall.Rotation = 90;
                window.Draw(wall);
            }
            if (Walls["left"])
            {
                wall.Size = new Vector2f(Size, 1);
                wall.Position = new Vector2f(X * Size, Y * Size);
                wall.Rotation = 90;
                window.Draw(wall);
            }
        }
    }
}
