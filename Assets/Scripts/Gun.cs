using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour{
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 30f;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;
    public int maxAmmo = 4;
    private int currentAmmo;
    public Text ammoText;
    public float reloadTime = 3f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;

    void Start(){
        currentAmmo = maxAmmo;
    }
    void OnEnable(){
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    void Update(){

        ammoText.text = "Ammo: " + currentAmmo + "/4";
        if(isReloading){
            return;
        }
        if(currentAmmo <= 0){
            StartCoroutine(Reload());
            return;
        }
        if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
    }
    IEnumerator Reload(){
        isReloading = true;
        animator.SetBool("Reloading", true);
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(reloadTime - .25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }
    void Shoot() {
        currentAmmo--;
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)){
            Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();

            if(target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null){
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 0.1f); 
        }
    }
}
