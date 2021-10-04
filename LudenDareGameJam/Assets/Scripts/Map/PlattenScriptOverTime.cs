using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattenScriptOverTime : MonoBehaviour
{
    public bool overTime;
    public Vector3 newPos;

    Vector3 pos;
    bool move;

    void Start()
    {
        pos = transform.localPosition;
        newPos.x = transform.localPosition.x;
        newPos.z = transform.localPosition.z;
    }

    private void Update()
    {
        if(move)
            transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, 3f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player" && overTime)
        {
            Invoke("DropPlattform", 0.5f);
            Destroy(gameObject, 2f);
        }
           
    }

    void DropPlattform()
    {
        move = true;
    }
}
