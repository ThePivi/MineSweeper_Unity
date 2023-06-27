using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileClickHandler : MonoBehaviour
{
    public GameObject wholeTile;
    private void OnMouseUp()
    {
        BaseTile wholeTileScript = wholeTile.GetComponent<BaseTile>();
        wholeTileScript.ClickedOnTile();
    }
}
