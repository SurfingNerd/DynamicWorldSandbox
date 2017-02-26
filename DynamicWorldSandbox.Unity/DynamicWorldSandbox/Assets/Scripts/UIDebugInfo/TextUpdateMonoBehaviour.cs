using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Applied to Text Component in Canvas
/// </summary>
public abstract class TextUpdateMonoBehaviour : MonoBehaviour
{

    public double Intervall = 0.2;
    private double mTimeWithoutUpdate;

    protected UnityEngine.UI.Text textComponent;
    // Use this for initialization
    void Start()
    {

        textComponent = GetComponent<UnityEngine.UI.Text>();

        if (textComponent == null)
        {
            Debug.LogError(GetType().FullName + " requires to be attached to a Text");
        }
    }

    // Update is called once per frame
    void Update()
    {
        mTimeWithoutUpdate += Time.deltaTime;
        if (mTimeWithoutUpdate > Intervall)
        {
            mTimeWithoutUpdate -= Time.deltaTime;
            UpdateTextComponent(textComponent);

        }
    }

    protected abstract void UpdateTextComponent(Text textComponent);
    
}
