using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMeleeControl : MonoBehaviour
{
    public float moveSpeed = 0.01f;
    Vector2 moveAxis;
    Vector2 mousePos;

    float health =  100f;

    float MeleeDetectRange = 7;
   public float MeleeAttackRange = 2;

    public Transform player;
    float shootBT = 0f;
    float DecisionChangeTimer;
    float DecisionValue;

   
   
    float AiDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    // Update is called once per frame
    void Update()
    {
        DecisionChangeTimer += Time.deltaTime;
        shootBT += Time.deltaTime;
        float DistanceAiNPlayer = Vector2.Distance(player.position, this.transform.position);
        if (DistanceAiNPlayer > MeleeDetectRange)
        {
            
            this.transform.Translate(moveSpeed, 0, 0);

            if (DecisionChangeTimer >3)
            {
                DecisionValue = Random.Range(0, 7);
                DecisionChangeTimer = 0;
            }
            switch(DecisionValue)
            {
                case 0:
                    AiDirection = 45;
                    break;
                case 1:
                    AiDirection = 90;
                    break;
                case 2:
                    AiDirection = 135;
                    break;
                case 3:
                    AiDirection = 180;
                    break;
                case 4:
                    AiDirection = 225;
                    break;
                case 5:
                    AiDirection = 270;
                    break;
                case 6:
                    AiDirection = 315;
                    break;
                case 7:
                    AiDirection = 360;
                    break;
            }
            transform.rotation = Quaternion.AngleAxis(AiDirection, Vector3.forward);
        }
        if (DistanceAiNPlayer < MeleeDetectRange)
        {
            Vector2 direction = player.position - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        }
        if (DistanceAiNPlayer < MeleeDetectRange && DistanceAiNPlayer > MeleeAttackRange)
        {
            this.transform.Translate(moveSpeed, 0, 0);

        }
        else if (DistanceAiNPlayer < MeleeAttackRange)
        {

        }
    }
}
