using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileClickHandler : MonoBehaviour
{
    public GameObject wholeTile;
    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject ()) {
            BaseTile wholeTileScript = wholeTile.GetComponent<BaseTile>();
            wholeTileScript.ClickedOnTile();
        }
    }
}
