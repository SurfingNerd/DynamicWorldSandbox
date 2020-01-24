using DynamicWorldSandbox.Engine.UpdateStrategies;
using DynamicWorldSandbox.Model;
using DynamicWorldSandbox.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DynamicWorldSandbox.Engine.Tiles
{

    /// <summary>
    /// Updates hydration values from all fields
    /// </summary>
    public class HydrationProcessor : IEveryTickSchedule //: Processor<Tile>
    {
        public World World;
        //IterativeGraduallyUpdateProcessor m_hydrationUpdateProcessor;  
        public IGraduallyUpdateProcessor HydrationUpdateProcessor;

        double m_maxHydrationTransferPerTick;
        double m_procentualHydrationTransferPerTick;
        double m_maxWaterDropPerTick;
        double m_maxWaterDropPerTickProcentual;

        bool m_isInit;

        public HydrationProcessor(DynamicWorldSandbox.Model.World world, int updateDistance = 13, double maxHydrationTransferPerTick = 0.005, double procentualHydrationTransferPerTick = 0.005, double maxWaterDropPerTick = 100, double maxWaterDropPerTickProcentual = 0.5)
        {
            //m_wholeWorldUpdateFrequency = wholeWorldUpdateFrequency;
            World = world;
            //HydrationUpdateProcessor = new RandomMemoryGraduallyUpdateProcessor(100);
            //HydrationUpdateProcessor = new IterativeGraduallyUpdateProcessor(updateDistance);
            HydrationUpdateProcessor = new PrimitiveUpdateProcessor();
            HydrationUpdateProcessor.Initialize(world, new ProcessFunction(UpdateHydrationTile));

            m_maxHydrationTransferPerTick = maxHydrationTransferPerTick;
            m_procentualHydrationTransferPerTick = procentualHydrationTransferPerTick;
            m_maxWaterDropPerTick = maxWaterDropPerTick;
            m_maxWaterDropPerTickProcentual = maxWaterDropPerTickProcentual;

        }

        public void Run(int tickNumber)
        {
            if (!m_isInit)
            {
                m_isInit = true;
            }
            
            if (m_isInit)
            {
                HydrationUpdateProcessor.ProcessStep(tickNumber);
            }
        }



        private void UpdateHydrationTile(int tickNumber, int x, int y)
        {
            Tile tile = World.Tiles[x, y];

            //Water Movement
            Tile[] neightbours = World.FieldCalculator.GetAllNeighbours(x, y, World);
            double[] neightboursPotentialDrop = new double[neightbours.Length];
            double neighboursTotalDrop = 0;
            double thisTileTotalDehydration = 0;
            bool wasWaterTile = tile.Hydration >= 1;

            

            for (int i = 0; i < neightbours.Length; i++)
            {
                Tile neighbour = neightbours[i];

                if (neighbour != null)
                {
                    if (tile.Hydration < 1)
                    {
                        //we only hydrate neighbours that is less hydrated.
                        if (tile.Hydration > neighbour.Hydration)
                        {
                            double hydration = tile.Hydration * (m_procentualHydrationTransferPerTick / neightbours.Length);
                            if (hydration > m_maxHydrationTransferPerTick)
                            {
                                hydration = m_maxHydrationTransferPerTick;
                            }
                            
                            double neibourHydration = neighbour.Hydration;
                            bool neightbourWasWater = neibourHydration >= 1;
                            neibourHydration += hydration;
                            neighbour.Hydration = neibourHydration;
                            thisTileTotalDehydration += hydration;
                        }
                    }
                    else
                    {
                        //water drop.
                        //theory:
                        //we look up all neighbours with a lower water level then we have.
                        //we sum it up (~ total water drop.)
                        if (neighbour.WaterLevelHeight < tile.WaterLevelHeight) 
                        {
                            neightboursPotentialDrop[i] = (tile.WaterLevelHeight - neighbour.WaterLevelHeight) / neightbours.Length;
                            neighboursTotalDrop += neightboursPotentialDrop[i];
                            //thisTileTotalDehydration += drop;
                            //neighbour.Hydration += drop;
                            //neightboursDrop[i] = drop;
                        }
                    }


                }

                //if (hydration > m_maxHydrationTransferPerTick)
                //neightboursHydration[i] = 
            }

            for (int i = 0; i < neightbours.Length; i++)
            {
                if (neightboursPotentialDrop[i] > 0) {
                    Tile neighbour = neightbours[i];
                    if ((neightboursPotentialDrop[i] / neighboursTotalDrop) == 1.0)
                    {
                        UnityEngine.Debug.LogWarning("100% drop detected at " + x.ToString() + " " + y.ToString() + " " + neightboursPotentialDrop[i]);                    
                    }
                    double actualDrop = (neightboursPotentialDrop[i] / neighboursTotalDrop) * neightboursPotentialDrop[i];
                    thisTileTotalDehydration += actualDrop;
                    neighbour.Hydration += actualDrop;
                }
            }

            tile.Hydration -= thisTileTotalDehydration;

            //todo: Implement rain ?

        }
    }
}
