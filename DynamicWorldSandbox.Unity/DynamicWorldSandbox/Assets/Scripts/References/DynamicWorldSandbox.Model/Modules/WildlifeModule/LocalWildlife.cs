using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DynamicWorldSandbox.Model.Wildlife
{
    /// <summary>
    /// Local Wildlife is wildlife that is to small to get handled as creature or building.
    /// basicly its measured by "Mass". Total mass of mountainflower , mass of Champions.
    /// </summary>
    public class LocalWildlife
    {
        public LocalWildlifeType Type;


        /// <summary>
        /// How Poisoness is the wildlife ?
        /// Can be used to treat deseases.
        /// </summary>
        public double Poison;

        public double GrowthRate;

        public double HydrationBestValue;

        public double HydrationDelta;

        public IDistributionFunction HydrationBestValueDistribution = new GaussianStandardFunction();

        /// <summary>
        /// Some local wildlifes 
        /// </summary>
        /// <param name="totalMass"></param>
        /// <param name="tile"></param>
        /// <returns></returns>
        //public List<IItem> GetDryingOutItem(double totalMass, Tile tile)
        //{

        //}

        public static void RandomInit(int count)
        {

        }        
    }


    public enum LocalWildlifeType
    {
        /// <summary>
        /// Flower Plants are plants that require animal fertilisation Bees in order to reproduce themselves.
        /// </summary>
        PlantFlower,

        /// <summary>
        /// SeedPlants are plants that do not require animal fertilisation.
        /// </summary>
        PlantSeed,

        /// <summary>
        /// Funghis that require dead wood in order to be able to grow.
        /// </summary>
        FungiDecomposing,

        /// <summary>
        /// Fungi that require trees in order to be able to grow.
        /// </summary>
        FungiSymbiotic
    }
}
