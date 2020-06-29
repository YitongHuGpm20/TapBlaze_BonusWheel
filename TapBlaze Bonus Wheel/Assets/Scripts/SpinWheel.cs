﻿using System.Collections;
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
        public int DropRate;
        public int DropRateMin;
        public int DropRateMax;
        public int SpinTimes;
        public float ActualDropRate;

        public Sector(string type, int amount, int dropRate, int min, int max, int spinTimes, float aDropRate)
        {
            Type = type;
            Amount = amount;
            DropRate = dropRate;
            DropRateMin = min;
            DropRateMax = max;
            SpinTimes = spinTimes;
            ActualDropRate = aDropRate;
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

    [Header("Main Game")]
    public TextMeshProUGUI[] itemAmount;
    public TextMeshProUGUI winText;
    public GameObject win;
    private Sector[] sectors = new Sector[8];
    private Item[] items = new Item[5];
    private bool canSpin;
    private int finalAngle;
    private int pointedSector;

    [Header("Report")]
    public TextMeshProUGUI totalSpinText;
    public TextMeshProUGUI[] sectorSpinText;
    public TextMeshProUGUI[] sectorRateText;
    public TextMeshProUGUI[] amountReportText;
    private int totalSpin;

    [Header("Custom")]
    public TextMeshProUGUI errorText;
    public TMP_InputField[] dropRateInput;
    public TextMeshProUGUI[] dropRateText;
    public TMP_InputField[] amountInput;
    public TextMeshProUGUI[] amountText;
    public TMP_Dropdown[] prizeType;

    [Header("Unit Testing")]
    private int[] testSectors = new int[8];

    [HideInInspector]
    public GameObject[] wheelGame;

    private void Awake()
    {
        //Default wheel sections
        sectors[0] = new Sector("Life", 30, 20, 0, 0, 0, 0);
        sectors[1] = new Sector("Brush", 3, 10, 0, 0, 0, 0);
        sectors[2] = new Sector("Gem", 35, 10, 0, 0, 0, 0);
        sectors[3] = new Sector("Hammer", 3, 10, 0, 0, 0, 0);
        sectors[4] = new Sector("Coin", 750, 5, 0, 0, 0, 0);
        sectors[5] = new Sector("Brush", 1, 20, 0, 0, 0, 0);
        sectors[6] = new Sector("Gem", 75, 5, 0, 0, 0, 0);
        sectors[7] = new Sector("Hammer", 1, 20, 0, 0, 0, 0);

        //Default item status
        items[0] = new Item("Life", 0);
        items[1] = new Item("Brush", 0);
        items[2] = new Item("Gem", 0);
        items[3] = new Item("Hammer", 0);
        items[4] = new Item("Coin", 0);

        canSpin = true;
        pointedSector = 0;
        totalSpin = 0;
    }

    private void Start()
    {
        //Update items status UI
        for (int i = 0; i < 5; i++)
            itemAmount[i].text = items[i].Amount.ToString();
        UpdateEstimatedDropRates();
        //TestSectorsDropRates();

        wheelGame = GameObject.FindGameObjectsWithTag("WheelGame");
    }

    private IEnumerator Spin()
    {
        canSpin = false;
        float timeInterval = 0.05f;
        totalSpin++;
        int spinResult = Random.Range(0, 100);
        int rotateTimes = 0;
        for (int i = 0; i < 8; i++)
        {
            if (spinResult >= sectors[i].DropRateMin && spinResult <= sectors[i].DropRateMax)
            {
                rotateTimes = (i + 24 - pointedSector) * 2;
                //Debug.Log((i+1).ToString() + " " + spinResult + " " + rotateTimes / 2);
            } 
        }

        //Spin the wheel and control spinning speed
        for (int i = 0; i < rotateTimes; i++) 
        {
            transform.Rotate(0, 0, 22.5f);
            if (i > Mathf.RoundToInt(rotateTimes * .5f))
                timeInterval = .1f;
            if (i > Mathf.RoundToInt(rotateTimes * .85f))
                timeInterval = .2f;
            yield return new WaitForSeconds(timeInterval);
        }

        //When wheel stops
        if (Mathf.RoundToInt(transform.eulerAngles.z) % 45 != 0)
            transform.Rotate(0, 0, 22.5f);

        //Find the index of current pointed sector
        finalAngle = Mathf.RoundToInt(transform.eulerAngles.z);
        pointedSector = finalAngle / 45;

        //Update spin result to UI
        for (int i = 0; i < 5; i++)
        {
            if(items[i].Type == sectors[pointedSector].Type)
            {
                items[i].Amount += sectors[pointedSector].Amount;
                itemAmount[i].text = items[i].Amount.ToString();
            }
        }
        totalSpinText.text = "Total Spin: " + totalSpin + " times";
        sectors[pointedSector].SpinTimes++;
        sectorSpinText[pointedSector].text = sectors[pointedSector].SpinTimes.ToString();
        UpdateActualDropRates();
        DisplaySpinResult();

        canSpin = true;
    }

    private void UpdateEstimatedDropRates()
    {
        sectors[0].DropRateMin = 0;
        sectors[0].DropRateMax = sectors[0].DropRate - 1;
        for (int i = 1; i < 8; i++)
        {
            sectors[i].DropRateMin = sectors[i - 1].DropRateMax + 1;
            sectors[i].DropRateMax = sectors[i].DropRateMin + sectors[i].DropRate - 1;
        }
        for (int i = 0; i < 8; i++)
            dropRateText[i].text = sectors[i].DropRate.ToString() + "%";
        //for (int i = 0; i < 8; i++)
        //    Debug.Log("#" + (i+1) + ": " + sectors[i].DropRateMin + " - " + sectors[i].DropRateMax);
    }

    private void UpdateActualDropRates()
    {
        for(int i = 0; i < 8; i++)
        {
            sectors[i].ActualDropRate = (float)sectors[i].SpinTimes / totalSpin * 100;
            if((int)sectors[i].ActualDropRate > sectors[i].DropRate)
            {
                sectorRateText[i].color = Color.green;
                sectorRateText[i].text = sectors[i].ActualDropRate + "% ↑";
            }
            else if ((int)sectors[i].ActualDropRate < sectors[i].DropRate)
            {
                sectorRateText[i].color = Color.red;
                sectorRateText[i].text = sectors[i].ActualDropRate + "% ↓";
            }
            else if ((int)sectors[i].ActualDropRate == sectors[i].DropRate)
            {
                sectorRateText[i].color = Color.black;
                sectorRateText[i].text = sectors[i].ActualDropRate + "%";
            }
        }
    }//Calculate the actual drop rate of each sector and push result to Report Window

    private void UpdatePrize()
    {
        for (int i = 0; i < 8; i++)
        {
            if (prizeType[i].value == 0)
                amountText[i].text = sectors[i].Amount + " min";
            else
                amountText[i].text = "x" + sectors[i].Amount;
            amountReportText[i].text = sectors[i].Type + " " + amountText[i].text;
        }
    }

    private void DisplaySpinResult()
    {
        //TODO: update icon

        if (sectors[pointedSector].Type == "Life")
            winText.text = sectors[pointedSector].Type + " " + sectors[pointedSector].Amount + " min";
        else
            winText.text = sectors[pointedSector].Type + " x" + sectors[pointedSector].Amount;
        foreach (GameObject w in wheelGame)
            w.gameObject.SetActive(false);
        win.SetActive(true);
    }

    //Button Functions------------------------------------------------------------------------------------
    public void SpinButton()
    {
        if(canSpin)
            StartCoroutine(Spin());
    }

    public void ApplyButton()
    {
        int sum = 0, tempRate = 0, tempAmount = 0;
        int[] rate = new int[8];
        bool madeChange_DropRate = false, madeChange_Amount = false, madeChange_PrizeType = false;

        for (int i = 0; i < 8; i++)
        {
            //Get drop rate input
            int.TryParse(dropRateInput[i].text, out tempRate); 
            if (dropRateInput[i].text != "" && tempRate != sectors[i].DropRate)
            {
                rate[i] = tempRate;
                madeChange_DropRate = true;
            }  
            else
                rate[i] = sectors[i].DropRate;
            sum += rate[i];
            
            //Get amount input
            int.TryParse(amountInput[i].text, out tempAmount);
            if (amountInput[i].text != "" && tempAmount != sectors[i].Amount)
            {
                sectors[i].Amount = tempAmount;
                madeChange_Amount = true;
            }

            //Get prize type change
            if (prizeType[i].options[prizeType[i].value].text != sectors[i].Type)
            {
                sectors[i].Type = prizeType[i].options[prizeType[i].value].text;
                madeChange_PrizeType = true;
            }
        }

        if (madeChange_DropRate) //Check drop rate input
        {
            if (sum == 100)
            {
                for (int i = 0; i < 8; i++)
                    sectors[i].DropRate = rate[i];
                errorText.text = "Change applied";
                UpdateEstimatedDropRates();
            }
            else
                errorText.text = "The total popularity isn't 100%";
        }
        if (madeChange_PrizeType || madeChange_Amount)
            UpdatePrize();
        if (!madeChange_DropRate && !madeChange_Amount && !madeChange_PrizeType)
            errorText.text = "you didn't make any change";

        for (int i = 0; i < 8; i++)
        {
            dropRateInput[i].text = "";
            amountInput[i].text = "";
        }
    }

    //Unit Testing----------------------------------------------------------------------------------------
    private void TestSectorsDropRates()
    {
        for (int i = 0; i < 1000; i++)
        {
            int r = Random.Range(0, 100);
            for (int j = 0; j < 8; j++)
            {
                if (r >= sectors[j].DropRateMin && r <= sectors[j].DropRateMax)
                {
                    testSectors[j]++;
                }
            }
        }
        for (int k = 0; k < 8; k++)
            Debug.Log((k+1).ToString() + ": " + testSectors[k]/10 + "%");
    }
}
