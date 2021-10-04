using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactReceiver : MonoBehaviour
{
    public static ImpactReceiver instance;

    [SerializeField] float mass = 3.0f;

    Vector3 impact = Vector3.zero;
    CharacterController character;

    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (impact.magnitude > 0.2f) 
            character.Move(impact * Time.deltaTime);

        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }
    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        dir.y = Mathf.Abs(dir.y);
        impact += dir * force / mass;
    }
}