using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdalhardWalk : StateMachineBehaviour
{
	public float speed = 2.5f;
	public float rangedAttackRange = 15f;
	public float dashAttackRange = 5f;

	Transform player;
	Rigidbody2D rigid;
	AdalhardAI adalhardAI;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rigid = animator.GetComponent<Rigidbody2D>();
		adalhardAI = animator.GetComponent<AdalhardAI>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		adalhardAI.LookAtPlayer();

		Vector2 target = new Vector2(player.position.x, rigid.position.y);
		Vector2 newPos = Vector2.MoveTowards(rigid.position, target, speed * Time.fixedDeltaTime);


		rigid.MovePosition(newPos);

		if(Vector2.Distance(player.position, rigid.position) >= dashAttackRange && Vector2.Distance(player.position, rigid.position) <= 12 && animator.GetBool("DashComplete") == false)
		{
			animator.SetTrigger("DashAttack");
		}
		if (Vector2.Distance(player.position, rigid.position) >= rangedAttackRange && Vector2.Distance(player.position, rigid.position) <= 16 && animator.GetBool("ProjectileBarrageComplete") == false)
		{
			animator.SetTrigger("ProjectileBarrage");
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.ResetTrigger("DashAttack");
		animator.ResetTrigger("ProjectileBarrage");
	}
}
