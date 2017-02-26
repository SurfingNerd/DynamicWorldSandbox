using DynamicWorldSandbox.Model;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Updates the Z Scale of the object corresponding to the Terrain Heig of a tile.
    /// </summary>
    public class HeightRenderer : MonoBehaviour, ISquareFieldModuleTileRenderer
    {
        

        public void Init()
        {
            
        }

        public void UpdateTile(Tile tile)
        {
            double height = DynamicWorldSandbox.Model.Modules.TerrainModule.TerrainHeightModule.LastInitializedInstance.TerrainHeightValues[tile.X, tile.Y];

            if (height < DynamicWorldSandboxRunner.LastStartedInstance.GroundOfTheWorld)
            {
                height = DynamicWorldSandboxRunner.LastStartedInstance.GroundOfTheWorld + 0.001; // some minimum delta so we at least create a small cube for it.
            }
            //

            double cubeHeight = height - DynamicWorldSandboxRunner.LastStartedInstance.GroundOfTheWorld;
            double cubeCenter = cubeHeight / 2;
            gameObject.transform.position = new Vector3(tile.X, tile.Y,(float)cubeCenter);

            gameObject.transform.localScale = new Vector3(1, 1, (float)(cubeHeight / 2));
            //tile.TerrainHeight
        }
    }
}
