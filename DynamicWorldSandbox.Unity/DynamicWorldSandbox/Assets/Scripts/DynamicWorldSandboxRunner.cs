using System.Collections;
using System.Collections.Generic;
using DynamicWorldSandbox.Model;
using UnityEngine;
using DynamicWorldSandbox.Engine.Tiles;

public class DynamicWorldSandboxRunner : MonoBehaviour {

    public static DynamicWorldSandboxRunner LastStartedInstance;
    public GameObject TilePrefab;
    public double GroundOfTheWorld = -10;

    private int SideSize = 100;
    public World CreatedWorld;

    private int mTickNumber = 0;

    SquareFieldTileRenderer[,] mRenderers;
    GameObject[,] mGameObjects;
    DynamicWorldSandbox.Scheduler.Scheduler mSheduler;

    public float UpdateSecondInterval = 0.25f;
    private float mTimePassedWithoutUpdate = 0;

    public HydrationProcessor HydrationProcessor { get; internal set; }

    //public int RoundsPerUpdateIntervall;
    //private int 



    // Use this for initialization
    void Start () {
        LastStartedInstance = this;
        Debug.Log("DynamicWorldSandboxRunner started");
        Init();

        if (TilePrefab == null)
        {
            Debug.LogError("TilePrefab not set!");
            return;
        }

        mRenderers = new SquareFieldTileRenderer[CreatedWorld.Width, CreatedWorld.Height];
        mGameObjects = new GameObject[CreatedWorld.Width, CreatedWorld.Height];

        for (int x = 0; x < CreatedWorld.Width; x++)
        {
            for (int y = 0; y < CreatedWorld.Height; y++)
            {
                GameObject squarefield = Instantiate(TilePrefab) as GameObject;
                mGameObjects[x, y] = squarefield;
                squarefield.transform.position = new Vector3(x, y, 0);

                SquareFieldTileRenderer tileRenderer = squarefield.GetComponent<SquareFieldTileRenderer>();
                mRenderers[x, y] = tileRenderer;

                tileRenderer.UpdateTile(CreatedWorld.Tiles[x, y]);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

        mTimePassedWithoutUpdate += Time.deltaTime;

        if (mTimePassedWithoutUpdate > UpdateSecondInterval)
        {
            mTimePassedWithoutUpdate -= UpdateSecondInterval;
            mSheduler.RunTick(mTickNumber);
            mTickNumber++;
            for (int x = 0; x < CreatedWorld.Width; x++)
            {
                for (int y = 0; y < CreatedWorld.Height; y++)
                {
                    mRenderers[x, y].UpdateTile(CreatedWorld.Tiles[x, y]);
                }
            }
        }

        
    }

    void Init()
    {
        Debug.Log("Init called");
        //int sideSize = 100;
        CreatedWorld = new DynamicWorldSandbox.Model.World(SideSize, SideSize);

        DynamicWorldSandbox.Model.Modules.TerrainModule.TerrainHeightModule
            terrainModule = new DynamicWorldSandbox.Model.Modules.TerrainModule.TerrainHeightModule();
        terrainModule.Initialize(CreatedWorld);

        DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule hydrationModule
            = new DynamicWorldSandbox.Model.Modules.HydrationModule.HydrationModule();
        hydrationModule.Initialize(CreatedWorld);

        //watch.Stop();
        
        //100.000.000
        //Console.WriteLine("Init took " + watch.Elapsed.TotalSeconds.ToString("#.####"));

        //Console.WriteLine("Let it rain.");

        mSheduler = new DynamicWorldSandbox.Scheduler.Scheduler(1000);

        World world = CreatedWorld;

        world.Tiles[10, 10].Hydration = 1000;

        world.Tiles[30, 30].TerrainHeight = -1;
        world.Tiles[29, 29].TerrainHeight = -1;
        world.Tiles[29, 30].TerrainHeight = -1;
        world.Tiles[31, 31].TerrainHeight = -1;
        world.Tiles[30, 31].TerrainHeight = -1;

        world.Tiles[10, 30].TerrainHeight = 3;
        world.Tiles[10, 29].TerrainHeight = 3;
        world.Tiles[11, 30].TerrainHeight = 3;
        world.Tiles[11, 31].TerrainHeight = 3;
        world.Tiles[12, 31].TerrainHeight = 3;


        BuildRiverNorthSoutch(world, 19, 0, 99, -3);
        BuildRiverNorthSoutch(world, 20, 0, 99, -3);
        BuildRiverNorthSoutch(world, 21, 0, 99, -3);

        BuildRiverNorthSoutch(world, 22, 0, 60, 5);
        BuildRiverNorthSoutch(world, 23, 0, 61, 5);
        BuildRiverNorthSoutch(world, 24, 0, 62, 5);
        BuildRiverNorthSoutch(world, 25, 0, 61, 5);
        BuildRiverNorthSoutch(world, 25, 0, 60, 5);
    
        for (int x = 90; x < 100; x++)
        {
            BuildRiverNorthSoutch(world, x, 0, 99, 3);
        }

        for (int y = 90; y < 100; y++)
        {
            BuildRiverWest(world, y, 0, 99, 5);
            
        }

        //Console.WriteLine("After initialisation.");
       // DebugWaterInfos(world);

        HydrationProcessor = new HydrationProcessor(world);
        mSheduler.RegisterJob(HydrationProcessor);

        //watch.Reset();
       // watch.Start();
        //watch.Restart();
       // int countOfSimulationTicks = MaxRounds;//25000000;

       // int saveImageFrequencyK = 1;
       // int saveImageMultiplier = 1;

        //string baseDir = Directory.GetCurrentDirectory();
        //DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(baseDir, DateTime.Now.ToString("mmddHHmmss")));

        //dirInfo.Create();

        //Console.WriteLine("Starting Simulation");

        //for (int i = 0; i < countOfSimulationTicks; i++)
        //{
        //    sheduler.RunTick(i);
        //    if (i % (saveImageFrequencyK * saveImageMultiplier) == 0)
        //    {
        //        //DebugWaterInfos(world);
        //        SaveImage(world, i, dirInfo, saveImageMultiplier);
        //    }
        //}
        //watch.Stop();

        //Console.WriteLine("Simulation took " + watch.Elapsed.TotalSeconds.ToString("#.####"));
        //SaveImage(world, countOfSimulationTicks, dirInfo, saveImageMultiplier);
        //DebugWaterInfos(world);
        //Console.ReadLine();
    }

    private static void BuildRiverNorthSoutch(World world, int x, int yStart, int yEnd, int depth)
    {
        for (int y = yStart; y <= yEnd; y++)
        {
            world.Tiles[x, y].TerrainHeight = depth;
        }
    }

    private static void BuildRiverWest(World world, int y, int xStart, int xEnd, int depth)
    {
        for (int x = xStart; x <= xEnd; x++)
        {
            world.Tiles[x, y].TerrainHeight = depth;
        }
    }
}

