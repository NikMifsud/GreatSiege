using UnityEngine;
using System.Collections;

public class GridManager: MonoBehaviour
{
	public static GridManager instance = null;
	//following public variable is used to store the hex model prefab;
	//instantiate it by dragging the prefab on this variable using unity editor
	public GameObject TileTemplate;
	public GameObject DirtTile;
	public GameObject OutPostTile;
	public GameObject MudTile;
	public GameObject StoneTile;
	//next two variables can also be instantiated using unity editor
	public int gridWidthInHexes = 10;
	public int gridHeightInHexes = 10;
	
	//Hexagon tile width and height in game world
	private float tileWidth;
	private float tileHeight;

	private GameObject tile;
	private int tileCounter = 0;
	private int stoneCounter = 0;
	private int num = 0;

	private bool outPostBoolean = false;
	private bool stoneCluster = false;
	void Awake ()
	{
		instance = this;
	}
	//Method to initialise Hexagon width and height
	void setSizes()
	{
		//renderer component attached to the Hex prefab is used to get the current width and height
		tileWidth = TileTemplate.GetComponent<Renderer>().bounds.size.x;
		tileHeight = TileTemplate.GetComponent<Renderer>().bounds.size.z;
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

		if (tileCounter >= 0 && tileCounter <= 60) {
			if(stoneCounter == 12){
				num = 3;
			}
			if (stoneCluster == true)
				num = Random.Range (1, 3);
			else
				num = Random.Range (1, 4);
		} else
			num = Random.Range (1, 5);
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

		if (stoneCounter == 35) {
			stoneCluster = false;
			stoneCounter = 0;
		}

		return tile;
	}

	//Finally the method which initialises and positions all the tiles
	void createGrid()
	{
		Vector2 gridPos;
		//Game object which is the parent of all the hex tiles
		GameObject hexGridGO = new GameObject("HexGrid");
		
		for (float y = 0; y < gridHeightInHexes; y++)
		{
			for (float x = 0; x < gridWidthInHexes; x++)
			{
				//GameObject assigned to Hex public variable is cloned
				GameObject  hex = SelectTileType();
				//Current position in grid
				gridPos = new Vector2(x, y);

				hex.transform.position = calcWorldCoord(gridPos);
				hex.transform.parent = hexGridGO.transform;
			}
		}
	}
	
	//The grid should be generated on game start
	void Start()
	{
		setSizes();
		createGrid();
		StoneTile.transform.position = new Vector3 (200, 0, 0);
		TileTemplate.transform.position = new Vector3 (200, 0, 0);
		DirtTile.transform.position = new Vector3 (200, 0, 0);
		OutPostTile.transform.position = new Vector3 (200, 0, 0);
		MudTile.transform.position = new Vector3 (200, 0, 0);
	}
}