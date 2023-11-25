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

    void Start()
    {
        laser.SetActive(false);

        StartCoroutine(ActivateLaser());
    }

    void Update()
    {
        
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
