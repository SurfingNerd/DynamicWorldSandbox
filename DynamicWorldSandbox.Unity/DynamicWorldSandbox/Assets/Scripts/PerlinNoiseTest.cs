using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseTest : MonoBehaviour
{
    public GameObject cube;
    public int worldWidthX;
    public int worldWidthZ;

    public float lowerNoiseScaleValue;
    public float upperNoiseScaleValue;

    public float lowerNoiseScaleModifierValue;
    public float upperNoiseScaleModifierValue;

    private float noiseScale;
    private float noiseScaleModifier;

    /// <summary>
    /// Generates the cubes as children of a GameObject.
    /// </summary>
    public void InitalizeCubes()
    {
        for (float x = 0; x <= this.worldWidthX; ++x)
        {
            for (float z = 0; z <= this.worldWidthZ; ++z)
            {
                GameObject cube = Instantiate(
                    this.cube,
                    new Vector3(x, 0, z),
                    this.cube.transform.rotation
                ) as GameObject;

                cube.transform.parent = this.transform;
            }
        }
    }

    /// <summary>
    /// Sets the color of the cube based on the cube's height.
    /// </summary>
    /// <param name="cube">Cube.</param>
    /// <param name="cubeHeight">Cube height.</param>
    public void SetCubeHeightColor(Transform cube, float cubeHeight)
    {
        cube.GetComponent<Renderer>().material.color = new Color(cubeHeight / 5, cubeHeight, cubeHeight / 5);
        //cube.renderer.material.color = new Color(cubeHeight / 5, cubeHeight, cubeHeight / 5);
    }

    /// <summary>
    /// Applies a height value to the transform of a cube.
    /// </summary>
    /// <param name="cube">Cube.</param>
    /// <param name="cubeHeight">Cube height.</param>
    public void ApplyHeightToCube(Transform cube, float cubeHeight)
    {
        int newHeight = Mathf.RoundToInt(cubeHeight * this.noiseScaleModifier);
        Vector3 newPosition = new Vector3(cube.transform.position.x, newHeight, cube.transform.position.z);
        cube.transform.position = newPosition;
    }

    /// <summary>
    /// Updates the positions of the cubes by giving
    /// each cube a specific perlin noise value,
    /// coloring them, and finally, applying the height
    /// values to the transform.position's of the cubes.
    /// </summary>
    public void GenerateCubes()
    {
        foreach (Transform cube in transform)
        {
            float noiseScaleAddition = Random.Range(this.noiseScale / 10, this.noiseScale / 9);
            float cubeHeight = Mathf.PerlinNoise(
                (cube.transform.position.x / (this.noiseScale + noiseScaleAddition)),
                (cube.transform.position.z / (this.noiseScale + noiseScaleAddition))
            );
            SetCubeHeightColor(cube, cubeHeight);
            ApplyHeightToCube(cube, cubeHeight);
        }
    }

    /// <summary>
    /// Start this instance.
    /// </summary>
    void Start()
    {
        this.noiseScale = Random.Range(this.lowerNoiseScaleValue, this.upperNoiseScaleValue);
        this.noiseScaleModifier = Random.Range(this.lowerNoiseScaleModifierValue, this.upperNoiseScaleModifierValue);

        this.InitalizeCubes();
        this.GenerateCubes();
        Screen.lockCursor = true;
    }
}
