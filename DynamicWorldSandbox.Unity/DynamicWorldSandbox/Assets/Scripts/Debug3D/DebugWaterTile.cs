using System;
using System.Collections;
using System.Collections.Generic;
using DynamicWorldSandbox.Model;
using UnityEngine;

public class DebugWaterTile : ConditionalTileChildRenderer
{
    public override bool ShouldRender(Tile tile)
    {
        return true; //DynamicWorldSandboxRunner.LastStartedInstance.HydrationProcessor.WaterTiles.Contains(tile);
    }

    protected override void ModifyDisplayedObject(Tile tile, GameObject gameObject)
    {
        
    }
}
