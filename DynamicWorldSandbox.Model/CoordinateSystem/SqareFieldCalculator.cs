using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model.CoordinateSystem
{
    public class SquareFieldCalculator : ITileFieldCalculator
    {
        public enum SquareDirection
        {
            North,
            East,
            South,
            West
        }

        Tile GetTile(int x, int y, World world, SquareDirection dir)
        {
            int xNew = 0;
            int yNew = 0;

            switch (dir)
            {
                case SquareDirection.North:
                    xNew = x;
                    yNew = y - 1;
                    break;
                case SquareDirection.East:
                    xNew = x + 1;
                    yNew = y;
                    break;
                case SquareDirection.South:
                    xNew = x;
                    yNew = y + 1;
                    break;
                case SquareDirection.West:
                    xNew = x - 1;
                    yNew = y;
                    break;
                default:
                    break;
            }

            if (xNew >= world.Width || xNew < 0)
            {
                return null;
            }
            if (yNew >= world.Height || yNew < 0)
            {
                return null;
            }

            return world.Tiles[xNew, yNew];
        }

        public Tile[] GetAllNeighbours(int x, int y, World world)
        {
            Tile[] result = new Tile[4];
            result[0] = GetTile(x, y, world, SquareDirection.North);
            result[1] = GetTile(x, y, world, SquareDirection.East);
            result[2] = GetTile(x, y, world, SquareDirection.South);
            result[3] = GetTile(x, y, world, SquareDirection.West);
            return result;
        }

    }
}
