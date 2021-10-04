using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] float attackDistance = 1f;
    [SerializeField] float timerToAttack = 1f;
    [SerializeField] float knockBackAmount = 50f;
    [SerializeField] float gettingKnockbackAmount = 10f;

    Animator anim;
    Player player;
    Rigidbody rigid;

    float timer;

    bool canDmg;
    bool inAttackRange;
    bool attackAcomplished;
    bool gettinHit;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        canDmg = true;
        timer = 0;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (gettinHit) return;

        transform.LookAt(Player.instance.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            anim.SetBool("move", false);
            if (timer >= timerToAttack)
            {
                inAttackRange = true;
                anim.SetTrigger("attack1");
                timer = 0;
            }

        }
        else
        {
            anim.SetBool("move", true);
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
            inAttackRange = false; 
        }
    }


    public void Attack()
    {
        if (!attackAcomplished)
        {
            Debug.Log("Player Attacked");
            var moveDir = player.transform.position - transform.position;
            Player.instance.KnockBack(this.transform.position, knockBackAmount);
            attackAcomplished = true;
        }
    }

    public void DoDamage()
    {
        if (canDmg)
        {
            Debug.Log("Enemy got damaged");
            var moveDir = transform.position - player.transform.position;
            rigid.AddForce(moveDir * gettingKnockbackAmount * 10f);
            anim.SetTrigger("hit");
            canDmg = false;
            gettinHit = true;
            StartCoroutine(Resetdmg());
        }
    }

    IEnumerator Resetdmg()
    {
        yield return new WaitForSeconds(.7f);
        gettinHit = false;
        canDmg = true;
    }

    public void AttackReset()
    {
        attackAcomplished = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
