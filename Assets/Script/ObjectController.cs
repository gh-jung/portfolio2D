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

    public void OnIdle()
    {
        state = ObjectTypes.IDLE;
        animator.SetTrigger("Idle");
    }

    public void OnJump()
    {
        state = ObjectTypes.JUMP;
        animator.SetTrigger("Jump");
    }

    public void OnRun()
    {
        state = ObjectTypes.RUN;
        animator.SetTrigger("Run");
    }

    public void OnAttack()
    {
        state = ObjectTypes.ATTACK;
        animator.SetTrigger("Attack");
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            BulletController bullet = collision.GetComponent<BulletController>();
            int comparePos = this.gameObject.tag == "Player" ? currentPos / 4 : currentPos;
            if (comparePos == bullet.DestinationTile)
            {
                OnDemage(bullet.info);
                Destroy(collision.gameObject);
            }
        }
    }
}
