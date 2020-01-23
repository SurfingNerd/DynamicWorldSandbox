using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Engine.UpdateStrategies
{
    public class RandomMemoryGraduallyUpdateProcessor : IGraduallyUpdateProcessor
    {
        ProcessFunction m_function;

        int[] m_xValues;
        int[] m_yValues;

        int m_worldTilesNum;
        int m_updatesPerTick;
        int m_currentPosition;

        public RandomMemoryGraduallyUpdateProcessor(int updatesPerTicks)
        {
            m_updatesPerTick = updatesPerTicks;
        }

        public void Initialize(Model.World world, ProcessFunction function)
        {
            m_function = function;
            m_worldTilesNum = world.Width * world.Height;

            //HashSet<KeyValuePair<int, int>> availablePoints = new HashSet<KeyValuePair<int, int>>();


            KeyValuePair<int, int>[] kvps = new KeyValuePair<int, int>[m_worldTilesNum];

            int i = 0;
            for (int x = 0; x < world.Width; x++)
            {
                for (int y = 0; y < world.Height; y++)
                {
                    kvps[i] = new KeyValuePair<int, int>(x, y);
                    i++;
                }
            }

            //shuffle array.
            Random rng = new Random();
            int n = kvps.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                var temp = kvps[n];
                kvps[n] = kvps[k];
                kvps[k] = temp;
            }

            m_xValues = new int[m_worldTilesNum];
            m_yValues = new int[m_worldTilesNum];

            for (i = 0; i < m_worldTilesNum; i++)
            {
                m_xValues[i] = kvps[i].Key;
                m_yValues[i] = kvps[i].Value;
            }
        }

        public void ProcessStep(int tickCount)
        {
            for (int i = 0; i < m_updatesPerTick; i++)
            {
                m_currentPosition++;
                if (m_currentPosition == m_worldTilesNum)
                {
                    m_currentPosition = 0;
                }
                m_function(tickCount, m_xValues[m_currentPosition], m_yValues[m_currentPosition]);
            }
        }
    }
}
