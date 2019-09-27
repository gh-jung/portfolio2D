using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTile : Tile
{
    public override void OnClickTile(Image image)
    {
        base.OnClickTile(image);

        if (player.state == ObjectTypes.RUN || 
            player.state == ObjectTypes.DEAD ||
            player.currentPos == tileNumber)
            return;

        player.currentPos = tileNumber;
        Vector3 newPos = transform.position;
        newPos.z = player.transform.position.z;
        player.SetPos(newPos);
    }
}
