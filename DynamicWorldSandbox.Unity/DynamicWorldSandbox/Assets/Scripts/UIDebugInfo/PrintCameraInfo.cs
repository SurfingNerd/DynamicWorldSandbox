using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintCameraInfo : TextUpdateMonoBehaviour
{
    protected override void UpdateTextComponent(Text textComponent)
    {
        textComponent.text = "Camera Position: " + Camera.main.transform.position.ToString();        
    }
}
