using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] float dodgeReichweite = 5f;
    [SerializeField] float rayCastHeightOffset = .5f;

    [SerializeField] Transform hitPoint;
    [SerializeField] float hitRadius = 1f;
    [SerializeField] LayerMask whatIsHitable = default;
    [Space]
    [SerializeField] Transform groundPos;
    [SerializeField] float groundRadius = .3f;
    [SerializeField] LayerMask groundMask = default;

    // Audio
    [SerializeField] AudioTrigger dodge;
    [SerializeField] AudioTrigger attack;

    // private Variables
    CharacterController controller;
    Animator anim;

    int random;

    bool rootMotion;
    bool isDodging;
    bool isPerformingAttack;
    bool isJumping;
    bool isGrounded;
    bool canDamageEnemy;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundPos.position, groundRadius, groundMask);

        HandleAttack();
        //HandleDodge();
        HandleFallingAndLanding();
        CheckIfRoot();
        CheckForEnemies();

    }

    private void LateUpdate()
    {
        rootMotion = anim.GetBool("RootMotion");
        isDodging = anim.GetBool("isDodging");
    }

    void HandleDodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !rootMotion && !isDodging && controller.isGrounded)
        {
            dodge.Play(transform.position);
            anim.SetTrigger("dodge");
        }
    }

    public void CheckIfRoot()
    {
        if (rootMotion && isGrounded)
        {
            anim.applyRootMotion = true;
            Player.instance.HandleMovementActivity(true);
        }
        else
        {
            anim.applyRootMotion = false;
            //Player.instance.HandleMovementActivity(false);
        }
    }

    void HandleFallingAndLanding()
    {
        if(!isGrounded)
        {
            anim.SetBool("falling", true);
        }
        else
        {
            anim.SetBool("falling", false);
        }
    }

    void HandleAttack()
    {
        if(Input.GetMouseButtonDown(0) && !isPerformingAttack && !isDodging && controller.isGrounded)
        {
            isPerformingAttack = true;
            random = Random.Range(1, 6);
            Player.instance.HandleMovementActivity(true);
            Attack();
            attack.Play(transform.position);

            StartCoroutine(Attacking());
        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(1.5f);

        Player.instance.HandleMovementActivity(false);
        isPerformingAttack = false;
    }

    void Attack()
    {
        if (random == 1)
        {
            anim.SetTrigger("Attack1");
        }
        if (random == 2)
        {
            anim.SetTrigger("Attack2");
        }
        if (random == 3)
        {
            anim.SetTrigger("Attack3");
        }
        if (random == 4)
        {
            anim.SetTrigger("Attack4");
        }
        if (random == 5)
        {
            anim.SetTrigger("Attack5");
        }
    }

    public void CheckForEnemies()
    {
        Collider[] collider = Physics.OverlapSphere(hitPoint.position, hitRadius, whatIsHitable);

        if (canDamageEnemy)
        {
            foreach (Collider item in collider)
            {
                Enemy damageToEnemy = item.GetComponent<Enemy>();

                if (damageToEnemy != null)
                {
                    damageToEnemy.DoDamage();

                }
            }
        }
    }

    public void DoDamage()
    {
        canDamageEnemy = true;
    }

    public void NoDamage()
    {
        canDamageEnemy = false;
    }

    public void HasAttacked()
    {
        isPerformingAttack = false;
        Player.instance.HandleMovementActivity(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitPoint.position, hitRadius);
    }
}
