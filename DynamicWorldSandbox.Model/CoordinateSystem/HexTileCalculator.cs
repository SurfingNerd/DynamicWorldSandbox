using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model.CoordinateSystem
{
    public enum HexTileDirection
    {
        NorthEast = 0,
        East,
        SouthEast,
        SouthWest,
        West,
        NorthWest
    }
    
    
    

    public class HexTileCalculator : ITileFieldCalculator
    {
        public Tile[] GetAllNeighbours(Tile waterTile, World world)
        {
            return GetAllNeighbours(waterTile.X, waterTile.Y, world);
        }

        public Tile[] GetAllNeighbours(int x, int y, World world)
        {
            Tile[] result = new Tile[6];
            result[(int)HexTileDirection.NorthEast] = GetTile(x, y, world, HexTileDirection.NorthEast);
            result[(int)HexTileDirection.East] = GetTile(x, y, world, HexTileDirection.East);
            result[(int)HexTileDirection.SouthEast] = GetTile(x, y, world, HexTileDirection.SouthEast);
            result[(int)HexTileDirection.SouthWest] = GetTile(x, y, world, HexTileDirection.SouthWest);
            result[(int)HexTileDirection.West] = GetTile(x, y, world, HexTileDirection.West);
            result[(int)HexTileDirection.NorthWest] = GetTile(x, y, world, HexTileDirection.NorthWest);

            return result;
        }


        public Tile GetTile(int x, int y, World world, HexTileDirection direction)
        {
            int xNew = 0;
            int yNew = 0;

            GetTilePos(x, y, out xNew, out yNew, direction);

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

        public void GetTilePos(int x, int y, out int xOut, out int yOut , HexTileDirection direction)
        {
            switch (direction)
            {
                case HexTileDirection.NorthEast:
                    xOut = x + 1;
                    yOut = y - 1;
                    break;
                case HexTileDirection.East:
                    xOut = x + 1;
                    yOut = y;
                    break;
                case HexTileDirection.SouthEast:
                    xOut = x;
                    yOut = y + 1;
                    break;
                case HexTileDirection.SouthWest:
                    xOut = x - 1;
                    yOut = y + 1;
                    break;
                case HexTileDirection.West:
                    xOut = x - 1;
                    yOut = y;
                    break;
                case HexTileDirection.NorthWest:
                    xOut = x;
                    yOut = y - 1;
                    break;
                default:
                    //just to satisfy the compiler.
                    xOut = -1;
                    yOut = -1;
                    break;
            }
        }
    }
}
