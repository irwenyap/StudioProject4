using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimic_Move_Behaviour : StateMachineBehaviour
{
    public GameObject mimic;
    GameObject[] Player;
    GameObject grabbedPlayer;
    public LayerMask player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        mimic = GameObject.FindGameObjectWithTag("Mimic");
        Player = GameObject.FindGameObjectsWithTag("Player");
      
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float nearestdistance = float.MaxValue;
        foreach (GameObject go in Player)
        {
            float distance = new Vector2(mimic.transform.position.x - go.transform.position.x, mimic.transform.position.y - go.transform.position.y).magnitude;
            if (distance < nearestdistance)
            {

                //setting the grabbed player
                grabbedPlayer = go;


                nearestdistance = distance;
            }

        }
        Vector2 direction = new Vector2(grabbedPlayer.transform.position.x - mimic.transform.position.x, grabbedPlayer.transform.position.y - mimic.transform.position.y).normalized;
        mimic.transform.position += new Vector3(direction.x, direction.y) * Time.deltaTime; 
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
