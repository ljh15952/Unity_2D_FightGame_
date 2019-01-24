using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectKind
{
    Attack1,Guard1
}
public class EffectMng : MonoBehaviour {

    public GameObject[] effects;
    public List<Effect> effectList;
    public void PlayEffect(EffectKind inputEffect)
    {
        Instantiate(effects[(int)inputEffect]);
    }
    public void PlayEffect(EffectKind inputEffect, Vector3 createVec3)
    {
        bool isCreate = false;
        for (int i = 0; i < effectList.Count; i++)
        {
            if (!effectList[i].gameObject.active && effectList[i].kind == inputEffect)
            {
                effectList[i].RePlay(createVec3);
                isCreate = true;
                break;
            }
        }
        if (!isCreate)
        {
            Effect tmepEffect = Instantiate(effects[(int)inputEffect], createVec3, Quaternion.identity).GetComponent<Effect>();
            tmepEffect.kind = inputEffect;
            effectList.Add(tmepEffect);
        }
    }
}
