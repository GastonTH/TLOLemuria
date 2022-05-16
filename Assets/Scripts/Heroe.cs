﻿using Json_Serializer;
using UnityEditor;
using UnityEngine;

public class Heroe
{
    public string Name { get; set; }
    public string GameClass { get; set; }
    public int Level { get; set; }
    public int MaxVit { get; set; }
    public int CurrentVit { get; set; }
    public int MaxMana { get; set; }
    public int CurrentMana { get; set; }
    public int Str { get; set; }
    public int XpMax { get; set; }
    public int CurrentXp { get; set; }
    
    public Heroe(PlayerSerializer h)
    {
        Name = h.name;
        GameClass = h.gameClass;
        Level = h.level;
        MaxVit = h.maxVit;
        CurrentVit = h.currentVit;
        MaxMana = h.maxMana;
        CurrentMana = h.currentMana;
        Str = h.str;
        XpMax = h.xpMax;
        CurrentXp = h.currentXp;

    }
    
    // constructor para cuando le pase los parametros a mano
    public Heroe(string name, string gameClass, int maxVit, int currentVit, int maxMana, int currentMana, int str,
        int xpMax, int currentXp)
    {
        Name = name;
        GameClass = gameClass;
        MaxVit = maxVit;
        CurrentVit = currentVit;
        MaxMana = maxMana;
        CurrentMana = currentMana;
        Str = str;
        XpMax = xpMax;
        CurrentXp = currentXp;
    }
    
    public void TakeDamage(int damage)
    {
        CurrentVit -= damage;
    }
    
    public void DoDamage(int damage)
    {
        CurrentVit -= damage;
    }

    public void Run()
    {
        Debug.Log("Running");
    }
}