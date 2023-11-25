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

    void Start()
    {
        laser.SetActive(false);

        stats = this.gameObject.GetComponent<Stats>();

        this.coolDownDuration = stats.GetStat(PowerUpTypes.cooldownReduction);

        this.laserActiveDuration = stats.GetStat(PowerUpTypes.laserDuration);

        StartCoroutine(ActivateLaser());
    }

    public void UpdateLaser()
    {
        this.coolDownDuration = stats.GetStat(PowerUpTypes.cooldownReduction);

        this.laserActiveDuration = stats.GetStat(PowerUpTypes.laserDuration);
    }

    private IEnumerator ActivateLaser()
    {
        yield return new WaitForSeconds(coolDownDuration);
        laser.SetActive(true);
        StartCoroutine(DeactivateLaser());
    }

    private IEnumerator DeactivateLaser()
    {
        yield return new WaitForSeconds(laserActiveDuration);
        laser.SetActive(false);
        StartCoroutine(ActivateLaser());
    }


}
