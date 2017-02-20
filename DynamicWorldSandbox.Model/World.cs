using DynamicWorldSandbox.Model.CoordinateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model
{
    public class World
    {
        public static Random Random = new Random();

        public int Width;
        public int Height;
        public Tile[,] Tiles;
        public ITileFieldCalculator FieldCalculator;

        /// <summary>
        /// How much "Biomass" a tile can have.
        /// </summary>
        public double TileFatness = 100;

        public World(int width, int height)
        {
            FieldCalculator = new SquareFieldCalculator();
            Width = width;
            Height = height;

            Tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Tiles[x, y] = new Tile(x, y);
                }
            }
        }
        
    }
}
