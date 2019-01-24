using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AI_STATE
{
    Idle = 1,
    Dash = 2,
    Walk = 3,
    Atk = 4,
    Gather_Atk = 5,
    Guard = 6,
    Back_Jump = 7,
    Wait_Attack
}

public class AI_Manager : MonoBehaviour
{

    public GameObject P2_AI;
    public GameObject P1;

    public float distance;

    public float moveSpeed;

    public AI_STATE currentState;

    public AI_Script my_char;

    public float randCount;

    public bool isActivieReady = false;
    private void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        isGameStart = true;
      //  StartCoroutine(Update__());
    }
    IEnumerator Update__()
    {
        while (true)
        {
            Update_Default_Statement();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void Update()
    {
        if (!isGameStart)
            return;
    }
    bool isGameStart = false;


    public float GetDistance()
    {
        distance = P2_AI.transform.position.x - P1.transform.position.x;
        return distance > 0 ? distance : -distance;
    }

    public float GetMoveSpeed()
    {
        return distance > 0 ? moveSpeed : -moveSpeed;
    }


    void Update_Default_Statement()
    {
        if (currentState == AI_STATE.Atk || 
            currentState == AI_STATE.Guard || 
            currentState == AI_STATE.Back_Jump||
            currentState == AI_STATE.Wait_Attack||
            currentState == AI_STATE.Gather_Atk)
            return;

        if (GetDistance() > 8.5f)
        {
            if (currentState != AI_STATE.Dash)
            {
                currentState = AI_STATE.Dash;
                my_char.playerAni.SetTrigger("IsDashFirst");
            }
        }
        else if (GetDistance() > 4.5f)
        {
            //if (isActivieReady)
            //    return;
            if (currentState == AI_STATE.Walk) //밑에랑중복
                return;
            int RandNum = Random.Range(0, 3);

            switch (RandNum)
            {
                case 0:
                    if (currentState != AI_STATE.Walk)
                    {
                        my_char.playerAni.SetBool("IsMove", true);
                        my_char.playerAni.SetTrigger("IsMoveFirst");
                        currentState = AI_STATE.Walk;
                    }
                    break;
                case 1:
                    if (currentState != AI_STATE.Back_Jump)
                    {
                        currentState = AI_STATE.Back_Jump;
                        my_char.playerAni.SetBool("IsJumpFirst", true);
                        my_char.playerAni.SetBool("IsJump", true);
                        StartCoroutine(Jump());
                        isActivieReady = true;
                    }
                    break;
                case 2:
                    if (currentState != AI_STATE.Wait_Attack)
                    {
                        currentState = AI_STATE.Wait_Attack;

                        isActivieReady = true;
                        StartCoroutine(Wait_Attack());
                    }
                    break;
            }
        }
        else
        { 
            //if (isActivieReady)
            //    return;

            int RandNum = Random.Range(0, 3);
            Debug.Log("asdasds" + RandNum);
            switch (RandNum)
            {
                case 0:
                    if (currentState != AI_STATE.Gather_Atk)
                    {
                        currentState = AI_STATE.Gather_Atk;

                        randCount = Random.Range(0.01f, 0.3f);
                        isActivieReady = true;
                        StartCoroutine(Count());
                    }
                    break;
                case 1:
                    if (currentState != AI_STATE.Guard)
                    {
                        currentState = AI_STATE.Guard;

                        randCount = Random.Range(0.5f, 0.9f);
                        isActivieReady = true;
                        StartCoroutine(Guard());
                    }
                    break;
                case 2:
                    if (currentState != AI_STATE.Wait_Attack)
                    {
                        currentState = AI_STATE.Wait_Attack;

                        isActivieReady = true;
                        StartCoroutine(Wait_Attack());
                    }
                    break;
            }


        }
    }
    //Wait Attack
    IEnumerator Wait_Attack()
    {
        float time=0f;
        P2_AI.GetComponent<Player>().isGuard = true;
        my_char.playerAni.SetTrigger("IsIdle");
        while (GetDistance() > 3f&&time<3)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine("Atk_Delay");
        Debug.Log("PLZ RAMDOM ATK");

        P2_AI.GetComponent<Player>().isGuard = false;
    }
    //


    //일반공격
    IEnumerator Count()
    {
        currentState = AI_STATE.Atk;

        my_char.playerAni.SetTrigger("IsIdle");

        P2_AI.GetComponent<Player>().isGuard = true;

        yield return new WaitForSeconds(randCount);

        P2_AI.GetComponent<Player>().isGuard = false;


        StartCoroutine("Atk_Delay");
    } //딜레이준뒤에
    IEnumerator Atk_Delay()
    {
        my_char.playerAni.SetTrigger("WeekPunch");
        yield return new WaitForSeconds(0.3f);
        my_char.playerAni.SetBool("b_NextPunch", true);
        yield return new WaitForSeconds(0.3f);
        my_char.playerAni.SetTrigger("IsIdle");
        my_char.playerAni.SetBool("b_NextPunch", false);
        currentState = AI_STATE.Idle;
        isActivieReady = false;
    }  //공격
    //

    //가드
    IEnumerator Guard()
    {
        my_char.playerAni.SetBool("IsMove", true);
        my_char.playerAni.SetTrigger("IsMoveFirst");
        P2_AI.GetComponent<Player>().isGuard = true;
        yield return new WaitForSeconds(randCount);
        P2_AI.GetComponent<Player>().isGuard = false;
        currentState = AI_STATE.Idle;
        isActivieReady = false;
    }
    //

    //점프
    IEnumerator Jump()
    {
        Vector3 moveVec3 = my_char.myRb.velocity;
        if (P2_AI.transform.position.x > 7.5f || P2_AI.transform.position.x < -7.5f)
        {
            moveVec3 = new Vector3(GetMoveSpeed() * Time.deltaTime, 10, 0); // 기본 값 * 점프력
        }
        else
        {
            moveVec3 = new Vector3(-GetMoveSpeed() * Time.deltaTime, 10, 0); // 기본 값 * 점프력
        }
        
        my_char.myRb.velocity = (moveVec3);
        yield return new WaitForSeconds(2f);
        currentState = AI_STATE.Idle;
        isActivieReady = false;
    }
    //
}
