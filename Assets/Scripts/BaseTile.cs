using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

[Serializable]
public class BaseTile : MonoBehaviour
{
    public TextMeshPro textMeshProText;
    public GameObject tile;
    private int tileValue;
    private int coordinateX;
    private int coordinateY;
    public bool isActivated = false;
    public MapGenerator mapGeneratorScript;
    public MenuController menuControllerScript;
    
    public void SetTileValue (int value) {
        tileValue = value;
    }
    
    public int GetTileValue () {
        return tileValue;
    }
    
    public void SetCoordinates (int x, int y) {
        this.coordinateX = x;
        this.coordinateY = y;
    }

    public void SetMapGeneratorScript (MapGenerator mapGenerator) {
        this.mapGeneratorScript = mapGenerator;
    }
    
    public bool IsCoordinates (int x, int y) {
        return (coordinateX == x) && (coordinateY == y);
    }

    public void ClickedOnTile () {
        if (isActivated) {
            // do nothing, because it is active already
        } else if (tileValue > 8) {
            // Scroll out, to see everything
            // Instantiate a curtain
            // activate every mine like a 0 tiles but slowly
            ActivateTile();
            menuControllerScript.ShowLose();
            mapGeneratorScript.ResetVariables();
        } else if (tileValue < 1) {
            mapGeneratorScript.ActivateAllEmptyConnection (coordinateX, coordinateY);
        } else {
            ActivateTile();
            mapGeneratorScript.IncereaseActivatedTileNumber();
        }
        if (mapGeneratorScript.CheckWinningCondition()) {
            menuControllerScript.ShowWin(mapGeneratorScript.mineNumber);
            mapGeneratorScript.ResetVariables();
        }
    }
   
    public void ActivateTile() {
        isActivated = true;
        tile.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        textMeshProText.text = tileValue>8?"*":tileValue.ToString();
        menuControllerScript = FindObjectOfType<MenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
