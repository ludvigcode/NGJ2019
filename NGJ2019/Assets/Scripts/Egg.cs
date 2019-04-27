﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float Speed;
    public Transform destination = null;

    private Animator animator;
    private Rigidbody rb;

    public bool standAlone;

    public Type type;

    public enum Type
    {
        Red = 0,
        Blue = 1,
        Black = 2,
        Green = 3
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void AssignDestinationTransform(Transform _destination)
    {
        destination = _destination;
    }

    private void Update()
    {
        if (destination)
        {
            Vector3 transformPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Vector3 destionationPos = new Vector3(destination.position.x, 0.0f, destination.position.z);
            if (Vector3.Distance(transformPos, destionationPos) > 0.01f)
            {
                Quaternion rotation = Quaternion.LookRotation((destionationPos - transformPos).normalized, Vector3.up);
                transform.rotation = Quaternion.Euler(0.0f, rotation.eulerAngles.y, 0.0f);
            }
        }
    }

    void FixedUpdate()
    {
        animator.SetFloat("Speed", new Vector2(rb.velocity.x, rb.velocity.z).sqrMagnitude);
        if (destination)
        {
            Vector3 force = (destination.position - transform.position).normalized * Speed;
            rb.AddForce(new Vector3(force.x, 0.0f, force.z), ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Egg collidedEgg = collision.gameObject.GetComponent<Egg>();
        EggArmy army = FindObjectOfType<EggArmy>();
        if (collidedEgg && army && collidedEgg.standAlone)
        {
            army.AddEggie(collidedEgg);
        }
    }
}
