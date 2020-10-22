using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackBtn : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject attackCollider;
    private Animator anim;
    private PlayerInfo pi;
    public bool isAttack;

    //공격 딜레이
    private float currTime = 0;
    private float attackDelay = 1f;

    void Start()
    {
        player = GameObject.Find("Player");
        pi = player.GetComponent<PlayerInfo>();
        anim = player.GetComponent<Animator>();

        attackCollider = player.transform.Find("AttackCollider").gameObject;

        isAttack = false;
    }

    public void Attack()
    {
        isAttack = true;
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetTrigger("Attack");
            attackCollider.gameObject.SetActive(true);
        }
       
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Attack();
        }

        if(isAttack)
        {
            currTime += Time.deltaTime;

            if (currTime > attackDelay)
            {
                isAttack = false;
                currTime = 0;
            }
        }
        if (!isAttack) attackCollider.gameObject.SetActive(false);
    }

    
}
