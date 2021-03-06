﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlBloodBehaviour : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("beingSuckedBlood", false);
        animator.GetComponent<Human_PlayerController>().isBeingSucked = false;
        Debug.Log("isBeingSucked is false");
    }

}
