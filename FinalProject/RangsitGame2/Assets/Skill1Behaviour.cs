using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Behaviour : StateMachineBehaviour
{
    public float speedMove = 2.5f;
    Transform playerPos;
    Rigidbody2D rb;
    #region Skill1
    public float startTimeSkill1;
    private float timeSkill1;
    BossManager bossManager;
    #endregion
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.Find("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        timeSkill1 = startTimeSkill1;
        bossManager = animator.GetComponent<BossManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(playerPos.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speedMove * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (timeSkill1 <= 0)
        {
            bossManager.FinishSkill();
            animator.SetBool("Skill1",false);
        }
        else
        {
            timeSkill1 -= Time.deltaTime;
            bossManager.Invoke("Skill1", 1.7f);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

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
