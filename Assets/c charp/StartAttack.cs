using UnityEngine;

public class firePoint : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "attack" || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "AirKick" || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "AirPunch" || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Kick" || animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "CrouchPunch"){
            animator.transform.GetComponent<test>().hitagain = true;
        }
        animator.transform.GetComponent<test>().isbackward = true;
        animator.SetBool("Gatling",false);
        if (animator.transform.GetComponent<test>().isgrounded){
                animator.transform.GetComponent<Rigidbody2D>().linearVelocityX = 0;
        }


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
