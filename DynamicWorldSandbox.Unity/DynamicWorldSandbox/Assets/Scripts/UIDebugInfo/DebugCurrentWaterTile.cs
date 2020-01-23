using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugCurrentWaterTile : TextUpdateMonoBehaviour
{

    protected override void UpdateTextComponent(Text textComponent)
    {
        int playerPositionX = Convert.ToInt32(Camera.main.transform.position.x);
        int playerPositionY = Convert.ToInt32(Camera.main.transform.position.y);
        if (playerPositionX >= 0 && playerPositionX < DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.World.Width &&
           playerPositionY >= 0 && playerPositionY <  DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.World.Height) 
        {
            textComponent.text = DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.HydrationValues[playerPositionX, playerPositionY].ToString("#.###");            
        }
        else
        {
            textComponent.text = "OutOfWorld.";
        }
        

    }
}
