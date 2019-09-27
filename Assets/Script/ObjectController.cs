using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectController : MonoBehaviour
{
    protected Animator animator;

    public BulletInfo bullet;
    public ObjectInfo character;
    public Transform bulletInitPos;
    public ObjectTypes state;
    public int currentPos;

    public Canvas canvas;
    public TextMeshProUGUI text;
    public Slider hpBar;

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

        text.SetText(character.health.ToString());
        hpBar.value = character.health / (float)character.maxHealth;
    }

    public void OnDemage(BulletInfo bulletInfo)
    {
        character.health = Mathf.Clamp(character.health - bulletInfo.demage, 0, character.health - bulletInfo.demage);
        text.SetText(character.health.ToString());
        hpBar.value = character.health / (float)character.maxHealth;
        StopCoroutine(ShowHPUI());
        StartCoroutine(ShowHPUI());

        if (!character.Alive)
        {
            StopAllCoroutines();
            OnDead();
            GetComponent<BoxCollider2D>().enabled = false;
            canvas.enabled = false;
            if(tag == "Enemy")
            {
                GameManager.Instance.RemoveEnemy(currentPos);
                TileManager.Instance.UnSlectTile();
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().SetAttackState();
            }
        }
    }

    public void OnIdle()
    {
        if (!character.Alive)
            return;

        state = ObjectTypes.IDLE;
        animator.SetTrigger("Idle");
    }

    public void OnJump()
    {
        if (!character.Alive)
            return;

        state = ObjectTypes.JUMP;
        animator.SetTrigger("Jump");
    }

    public void OnRun()
    {
        if (!character.Alive)
            return;

        state = ObjectTypes.RUN;
        animator.SetTrigger("Run");
    }

    public void OnDead()
    {
        if (state == ObjectTypes.DEAD)
            return;
        animator.speed = 1;
        state = ObjectTypes.DEAD;
        animator.SetTrigger("Dead");
    }

    public void OnAttack()
    {
        if (!character.Alive)
            return;

        state = ObjectTypes.ATTACK;
        animator.SetTrigger("Attack");
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            BulletController bullet = collision.GetComponent<BulletController>();
            if (bullet.info.type == BulletTypes.EMENY && tag == "Enemy" ||
                bullet.info.type == BulletTypes.PLAYER && tag == "Player")
                return;

                int comparePos = this.gameObject.tag == "Player" ? currentPos / 4 : currentPos;
            if (comparePos == bullet.DestinationTile)
            {
                OnDemage(bullet.info);
                Destroy(collision.gameObject);
            }
        }
    }

    IEnumerator ShowHPUI()
    {
        canvas.enabled = true;
        yield return new WaitForSeconds(3);
        canvas.enabled = false;
    }
}
