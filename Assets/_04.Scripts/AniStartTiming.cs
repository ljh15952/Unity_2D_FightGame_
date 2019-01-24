using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AniStartTiming : MonoBehaviour {

    Animator myAni;
    public float aniStartTime;
    public SpriteRenderer[] fadeInSprite;
    public Light fadeInLight;
	// Use this for initialization
	void Start () {
        if (GetComponent<Animator>() != null)
        {
            myAni = GetComponent<Animator>();
            for (int i = 0; i < fadeInSprite.Length; i++)
                fadeInSprite[i].DOFade(0, 0);
            fadeInLight.DOColor(new Color(1, 1, 1, 0), 0);
            StartCoroutine("StartAni");
        }
	}
	
    IEnumerator StartAni()
    {
        yield return new WaitForSeconds(aniStartTime);
        for (int i = 0; i < fadeInSprite.Length; i++)
            fadeInSprite[i].DOFade(0.75f, 1);
        fadeInLight.DOColor(new Color(1, 1, 1, 1), 0.5f);
        myAni.enabled = true;
    }
}
