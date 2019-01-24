using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Strategy_Atk
{
    public abstract void Atk();
}

class Normal_Atk : Strategy_Atk
{
    public override void Atk()
    {
        Debug.Log("Normal Atk DESU!");
    }
}

class Skill_1 : Strategy_Atk
{
    public override void Atk()
    {
        Debug.Log("SKILL_1 DESU!");
    }
}

class Skill_2 : Strategy_Atk
{
    public override void Atk()
    {
        Debug.Log("SKILL_2 DESU!");
    }
}

class Combo_1 : Strategy_Atk
{
    public override void Atk()
    {
        Debug.Log("Combo_1 DESU!");
    }
}

class WaitAtk_Strategy : Strategy_Atk
{
    public override void Atk()
    {
        Debug.Log("WAIT ATK");
    }
}



public abstract class Strategy_Move
{
    public abstract void Move(Animator a,Rigidbody2D b,AI_Manager AM);
}

class Walk_Strategy : Strategy_Move
{
    public override void Move(Animator a,Rigidbody2D rb,AI_Manager AM)
    {
        Vector3 moveVec3 = rb.velocity;
        moveVec3.x = AM.GetMoveSpeed() * Time.deltaTime * 1; // 이동속도 * 프레임 * 대쉬 속도
        rb.velocity = (moveVec3);
    }
}

class Run_Strategy : Strategy_Move
{
    public override void Move(Animator a, Rigidbody2D rb, AI_Manager AM)
    {
        Vector3 moveVec3 = rb.velocity;
        moveVec3.x = AM.GetMoveSpeed() * Time.deltaTime * 3.5f; // 이동속도 * 프레임 * 대쉬 속도
        rb.velocity = (moveVec3);
    }
}

class BackWalk_Strategy : Strategy_Move
{
    public override void Move(Animator a, Rigidbody2D rb, AI_Manager AM)
    {
        Debug.Log("BackWalk DESU!!");
    }
}

class BackJump_Strategy : Strategy_Move
{
    public override void Move(Animator a, Rigidbody2D rb, AI_Manager AM)
    {
        Debug.Log("BACK_JUMP DESU!!");
    }
}

