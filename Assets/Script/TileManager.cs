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
    public const float MAX_COLOR_VALUE = 255;
    public const float TILE_ALPHA = 0.5f;

    public Image[] playerTiles;
    public Image[] enemyTiles;

    private int playerPos = -1;
    private int SelectEnemyPos = -1;

    //적 타일 클릭 후 플레이어 타일도 바뀌는지 여부추가 
    public void UnSelectTile(int newNumber,Color oldColor, TileTypes type)
    {
        if (type == TileTypes.PLAYER_TILE)
        {
            if (playerPos <= -1)
            {
                playerPos = newNumber;
                return;
            }

            playerTiles[playerPos].color = oldColor;
            playerPos = newNumber;
        }
        else
        {
            if (SelectEnemyPos <= -1)
            {
                SelectEnemyPos = newNumber;
                return;
            }

            enemyTiles[SelectEnemyPos].color = oldColor;
            SelectEnemyPos = newNumber;
        }
    }

    public static void ConvertStringToColor(string colorValue, ref Color color)
    {
        color.r = Convert.ToInt32(colorValue.Substring(0, 2), 16) / MAX_COLOR_VALUE;
        color.g = Convert.ToInt32(colorValue.Substring(2, 2), 16) / MAX_COLOR_VALUE;
        color.b = Convert.ToInt32(colorValue.Substring(4, 2), 16) / MAX_COLOR_VALUE;
    }
}
