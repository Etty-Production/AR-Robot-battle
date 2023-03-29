using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrakeAll : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        string[] Anims = new string[] { "Run", "Shoot" };
        for (int i = 0; i < Anims.Length; i++)
        {
            animator.SetBool("EndOf" + Anims[i], false);
            animator.SetBool( Anims[i], false);
        }
    }

}
