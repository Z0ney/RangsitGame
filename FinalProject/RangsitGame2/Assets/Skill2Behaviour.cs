using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2Behaviour : StateMachineBehaviour
{
    public float speedMove = 2.5f;
    Transform playerPos;
    Rigidbody2D rb;
    #region Skill1
    public float startTimeSkill2;
    private float timeSkill2;
    BossManager bossManager;
    #endregion
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.Find("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        timeSkill2 = startTimeSkill2;
        bossManager = animator.GetComponent<BossManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(playerPos.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speedMove * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (timeSkill2 <= 0)
        {
            animator.SetBool("Skill2", false);
        }
        else
        {
            timeSkill2 -= Time.deltaTime;
            bossManager.Invoke("Skill2", 1.7f);
        }
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossManager.FinishSkill();
    }

}
