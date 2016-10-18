using DynamicWorldSandbox.Engine.UpdateStrategies;
using DynamicWorldSandbox.Model;
using DynamicWorldSandbox.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Engine.Tiles
{

    /// <summary>
    /// Updates hydration values from all fields
    /// </summary>
    public class HydrationProcessor : IEveryTickSchedule //: Processor<Tile>
    {
        World m_world;
        //IterativeGraduallyUpdateProcessor m_hydrationUpdateProcessor;  
        RandomMemoryGraduallyUpdateProcessor m_hydrationUpdateProcessor;

        double m_maxHydrationTransferPerTick;
        double m_procentualHydrationTransferPerTick;
        double m_maxWaterDropPerTick;
        double m_maxWaterDropPerTickProcentual;
        

        HashSet<Tile> m_waterTiles = new HashSet<Tile>();

        public HydrationProcessor(DynamicWorldSandbox.Model.World world, int updateDistance = 13, double maxHydrationTransferPerTick = 0.5, double procentualHydrationTransferPerTick = 0.5, double maxWaterDropPerTick = 100, double maxWaterDropPerTickProcentual = 0.5)
        {
            //m_wholeWorldUpdateFrequency = wholeWorldUpdateFrequency;
            m_world = world;
            m_hydrationUpdateProcessor = new RandomMemoryGraduallyUpdateProcessor(100);
            m_hydrationUpdateProcessor.Initialize(world, new ProcessFunction(UpdateHydrationTile));

            m_maxHydrationTransferPerTick = maxHydrationTransferPerTick;
            m_procentualHydrationTransferPerTick = procentualHydrationTransferPerTick;
            m_maxWaterDropPerTick = maxWaterDropPerTick;
            m_maxWaterDropPerTickProcentual = maxWaterDropPerTickProcentual;


            for (int x = 0; x < world.Width; x++)
            {
                for (int y = 0; y < world.Height; y++)
                {
                    Tile tile = world.Tiles[x, y];
                    if (tile.Hydration > 1)
                    {
                        m_waterTiles.Add(tile);
                    }
                }
            }
        }

        public void Run(int tickNumber)
        {
            //UpdateHydrationTiles(tickNumber);
            m_hydrationUpdateProcessor.ProcessStep(tickNumber);
            UpdateWaterTiles(tickNumber);
        }

        private void UpdateWaterTiles(int tickNumber)
        {
           
            //double freeDropCalc = 0.00001; //little mathematical trick. we add a tiny amount to the drop in order to let it even flow in flat areas

            

            foreach (Tile waterTile in m_waterTiles.ToArray())
            {
               
                if (waterTile.Hydration <= 1 + m_maxWaterDropPerTickProcentual)
                {
                    //This makes a tile to just get removed if it get's close to 1.
                    m_waterTiles.Remove(waterTile);
                    continue;
                }


                //this is the line of the water surface.

                double currentWaterLevelAbsolute = waterTile.TerrainHeight + waterTile.Hydration;

                Tile[] neightbours = m_world.FieldCalculator.GetAllNeighbours(waterTile.X, waterTile.Y, m_world);
                bool isDeepestHoleInDaHood = true;
                double totalDrop = 0;
                GetNeighbourhoodInfo(waterTile, neightbours , out isDeepestHoleInDaHood, out totalDrop);

                

                
                if (!isDeepestHoleInDaHood)
                {
                    double totalSpilledWater = waterTile.Hydration * m_maxWaterDropPerTickProcentual;
                    if (totalSpilledWater > m_maxWaterDropPerTick)
                    {
                        totalSpilledWater = m_maxWaterDropPerTick;
                    }

                    //If tile converts to normal hydrated tile, instead of water tile
                    // we keep 100% hydration and spill the rest over the neightbour.s
                    //if (waterTile.Hydration - totalSpilledWater < 1)
                    //{
                    //    totalSpilledWater = waterTile.Hydration - 1;
                    //}

                    for (int i = 0; i < neightbours.Length; i++)
                    {
                        Tile neightbour = neightbours[i];
                        if (neightbour != null)
                        {
                            double neightbourWaterLevelAbsolute = neightbour.TerrainHeight + neightbour.Hydration;
                            double drop = currentWaterLevelAbsolute - neightbourWaterLevelAbsolute;

                            if ( drop > 0)
                            {
                                double dropPart = (drop / totalDrop);
                                //double destructionCalcSpilledWater = dropPart * waterTile.Hydration * m_maxWaterDropPerTickProcentual;
                                
                                double spilledWater = totalSpilledWater * dropPart;


                                double oldNeightbourHydration = neightbour.Hydration;
                                //todo: lower terrain height here if big amounts of water are dropped deep (waterfall, canyon building effect).

                                neightbour.Hydration += spilledWater;

                                if (oldNeightbourHydration <= 1 + m_maxWaterDropPerTickProcentual && neightbour.Hydration > 1 + m_maxWaterDropPerTickProcentual)
                                {
                                    if (!m_waterTiles.Contains(neightbour))
                                    {
                                        Tile[] tiles = m_world.FieldCalculator.GetAllNeighbours(waterTile.X, waterTile.Y, m_world);
                                        bool neighbourIsLowest = true;
                                        double neighboursTotalDrop = 0;
                                        GetNeighbourhoodInfo(neightbour, tiles, out neighbourIsLowest, out neighboursTotalDrop);
                                        if (!neighbourIsLowest)
                                        {
                                            m_waterTiles.Add(neightbour);
                                        }
                                    }   
                                }
                            }
                        }
                    }

                    waterTile.Hydration -= totalSpilledWater;
                }

                if (waterTile.Hydration <= 1 + m_maxWaterDropPerTickProcentual) //|| isDeepestHoleInDaHood)
                {
                    m_waterTiles.Remove(waterTile);
                }
            }
        }

        private void GetNeighbourhoodInfo(Tile waterTile, Tile[] neightbours, out bool isDeepestHoleInDaHood, out double totalDrop)
        {
            totalDrop = 0;
            isDeepestHoleInDaHood = true;

            double currentWaterLevelAbsolute = waterTile.WaterLevelHeight;
            for (int i = 0; i < neightbours.Length; i++)
            {
                Tile neightbour = neightbours[i];
                if (neightbour != null)
                {
                    double neightbourWaterLevelAbsolute = neightbour.TerrainHeight + neightbour.Hydration;

                    if (neightbourWaterLevelAbsolute <= currentWaterLevelAbsolute)
                    {
                        totalDrop += currentWaterLevelAbsolute - neightbourWaterLevelAbsolute;
                        isDeepestHoleInDaHood = false;
                    }
                }
            }
        }

        private void UpdateHydrationTile(int tickNumber, int x, int y)
        {
            Tile tile = m_world.Tiles[x, y];

            //Water Movement
            Tile[] neightbours = m_world.FieldCalculator.GetAllNeighbours(x, y, m_world);
            double[] neightboursHydration = new double[neightbours.Length];
            double thisTileTotalDehydration = 0;
            bool wasWaterTile = tile.Hydration >= 1;

            

            for (int i = 0; i < neightbours.Length; i++)
            {
                Tile neighbour = neightbours[i];
                if (neighbour != null)
                {
                    //some tiles will give the hydration out of the world.
                    //but its OK, you fall out there anywhere in the flat world...
                    double hydration = tile.Hydration * (m_procentualHydrationTransferPerTick / neightbours.Length);

                    if (hydration > m_maxHydrationTransferPerTick)
                    {
                        hydration = m_maxHydrationTransferPerTick;
                    }
                    neighbour.Hydration += hydration;



                    thisTileTotalDehydration += hydration;
                }

                //if (hydration > m_maxHydrationTransferPerTick)
                //neightboursHydration[i] = 
            }

            tile.Hydration -= thisTileTotalDehydration;

            if (tile.Hydration > 1)
            {
                bool isLowestInTheHood = true;
                foreach (Tile neighbour in neightbours)
                { 
                    if (neighbour != null && neighbour.TerrainHeight > tile.TerrainHeight)
                    {
                        isLowestInTheHood = false;
                    }
                }

                if (!isLowestInTheHood && !m_waterTiles.Contains(tile))
                {
                    m_waterTiles.Add(tile);
                }
            }

            //todo: Implement rain ?

        }
    }
}
