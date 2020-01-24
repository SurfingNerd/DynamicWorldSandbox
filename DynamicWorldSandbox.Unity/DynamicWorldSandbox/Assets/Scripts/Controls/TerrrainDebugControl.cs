using DynamicWorldSandbox.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrrainDebugControl : MonoBehaviour {

    public double FillWaterAmount = 10;

    public double TerrainRaise = 2;

    // Use this for initialization
    void Start () {
		
        

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            FillWater(1);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            FillWater(2);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            FillWater(3);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            FillWater(4);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            FillWater(5);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            RaiseEarth(1);
            //FillWater(1);
        }
        if (Input.GetKey(KeyCode.Alpha7))
        {
            RaiseEarth(2);
        }
        if (Input.GetKey(KeyCode.Alpha8))
        {
            RaiseEarth(3);
        }
        if (Input.GetKey(KeyCode.Alpha9))
        {
            RaiseEarth(4);
        }

        //for( char i = '1'; i <= '5'; i++)
        //{
        //    if (Input.GetKey(new string(i)))
        //    {

        //    }
        //}
        //if (Input.GetKey("1"))
        //{
        //    FillWater()
        //}
    }

    private void RaiseEarth(int sideSize)
    {
        int playerPositionX = Convert.ToInt32(Camera.main.transform.position.x);
        int playerPositionY = Convert.ToInt32(Camera.main.transform.position.y);


        for (int offsetX = 0; offsetX < sideSize - 1; offsetX++)
        {
            for (int offsetY = 0; offsetY < sideSize - 1; offsetY++)
            {
                int tileX = playerPositionX + offsetX;
                int tileY = playerPositionY + offsetY;

                DynamicWorldSandbox.Model.Modules.TerrainModule.TerrainHeightModule.LastInitializedInstance.TerrainHeightValues[tileX, tileY] += TerrainRaise;
            }
        }
    }

    private void FillWater(int sideSize)
    {
        int playerPositionX = Convert.ToInt32(Camera.main.transform.position.x);
        int playerPositionY = Convert.ToInt32(Camera.main.transform.position.y);


        for (int offsetX = 0; offsetX < sideSize - 1; offsetX++)
        {
            for (int offsetY = 0; offsetY < sideSize - 1; offsetY++)
            {
                int tileX = playerPositionX + offsetX;
                int tileY = playerPositionY + offsetY;

                DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.HydrationValues[tileX, tileY] += FillWaterAmount;

                //Tile tile = DynamicWorldSandboxRunner.LastStartedInstance.CreatedWorld.Tiles[tileX, tileY];
                //DynamicWorldSandboxRunner.LastStartedInstance.HydrationProcessor.WaterTiles.Add(tile);

                
            }
        }

        //FillWaterAmount = 
    }
}
