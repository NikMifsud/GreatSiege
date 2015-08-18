using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class VoxelInterface : MonoBehaviour {
	
	public Shader grassShader;
	public Voxel.VoxelTerrain[] voxelTerrain;
	public GameObject voxelPrefab;

	public int selectedTerrain;
	
	public Material[] grassMaterials;

	AGF_AssetLoader m_AssetLoader;
	AGF_TerrainManager m_TerrainManager;

	void Start(){
		m_AssetLoader = FindObjectOfType<AGF_AssetLoader> ();
		m_TerrainManager = FindObjectOfType<AGF_TerrainManager> ();
	}
	
	public void LoadVoxelData(string voxelData){


		if(File.Exists(FindObjectOfType<AGF_LevelLoader>().GetCurrentProjectDirectory()+"/Voxels/" +
		               FindObjectOfType<AGF_LevelLoader>().GetCurrentActiveSceneName() + ".txt")){
		
			if(!voxelTerrain[selectedTerrain]){
				GameObject newTerrain = GameObject.Instantiate(voxelPrefab, new Vector3(.5f,-1,.5f), Quaternion.identity) as GameObject;
				voxelTerrain[selectedTerrain] = newTerrain.GetComponent<Voxel.VoxelTerrain>();
			}
			voxelTerrain[selectedTerrain].Rebuild();

			string path = FindObjectOfType<AGF_LevelLoader>().GetCurrentProjectDirectory()+"/Voxels/" + 
				FindObjectOfType<AGF_LevelLoader>().GetCurrentActiveSceneName() + ".txt";
			
			using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open))
				using (System.IO.StreamReader reader = new System.IO.StreamReader(fs)){
					voxelTerrain[selectedTerrain].data.LoadFromString( reader.ReadToEnd() );
				}

			voxelTerrain[selectedTerrain].data.compressed = voxelTerrain[selectedTerrain].data.SaveToByteList();
		}
		else{
			return;
		}
		
		string[] split = voxelData.Split (new char[]{'!'});
		int index = -1;
		
		//		voxelTerrain [selectedTerrain].data.LoadFromString (split [++index]);
		
		if(split[++index] == "1"){
			voxelTerrain[selectedTerrain].gameObject.SetActive(true);
		}
		else{
			voxelTerrain[selectedTerrain].gameObject.SetActive(false);
		}
		
		if(split.Length > index + 1){
			string[] split1 = split[++index].Split (new char[]{'#'});
			
			for(int i = 0; i < split1.Length; i++){
				if(i >= voxelTerrain[selectedTerrain].types.Length){
					AddVoxelType(false);
				}
				if(i >= voxelTerrain[selectedTerrain].types.Length){
					AddVoxelType(false);
				}

				voxelTerrain[selectedTerrain].types[i].grassMaterial = grassMaterials[i];
				
				
				string[] split2 = split1[i].Split (new char[]{'@'});
				int j = -1;

				if(split2.Length >= 23){
					voxelTerrain[selectedTerrain].types[i].topTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].topTexture);
					voxelTerrain[selectedTerrain].types[i].texture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].texture);
					voxelTerrain[selectedTerrain].types[i].leftTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].leftTexture);
					voxelTerrain[selectedTerrain].types[i].rightTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].rightTexture);
					voxelTerrain[selectedTerrain].types[i].frontTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].frontTexture);
					voxelTerrain[selectedTerrain].types[i].backTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].backTexture);

					int shaderSize = (int)voxelTerrain[selectedTerrain].types[i].shaderSize;
					int.TryParse(split2[++j], out shaderSize);
					voxelTerrain[selectedTerrain].types[i].shaderSize = (Voxel.VoxelBlockType.ShaderSizes)shaderSize;

					string grassName = split2[++j];
					string grassBundle = split2[++j];
					SetVoxelGrass(grassName, grassBundle, i);

					Color grassColor = Color.white;
					float.TryParse(split2[++j], out grassColor.r);
					float.TryParse(split2[++j], out grassColor.g);
					float.TryParse(split2[++j], out grassColor.b);
					voxelTerrain[selectedTerrain].types[i].grassMaterial.SetColor("_Color", grassColor);
					
					Color grassAmbient = Color.white;
					float.TryParse(split2[++j], out grassAmbient.r);
					float.TryParse(split2[++j], out grassAmbient.g);
					float.TryParse(split2[++j], out grassAmbient.b);
					voxelTerrain[selectedTerrain].types[i].grassMaterial.SetColor("_Ambient", grassAmbient);

					float grassAnimation = .5f;
					float.TryParse(split2[++j], out grassAnimation);
//					voxelTerrain.types[i].grassMaterial.SetFloat("_AnimStrength", grassAnimation);


					int showGrass = voxelTerrain[selectedTerrain].types[i].showGrass ? 1: 0;
					int.TryParse(split2[++j], out showGrass);
					voxelTerrain[selectedTerrain].types[i].showGrass = showGrass == 1;

					voxelTerrain[selectedTerrain].types[i].topBumpTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].topBumpTexture);
					voxelTerrain[selectedTerrain].types[i].bumpTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].bumpTexture);
					voxelTerrain[selectedTerrain].types[i].leftBumpTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].leftBumpTexture);
					voxelTerrain[selectedTerrain].types[i].rightBumpTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].rightBumpTexture);
					voxelTerrain[selectedTerrain].types[i].frontBumpTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].frontBumpTexture);
					voxelTerrain[selectedTerrain].types[i].backBumpTexture = GetVoxelPaint(split2[++j], voxelTerrain[selectedTerrain].types[i].backBumpTexture);

				}
			}
		}
		
		if(split.Length > index + 6){
			float.TryParse(split[++index], out voxelTerrain [selectedTerrain].landSpecular.r);
			float.TryParse(split[++index], out voxelTerrain [selectedTerrain].landSpecular.g);
			float.TryParse(split[++index], out voxelTerrain [selectedTerrain].landSpecular.b);
			float.TryParse(split[++index], out voxelTerrain [selectedTerrain].landShininess);
			
			float newAmbient = 0;
			float.TryParse(split[++index], out newAmbient);
			voxelTerrain [selectedTerrain].landAmbient = new Color(newAmbient, newAmbient, newAmbient, 1);
			
			float.TryParse(split[++index], out voxelTerrain [selectedTerrain].landTile);
		}
		
		voxelTerrain[selectedTerrain].Rebuild();

		foreach(Voxel.Chunk chunk in FindObjectsOfType<Voxel.Chunk>()){
			if(chunk.loFilter)
				DestroyImmediate(chunk.loFilter.gameObject);
			if(chunk.grassFilter && !chunk.grassFilter.GetComponent<Renderer>().sharedMaterial.mainTexture)
				DestroyImmediate(chunk.grassFilter.gameObject);

		}
	}
	
	Texture GetVoxelPaint(string textureName, Texture prevTexture){
		if(textureName == "")
			return prevTexture;

		m_AssetLoader = FindObjectOfType<AGF_AssetLoader> ();
		m_TerrainManager = FindObjectOfType<AGF_TerrainManager> ();
		
		foreach (Texture2D texture in m_AssetLoader.GetCustomPaint()) {
			if(texture.name == textureName){
				
				Texture2D newTexture = new Texture2D(texture.width, texture.height,TextureFormat.ARGB32,false);
				newTexture.SetPixels(texture.GetPixels());
				newTexture.name = textureName;
				newTexture.Apply();
				
				return newTexture;
			}
		}
		foreach (Texture2D texture in m_AssetLoader.GetCustomPaintNormals()) {
			if(texture.name == textureName){
				
				Texture2D newTexture = new Texture2D(texture.width, texture.height,TextureFormat.ARGB32,false);
				newTexture.SetPixels(texture.GetPixels());
				newTexture.name = textureName;
				newTexture.Apply();
				
				return newTexture;
			}
		}
		
		foreach (KeyValuePair<string,Texture2D> texture in m_TerrainManager.GetLoadedColormaps ()) {
			if(texture.Value.name == textureName){
				
				Texture2D newTexture = new Texture2D(texture.Value.width, texture.Value.height,TextureFormat.ARGB32,false);
				newTexture.SetPixels(texture.Value.GetPixels());
				newTexture.name = textureName;
				newTexture.Apply();
				
				return newTexture;
			}
		}
		foreach (KeyValuePair<string,Texture2D> texture in m_TerrainManager.GetLoadedNormalmaps()) {
			if(texture.Value.name == textureName){
				
				Texture2D newTexture = new Texture2D(texture.Value.width, texture.Value.height,TextureFormat.ARGB32,false);
				newTexture.SetPixels(texture.Value.GetPixels());
				newTexture.name = textureName;
				newTexture.Apply();
				
				return newTexture;
			}
		}
		
		return prevTexture;
	}
	
	public void SetVoxelGrass(string textureName, string bundleName, int grassSlot){
		if(textureName == "")
			return;

		if(!m_TerrainManager)
			m_TerrainManager = FindObjectOfType<AGF_TerrainManager>();
		if(!m_AssetLoader)
			m_AssetLoader = FindObjectOfType<AGF_AssetLoader>();
		
		if (bundleName == "") {
			foreach (Texture2D texture in m_AssetLoader.GetCustomVegetations()) {
				if (texture.name == textureName) {
					voxelTerrain[selectedTerrain].types[grassSlot].grassMaterial.mainTexture = texture;
					voxelTerrain[selectedTerrain].types[grassSlot].grassBundle = "";
				}
			}
		} else {
			foreach (KeyValuePair<string,Texture2D> texture in m_TerrainManager.GetLoadedVegetationTextures ()) {
				if (texture.Value.name == textureName && texture.Key == bundleName) {
					voxelTerrain[selectedTerrain].types[grassSlot].grassMaterial.mainTexture = texture.Value;
					voxelTerrain[selectedTerrain].types[grassSlot].grassBundle = texture.Key;
				}
			}
		}
	}
	
	public void AddVoxelType(bool rebuild = true){
		if(voxelTerrain[selectedTerrain].types.Length >= grassMaterials.Length)
			return;
		
		Voxel.VoxelBlockType[] array = voxelTerrain[selectedTerrain].types;
		Voxel.VoxelBlockType[] newArray= new Voxel.VoxelBlockType[ array.Length+1 ];
		for (int i=0; i<array.Length; i++) newArray[i] = array[i];
		newArray[ array.Length ] = new Voxel.VoxelBlockType("New Voxel " + array.Length, true);
		
		newArray [array.Length].shaderSize = voxelTerrain [selectedTerrain].types [voxelTerrain [selectedTerrain].selected].shaderSize;
		newArray[ array.Length ].backTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].backTexture;
		newArray[ array.Length ].texture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].texture;
		newArray[ array.Length ].frontTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].frontTexture;
		newArray[ array.Length ].topTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].topTexture;
		newArray[ array.Length ].leftTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].leftTexture;
		newArray[ array.Length ].rightTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].rightTexture;
		newArray[ array.Length ].backBumpTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].backBumpTexture;
		newArray[ array.Length ].bumpTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].bumpTexture;
		newArray[ array.Length ].frontBumpTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].frontBumpTexture;
		newArray[ array.Length ].topBumpTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].topBumpTexture;
		newArray[ array.Length ].leftBumpTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].leftBumpTexture;
		newArray[ array.Length ].rightBumpTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].rightBumpTexture;
		newArray [array.Length].grassMaterial = grassMaterials [array.Length];
		newArray [array.Length].grassMaterial.mainTexture = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].grassMaterial.mainTexture;
		newArray [array.Length].showGrass = voxelTerrain [selectedTerrain].types [voxelTerrain [selectedTerrain].selected].showGrass;
		newArray[array.Length].smooth = voxelTerrain[selectedTerrain].types[voxelTerrain[selectedTerrain].selected].smooth;
		newArray [array.Length].filled = true;
		
		voxelTerrain[selectedTerrain].types = newArray;
		voxelTerrain[selectedTerrain].selected = array.Length;
		
		if(rebuild)
			voxelTerrain[selectedTerrain].Rebuild();
	}
}