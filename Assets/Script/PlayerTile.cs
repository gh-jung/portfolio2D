using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerTile : MonoBehaviour, IOnClickTileEvent
{
    public LayerMask clickableLayer;
    public EventVector3 clickEvent;

    private PlayerController player;
    private int tileNumber;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        tileNumber = int.Parse(gameObject.name);
        clickEvent.AddListener(OnClickTile);

    }

    private void Update()
    {
            // Raycast into scene
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    clickEvent.Invoke(hit.collider.GetComponent<Transform>().position);
                }
            }
    }

    //플레이어 이동 구현 필요(애니메이션 점프)
    //공격 중일때만 가능하도록 수정
    public void OnClickTile(Vector3 pos)
    {
        player.currentPos = tileNumber;
        Vector3 newPos = pos;
        newPos.z = player.transform.position.z;
        player.transform.position = newPos;
    }
}
