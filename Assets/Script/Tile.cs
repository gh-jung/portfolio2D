using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tile : MonoBehaviour
{
    public TileTypes type;
    public int tileNumber;
    protected PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        tileNumber = int.Parse(gameObject.name);
    }

    public virtual void OnClickTile(Image image)
    {
        if (player.state != ObjectTypes.ATTACK)
            return;

        Color newColor = Color.black;
        Color oldColor = Color.black;
        string oldTile;
        string newTile;

        if(type == TileTypes.PLAYER_TILE)
        {
            oldTile = TileManager.PLAYER_NORMAL_TILE;
            newTile = TileManager.PLAYER_SELLECT_TILE;
        }
        else
        {
            oldTile = TileManager.ENEMY_NORMAL_TILE;
            newTile = TileManager.ENEMY_SELLECT_TILE;
        }


        oldColor.a = newColor.a = TileManager.TILE_ALPHA;

        TileManager.SetColorToHexRGB(oldTile, ref oldColor);
        TileManager.SetColorToHexRGB(newTile, ref newColor);

        image.color = newColor;
        TileManager.Instance.UnSelectTile(tileNumber, oldColor, type);
    }
}
