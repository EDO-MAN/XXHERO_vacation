using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enmey_Run : StateMachineBehaviour
{
    public float speed = 20f;
    public float attackRange1 = 1f;
  
    Transform player;
    Rigidbody2D rb;
    Enemy enemy;
	
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = animator.GetComponent<Rigidbody2D>();
		enemy = animator.GetComponent<Enemy>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		enemy.LookAtPlayer();

		Vector2 target = new Vector2(player.position.x, rb.position.y);
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);

		/*if (Vector2.Distance(player.position, rb.position) <= attackRange1)
		{
			animator.SetTrigger("TailAttack_motion");
		}*/
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//animator.ResetTrigger("TailAttack_motion");
	}
}
