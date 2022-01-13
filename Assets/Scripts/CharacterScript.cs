using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour
{
    public float speed = 5f;
    public float upgradeSpeed;

    public bool isMoving = false;
    public bool isSpeedUpgrade = false;
    public bool isLerping = false;

    private Vector3 target;
    private Vector3 offset;

    public Animator animator;

    public Camera cameraa;
    public Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        offset = transform.position - cameraa.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.position;
        Vector2 joystickVal = joystick.Direction;
        joystickVal *= 5f;
        Vector3 moveJoy = new Vector3(joystickVal.x, 0f, joystickVal.y);
        target += moveJoy;
        //Debug.Log(target);
        //Debug.Log(Vector3.Distance(transform.position, target));   // orignial 0.39   STILL REQUIERS HIGHER VALUE OR FIND NEW METHOD
        if (Vector3.Distance(transform.position, target) <= 1.2f)
        {
            isMoving = false;
            animator.SetFloat("Blend", 0f, 0.05f, Time.deltaTime);
        }   
        else  //not accurate enough
        {
            isMoving = true;
            animator.SetFloat("Blend", 1f, 0.005f, Time.deltaTime);
        }

        if (isSpeedUpgrade)
        {
            speed = upgradeSpeed;
        }

        //transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
        Vector3 dir = target - transform.position;
        // calculate movement at the desired speed:
        Vector3 movement= dir.normalized * speed * Time.deltaTime;
        // limit movement to never pass the target position:
        if (movement.magnitude > dir.magnitude) movement = dir;
        movement.y = -0.065f;
        // move the character:
        gameObject.GetComponent<CharacterController>().Move(movement);


        transform.LookAt(new Vector3(target.x, transform.position.y, target.z));    //not smooth
        if (!isLerping)
        {
            cameraa.transform.position = transform.position - offset;
        }
    }
}
