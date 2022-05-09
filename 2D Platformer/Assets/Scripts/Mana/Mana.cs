using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Mana : MonoBehaviour
{
    [SerializeField] private float maxMana;

    public float CurrentMana { get; private set; }
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        CurrentMana = maxMana;
    }

    public bool SpendMana(float mana)
    {
        if (CurrentMana - mana >= 0)
        {
            CurrentMana -= mana;
            return true;
        }

        return false;
    }

    public void RestoreMana(float mana)
    {
        CurrentMana = (CurrentMana + mana) % maxMana;
    }

    public float GetMaxMana()
    {
        return maxMana;
    }
}
