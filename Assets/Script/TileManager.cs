using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : Singleton<TileManager>
{
    public const string PLAYER_SELLECT_TILE = "FF6400";
    public const string ENEMY_SELLECT_TILE = "00C800";
    public const string PLAYER_NORMAL_TILE = "9C3200";
    public const string ENEMY_NORMAL_TILE = "006000";
    public const float TILE_ALPHA = 0.5f;

    public Image[] playerTiles;
    public GameObject[] enemyTiles;

    public int playerPos = -1;
    private int SelectEnemyPos;


    public void UnSelectTile(int newNumber)
    {
        if (playerPos <= -1)
        {
            playerPos = newNumber;
            return;
        }

        Color oldColor = Color.black;
        oldColor.a = TILE_ALPHA;

        oldColor.r = Convert.ToInt32(PLAYER_NORMAL_TILE.Substring(0, 2), 16) / 255.0f;
        oldColor.g = Convert.ToInt32(PLAYER_NORMAL_TILE.Substring(2, 2), 16) / 255.0f;
        oldColor.b = Convert.ToInt32(PLAYER_NORMAL_TILE.Substring(4, 2), 16) / 255.0f;

        playerTiles[playerPos].color = oldColor;
        playerPos = newNumber;
    }
}
