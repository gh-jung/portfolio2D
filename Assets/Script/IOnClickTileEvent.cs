using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IOnClickTileEvent
{
    void OnClickTile(Vector3 pos);
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>
{

}