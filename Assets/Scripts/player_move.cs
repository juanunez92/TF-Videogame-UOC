using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move : MonoBehaviour
{

    private Rigidbody2D rig;
    private SpriteRenderer spriteKnight;
    float forcejump = 8f;
    int numJump = 1;
    int useJump;
    Animator anim;

    public static player_move instance;

    [SerializeField] public float speedKnight;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        spriteKnight = GetComponent<SpriteRenderer>();
        useJump = 0;
        anim = GetComponent<Animator>();
        instance = this;
    }

    public void Jump()
    {
        if (Input.GetKey("k"))
        {
            if (useJump < numJump)
            {
                rig.AddForce(new Vector3(0f, forcejump), ForceMode2D.Impulse);
                useJump++;
            }
    
        }
        
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.collider.tag == "ground")
        {
            useJump = 0;
        }
        
    }
    private void FixedUpdate()
    {
        Jump();
        Attack();

        float speedInput = Input.GetAxisRaw("Horizontal");
        rig.velocity = new Vector2(speedInput * speedKnight, rig.velocity.y);
        if (speedInput < 0)
        {
            spriteKnight.flipX = true;
        }
        if (speedInput > 0)
        {
            spriteKnight.flipX = false;
        }
        if (rig.velocity.x !=0)
        {
            anim.SetBool("walk", true);
        }
        else {
            anim.SetBool("walk",false);
        }

    }
    public void Attack()
    {
        if (Input.GetKey("l"))
        {

            anim.SetBool("atack", true);

        }
        else
        {

            anim.SetBool("atack", false);
        }


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {
            transform.position = new Vector3(-14,-10 , 0);
        }
    }
}

