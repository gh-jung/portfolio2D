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

    private const float MAX_COLOR_VALUE = 255;
    private const int NOT_INIT = -1;

    public Image[] playerTiles;
    public Image[] enemyTiles;

    private int playerPos = -1;
    private int SelectEnemyPos = -1;

    private void OnTileColorChange(int number, Color color, ref int targetTileNumber, ref Image[] targetTileArray)
    {
        if (targetTileNumber <= NOT_INIT)
        {
            targetTileNumber = number;
            return;
        }
        targetTileArray[targetTileNumber].color = color;
        targetTileNumber = number;
    }

    //적 타일 클릭 후 플레이어 타일도 바뀌는지 여부추가 
    public void UnSelectTile(int newNumber,Color oldColor, TileTypes type)
    {
        if (type == TileTypes.PLAYER_TILE)
        {
            OnTileColorChange(newNumber, oldColor, ref playerPos, ref playerTiles);
        }
        else
        {
            OnTileColorChange(newNumber, oldColor, ref SelectEnemyPos, ref enemyTiles);
        }
    }

    public static void SetColorToHexRGB(string hexColorString, ref Color color)
    {
        color.r = Convert.ToInt32(hexColorString.Substring(0, 2), 16) / MAX_COLOR_VALUE;
        color.g = Convert.ToInt32(hexColorString.Substring(2, 2), 16) / MAX_COLOR_VALUE;
        color.b = Convert.ToInt32(hexColorString.Substring(4, 2), 16) / MAX_COLOR_VALUE;
    }
}
