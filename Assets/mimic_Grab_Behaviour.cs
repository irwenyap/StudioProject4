using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimic_Grab_Behaviour : StateMachineBehaviour
{
    public float grabTimer;
    public  BoxCollider2D grabzone;
    GameObject[] Player;
    GameObject grabbedPlayer;
    public LayerMask player;
    float BiteTimer =0.0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float nearestdistance = float.MaxValue;
        grabzone = GameObject.FindGameObjectWithTag("Mimic").GetComponentInChildren<BoxCollider2D>();
        Player = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject go in Player)
        {
            if(grabzone.IsTouching(go.GetComponent<Collider2D>()))
            {
                float distance = new Vector2(grabzone.transform.position.x - go.transform.position.x, grabzone.transform.position.y - go.transform.position.y).magnitude;
                if ( distance < nearestdistance)
                {
                    if(grabbedPlayer != null)
                    {
                        //grabbed to false
                    }
                    //setting the grabbed player
                    grabbedPlayer = go;
                    
                    //grabbed to true

                    nearestdistance = distance;
                }
                
            }
        }
        grabTimer = 5.0f;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(grabTimer <0.0f || grabbedPlayer == null)
        {
            animator.SetBool("IsGrab", false);
            animator.SetBool("isMoveing", true);
            return;
        }
       else
        {
            grabTimer -= Time.deltaTime;
        }

        if(BiteTimer <= 0.0f && grabTimer <= 1.0f)
        {
            //deal  double damage
            BiteTimer = 1.0f;
        }
        else if(BiteTimer<= 0.0f)
        {
            //deal  damage
            BiteTimer = 1.0f;
        }
        else
        {
            BiteTimer -= Time.deltaTime;
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
