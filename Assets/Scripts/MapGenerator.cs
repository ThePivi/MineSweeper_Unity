using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int [,] map;
    public int mapSizeX = 0;
    public int mapSizeY = 0;
    public int mineNumber = 0;
    public float gap = 0.1f;
    public GameObject [,] mapBaseTile;
    public GameObject wholeTile;
    private int activatedTileNumber = 0;
    private int difficulty = 2;
    private int mapWidth = 2;
    private int mapHeight = 2;
    public bool gameIsActive = false;
    public void SetDifficulty (int newDifficulty) {
        difficulty = newDifficulty;
    }
    public void SetMapWidth (int newWidth) {
        mapWidth = newWidth;
    }
    public void SetmapHeight (int newHeight) {
        mapHeight = newHeight;
    }
    private void RecalculateMapDetails() {
        switch(mapWidth) {
            case 0:
                SetMapSizeX(5);
                break;
            case 1:
                SetMapSizeX(10);
                break;
            case 2:
                SetMapSizeX(15);
                break;
            case 3:
                SetMapSizeX(20);
                break;
            case 4:
                SetMapSizeX(30);
                break;
            case 5:
                SetMapSizeX(50);
                break;
        }
        switch(mapHeight) {
            case 0:
                SetMapSizeY(5);
                break;
            case 1:
                SetMapSizeY(10);
                break;
            case 2:
                SetMapSizeY(15);
                break;
            case 3:
                SetMapSizeY(20);
                break;
            case 4:
                SetMapSizeY(30);
                break;
            case 5:
                SetMapSizeY(50);
                break;
        }
        SetMineNumber((int)((mapSizeX*mapSizeY)*(0.19*difficulty))+1);
    }
    
    public void SetMineNumber (int newValue) {
        this.mineNumber = newValue;
    }

    public void SetMapSizeX (int newValue) {
        this.mapSizeX = newValue;
    }

    public void SetMapSizeY (int newValue) {
        this.mapSizeY = newValue;
    }

    public bool GetGameIsActive () {
        return gameIsActive;
    }
    
    public void GenerateMap () {
        if (map != null) {
            DeleteMap ();
        }
        
        RecalculateMapDetails();
        map = new int [mapSizeX, mapSizeY];
        mapBaseTile = new GameObject [mapSizeX, mapSizeY];
    	
        if (!CheckMineNumberInvalid()) {
    	    Debug.Log("Map generation is not possible. Setup the mine number properly.");
    	} else {
      	    int mineCounter = 0;
    	    while (mineCounter < mineNumber) {
            //here required a random times mineNumber
                int x = GenerateRandomCoordinate(mapSizeX);
                int y = GenerateRandomCoordinate(mapSizeY);
            //and a quick check to know if it is occupied
                while (IsOccupied (x, y)) {
                    x = GenerateRandomCoordinate(mapSizeX);
                    y = GenerateRandomCoordinate(mapSizeY);
                }
                PlaceMine(x, y);
                IncereaseNegirhorbordsValues (x, y);
                mineCounter ++;
	        }
  	    
    	    for (int i = 0; i < mapSizeX; i++) {
    	        for (int j = 0; j < mapSizeY; j++) {
                    mapBaseTile [i, j] = InstantiateNewTile(i, j);
    	        }
    	    }
    	}
        gameIsActive = true;
    }

    private void DeleteMap() {
        for (int i = 0; i < mapSizeX; i++) {
            for (int j = 0; j < mapSizeY; j++) {
                Destroy(mapBaseTile [i, j]);
                mapBaseTile [i, j] = null;
            }
        }
        for (int i = 0; i < mapSizeX; i++) {
            System.Array.Clear(map, i * mapSizeY, mapSizeY);
        }
    }
    public bool CheckMineNumberInvalid () {
        return (mineNumber < (mapSizeX*mapSizeY) && mineNumber > 0);
    }
    
    private int GenerateRandomCoordinate (int maximum) {
        return Random.Range(0, maximum);
    }
    
    private bool IsOccupied (int x, int y) {
        return map[x, y] > 8;
    }
    
    private void PlaceMine (int x, int y) {
        map[x, y] = 9;
    }

    private void IncereaseNegirhorbordsValues (int x, int y) {
    	int minimumX = x - 1;
    	int minimumY = y - 1;
    	int maximumX = x + 1;
    	int maximumY = y + 1;
        if (x == 0) {minimumX = x;}
        if (y == 0) {minimumY = y;}
        if (maximumX == mapSizeX) {maximumX = x;}
        if (maximumY == mapSizeY) {maximumY = y;}
        // 3by3 duble for to incerease neighorbords
        for (int i = minimumX; i <= maximumX; i++) {
            for (int j = minimumY; j <= maximumY; j++) {
                map [i, j] ++;
            }
        }
    }
    
    private GameObject InstantiateNewTile (int i, int j) {
        GameObject newTile = Instantiate(wholeTile, new Vector3(((1 + gap) * i)+0.5f, ((1 + gap) * j)+0.5f, 0f), transform.rotation);
        BaseTile actualTileScript = newTile.GetComponent<BaseTile>();
        actualTileScript.SetCoordinates(i, j);
        actualTileScript.SetTileValue(map[i, j]);
        actualTileScript.SetMapGeneratorScript(this);
        return newTile;
    }

    public void ActivateAllEmptyConnection (int x, int y) {
        int minimumX = x - 1;
    	int minimumY = y - 1;
    	int maximumX = x + 1;
    	int maximumY = y + 1;
        if (x == 0) {minimumX = x;}
        if (y == 0) {minimumY = y;}
        if (maximumX == mapSizeX) {maximumX = x;}
        if (maximumY == mapSizeY) {maximumY = y;}
        // 3by3 duble for to incerease neighorbords
        for (int i = minimumX; i <= maximumX; i++) {
            for (int j = minimumY; j <= maximumY; j++) {
                BaseTile baseTileScript = mapBaseTile [i, j].GetComponent<BaseTile>();
                if (baseTileScript.GetTileValue() == 0 &&
                    !baseTileScript.isActivated) {
                    baseTileScript.ActivateTile();
                    IncereaseActivatedTileNumber();
                    ActivateAllEmptyConnection(i, j);
                } else if (!baseTileScript.isActivated) {
                    baseTileScript.ActivateTile();
                    IncereaseActivatedTileNumber();
                }
            }
        }
    }
    public void IncereaseActivatedTileNumber () {
        activatedTileNumber++;
    }

    public bool CheckWinningCondition() {
        return (mapSizeX*mapSizeY) == (activatedTileNumber+mineNumber);
    }
    public void ResetVariables() {
        activatedTileNumber = 0;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
