using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox_AI : MonoBehaviour
{
    Fox _Fox = new Fox();

    public AI_Manager AM;
    Animator Ani;
    Rigidbody2D myRb;
    private void Start()
    {
        Ani = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();
    }

    void UpdateFox()
    {
        if (_Fox.curState == AI_STATE.Atk ||
            _Fox.curState == AI_STATE.Guard ||
            _Fox.curState == AI_STATE.Back_Jump ||
            _Fox.curState == AI_STATE.Wait_Attack ||
            _Fox.curState == AI_STATE.Gather_Atk)
            return;

        if (AM.GetDistance() > 8.5f)
        {
            if (_Fox.curState != AI_STATE.Dash)
            {
                _Fox.curState = AI_STATE.Dash;
                Ani.SetTrigger("IsDashFirst");
                _Fox.SetmoveStrategy(new Run_Strategy());
            }
        }
        else if (AM.GetDistance() > 4.5f)
        {
            if (_Fox.curState == AI_STATE.Walk)
                return;

            int RandNum = Random.Range(0, 3);

            switch (RandNum)
            {
                case 0:
                    if (_Fox.curState != AI_STATE.Walk)
                    {
                        _Fox.curState = AI_STATE.Walk;
                        Ani.SetBool("IsMove", true);
                        Ani.SetTrigger("IsMoveFirst");
                        _Fox.SetmoveStrategy(new Walk_Strategy());
                    }
                    break;
                case 1:
                    if (_Fox.curState != AI_STATE.Back_Jump)
                    {
                        _Fox.curState = AI_STATE.Back_Jump;
                        Ani.SetBool("IsJumpFirst", true);
                        Ani.SetBool("IsJump", true);
                        _Fox.SetmoveStrategy(new BackJump_Strategy());
                    }
                    break;
                case 2:
                    if (_Fox.curState != AI_STATE.Wait_Attack)
                    {
                        _Fox.curState = AI_STATE.Wait_Attack;
                        _Fox.SetAtkStrategy(new WaitAtk_Strategy());
                    }
                    break;
            }
        }
        //else
        //{
        //    //if (isActivieReady)
        //    //    return;

        //    int RandNum = Random.Range(0, 3);
        //    Debug.Log("asdasds" + RandNum);
        //    switch (RandNum)
        //    {
        //        case 0:
        //            if (currentState != AI_STATE.Gather_Atk)
        //            {
        //                currentState = AI_STATE.Gather_Atk;

        //                randCount = Random.Range(0.01f, 0.3f);
        //                isActivieReady = true;
        //                StartCoroutine(Count());
        //            }
        //            break;
        //        case 1:
        //            if (currentState != AI_STATE.Guard)
        //            {
        //                currentState = AI_STATE.Guard;

        //                randCount = Random.Range(0.5f, 0.9f);
        //                isActivieReady = true;
        //                StartCoroutine(Guard());
        //            }
        //            break;
        //        case 2:
        //            if (currentState != AI_STATE.Wait_Attack)
        //            {
        //                currentState = AI_STATE.Wait_Attack;

        //                isActivieReady = true;
        //                StartCoroutine(Wait_Attack());
        //            }
        //            break;
        //    }


        //}
    }

    private void Update()
    {
        _Fox.Attack();
        _Fox.Move(Ani,myRb,AM);

        UpdateFox();
       
    }
}
