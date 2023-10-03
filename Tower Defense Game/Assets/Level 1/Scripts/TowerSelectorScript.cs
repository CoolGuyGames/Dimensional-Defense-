using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectorScript : MonoBehaviour
{
    public AnimationClip upClip;
    public AnimationClip downClip;

    public Animator anim;

    public GameObject[] arrowButtons;
    public GameObject[] backgroundSprites;
    private int currentBackground = 0;

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

    private void Update()
    {
        for(int i = 0; i < backgroundSprites.Length; i++)
        {
            if(i != currentBackground)
                backgroundSprites[i].SetActive(false);
            else
                backgroundSprites[currentBackground].SetActive(true);
        }

        if(currentBackground == 0)
        {
            arrowButtons[0].SetActive(false);
        }
        else
        {
            arrowButtons[0].SetActive(true);
        }

        if(currentBackground == 3)
        {
            arrowButtons[1].SetActive(false);
        }
        else
        {
            arrowButtons[1].SetActive(true);
        }
    }

    public void RightArrow()
    {
        currentBackground++;
    }

    public void LeftArrow()
    {
        currentBackground--;
    }
}
