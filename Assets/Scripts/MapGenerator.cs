using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private int [,] map;
    public int mapSizeX = 5;
    public int mapSizeY = 5;
    public int mineNumber = 11;
    public BaseTile [,] basicTileMap;
    
    public void SetMineNumber (int newValue) {
        this.mineNumber = newValue;
        GenerateMap();
    }

    public void SetMapSizeX (int newValue) {
        this.mapSizeX = newValue;
        GenerateMap();
    }

    public void SetMapSizeY (int newValue) {
        this.mapSizeY = newValue;
        GenerateMap();
    }
    
    public void GenerateMap () {
    	if (CheckMineNumberInvalid()) {
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
                    basicTileMap [i, j].SetCoordinates(i, j);
                    basicTileMap [i, j].SetTileValue(map[i, j]);
    	        }
    	    }
    	}
    }

    public bool CheckMineNumberInvalid () {
        return (mineNumber < (mapSizeX*mapSizeY) && mineNumber > 0);
    }
    
    private int GenerateRandomCoordinate (int maximum) {
        return 1;//Mathf.Random(maximum);
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
        if (x > mapSizeX) {minimumX = x;}
        if (y > mapSizeY) {minimumY = y;}
        // 3by3 duble for to incerease neighorbords
        for (int i = minimumX; i < maximumX; i++) {
            for (int j = minimumY; j < maximumY; j++) {
                map [i, y] ++;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        map = new int [mapSizeX, mapSizeY];
        basicTileMap = new BaseTile [mapSizeX, mapSizeY];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
