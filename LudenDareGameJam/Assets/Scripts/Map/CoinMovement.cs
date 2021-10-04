using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float rotateSpeed;

    public Vector3 pos1 = new Vector3(0, 61, 5);
    public Vector3 pos2 = new Vector3(0, 62, 5);
    public float speed = 1.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));

        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
