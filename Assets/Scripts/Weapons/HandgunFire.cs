using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandgunFire : MonoBehaviour
{
    public GameObject theGun;
    public GameObject muzzleFlash;
    public AudioSource gunFire;
    public bool isFiring = false;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isFiring)
            {
                StartCoroutine(FiringHandgun());
            }
        }
    }

    IEnumerator FiringHandgun()
    {
        isFiring = true;
        theGun.GetComponent<Animator>().Play("HandgunFire");
        muzzleFlash.SetActive(true);
        gunFire.Play();
        yield return new WaitForSeconds(.05f);
        muzzleFlash.SetActive(false);
        yield return new WaitForSeconds(.25f);
        isFiring = false;
        theGun.GetComponent<Animator>().Play("New State");
    }
}
