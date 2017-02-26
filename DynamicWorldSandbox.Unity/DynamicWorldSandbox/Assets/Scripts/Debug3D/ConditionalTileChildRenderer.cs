using System;
using System.Collections;
using System.Collections.Generic;
using DynamicWorldSandbox.Model;
using UnityEngine;

public abstract class ConditionalTileChildRenderer : MonoBehaviour, ISquareFieldModuleTileRenderer
{
    public GameObject DebugObjectPrefab;

    protected int LocalPositionOffsetZ;

    Dictionary<Tile, GameObject> mGameObjects = new Dictionary<Tile, GameObject>();


    public void Init()
    {

        if (DebugObjectPrefab == null)
        {
            Debug.LogError("DebugObjectPrefab is not set.");
            return;
        }

        //mRenderer = GetComponent<Renderer>();
        //DynamicWorldSandbox.Model.World
        //DynamicWorldSandbox.Model.World
    }

    public abstract bool ShouldRender(Tile tile);

    public void UpdateTile(Tile tile)
    {
        if (DebugObjectPrefab == null)
        {
            return;
        }
        //TODO: GameObject Pooling for better Performance.

        bool shouldRender = ShouldRender(tile);

        GameObject displayGame;
        if (mGameObjects.TryGetValue(tile, out displayGame))
        {
            if (shouldRender)
            {
                ModifyDisplayedObject(tile, displayGame);
            }
            else if (!shouldRender)
            {
                mGameObjects.Remove(tile);
                Destroy(displayGame);
            }
        }
        else if (shouldRender)
        {
            GameObject instance= Instantiate<GameObject>(DebugObjectPrefab);
            mGameObjects.Add(tile, instance);
            instance.transform.SetParent(gameObject.transform);
            instance.transform.localPosition = new Vector3(0, 0, LocalPositionOffsetZ);
            ModifyDisplayedObject(tile, displayGame);
        }// else: it should not renderer, and its not available, so everything is Fine.
    }
    
    abstract protected void ModifyDisplayedObject(Tile tile, GameObject gameObject);

    //Remove if Neccessare

}


