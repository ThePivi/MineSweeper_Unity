using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
//using Tmp;

[Serializable]
public class BaseTile : MonoBehaviour
{
    public GameObject text;
    private int tileValue;
    private int coordinateX;
    private int coordinateY;
    
    public void SetTileValue (int value) {
        tileValue = value;
    }
    
    public void SetCoordinates (int x, int y) {
        this.coordinateX = x;
        this.coordinateY = y;
    }
    
    public bool IsCoordinates (int x, int y) {
        return (coordinateX == x) && (coordinateY == y);
    }
    
    public void OnButtonDown() {
        if (tileValue > 8) {
            Debug.Log("game over");
        } else if (tileValue < 1) {
//            GameObject.Find("MapGenerator").GetComponent<Script>("MapGenerator").ActivateAllEmptyConnection (coordinateX, coordinateY);
        } else {
            ActivateTile();
        }
    }
    
    public void ActivateTile() {
        text.SetEnabled(true);
        SetEnabled(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        text.SetEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
