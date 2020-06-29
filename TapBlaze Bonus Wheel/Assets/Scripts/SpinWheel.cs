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

    private Sector[] sectors = new Sector[8];
    private Item[] items = new Item[5];
    private bool canSpin;
    private int finalAngle;
    private int pointedSector;
    private int[] testSectors = new int[8];
    private int totalSpin;

    public TextMeshProUGUI[] itemAmount;
    public TextMeshProUGUI totalSpinText;
    public TextMeshProUGUI[] sectorSpinText;
    public TextMeshProUGUI[] sectorRateText;
    public TextMeshProUGUI winText;
    public GameObject win;

    [HideInInspector]
    public GameObject[] wheelGame;

    //string[] prizeTypes = new string[] { "Life", "Brush", "Gem", "Hammer", "Coin" };

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
        //for (int i = 0; i < 8; i++)
            //Debug.Log("#" + (i+1) + ": " + sectors[i].DropRateMin + " - " + sectors[i].DropRateMax);
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

    private void DisplaySpinResult()
    {
        //TODO: update icon

        if (sectors[pointedSector].Type == "Life")
            winText.text = sectors[pointedSector].Type + " " + sectors[pointedSector].Amount + " min";
        else
            winText.text = sectors[pointedSector].Type + " x" + sectors[pointedSector].Amount;
        if (wheelGame != null)
        {
            
            Debug.Log("hi");
        }  
        foreach (GameObject w in wheelGame)
            w.gameObject.SetActive(false);
        win.SetActive(true);
    }

    //Button Functions------------------------------------------------------------------------------------
    public void PressSpinButton()
    {
        if(canSpin)
            StartCoroutine(Spin());
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

    private bool areDropRatesGood()
    {
        int sum = 0;
        for(int i = 0; i < 8; i++)
            sum += sectors[i].DropRate;
        return sum == 100;
    }//Check if the total of every sector's drop chance is 100%
}
