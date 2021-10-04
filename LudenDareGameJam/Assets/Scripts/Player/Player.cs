using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    // Singleton
    public static Player instance;

    // Movement
    [Header("Player Movement")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float movementAcceleration = 6f;
    [SerializeField] float rotationAcceleration = 10f;
    [Space]
    [SerializeField] float animMoveAcceleration = 50f;
    [SerializeField] float animStopAcceleration = 100f;

    // Air Movement
    [Header("Air")]
    [SerializeField] float jumpStrenght = 7f;
    [SerializeField] float gravity = 50f;
    [SerializeField] float gravityAcceleration = 25f;

    // Audio
    [SerializeField] AudioTrigger stepSound;
    [SerializeField] AudioTrigger jump;
    [SerializeField] AudioTrigger attack;

    // Private Variables
    ImpactReceiver impact;
    CharacterController controller;
    Rigidbody rigid;
    Animator anim;
    Vector3 velocity;
    Quaternion rot;

    float changeAnimSpeed;
    bool isRunning;
    bool inAir;
    bool active;
    bool playerDodging;

    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        instance = this;

        impact = GetComponent<ImpactReceiver>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        transform.rotation = rot;
    }

    void Update()
    {
        UpdateAnimation();
        if (active) return;
        HandleMovement();
    }

    private void LateUpdate()
    {
        playerDodging = anim.GetBool("isDodging");
    }

    #endregion

    #region Movement
    void HandleMovement()
    {
        if (controller.isGrounded) inAir = false;
        else inAir = true;

        Vector3 rotDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        // Desired Velocity
        Vector3 desiredSpeed = rotDir * movementSpeed;
        desiredSpeed.y = velocity.y;
        velocity = Vector3.Lerp(velocity, desiredSpeed, Time.deltaTime * movementAcceleration);

        // Gravity
        velocity.y = Mathf.Max(velocity.y - Time.deltaTime * gravityAcceleration, controller.isGrounded ? -1f : -gravity);

        // Change Player Rotation
        if (rotDir != Vector3.zero) rot = Quaternion.LookRotation(rotDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationAcceleration * Time.deltaTime);

        // Jump
        if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = jumpStrenght;
            jump.Play(transform.position);

            anim.CrossFade("Jump", .2f);
        }

        // Animation
        isRunning = rotDir != Vector3.zero ? true : false;

        changeAnimSpeed = isRunning ? Mathf.Lerp(changeAnimSpeed, Mathf.Clamp(changeAnimSpeed + Time.deltaTime, 0, 0.6f), Time.deltaTime * (movementAcceleration * animMoveAcceleration)) :
                                      Mathf.Lerp(changeAnimSpeed, Mathf.Max(changeAnimSpeed - Time.deltaTime, 0), Time.deltaTime * (movementAcceleration * animStopAcceleration));

        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleMovementActivity(bool act)
    {
        active = act;
    }


    // Default Wert -> MovementAcceleration = 6f;
    public void ChangeSpeed(float newSpeed)
    {
        movementAcceleration = newSpeed;
    }
    #endregion
    public void KnockBack(Vector3 pos, float knockBack)
    {
        if (playerDodging) return;
        //rigid.AddExplosionForce(100, transform.position, 1f);
        impact.AddImpact(transform.position - pos, knockBack);
    }

    #region UpdateStuff
    void UpdateAnimation()
    {
        anim.SetFloat("Blend", changeAnimSpeed);
        anim.SetFloat("velY", velocity.y);
        anim.SetBool("inAir", inAir);
        anim.SetBool("isGrounded", controller.isGrounded);
    }

    public void PlayStepSounds()
    {
        stepSound.Play(transform.position);
    }

    public bool GetAirCondition()
    {
        return inAir;
    }
    #endregion
}
