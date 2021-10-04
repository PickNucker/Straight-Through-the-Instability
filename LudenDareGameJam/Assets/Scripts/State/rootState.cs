using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rootState : StateMachineBehaviour
{
    public string RootName;
    public bool RootMotion;

    public string dodgeName;
    public bool isDodging;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(RootName, RootMotion);
        animator.SetBool(dodgeName, isDodging);
    }


}
