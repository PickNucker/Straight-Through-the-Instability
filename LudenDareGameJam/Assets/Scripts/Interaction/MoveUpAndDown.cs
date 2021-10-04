using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float offset = .1f;

    Vector3 startPos;
    int counter = 0;

    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, counter > 0 ? new Vector3(transform.localPosition.x, offset, transform.localPosition.z) : startPos, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            counter = Mathf.Max(counter + 1, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            counter = Mathf.Max(counter - 1, 0);
        }
    }
}
