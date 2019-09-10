using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObj : MonoBehaviour
{
    public BulletInfo info;
    private SpriteRenderer mRenderer;
    private BoxCollider2D mCollider;
    private int destinationPos;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (int)info.type * info.speed * Time.deltaTime);
    }

    public void SetBulletObj(BulletInfo info, int destinationPos)
    {
        this.info = Instantiate(info);
        this.destinationPos = destinationPos;

        mRenderer = GetComponent<SpriteRenderer>();
        mCollider = GetComponent<BoxCollider2D>();
        mRenderer.sprite = info.bulletImage;
        mCollider.size = new Vector2(info.bulletImage.rect.width / GameManager.SIZE_X, info.bulletImage.rect.height / GameManager.SIZE_Y);
    }
}
