using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup
{
    public float value;
    public PowerUpTypes type;
    public Stats stats;
    public string UpgradeName;

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

    public void RandomizeSetup()
    {
        int rand = Random.Range(0, System.Enum.GetNames(typeof(PowerUpTypes)).Length);

        this.type = (PowerUpTypes)rand;
        this.value = Upgrade(this.type);
        Debug.Log(value);
        this.UpgradeName = PowerUpNames[rand];

    }

    public float Upgrade(PowerUpTypes type)
    {
        float stat = stats.GetStat(type);
        switch (type)
        {
            default:
                return (stat + 1);
        }
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


