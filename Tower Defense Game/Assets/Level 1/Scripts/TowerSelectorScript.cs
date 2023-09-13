using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectorScript : MonoBehaviour
{
    public AnimationClip upClip;
    public AnimationClip downClip;

    public Animator anim;

    public void ChangeStates()
    {
        if(anim.GetBool("isOpen"))
        {
            anim.SetTrigger("Close");
        }
        else
        {
            anim.SetTrigger("Open");
        }
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));
    }
}
