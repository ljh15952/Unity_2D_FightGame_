using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum states
//{
//    Idle,
//    Dash,
//    Walk,
//    Atk,
//    Gather_Atk,
//    Guard,
//    Back_Jump,
//    Wait_Attack,
//    Ultimate,
//    Tec1,
//    Tec2,
//}

public class AI_Super_Class
{
    private Strategy_Atk _Atk;
    private Strategy_Move _Move;

    public AI_STATE curState;

    public AI_Super_Class()
    {
        _Atk = null;
        _Move = null;
    }

    public void Attack()
    {
        _Atk.Atk();
    }
    public void Move(Animator a,Rigidbody2D rb,AI_Manager AM)
    {
        _Move.Move(a,rb,AM);
    }


    public void SetmoveStrategy(Strategy_Move move)
    {
        _Move = move;
    }
    public void SetAtkStrategy(Strategy_Atk Atk)
    {
        _Atk = Atk;
    }
}
