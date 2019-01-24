using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float playTime;
    public EffectKind kind;
    Animator effAni;
    // Use this for initialization
    void Start()
    {
        effAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void RePlay(Vector3 createVec3)
    {
        effAni.SetTrigger("Play");
        transform.position = createVec3;
        gameObject.SetActive(true);
    }
    private void Update()
    {
        if (gameObject.active && effAni.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            gameObject.SetActive(false);
    }
}
