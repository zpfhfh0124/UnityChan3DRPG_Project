using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class JoystickPlayer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private RectTransform rect_BG;
    [SerializeField] private RectTransform rect_JS;

    private float radius;

    [SerializeField] private GameObject player;
    [SerializeField] private Animator anim;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Camera mainCam;

    private bool isTouch = false;
    private Vector3 direction; //플레이어 애니메이션 스크립트에서 움직이고 있는 방향과 값에 따라 애니메이션을 처리할 것.
    private float distance;
    
    private enum PlayerState : int
    {
        Stand = 0,
        Walk,
        Run,
        Attack
    }

    private PlayerState state;

    void Start()
    {
        mainCam = Camera.main;
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();
        state = PlayerState.Stand;
        radius = rect_BG.rect.width * 0.5f;
        moveSpeed = 1.0f;
    }

    void Update()
    {
        switch (state)
        {
            case PlayerState.Stand:
                {
                    StartCoroutine(SetAniStand());
                }
            break;
            case PlayerState.Walk:
                {
                    StartCoroutine(SetAniWalk());
                }
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Attack:
                break;
        }

        if(isTouch)
        {
            player.transform.Translate(direction * moveSpeed * distance * Time.deltaTime, Space.World);
            //player.transform.Translate(direction);
            //player.transform.position += direction;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 dataPos = eventData.position;
        Vector2 value = eventData.position - (Vector2)rect_BG.position;
        value = Vector2.ClampMagnitude(value, radius); //좌표 가두기 
        rect_JS.localPosition = value;
        
        distance = Vector2.Distance(rect_BG.position, rect_JS.position) / radius; // 거리 차에 따라 속도를 조절
        //distance는 0.0f ~ 1.0f
        value = value.normalized; // 벡터의 정규화 방향만 남김
        //direction = new Vector3(value.x * moveSpeed * distance * Time.deltaTime, 0f, value.y * moveSpeed * distance * Time.deltaTime);
        direction = new Vector3(value.x, 0, value.y);
        direction = Camera.main.transform.TransformDirection(direction.normalized); //카메라 기준 방향으로 계산해서 움직인다.
        direction.y = 0f;
        direction = direction.normalized;
        //direction = Camera.main.transform.TransformDirection(direction);
        state = PlayerState.Walk;

        //player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(joyVec.x, joyVec.y) * Mathf.Rad2Deg, 0);
        player.transform.eulerAngles = new Vector3(0, Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_JS.localPosition = Vector3.zero;
        direction = Vector3.zero;
        state = PlayerState.Stand;
    }

    IEnumerator SetAniStand()
    {
        yield return null;
        anim.SetBool("IsMove", false);
    }

    IEnumerator SetAniWalk()
    {
        yield return null;
        anim.SetBool("IsMove", true);
    }


}
