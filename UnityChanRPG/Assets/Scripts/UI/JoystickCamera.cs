using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Utility;

public class JoystickCamera : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private SmoothFollow camFollow;
    [SerializeField] private RectTransform rect_BG;
    [SerializeField] private RectTransform rect_JS;
    //조이스틱 최초 지점 저장용
    private Vector2 jsInitPos;
    //조이스틱 배경의 반경
    private float radius;

    [SerializeField] private Camera mainCam;
    //[SerializeField] private GameObject player;
    [SerializeField] private float rotateSpeed;
    //카메라 수직 이동 속도
    [SerializeField] private float verticalSpeed = 0.2f;

    private bool isTouch = false;
    private Vector3 moveRotation;

    private float currAngleX;
    private float currAngleY;

    void Start()
    {
        //player = GameObject.Find("Player");
        mainCam = Camera.main;
        camFollow = mainCam.GetComponent<SmoothFollow>();
        jsInitPos = rect_JS.position;
        radius = rect_BG.rect.width * 0.5f;
        verticalSpeed = 0.2f;
    }

    void Update()
    {
        if (isTouch)
        {
            //player.transform.eulerAngles += moveRotation;
            //mainCam.transform.eulerAngles += moveRotation;
            camFollow.currentRotationAngleY += currAngleY;

            // 조이스틱 수직 조절로 카메라 높이 값 조절
            if (rect_JS.position.y > jsInitPos.y)
            {
                camFollow.height -= verticalSpeed * Time.deltaTime;
            }
            else if (rect_JS.position.y < jsInitPos.y)
            {
                camFollow.height += verticalSpeed * Time.deltaTime;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 value = eventData.position - (Vector2)rect_BG.position;
        value = Vector2.ClampMagnitude(value, radius);
        rect_JS.localPosition = value;

        float distance = Vector2.Distance(rect_BG.position, rect_JS.position) / radius; // 거리 차에 따라 속도를 조절
        //value = value.normalized;
        currAngleY = value.x * rotateSpeed * distance * Time.deltaTime; // Y축 기준 좌우회전 

        
        
        //moveRotation = new Vector3(0f, currAngle, 0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouch = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouch = false;
        rect_JS.localPosition = Vector3.zero;
        moveRotation = Vector3.zero;
    }
}
