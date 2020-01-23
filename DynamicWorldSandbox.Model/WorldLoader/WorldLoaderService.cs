using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model.WorldLoader
{


    public class WorldLoaderService
    {
        List<ILayerLoader> LayerLoaders = new List<ILayerLoader>();


        /// <summary>
        /// File to HeightMap graphic.
        /// </summary>
        public string HeightMap { get; set; }

        /// <summary>
        /// File to map for Hydration value
        /// </summary>
        public string AquaMap { get; set; }

        public string FloraMap { get; set; }


        //public Dictionary<string, FileInfo> LocalWildlifeMap { get; set; }
        public Dictionary<string, FileInfo> CreatureMap { get; set; }


        public static WorldLoaderService InitFromDirectory(string directory)
        {
            WorldLoaderService result = new WorldLoaderService();

            DirectoryInfo dirInfo = new DirectoryInfo(directory);

            foreach (var file in dirInfo.GetFiles("*.png"))
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(file.Name).ToLower();
                if (fileName == "height" || fileName == "terrain")
                {
                    result.HeightMap = file.FullName;
                }
                else if (fileName == "aqua")
                {
                    result.AquaMap = file.FullName;
                }
                else if (fileName == "flora")
                {
                    result.FloraMap = file.FullName;
                }
            }

            return result;            
        }
    }
}
