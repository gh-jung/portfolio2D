using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    protected Animator animator;

    public BulletInfo bullet;
    public ObjectInfo character;
    public Transform bulletInitPos;
    public ObjectTypes state;
    public int currentPos;

    protected void InitObj(string path)
    {
        animator = GetComponent<Animator>();
        ObjectInfo tempInfo = GameManager.LoadScriptable(path);
        if (tempInfo != null)
        {
            character = Instantiate(tempInfo);
            bullet = character.bullet;
        }
        animator.runtimeAnimatorController = character.overrideController;
    }

    public void OnDemage(BulletInfo bulletInfo)
    {
        character.health = Mathf.Clamp(character.health - bulletInfo.demage, 0, character.health - bulletInfo.demage);
        Debug.Log(character.health);
    }
}
