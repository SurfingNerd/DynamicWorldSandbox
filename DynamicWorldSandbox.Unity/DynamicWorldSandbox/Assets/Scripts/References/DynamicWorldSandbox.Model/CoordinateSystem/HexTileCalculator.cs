using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


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

            switch (direction)
            {
                case HexTileDirection.NorthEast:
                    xNew = x + 1;
                    yNew = y - 1;
                    break;
                case HexTileDirection.East:
                    xNew = x + 1;
                    yNew = y;
                    break;
                case HexTileDirection.SouthEast:
                    xNew = x;
                    yNew = y + 1;
                    break;
                case HexTileDirection.SouthWest:
                    xNew = x - 1;
                    yNew = y + 1;
                    break;
                case HexTileDirection.West:
                    xNew = x - 1;
                    yNew = y;
                    break;
                case HexTileDirection.NorthWest:
                    xNew = x;
                    yNew = y - 1;
                    break;
                default:
                    //just to satisfy the compiler.
                    xNew = -1;
                    yNew = -1;
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
    }
}
