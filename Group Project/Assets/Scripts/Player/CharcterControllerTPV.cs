using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CharacterController))]

public class CharcterControllerTPV : MonoBehaviour
{
    public CharacterController controller;

    [Header("Movement")]
    public float Speed;
    public float Jump;
    Vector3 move;

   [Header("Gravity")]
    public float Gravity;
    public float FallVelocity;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask Ground;
    [SerializeField] bool isGrounded;
    //[SerializeField] Vector3 velocity;

    [Header("Camera Input")]
    public Transform Camera;
    public float CamLerp= 0.5f;
    float Torque;

    //colour 
  

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true ;
    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        float HoriInput = Input.GetAxisRaw("Horizontal");
        float VertiInput = Input.GetAxisRaw("Vertical");
        //Gravity -= 9.81f * Time.deltaTime;
        Vector3 Direction = new Vector3(HoriInput, 0f, VertiInput).normalized;
        //Gravity
        GravityForce();
        if (Physics.CheckSphere(GroundCheck.position, 0.1f, Ground))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (Direction.magnitude>= 0.1f)
        {
            float Angle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float angle_ = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref Torque, CamLerp);
            transform.rotation = Quaternion.Euler(0f, angle_, 0f);
            move = Quaternion.Euler(0f, Angle, 0f) * Vector3.forward;
            controller.Move(move.normalized * Speed * Time.deltaTime);
        }

       
        // Jumping 
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.CheckSphere(GroundCheck.position, 0.1f, Ground))
            {
               
            }

        }*/
    }
    void GravityForce()
    {
        if (!isGrounded)
        {
            // move += Physics.gravity;
           Vector3 Grav = new Vector3(0f, Gravity, 0f);
           controller.Move(Grav* FallVelocity* Time.deltaTime);
        }
        else
        {
            return;
        }

    }
   
}
