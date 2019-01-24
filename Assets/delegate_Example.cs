using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delegate_Example : MonoBehaviour
{
    delegate T MyDelegate<T>(T a, T b); //대리자 선언 함수포인터하고 비슷한거


    void Calculator<T>(T a, T b, MyDelegate<T> dele)
    {
        Debug.Log(dele(a, b));
    }

    public int Plus(int a, int b) { return a + b; }
    public float Plus(float a, float b) { return a + b; }
    public double Plus(double a,double b) { return a + b; }

    private void Start()
    {
        MyDelegate<int> plus1 = new MyDelegate<int>(Plus);
        MyDelegate<float> plus2 = new MyDelegate<float>(Plus);
        MyDelegate<double> plus3 = new MyDelegate<double>(Plus);

        Calculator(11, 22, plus1);
        Calculator(33, 22, plus2);
        Calculator(11, 22, plus3);
    }

}
