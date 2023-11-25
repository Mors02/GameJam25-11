using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerup
{
    public float value;
    public PowerUpTypes type;
    public Stats stats;
    public string UpgradeName;
    public Sprite sprite;

    static string[] PowerUpNames = new string[]
    {
        "Riduci cooldown",
        "Aumenta durata laser",
        "Aggiungi laser",
        "Aumenta vita",
        "Aumenta velocità"
    };

    public Powerup(float value, string name, PowerUpTypes type)
    {
        this.value = value;
        this.type = type;
        this.UpgradeName = name;
        this.stats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    public Powerup(Stats stats)
    {
        this.value = 0;
        this.type = 0;
        this.UpgradeName = "null";
        this.stats = stats;
    }

    public void RandomizeSetup(int[] without)
    {
        int rand = -1;
        bool found = false;
        int safeValue = 300;
        do
        {
            rand = Random.Range(0, System.Enum.GetNames(typeof(PowerUpTypes)).Length);
            
            foreach (int n in without) // go over every number in the list
            {
                if (n == rand) // check if it matches
                {
                    found = true;
                    break; // no need to check any further
                } else
                {
                    found = false;
                }
            }
            safeValue--;
        } while (found && safeValue > 0);

        Debug.Log(safeValue);

        this.type = (PowerUpTypes)rand;
        this.value = Upgrade(this.type);
        this.sprite = GameAssets.i.buttons[rand];
        
        this.UpgradeName = PowerUpNames[rand];

    }

    public float Upgrade(PowerUpTypes type)
    {
        float stat = stats.GetStat(type);
        switch (type)
        {
            case PowerUpTypes.health:
                stat++;
                if (stat > 20)
                    stat = 20;
                break;
            default:
                return (stat + 1);
        }
        return stat;
    }
}

public enum PowerUpTypes
{
    cooldownReduction,
    laserDuration,
    laserNumber,
    health,
    movementSpeed,
}


