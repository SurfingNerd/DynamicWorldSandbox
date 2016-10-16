using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model
{
    /// <summary>
    /// Local Wildlife is wildlife that is to small to get handled as creature or building.
    /// basicly its measured by 
    /// </summary>
    public class LocalWildlife
    {
        public LocalWildlifeType Type;
        /// <summary>
        /// How Poisoness is the wildlife ?
        /// Can be used to treat deseases.
        /// </summary>
        public float Poison;


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
