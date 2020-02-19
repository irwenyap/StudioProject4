using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AiControl : MonoBehaviour
{
    // Public
    public float moveSpeed = 0.01f;
    public Rigidbody2D bullet;
    // Private
    Rigidbody2D myRigidbody;
    Vector2 moveAxis;
    Vector2 mousePos;
    float health = 100f;
    public Transform player;
    float shootBT = 0f;
    int weaponType;

    private void Start()
    {
        //myRigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        shootBT += Time.deltaTime;
        float DistanceAiNPlayer =  Vector2.Distance(player.position, this.transform.position);

        if (DistanceAiNPlayer < 7 )
        {
            Vector2 direction = player.position - this.transform.position;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        }
        if (DistanceAiNPlayer < 7 && DistanceAiNPlayer > 4)
        {
            this.transform.Translate(moveSpeed, 0, 0);
            
        }
        else if (DistanceAiNPlayer <4)
        {
             
        }

        // Cursor Follow
        //mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.z, Camera.main.transform.position.z - transform.position.z));
        //transform.LookAt(mousePos);
        //var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

    }

    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + moveAxis * moveSpeed * Time.deltaTime);
    }
}
