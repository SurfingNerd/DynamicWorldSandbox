using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Applied to Text Component in Canvas
/// </summary>
public class TotalWaterUpdate : TextUpdateMonoBehaviour
{
    protected override void UpdateTextComponent(Text textComponent)
    {
        double totalWater = DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule.LastInitializedInstance.GetTotalWaterAmount();
        textComponent.text = "Total Water: " + totalWater.ToString("0.000");
    }
}
