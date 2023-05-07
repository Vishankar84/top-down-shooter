using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour{

    public  GameObject  projectile;
    public  Transform   shotPoint;
    private Animator    animator;
    private AudioSource gunShot;

    public float timeBtwShot = .2f;
    public float timeBtwAutoShot = .1f;

    private float shotTime;
    private bool  autoShoot = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gunShot  = GetComponent<AudioSource>();  
    }

    void Update()
    {
        if(!GameManager.Instance.gameOver)
        {
            GetWeaponRotation();

            GetWeaponAutoShoot();

            WeaponShoot();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void GetWeaponRotation()
    {
        if (!PauseMenu.gameIsPaused)
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float desiredAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
            transform.rotation = rotation;
        }
    }

    private void GetWeaponAutoShoot()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            autoShoot = !autoShoot;
        }
    }

    private void WeaponShoot()
    {
        if (Input.GetButton("Fire1") && autoShoot)
        {
            HandleAutoShooting();
        }

        if (Input.GetButtonDown("Fire1") && !autoShoot)
        {
            HandleShooting();
        }
    }

    private void HandleAutoShooting()
    {
        Shoot("shooting", timeBtwAutoShot);
    }

    private void HandleShooting()
    {
        Shoot("shoot", timeBtwShot);
    }

    private void Shoot( string shootAnimation, float shootMode)
    {
        if (Time.time > shotTime)
        {
            Instantiate(projectile, shotPoint.position, transform.rotation);
            gunShot.Play();
            CameraShake.Instance.ShakeCamera(2f, 0.1f);
            animator.SetTrigger(shootAnimation);
            shotTime = Time.time + shootMode;
        }
    }
}
