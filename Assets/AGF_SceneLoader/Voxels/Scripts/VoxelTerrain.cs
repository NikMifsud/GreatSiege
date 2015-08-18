
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Voxel
{

	[System.Serializable]
	public class VoxelBlockType
	{
		#region class BlockType
		public enum ShaderSizes{
			One, Two, Six
		}

		public string name;
		
		public bool filled;
		//public bool visible = true;
		public bool differentTop;

		[System.NonSerialized] public ShaderSizes shaderSize;
		//public Constructor constructor; //DungeonConstructor
		public Shader voxelShader;

		public Texture bumpTexture;
		public Texture topBumpTexture;
		public Texture leftBumpTexture;
		public Texture rightBumpTexture;
		public Texture frontBumpTexture;
		public Texture backBumpTexture;

		public Texture texture;
		public Texture topTexture;
		public Texture leftTexture;
		public Texture rightTexture;
		public Texture frontTexture;
		public Texture backTexture;


		public bool showGrass;
		public Material grassMaterial;
		public string grassBundle;
		
		public float smooth = .5f; 
		
		public Transform obj; 
		public List<Transform> prefabPool = new List<Transform>();
		
		public VoxelBlockType (string n, bool f) { name = n; filled = f; }
		#endregion
	}
	
	//[ExecuteInEditMode] //to rebuild on scene loading. Only OnEnable uses it
	public class VoxelTerrain : MonoBehaviour
	{

		[HideInInspector] public Data data;
		
		public VoxelBlockType[] types;
		[HideInInspector] public int selected = 1;
		
		
		[HideInInspector] public int chunkSize = 32;
		
		[System.NonSerialized] public List<Chunk> activeChunks = null;
		[System.NonSerialized] public List<Chunk> chunkPool = new List<Chunk>();
	
		[HideInInspector] public int overlap = 2;
	
		[HideInInspector] public float grassAnimState = 0.5f;
		[HideInInspector] public float grassAnimSpeed = 0.75f;
		[HideInInspector] public bool  grassAnimInEditor;
		
		[HideInInspector] public bool  weldVerts = true;

		[HideInInspector] public int displayCenterX;
		[HideInInspector] public int displayCenterZ;
		[HideInInspector] public int displayExtend = 256;

		public Shader landShader;
		[HideInInspector] public Material grassMaterial;
		[HideInInspector] public Color landAmbient = new Color(0.5f,0.5f,0.5f,1);
		[HideInInspector] public Color landSpecular = new Color(0,0,0,1);
		[HideInInspector] public float landShininess;
		[HideInInspector] public float landBakeAmbient;
		[HideInInspector] public float landTile = 0.5f;

		[HideInInspector] public int ambientMargins = 5;
		[HideInInspector] public int ambientSpread = 4;
		
		[HideInInspector] public float normalsRandom = 0;
		
		[HideInInspector] public bool displayArea = false;
		
		#region vars GUI
		[HideInInspector] public int guiFarChunks = 25;
		[HideInInspector] public int guiFarDensity = 10;
		#endregion
	
		[HideInInspector] public bool generateLightmaps = false; //this should be always off exept baking with lightmaps
		[HideInInspector] public float lightmapPadding = 0.1f;
		[System.NonSerialized] public bool  hideChunks = false;
		[HideInInspector] public bool hideWire = true;

		[HideInInspector] public bool  autoGenerate = true;
		[HideInInspector] public int lastRebuildX = 0;
		[HideInInspector] public int lastRebuildZ = 0;
		
		[HideInInspector] public bool  generateCollider = true; 
		
		//public static int mainThreadId;
		[HideInInspector] public bool multiThreadEdit;
		
		[HideInInspector] public bool chunksUnparented = false; //for saving scene without meshes. If chunks unparented they should be returned

		AGF_TerrainManager m_TerrainManager;
		VoxelInterface m_VoxelInterface;
		
		public void Refresh (int x, int z, int extend) //creates new chunks, removes old on camera pos change (or manual call). Called every frame
		{
			
			#region loading compressed data
			if (data.areas == null && data.compressed.Count != 0) 
			{
				//if (benchmark) Debug.Log("Loading Byte List");
				data.LoadFromByteList(data.compressed);
			}
			#endregion
			
			#region Clearing if no terrain
			if (activeChunks == null) Rebuild(); //same as in display
			#endregion
			
			if (Mathf.Abs(x-lastRebuildX)>extend/3 || Mathf.Abs(z-lastRebuildZ)>extend/3)
			{
				GetChunksInRange(x, z, extend, true, true); //createInside, removeOutside //just creating new chunks, do not modify them. And removing out-of area chunks
				lastRebuildX = x; lastRebuildZ = z;
			}
		}
	
		public void Display () //refreshing terrain using last camera coordinates
		{				
			
			#region Clearing if no terrain
			if (activeChunks == null) Rebuild();
			#endregion
			
			#region Returning unparented chunks
			if (chunksUnparented)
			{
				for (int i=0; i<activeChunks.Count; i++) activeChunks[i].transform.parent = transform;
				chunksUnparented = false;
			}
			#endregion
			 
			#region Setting exist types
			for (int i=0; i<types.Length; i++)
				data.exist[i] = types[i].filled;
			#endregion

			#region Animating grass
			grassAnimState += Time.deltaTime * grassAnimSpeed;
			grassAnimState = Mathf.Repeat(grassAnimState, 6.283185307179586476925286766559f);
			Shader.SetGlobalFloat("_GrassAnimState", grassAnimState);
			#endregion
	
			#region Finishing Rebuild
			int unbuildChunksCount = 0; //for benchmarking
			
			for (int i=0; i<activeChunks.Count; i++)
			{
				
				//calculating mesh
				if (activeChunks[i].terrainProgress == Chunk.Progress.notCalculated) 
				{
					if (multiThreadEdit) 
					{
						activeChunks[i].terrainProgress = Chunk.Progress.threadStarted; 
						ThreadPool.QueueUserWorkItem(new WaitCallback(activeChunks[i].CalculateTerrain));
					}
					else activeChunks[i].CalculateTerrain();
				}
				
				//apply meshes 
				if (activeChunks[i].terrainProgress == Chunk.Progress.calculated) 
				{
					activeChunks[i].ApplyTerrain();
				}
				
				//calculate ambient
				if (activeChunks[i].ambientProgress == Chunk.Progress.notCalculated) 
				{
					if (multiThreadEdit) 
					{
						activeChunks[i].ambientProgress = Chunk.Progress.threadStarted; 
						ThreadPool.QueueUserWorkItem(new WaitCallback(activeChunks[i].CalculateAmbient));
					}
					else activeChunks[i].CalculateAmbient();
				}
				
				//apply ambient
				if (activeChunks[i].ambientProgress == Chunk.Progress.calculated && 
				    activeChunks[i].terrainProgress == Chunk.Progress.applied) activeChunks[i].ApplyAmbient();
				  
				//calculate grass
				if (activeChunks[i].grassProgress == Chunk.Progress.notCalculated && 
				    (activeChunks[i].terrainProgress == Chunk.Progress.calculated || activeChunks[i].terrainProgress == Chunk.Progress.applied) )
					activeChunks[i].BuildGrass();

			}
			#endregion
			
			
			displayExtend = Mathf.CeilToInt(FindObjectOfType<AGF_TerrainManager>().GetTerrainSize()/2);
			data.areaSize = displayExtend;
			foreach(Voxel.Chunk chunk in activeChunks){
				chunk.terrainProgress = Voxel.Chunk.Progress.notCalculated;
				chunk.grassProgress = Voxel.Chunk.Progress.notCalculated;
			}
		}
		
		public void Rebuild () //clears all terrain and creates new one
		{
			#region Clearing
			if (activeChunks==null) activeChunks = new List<Chunk>();
			activeChunks.Clear();
			for(int i=transform.childCount-1; i>=0; i--)
				DestroyImmediate(transform.GetChild(i).gameObject); //destroing everything
			#endregion
			
			#region Re-create
			lastRebuildX = 100000000;
			lastRebuildZ = 100000000;
			
			Refresh(displayCenterX, displayCenterZ, displayExtend);

			Display();
			#endregion
		}
		
		public Chunk[] GetChunksInRange (int x, int z, int range, bool createInside, bool removeOutside)
		{
			#region Chunks min and max
			int visChunkSize = chunkSize-overlap*2;
			
			int csx = (int)(1.0f*(x-range-overlap) / visChunkSize); 
			int cex = (int)(1.0f*(x+range-overlap) / visChunkSize); 
			int csz = (int)(1.0f*(z-range-overlap) / visChunkSize); 
			int cez = (int)(1.0f*(z+range-overlap) / visChunkSize); 
			
			if (x-range<0) csx--; if (x+range<0) cex--;
			if (z-range<0) csz--; if (z+range<0) cez--;
			#endregion
	
			#region defining array dimensions and filling array
			int chunksCountX = Mathf.Abs(cex-csx)+1;
			int chunksCountZ = Mathf.Abs(cez-csz)+1; 
			Chunk[] chunks = new Chunk[chunksCountX*chunksCountZ];
			#endregion
			
			#region filling existant chunks list (and removing outside)
			for (int i=activeChunks.Count-1; i>=0; i--)
			{
				if (activeChunks[i].coordX < csx || activeChunks[i].coordX > cex ||
				    activeChunks[i].coordZ < csz || activeChunks[i].coordZ > cez) //out of range
				{
				    if (removeOutside) 
				    {
						//DestroyImmediate(activeChunks[i].gameObject);
						chunkPool.Add(activeChunks[i]);
						activeChunks[i].Clear();
						
						activeChunks.RemoveAt(i);
				    }
				}
				   
				//if in range 
				else chunks[ (activeChunks[i].coordZ-csz)*chunksCountX + (activeChunks[i].coordX-csx) ] = activeChunks[i];
			}
			#endregion
			
			#region creating non-existant chunks
			if (createInside)
				for (int cx=0; cx<chunksCountX; cx++)
					for (int cz=0; cz<chunksCountZ; cz++)
			{
				int chunkCoord = cz*chunksCountX + cx;
				if (chunks[chunkCoord] == null) 
				{
					//taking from a pool
					if (chunkPool.Count > 0) 
					{
						chunks[cz*chunksCountX + cx] = chunkPool[chunkPool.Count-1];
						chunkPool.RemoveAt(chunkPool.Count-1);
					}	
					
					//or creating new
					else chunks[chunkCoord] = Chunk.CreateChunk(this);
					
					//and initializing
					chunks[chunkCoord].Init(cx+csx,cz+csz);

					if (!activeChunks.Contains(chunks[chunkCoord])) activeChunks.Add(chunks[chunkCoord]);
				}
			}
			#endregion
			
			return chunks;
		}
	

	}

}//namespace