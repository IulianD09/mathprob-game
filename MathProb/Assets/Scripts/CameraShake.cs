using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Animator anim;

    public IEnumerator CamShake()
    {
        anim.SetBool("shake", true);
        yield return new WaitForSeconds(0.3f);
        anim.SetBool("shake", false);
    }
}
