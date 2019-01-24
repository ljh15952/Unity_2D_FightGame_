 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

    public Slider healthFill;
    public Slider healthFill2;

    public float currentHp;
    
    public float maxHp;
    public float currenteHp2;
    public float timer;
    public float timerLimit;

    bool isAttack=false;

    bool isDoing = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeHealth(-10);
            //StartCoroutine("SlowDecrease");
            timer = 0;
        }
        //if(isAttack)
      //  {
        //    isDoing = false;
          //  Debug.Log(isAttack);
        //}
        if(timer >= timerLimit)
        {
                currenteHp2 -= 30 * Time.deltaTime;
            healthFill2.value = currenteHp2 / maxHp;
            if (currenteHp2 <= currentHp)
                timer = 0;
        }
        if (currenteHp2 > currentHp)
            timer += Time.deltaTime;
    }



    public void ChangeHealth(int amount)
    {
        Debug.Log(11);
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxHp);

        healthFill.value = currentHp / maxHp;
    }

    IEnumerator SlowDecrease()
    {
        
        yield return new WaitForSeconds(timer);

        isDoing = true;

    }
}
