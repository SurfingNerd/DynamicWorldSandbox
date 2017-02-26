using DynamicWorldSandbox.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareFieldTileRenderer : MonoBehaviour {

    public Tile Tile;

    private List<ISquareFieldModuleTileRenderer> mModuleRenderers = new List<ISquareFieldModuleTileRenderer>();

    // Use this for initialization
    void Start ()
    {
        //TODO: Do recursive ?
        ISquareFieldModuleTileRenderer[] renderers = GetComponentsInChildren<ISquareFieldModuleTileRenderer>();
        mModuleRenderers.AddRange(renderers);
    }
	
	//// Update is called once per frame
	//void Update () {
		
	//}


    public void UpdateTile(Tile tile)
    {
        foreach (ISquareFieldModuleTileRenderer renderer in mModuleRenderers)
        {
            renderer.UpdateTile(tile);
        }
    }
}
