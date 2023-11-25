using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private float coolDownDuration;

    [SerializeField]
    private float laserActiveDuration;


    [SerializeField]
    private Stats stats;

    [SerializeField]
    private GameObject[] laserArray;

    private float numberOfLasers = 1;

    void Start()
    {
        //laser.SetActive(false);

        StartCoroutine(ActivateLaser());
    }

    void Update()
    {

        this.coolDownDuration = stats.GetStat(PowerUpTypes.cooldownReduction);

        this.laserActiveDuration = stats.GetStat(PowerUpTypes.laserDuration);

        this.numberOfLasers = stats.GetStat(PowerUpTypes.laserNumber);

    }

    private IEnumerator ActivateLaser()
    {
        yield return new WaitForSeconds(coolDownDuration);
        foreach (GameObject laser in laserArray)
        {
            laser.SetActive(false);
        }
        laserArray[Mathf.Min((int)numberOfLasers-1, 2)].SetActive(true);
        StartCoroutine(DeactivateLaser());
    }

    private IEnumerator DeactivateLaser()
    {
        yield return new WaitForSeconds(laserActiveDuration);
        foreach (GameObject laser in laserArray)
        {
            laser.SetActive(false);
        }
        StartCoroutine(ActivateLaser());
    }


}
