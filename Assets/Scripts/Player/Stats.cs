using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stats : MonoBehaviour
{
    Dictionary<PowerUpTypes, float> stats;


    public UnityEvent OnModifyStats;

    void Start()
    {
        if (OnModifyStats == null)
            OnModifyStats = new UnityEvent();

        stats = new Dictionary<PowerUpTypes, float>()
        {
            { PowerUpTypes.cooldownReduction, 10f },
            { PowerUpTypes.laserDuration, 3f },
            { PowerUpTypes.laserNumber,  1f },
            { PowerUpTypes.health,  20f },
            { PowerUpTypes.movementSpeed, 10f }
        };
    }

    public void UpdateStat(Powerup powerup)
    {
        this.stats[powerup.type] = powerup.value;
        OnModifyStats.Invoke();
        Debug.Log("UPGRADED: " + powerup.UpgradeName + " - " + this.stats[powerup.type]);
    }

    public void ReceiveDamage()
    {
        this.stats[PowerUpTypes.health]--;
        OnModifyStats.Invoke();
    }

    public float GetStat(PowerUpTypes type)
    {
        return this.stats[type];
    }
}
