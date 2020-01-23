using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DynamicWorldSandbox.Model.WorldLoader
{
    public interface ILayerLoader
    {
        void Load(World world);
    }

    //public abstract class ImageLayerLoader  : ILayerLoader
    //{
    //    public string FileName { get; set; }

    //    public void Load(World world)
    //    {
    //        System.Drawing.Image bmp = System.Drawing.Bitmap.FromFile(FileName);
    //        System.Drawing.Bitmap thumbNail = 
    //            (System.Drawing.Bitmap)(bmp.GetThumbnailImage(world.Width, world.Height, new System.Drawing.Image.GetThumbnailImageAbort(AbortFunction), IntPtr.Zero));

    //        for (int x = 0; x < world.Width; x++)
    //        {
    //            for (int y = 0; y < world.Height; y++)
    //            {
    //                System.Drawing.Color color = thumbNail.GetPixel(x, y);
    //                ApplyColorInformation(world, world.Tiles[x, y], color, x, y);
    //            }
    //        }
    //    }

    //    protected abstract void ApplyColorInformation(World world, Tile tile, Color color, int x, int y);
        

    //    bool AbortFunction()
    //    {
    //        return false;
    //    }
    //}

    //public class TerrainHeightLayerLoader : ImageLayerLoader
    //{

    //    public double HeighModifier = 1;
    //    public double HeightMinus = 1.5 + 0.5 * 3;

    //    protected override void ApplyColorInformation(World world, Tile tile, Color color, int x, int y)
    //    {
    //        tile.TerrainHeight = ((color.R + color.G + color.B) - HeightMinus) * HeighModifier;
    //        if (tile.TerrainHeight < 0)
    //        {
    //            //make it an ocean tile if below 0
    //            tile.Hydration = double.MaxValue;
    //        }
    //        //tile.Hydration = 100;
    //    }
    //}
}
