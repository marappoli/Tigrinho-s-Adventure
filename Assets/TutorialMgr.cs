using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMgr : MonoBehaviour
{
   Animator anim;

   private void Start(){
    anim = GetComponent<Animator>();
   }

   void ChangeAnimation(){
    anim.SetInteger("Change", anim.GetInteger("Change")+1);
   }

   private void Update(){
    if(Input.anyKeyDown){
        ChangeAnimation();
    }
   }
}
