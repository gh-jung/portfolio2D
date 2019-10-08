using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ObjectInfo", menuName = "GameObj/ObjectInfo", order = 1)]
public class ObjectInfo : ScriptableObject
{
    public int maxHealth;
    public int health;
    public BulletInfo bullet;
    public float attackSpeed;
    public int demagePoint;
    public int deathPoint;
    public AnimatorOverrideController overrideController;
    public AudioClip[] deathSound;

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

    public void SetAttackSpeed(float speed)
    {
        attackSpeed = speed;
    }
}
