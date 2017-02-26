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
        textComponent.text = DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.HydrationValues[playerPositionX, playerPositionY].ToString("#.###");

    }
}
