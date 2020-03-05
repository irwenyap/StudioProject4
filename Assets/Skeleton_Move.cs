using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_Move : StateMachineBehaviour
{
    public Boss_Water_Stats stats;
    public Transform myPos;
    public GameObject nearesttarget;
    public float shootTImer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        stats = animator.gameObject.GetComponent<Boss_Water_Stats>();
        myPos = animator.gameObject.transform;
        shootTImer = 1.0f;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float nearestdistance = float.MaxValue;
        foreach (GameObject go in stats.player.playerList)
        {
            float distance = new Vector2(myPos.transform.position.x - go.transform.position.x, myPos.transform.position.y - go.transform.position.y).magnitude;
            if (distance < nearestdistance)
            {

                //setting the grabbed player
               nearesttarget = go;


                nearestdistance = distance;
            }
        }
        Vector2 direction = new Vector2(nearesttarget.transform.position.x - myPos.transform.position.x, nearesttarget.transform.position.y - myPos.transform.position.y).normalized;
        myPos.transform.position += new Vector3(direction.x, direction.y) * Time.deltaTime*stats.moveSpeed;
        if (shootTImer <= 0)
        {
            GameObject bullet = Instantiate(stats.bulletRef, myPos.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * 8);
            shootTImer = 1.0f;
        }
        else
            shootTImer -= Time.deltaTime;

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
