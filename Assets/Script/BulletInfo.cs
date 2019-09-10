using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletInfo", menuName = "GameObj/BulletInfo", order = 0)]
public class BulletInfo : ScriptableObject
{
    public int demage;
    public BulletTypes type;

    public ObjectInfo OnDemage(ObjectInfo obj)
    {
        obj.health = Mathf.Max(obj.health - demage, 0);
        return obj;
    }
}
