﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{
    public Egg.Type EggType;

    private EggArmy army;
    private int inReach = 0;

    // Start is called before the first frame update
    void Start()
    {
        army = FindObjectOfType<EggArmy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (inReach > 0 && army)
            {
                army.ActivateSpecialEgg(EggType, transform);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Egg egg = other.GetComponent<Egg>();
        if (egg)
        {
            if (egg.type == EggType) ++inReach;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Egg egg = other.GetComponent<Egg>();
        if (egg)
        {
            if (egg.type == EggType) --inReach;
        }
    }
}
