using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTile : Tile
{
    public override void OnClickTile(Image image)
    {
        base.OnClickTile(image);

        if(!TileManager.Instance.IsSameLine())
        {
            TileManager.Instance.SetMovePlay();
            player.OnRun();
        }
    }
}
