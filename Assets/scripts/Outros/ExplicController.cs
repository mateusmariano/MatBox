using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplicController : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActvDtcExplicPanel(bool actv){
        if(actv){
            anim.SetBool("in", true);
            anim.SetBool("out", false);
        } else {
            anim.SetBool("in", false);
            anim.SetBool("out", true);
        }
    }
}
