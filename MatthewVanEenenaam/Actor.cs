using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : ScriptableObject{

    //name of actor
    private new string namePrivate;
    private int healthPrivate;
    private int maxHealthPrivate;

    public new string name
    {
        get
        {
            return namePrivate;
        }
    }
    public int health
    {
        get
        {
            return healthPrivate;
        }
        set
        {
            healthPrivate = 0;
        }
    }

    public int maxHealth
    {
        get
        {
            return maxHealthPrivate;
        }
        set
        {
            maxHealthPrivate = 0;
        }
    }

    //money its carrying
    private int goldPrivate;
    private Vector2 attackRangePrivate;

    public int gold
    {
        get
        {
            return goldPrivate;
        }
        set
        {
            goldPrivate = 0;
        }
    }

    public Vector2 attackRange
    {
        get
        {
            return attackRangePrivate;
        }
        set
        {
            attackRangePrivate = Vector2.one;
        }
    }


    public bool alive {
        get
        {
            return health > 0;
        }
    }
    
    
    //never drop hp below 0
    public void DecreaseHealth(int value) {
        health = Mathf.Max(health - value, 0);
    }


    public void ResetHealth() {
        health = maxHealth;
    }


    public void IncreaseGold(int value) {
        gold += value;
    }


    public void DecreaseGold(int value) {
        gold -= value;
    }


    //cloning  actor 
    public T Clone<T>() where T : Actor {

        var clone = ScriptableObject.CreateInstance<T>();
        clone.name = name;
        clone.health = health;
        clone.maxHealth = maxHealth;
        clone.gold = gold;
        clone.attackRange = attackRange;

        return clone;

    }

}
