using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugWaterTileCount : TextUpdateMonoBehaviour
{
    protected override void UpdateTextComponent(Text textComponent)
    {
        textComponent.text = "Floating WAter Tiles: " + DynamicWorldSandboxRunner.LastStartedInstance.HydrationProcessor.WaterTiles.Count;
    }
    
}
