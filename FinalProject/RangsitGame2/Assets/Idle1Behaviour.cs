using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle1Behaviour : StateMachineBehaviour
{
    public float speedMove = 2.5f;
    Transform playerPos;
    Rigidbody2D rb;
    BossManager bossManager;
    #region Skill
    public float startCooldownSkill;
    private float cooldownSkill;
    private int Skill;
    private float fly;

    #endregion
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.Find("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        cooldownSkill = startCooldownSkill;
        bossManager = animator.GetComponent<BossManager>();
        Skill = Random.Range(0, 2);
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Movement
        fly = Random.Range(-.1f, .1f);  
        //CoolDown For NextSkill
        Vector2 target = new Vector2(playerPos.position.x, playerPos.position.y + 5f + fly);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speedMove * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
        if (cooldownSkill <= 0)
        {
            if (Skill == 0)
            {
                animator.SetBool("Skill1", true);
            }
            else
            {
                animator.SetBool("Skill2", true);

            }
        }
        else
        {
            cooldownSkill -= Time.deltaTime;
            bossManager.FinishSkill();
        }
        Debug.Log(Skill);
        //Skill 1 shoot
       
       
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
