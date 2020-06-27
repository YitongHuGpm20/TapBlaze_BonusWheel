﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpinWheel : MonoBehaviour
{
    public class Sector
    {
        public string Type;
        public int Amount;
        public int Chance;

        public Sector(string type, int amount, int chance)
        {
            Type = type;
            Amount = amount;
            Chance = chance;
        }
    }

    Sector[] sectors = new Sector[8];
    int randomValue;
    float timeInterval;
    bool coroutineAllowed;
    int finalAngle;

    [SerializeField]
    TextMeshPro winText;

    //string[] prizeTypes = new string[] { "Life", "Brush", "Gem", "Hammer", "Coin" };

    void Start()
    {
        //Defalt case
        sectors[0] = new Sector("Life", 30, 20);
        sectors[1] = new Sector("Brush", 3, 10);
        sectors[2] = new Sector("Gem", 35, 10);
        sectors[3] = new Sector("Hammer", 3, 10);
        sectors[4] = new Sector("Coin", 750, 5);
        sectors[5] = new Sector("Brush", 1, 20);
        sectors[6] = new Sector("Gem", 75, 5);
        sectors[7] = new Sector("Hammer", 1, 20);
    }

    void Update()
    {
        
    }

    bool areDropChancesGood()
    {
        int sum = 0;
        for(int i = 0; i < 8; i++)
            sum += sectors[i].Chance;
        return sum == 100;
    }//Check if the total of every sector's drop chance is 100%
}
