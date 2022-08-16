using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform aimTransform;
    private SpriteRenderer railGunSprite;

    public Transform shootPoint;
    public GameObject BulletPlasma;
    public float startTimeBTWShoot;
    private float TimeBTWShoot;
    public float startChargeTime;
    private float chargeTime;

    public float laserDamage;
    public LineRenderer lineRenderer;
    public Transform EndLaser;
    public GameObject startVFX;
    public GameObject endVFX;
    private enum Mode
    {
        Mode1,
        Mode2,
    }
    private Mode mode;

    void Start()
    {
        mode = Mode.Mode1;
        chargeTime = startChargeTime;
    }

    // Update is called once per frame
    void Update()
    {
        Aim();
        Shooting();
    }
    private void Aim()
    {
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = (mousePos3D - transform.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        if (angle > 90 || angle < -90)
        {
            if (transform.eulerAngles.y == 0)
            {
                aimTransform.localRotation = Quaternion.Euler(180, 0, -angle);
            }
            else if (transform.eulerAngles.y == 180)
            {
                aimTransform.localRotation = Quaternion.Euler(180, 180, -angle);
            }
        }
    }
    private void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            chargeTime = startChargeTime;
            if (mode == Mode.Mode1)
            {
                mode = Mode.Mode2;
            }
            else if (mode == Mode.Mode2)
            {
                lineRenderer.enabled = false;
                endVFX.SetActive(false);
                mode = Mode.Mode1;
            }
        }
        switch (mode)
        {
            case Mode.Mode1:
                ShootingMode1();
                break;
            case Mode.Mode2:
                ShootingMode2();
                break;
        }
    }
    private void ShootingMode1()
    {
        if (Input.GetMouseButton(0))
        {
            startVFX.SetActive(true);
            //Mode1 minigun
            chargeTime -= Time.deltaTime;
            if (chargeTime <= 0)
            {
                if (TimeBTWShoot <= 0)
                {
                    Bullet();
                }
                else
                {
                    TimeBTWShoot -= Time.deltaTime;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            startVFX.SetActive(false);
            chargeTime = startChargeTime;
        }
    }
    private void ShootingMode2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startVFX.SetActive(true);
        }
        if (Input.GetMouseButton(0))//Ex charge shooting
        {
            chargeTime -= Time.deltaTime;
            if(chargeTime <= 0)
            {
                UpdateLaser();
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();
        }
    }
    void Bullet()
    {
        Instantiate(BulletPlasma, shootPoint.position, aimTransform.rotation);
        TimeBTWShoot = startTimeBTWShoot;
    }
    private void UpdateLaser()
    {
        lineRenderer.enabled = true;
        Vector3 mousePos3D = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDir = (mousePos3D - transform.position).normalized;
        lineRenderer.SetPosition(0, shootPoint.position);
        lineRenderer.SetPosition(1, EndLaser.position);
        RaycastHit2D hitInfo = Physics2D.Raycast(shootPoint.position, aimDir.normalized);
        if (hitInfo)
        {
            HPEnemy hpEnemy = hitInfo.transform.GetComponent<HPEnemy>();
            if (hpEnemy != null)
            {
                hpEnemy.TakeDamage(laserDamage);
            }
            lineRenderer.SetPosition(1, hitInfo.point);
            endVFX.SetActive(true);
        }
        endVFX.transform.position = lineRenderer.GetPosition(1);
    }
    private void DisableLaser()
    {
        lineRenderer.enabled = false;
        endVFX.SetActive(false);
        startVFX.SetActive(false);
        chargeTime = startChargeTime;
    }
}
