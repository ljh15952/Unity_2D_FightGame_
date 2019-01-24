using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Script : MonoBehaviour
{
    public Animator playerAni;
    public Rigidbody2D myRb; // 플레이어 리지드바디

    public AI_Manager AM;





    private void Start()
    {
        myRb = GetComponent<Rigidbody2D>();

        playerAni = GetComponent<Animator>();
    }

    private void Update()
    {
        Example();
    }





    void DashStatement()
    {
        Vector3 moveVec3 = myRb.velocity;
        moveVec3.x = AM.GetMoveSpeed() * Time.deltaTime * 3.5f; // 이동속도 * 프레임 * 대쉬 속도
        myRb.velocity = (moveVec3);

    }

    void IdleStatement()
    {
    }

    void WalkStatement()
    {
        Vector3 moveVec3 = myRb.velocity;
        moveVec3.x = AM.GetMoveSpeed() * Time.deltaTime * 1; // 이동속도 * 프레임 * 대쉬 속도
        myRb.velocity = (moveVec3);
    }

    void Attack()
    {


    }

    void Gather_Attack()
    {
        Debug.Log("가드중삐삐삐");
    }
    void Guard()
    {
        Vector3 moveVec3 = myRb.velocity;
        moveVec3.x = -AM.GetMoveSpeed() * Time.deltaTime * 1; // 이동속도 * 프레임 * 대쉬 속도
        myRb.velocity = (moveVec3);
        //AM.P2_AI.GetComponent<Player>().isGuard = true;
    }

    void Back_Jump()
    {

    }

    void Wait_Attack()
    {
        Vector3 moveVec3 = myRb.velocity;
        moveVec3.x = 0; // 이동속도 * 프레임 * 대쉬 속도
        myRb.velocity = (moveVec3);
    }


    void Example()
    {
        switch (AM.currentState)
        {
            case AI_STATE.Idle:
                //idle상태
                IdleStatement();
                break;
            case AI_STATE.Dash:
                DashStatement();
                //dash상태
                break;
            case AI_STATE.Walk:
                WalkStatement();
                //walk상태 
                break;
            case AI_STATE.Atk:
                //Attack();
                break;
            case AI_STATE.Gather_Atk:
                Gather_Attack();
                break;
            case AI_STATE.Guard:
                Guard();
                break;
            case AI_STATE.Back_Jump:
                Back_Jump();
                break;
            case AI_STATE.Wait_Attack:
                Wait_Attack();
                break;
        }
    }


}
