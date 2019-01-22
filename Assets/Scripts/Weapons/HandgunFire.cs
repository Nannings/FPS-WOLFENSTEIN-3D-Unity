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

    public float damage = 10;
    public float range = 100f;

    public Camera fpsCam;

    public GameObject impactFX;
    public float impactForce = 100;
    public float fireRate = 15f;
    public float nextTimeToFire = 0f;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (!isFiring)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
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

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            Instantiate(impactFX, hit.point, Quaternion.LookRotation(hit.normal));
        }


        yield return new WaitForSeconds(.05f);
        muzzleFlash.SetActive(false);
        yield return new WaitForSeconds(.25f);
        isFiring = false;
        theGun.GetComponent<Animator>().Play("New State");
    }
}
