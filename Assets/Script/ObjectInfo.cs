using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectInfo", menuName = "GameObj/ObjectInfo", order = 1)]
public class ObjectInfo : ScriptableObject
{
    public int health;
    //public Bullet bullet;
    public float attackSpeed;

    public bool Alive
    {
        get
        {
            return health != 0;
        }
    }
    
    public float AttackSpeed
    {
        get
        {
            return attackSpeed;
        }
        set
        {
            attackSpeed *= value;
        }
    }
}
