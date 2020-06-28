using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public class Item
    {
        public string Type;
        public int Amount;
        
        public Item(string type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }

    Sector[] sectors = new Sector[8];
    Item[] items = new Item[5];

    int randomValue;
    float timeInterval;
    bool coroutineAllowed;
    int finalAngle;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI[] itemAmount;

    //string[] prizeTypes = new string[] { "Life", "Brush", "Gem", "Hammer", "Coin" };

    void Awake()
    {
        //Default wheel sections
        sectors[0] = new Sector("Life", 30, 20);
        sectors[1] = new Sector("Brush", 3, 10);
        sectors[2] = new Sector("Gem", 35, 10);
        sectors[3] = new Sector("Hammer", 3, 10);
        sectors[4] = new Sector("Coin", 750, 5);
        sectors[5] = new Sector("Brush", 1, 20);
        sectors[6] = new Sector("Gem", 75, 5);
        sectors[7] = new Sector("Hammer", 1, 20);

        //Default item status
        items[0] = new Item("Life", 0);
        items[1] = new Item("Brush", 0);
        items[2] = new Item("Gem", 0);
        items[3] = new Item("Hammer", 0);
        items[4] = new Item("Coin", 0);

        for (int i = 0; i < 5; i++)
            itemAmount[i].text = items[i].Amount.ToString();

        coroutineAllowed = true;
    }

    void Update()
    {

    }

    IEnumerator Spin()
    {
        coroutineAllowed = false;
        randomValue = Random.Range(20, 30);
        timeInterval = 0.1f;

        //Slow down the wheel
        for (int i = 0; i < randomValue; i++) 
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(randomValue * .5f))
                timeInterval = .2f;
            if (i > Mathf.RoundToInt(randomValue * .85f))
                timeInterval = .4f;
            yield return new WaitForSeconds(timeInterval);
        }

        //When wheel stops
        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
            transform.Rotate(0, 0, 22.5f);

        //Update result
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
        int sectorNum = finalAngle / 45 + 1;
        winText.text = "You win Prize #" + sectorNum.ToString();
        for(int i = 0; i < 5; i++)
        {
            if(items[i].Type == sectors[sectorNum - 1].Type)
            {
                items[i].Amount += sectors[sectorNum - 1].Amount;
                itemAmount[i].text = items[i].Amount.ToString();
            }
        }

        coroutineAllowed = true;
    }

    public void PressSpinButton()
    {
        if(coroutineAllowed)
            StartCoroutine(Spin());
    }

    bool areDropChancesGood()
    {
        int sum = 0;
        for(int i = 0; i < 8; i++)
            sum += sectors[i].Chance;
        return sum == 100;
    }//Check if the total of every sector's drop chance is 100%
}
