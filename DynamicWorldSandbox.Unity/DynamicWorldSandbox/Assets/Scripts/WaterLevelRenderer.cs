using DynamicWorldSandbox.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaterLevelRenderer : MonoBehaviour, ISquareFieldModuleTileRenderer
{
    public Color DeepWaterColor; 
    public Color LowerWaterColor;

    public void Init()
    {
        
    }

    public void UpdateTile(Tile tile)
    {

        double waterLevel = DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.HydrationValues[tile.X, tile.Y];
        double terrainHeight = DynamicWorldSandbox.Model.Modules.TerrainModule.TerrainHeightModule.LastInitializedInstance.TerrainHeightValues[tile.X, tile.Y];

        // if (waterLevel != 1)
        //{
        //    Debug.Log("Height: " + waterLevel);
        // }

        if (terrainHeight < DynamicWorldSandboxRunner.LastStartedInstance.GroundOfTheWorld)
        {
            terrainHeight = DynamicWorldSandboxRunner.LastStartedInstance.GroundOfTheWorld + 0.001; // some minimum delta so we at least create a small cube for it.
        }
        //

        double terrainCubeHeight = terrainHeight - DynamicWorldSandboxRunner.LastStartedInstance.GroundOfTheWorld;
        //double cubeCenter = cubeHeight / 2;

        //gameObject.transform.position = new Vector3(tile.X, tile.Y, (float)cubeCenter);
        //gameObject.transform.localScale = new Vector3(1, 1, (float)(cubeHeight / 2));

        double waterHeight = terrainCubeHeight + (waterLevel / 2) - 2; //why -1 ?

        gameObject.transform.localScale = new Vector3(1,1, (float)waterLevel);
        gameObject.transform.position = new Vector3(tile.X , tile.Y,(float)waterHeight);
        Renderer renderer = gameObject.GetComponent<Renderer>();
        renderer.material.color = waterLevel > 1 ? DeepWaterColor : LowerWaterColor;
        //tile.TerrainHeight
    }
}
