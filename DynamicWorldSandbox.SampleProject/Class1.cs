using DynamicWorldSandbox.Engine.Tiles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicWorldSandbox.Model;
using System.IO;
using System.Drawing;

namespace DynamicWorldSandbox.SampleProject
{
    public static class Program
    {
        public static void Main()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();


            int sideSize = 100;
            DynamicWorldSandbox.Model.World world = new Model.World(sideSize, sideSize);

            watch.Stop();
            //100.000.000
            Console.WriteLine("Init took " + watch.Elapsed.TotalSeconds.ToString("#.####"));

            Console.WriteLine("Let it rain.");


            Scheduler.Scheduler sheduler = new Scheduler.Scheduler(1000);

            world.Tiles[10, 10].Hydration = 8000;


            
            world.Tiles[30, 30].TerrainHeight = -1;
            world.Tiles[29, 29].TerrainHeight = -1;
            world.Tiles[29, 30].TerrainHeight = -1;
            world.Tiles[31, 31].TerrainHeight = -1;
            world.Tiles[30, 31].TerrainHeight = -1;
            
            Console.WriteLine("After initialisation.");
            DebugWaterInfos(world);

            HydrationProcessor processor = new HydrationProcessor(world);
            sheduler.RegisterJob(processor);

            watch.Restart();
            int countOfSimulationTicks = 25000000;

            int saveImageFrequencyK = 1;

            string baseDir = Directory.GetCurrentDirectory();
            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(baseDir, DateTime.Now.ToString("mmddHHmmss")));

            dirInfo.Create();

            Console.WriteLine("Starting Simulation");
            for (int i = 0; i < countOfSimulationTicks; i++)
            {
                sheduler.RunTick(i);     
                if (i % (saveImageFrequencyK * 1000) == 0)
                {
                    //DebugWaterInfos(world);
                    SaveImage(world, i, dirInfo);
                }           
            }
            watch.Stop();

            Console.WriteLine("Simulation took " + watch.Elapsed.TotalSeconds.ToString("#.####"));
            SaveImage(world, countOfSimulationTicks, dirInfo);
            DebugWaterInfos(world);
            Console.ReadLine();
        }

        private static void DebugWaterInfos(World world)
        {
            double totalHydration = 0;

            for (int x = 0; x < world.Width; x++)
            {
                for (int y = 0; y < world.Height; y++)
                {
                    totalHydration += world.Tiles[x, y].Hydration;
                }
            }

            
            Console.WriteLine("Total Hydration: " + totalHydration.ToString());
        }

        private static void SaveImage(World world, int tickCount, DirectoryInfo dirInfo)
        {
            FileInfo waterImageFile = new FileInfo(Path.Combine(dirInfo.FullName, "Water" + (tickCount / 1000).ToString() + "K.png"));
            System.Drawing.Bitmap waterBitmap = new System.Drawing.Bitmap(world.Width, world.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);


            for (int x = 0; x < world.Width; x++)
            {
                for (int y = 0; y < world.Height; y++)
                {
                    if (world.Tiles[x, y].Hydration < 1.3)
                    {
                        int blue;
                        double blueDouble = world.Tiles[x, y].Hydration * 256;
                        if (blueDouble >= 255)
                        {
                            blue = 255;
                        }
                        else
                        {
                            blue = Convert.ToInt32(blueDouble);
                        }

                        System.Drawing.Color color = Color.FromArgb(0, 0, blue);
                        waterBitmap.SetPixel(x, y, color);
                    }
                    else
                    {
                        int red;
                        double redDouble = world.Tiles[x, y].Hydration * 128;
                        if (redDouble >= 255)
                        {
                            red = 255;
                        }
                        else
                        {
                            red = Convert.ToInt32(redDouble);
                        }

                        System.Drawing.Color color = Color.FromArgb(red, 0, 0);
                        waterBitmap.SetPixel(x, y, color);
                    }
                }
            }
            waterBitmap.Save(waterImageFile.FullName);
        }
    }
}
