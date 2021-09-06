using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Animator atk;
    public CharacterController cPlayer;
    public GameObject hitbox;
    private Inputs pcontroller;
    private Vector2 desiredDirection;
    private AudioClip swing;
    private float jumpVelocity = 10f;
    [SerializeField]
    private int speed = 1;

    private void OnEnable()
    {
        //hitbox = transform.GetChild(0);
        //Debug.Log(hitbox.name);
        swing = transform.GetComponent<AudioClip>();
        cPlayer = GetComponent<CharacterController>();
        pcontroller = new Inputs();
        pcontroller.Enable();
    }
    private void OnDisable()
    {
        pcontroller.Disable();
    }

    void Start()
    {
        cPlayer = GetComponent<CharacterController>();
    }

    void Update()
    {
        movement();
        if (pcontroller.player.attack.triggered)
        {
            //Debug.Log("attack");
            attack();
        }
        if (pcontroller.player.jump.triggered/* && isGrounded()*/)
        {
            jump();
        }
    }

    void movement()
    {
        desiredDirection.x = pcontroller.player.movement.ReadValue<Vector2>().x;
        cPlayer.Move(desiredDirection * Time.deltaTime * speed);

    }

    void attack()
    {
        /*attack animation
        if (atk.GetBool("attack") == false) //atk's attack goes back to false at end of animation
        {
            atk.SetBool("attack", true);
        }else{}
        */

        hitbox.gameObject.SetActive(true);
        //hitbox.gameObject.SetActive(false);
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(cPlayer.bounds.center, cPlayer.bounds.size, 0f, Vector2.down, .1f);

        Debug.Log(raycastHit2D);

        return raycastHit2D.collider != null;
    }

    void jump()
    {
        //playerbody.velocity = Vector2.up * jumpVelocity;
        //cPlayer.velocity = Vector2.up * jumpVelocity;
        transform.Translate(0, jumpVelocity * Time.deltaTime, 0);
        //cPlayer.transform.Translate(0, jumpVelocity * Time.deltaTime, 0);
        Debug.Log("jump");
    }

}
