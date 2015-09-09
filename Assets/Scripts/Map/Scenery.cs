using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

//creates the tiles of the scenery, to make it bigger change the size of the plane it is attached to in the Scene of Unity

public class Scenery: MonoBehaviour
{
	
	public Dictionary<Point, TileBehaviour> Board;

	//following public variable is used to store the hex model prefab;
	//instantiate it by dragging the prefab on this variable using unity editor
	public GameObject Ground;
	public GameObject TileTemplate;
	public GameObject DirtTile;
	public GameObject OutPostTile;
	public GameObject MudTile;
	public GameObject StoneTile;
	//next two variables can also be instantiated using unity editor
	public int gridWidthInHexes;
	public int gridHeightInHexes;
	private float groundWidth;
	private float groundHeight;
	public GameObject selectedCharacter;
	//public GameObject Character;
	
	//Hexagon tile width and height in game world
	private float tileWidth;
	private float tileHeight;
	
	private GameObject tile;
	private int tileCounter = 0;
	private int stoneCounter = 0;
	private int num = 0;
	
	private bool outPostBoolean = false;
	private bool stoneCluster = false;
	
	//second tutorial 
	//selectedTile stores the tile mouse cursor is hovering on
	public Tile selectedTile = null;
	//TB of the tile which is the start of the path
	public TileBehaviour originTileTB = null;
	//TB of the tile which is the end of the path
	public TileBehaviour destTileTB = null;
	
	public GameObject Line;
	
	List<GameObject> path;

	//Method to initialise Hexagon width and height
	void setSizes()
	{
		//renderer component attached to the Hex prefab is used to get the current width and height
		tileWidth = TileTemplate.GetComponent<Renderer>().bounds.size.x;
		tileHeight = TileTemplate.GetComponent<Renderer>().bounds.size.z;
		groundWidth = Ground.GetComponent<Renderer>().bounds.size.x;
		groundHeight = Ground.GetComponent<Renderer>().bounds.size.z;
	}
	
	public Vector2 calcGridPos(Vector3 coord)
	{
		Vector3 initPos = calcInitPos();
		Vector2 gridPos = new Vector2();
		float offset = 0;
		gridPos.y = Mathf.RoundToInt((initPos.z - coord.z) / (tileHeight * 0.75f));
		if (gridPos.y % 2 != 0)
			offset = tileWidth / 2;
		gridPos.x = Mathf.RoundToInt((coord.x - initPos.x - offset) / tileWidth);
		return gridPos;
	}
	
	public Vector2 calcGridSize()
	{
		//According to the math textbook hexagon's side length is half of the height
		float sideLength = tileHeight / 2;
		//the number of whole hex sides that fit inside inside ground height
		int nrOfSides = (int)(groundHeight / sideLength);
		//I will not try to explain the following calculation because I made some assumptions, which might not be correct in all cases, to come up with the formula. So you'll have to trust me or figure it out yourselves.
		int gridHeightInHexes = (int)( nrOfSides * 2 / 3);
		//When the number of hexes is even the tip of the last hex in the offset column might stick up.
		//The number of hexes in that case is reduced.
		if (gridHeightInHexes % 2 == 0
		    && (nrOfSides + 0.5f) * sideLength > groundHeight)
			gridHeightInHexes--;
		//gridWidth in hexes is calculated by simply dividing ground width by hex width
		return new Vector2((int)(groundWidth / tileWidth), gridHeightInHexes);
	}
	
	//Method to calculate the position of the first hexagon tile
	//The center of the hex grid is (0,0,0)
	Vector3 calcInitPos()
	{
		Vector3 initPos;
		//the initial position will be in the left upper corner
		initPos = new Vector3(-tileWidth * gridWidthInHexes / 2f + tileWidth / 2, 0,
		                      gridHeightInHexes / 2f * tileHeight - tileHeight / 2);
		
		return initPos;
	}
	
	//method used to convert hex grid coordinates to game world coordinates
	public Vector3 calcWorldCoord(Vector2 gridPos)
	{
		//Position of the first hex tile
		Vector3 initPos = calcInitPos();
		//Every second row is offset by half of the tile width
		float offset = 0;
		if (gridPos.y % 2 != 0)
			offset = tileWidth / 2;
		
		float x =  initPos.x + offset + gridPos.x * tileWidth;
		//Every new line is offset in z direction by 3/4 of the hexagon height
		float z = initPos.z - gridPos.y * tileHeight * 0.75f;
		return new Vector3(x, 0, z);
	}
	
	GameObject SelectTileType(){
		
		//int[] integers = new int[] {0,1,2,4};
		
		if (tileCounter >= 0 && tileCounter <= 50) { //75
			if(stoneCounter == 15){
				num = 3;
			}
			if (stoneCluster == true)
				num = UnityEngine.Random.Range (1, 3);
			else
				num = UnityEngine.Random.Range (1, 4);
		} else
			num = UnityEngine.Random.Range (1, 5);
		// num = integers [Random.Range (0, integers.Length)];
		
		if (num == 1) {
			tile = (GameObject)Instantiate (DirtTile);
		} else if (num == 2) {
			tile = (GameObject)Instantiate (MudTile);
		} else if (num == 3) {
			tile = (GameObject)Instantiate (StoneTile);
			stoneCluster = true;
		} else if (num == 4) {
			tile = (GameObject)Instantiate (OutPostTile);
			outPostBoolean = true;
		}
		
		tileCounter ++;
		if(stoneCluster == true)
			stoneCounter ++;
		
		if (outPostBoolean == true) {
			outPostBoolean = false;
			tileCounter = 0;
		}
		
		if (stoneCounter == 15) {
			stoneCluster = false;
			stoneCounter = 0;
		}
		
		return tile;
	}
	
	//Finally the method which initialises and positions all the tiles
	void createGrid()
	{
		Vector2 gridSize = calcGridSize();
		//Game object which is the parent of all the hex tiles
		GameObject hexGridGO = new GameObject("Scenery");
		
		for (float y = 0; y < gridSize.y; y++)
		{
			float sizeX = gridSize.x;
			//if the offset row sticks up, reduce the number of hexes in a row
			if (y % 2 != 0 && (gridSize.x + 0.5) * tileWidth > groundWidth)
				sizeX--;
			for (float x = 0; x < gridSize.x; x++)
			{
				//GameObject assigned to Hex public variable is cloned
				GameObject  hex = SelectTileType();
				//Current position in grid
				Vector2 gridPos = new Vector2(x, y);
				
				hex.transform.position = calcWorldCoord(gridPos);
				hex.transform.parent = hexGridGO.transform;

			}
		}

		hexGridGO.transform.position = new Vector3(-1.44f,-0.07f,2.4f);
	}
	
	//Distance between destination tile and some other tile in the grid
	public double calcDistance(Tile tile)
	{
		Tile destTile = destTileTB.tile;
		//Formula used here can be found in Chris Schetter's article
		float deltaX = Mathf.Abs(destTile.X - tile.X);
		float deltaY = Mathf.Abs(destTile.Y - tile.Y);
		int z1 = -(tile.X + tile.Y);
		int z2 = -(destTile.X + destTile.Y);
		float deltaZ = Mathf.Abs(z2 - z1);
		
		return Mathf.Max(deltaX, deltaY, deltaZ);
	}

	//The grid should be generated on game start
	void Start()
	{
		setSizes(); 
		createGrid();
	}
}