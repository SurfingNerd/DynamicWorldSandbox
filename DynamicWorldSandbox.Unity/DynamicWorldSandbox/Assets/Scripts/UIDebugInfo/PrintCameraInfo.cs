using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintCameraInfo : TextUpdateMonoBehaviour
{
    protected override void UpdateTextComponent(Text textComponent)
    {
        if (Camera.main == null)
        {
            Debug.LogWarning("Main Camera not set!");
            return;
        }
        textComponent.text = "Camera Position: " + Camera.main.transform.position.ToString();        
    }
}
