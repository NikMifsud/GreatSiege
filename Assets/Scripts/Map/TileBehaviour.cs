using UnityEngine;
using System.Collections;

public class TileBehaviour: MonoBehaviour
{
	public Tile tile;
	public Material mudTexture;
	public Material mudTextureHighlighted;
	public Material dirtTexture;
	public Material dirtTextureHighlighted;
	public Material stoneTexture;
	public Material stoneTextureHighlighted;
	public Material outpostTexture;
	public Material outpostTextureHighlighted;
	public Vector3 endPosition;
	public GameMaster gameMaster;
	public Material allyMaterial;
	public Material enemyMaterial;
	public PlayerControl playerControl;
	public bool isPassable;
	public bool isEnemy;

	//have a memory in order to keep the colours consistant 
	public TileBehaviour previousTile;
	
	void Start(){
		gameMaster = Camera.main.GetComponent<GameMaster> ();
		playerControl = Camera.main.GetComponent<PlayerControl> ();
		this.isPassable = true;
		this.isEnemy = false;
	}

	void Update(){
	//	if (this.isEnemy == true && this.isPassable == false) {
	//		Transform mychildtransform = this.transform.FindChild("Cylinder");
	//		mychildtransform.GetComponent<Renderer> ().material = enemyMaterial;
	//	}

		if (this.isEnemy == false && this.isPassable == false) {
			Transform mychildtransform = this.transform.FindChild("Cylinder");
			mychildtransform.GetComponent<Renderer> ().material = allyMaterial;
		}

		if(playerControl.highlightingTiles == false){
			if(this.tag == "MudTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = mudTexture;
			}
			if(this.tag == "StoneTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = stoneTexture;
			}
			if(this.tag == "OutpostTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = outpostTexture;
			}
			if(this.tag == "DirtTile"){
				Transform mychildtransform = this.transform.FindChild("Cylinder");
				mychildtransform.GetComponent<Renderer> ().material = dirtTexture;
			}
		}
	}

	void OnMouseEnter()
	{
		GridManager.instance.selectedTile = tile;
		//when mouse is over some tile, the tile is passable and the current tile is neither destination nor origin tile, change color to orange
	}
	
	//changes back to fully transparent material when mouse cursor is no longer hovering over the tile
	void OnMouseExit()
	{
		GridManager.instance.selectedTile = null;
	}
	//called every frame when mouse cursor is on this tile
	void OnMouseDown()
	{

			tile.Passable = true;

			TileBehaviour originTileTB = GridManager.instance.originTileTB;
			//if user clicks on origin tile or origin tile is not assigned yet

			if (this == originTileTB || originTileTB == null)
				originTileChanged ();
			else
				destTileChanged ();
		
		//	GridManager.instance.generateAndShowPath ();

	}

	void originTileChanged()
	{
		var originTileTB = GridManager.instance.originTileTB;
		//deselect origin tile if user clicks on current origin tile
		if (this == originTileTB)
		{
			GridManager.instance.originTileTB = null;
			return;
		}
		//if origin tile is not specified already mark this tile as origin
		GridManager.instance.originTileTB = this;
		//Transform mychildtransformm = this.transform.FindChild("Cylinder");
		//mychildtransformm.GetComponent<Renderer> ().material = red;

	}

	void destTileChanged()
	{
		var destTile = GridManager.instance.destTileTB;

		//deselect destination tile if user clicks on current destination tile
		if (this == destTile)
		{
			GridManager.instance.destTileTB = null;
			GridManager.instance.originTileTB = null;
			return;
		}
		GridManager.instance.destTileTB = this;
		previousTile = destTile;
		if (previousTile != null) {
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Wall")
		{
			Destroy(this.gameObject);
		}
	}
}