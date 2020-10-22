using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Attack,
        GetHit,
        Die
    };

    private Animator anim;
    private NavMeshAgent nav;
    private GameObject player;
    private PlayerInfo pi;
    private GameObject inven;
    private QuestItemBuffer qItemBuffer;
    //private DropManager dropManager;
    //[SerializeField] private GameObject itemPrefab;
    //[SerializeField] private GameObject goldPrefab;
    public GameObject dropManager;
    private DropManager dm;

    private float moveSpeed = 3.0f;
    private int maxHP = 200;
    private int currHP = 200;
    private float attackDistance = 2f;  //공격 거리
    private float currTime = 0;         //공격 딜레이 시간
    private float attackDelay = 2.0f;   //공격 딜레이
    private float distance;             //플레이어와의 거리
    private float findDistrance = 10f;  //플레이어를 감지하는 범위 거리

    //원위치 저장용
    Vector3 originPos;
    Quaternion originRot;

    public int enemyAtk;
    public bool isHit = false;
    public bool isDie = false;

    public EnemyState state;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        pi = player.GetComponent<PlayerInfo>();
        inven = player.transform.Find("InvenCanvas").Find("Inventory").gameObject;
        dropManager = GameObject.Find("DropManager").gameObject;
        dm = dropManager.GetComponent<DropManager>();

        //qItemBuffer = GameObject.Find("QuestItemBuffer").GetComponent<QuestItemBuffer>();
        //dropManager = GameObject.Find("ItemDropManager").GetComponent<DropManager>();
        //itemPrefab = dropManager.itemPrefab;
        //goldPrefab = dropManager.goldPrefab;

        enemyAtk = Random.Range(15, 30);
        state = EnemyState.Idle;

        originPos = transform.position;
        originRot = transform.rotation;
    }
    
    void Update()
    {
        distance = (player.transform.position - transform.position).magnitude;

        switch (state)
        {
            case EnemyState.Idle:

            break;
            case EnemyState.Attack:

                break;
            case EnemyState.GetHit:
                GetHit();
                break;
            case EnemyState.Die:
                Die();
                break;
            default:
                break;
        }
    }

    /*void Idle()
    {
        
    }

    void Attack()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < attackDistance)
        {

        }
    }*/

    void GetHit()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("GetHit") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            state = EnemyState.Idle;
            anim.Play("Idle");
        }
        
    }

    void Die()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Die") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            dm.DropItem(transform.position);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "PlayerAttack" && state != EnemyState.GetHit)
        {
            currHP -= pi.Atk();

            if (currHP > 0)
            {
                anim.SetTrigger("GetHit");
                state = EnemyState.GetHit;
            }

            if (currHP <= 0 && !isDie)
            {
                //currHP = 0;
                state = EnemyState.Die;
                isDie = true;

                anim.SetTrigger("Die");
                gameObject.layer = 9;
            }
        }
    }


}
