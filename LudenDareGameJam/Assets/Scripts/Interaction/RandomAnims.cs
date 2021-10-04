using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnims : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float timerToDoActions = 4f;

    float timer = Mathf.Infinity;
    int random;

    IEnumerator Start()
    {
        this.enabled = false;
        yield return new WaitForSeconds(1.5f);
        this.enabled = true;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timerToDoActions)
        {
            random = Random.Range(1, 5);

            if(random <= 1)
            {
                anim.CrossFade("Formal Bow", .2f);
            }
            if (random == 2)
            {
                anim.CrossFade("Dead", .2f);
            }
            if (random == 3)
            {
                anim.CrossFade("Pointing Gesture", .2f);
            }
            if (random >= 4)
            {
                anim.CrossFade("Arm Gesture", .2f);
            }

            timer = 0;
        }
    }
}
