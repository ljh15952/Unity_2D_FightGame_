using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Fox : AI_Super_Class
{
    public Fox()
    {
        SetmoveStrategy(new Walk_Strategy());
        SetAtkStrategy(new Normal_Atk());
    }
}

class Iyori
{

}

