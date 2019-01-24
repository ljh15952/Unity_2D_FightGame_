using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lamda_Example : MonoBehaviour {


    delegate void MyDelegate2();

    delegate T MyDelegate<T>(T a, T b); //대리자 선언 함수포인터하고 비슷한거


    private void Start()
    {
        MyDelegate<int> add = (a, b) => a + b;
        MyDelegate2 lamda = () => Debug.Log("람다식");

        Debug.Log("11+22 = "+add(11,22));

        lamda();
    }
}
