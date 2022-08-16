using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform aimTransform;
    public SpriteRenderer railGunSprite;

    public Transform shootPoint;
    public GameObject BulletPlasma;
    public float startTimeBTWShoot;
    private float TimeBTWShoot;
    private float TestTime;

    public float laserDamage;

    private enum Mode
    {
        Mode1,
        Mode2,
    }
    private Mode mode;
    
    void Start()
    {
        mode = Mode.Mode1;
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
            if(mode == Mode.Mode1)
            {
                mode = Mode.Mode2;
            } else if (mode == Mode.Mode2)
            {
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
            //Mode1 minigun
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
    private void ShootingMode2()
    {
        if (Input.GetMouseButton(0))//Ex charge shooting
        {
            TestTime -= Time.deltaTime;
            Debug.Log(TestTime);
            RaycastHit2D hitInfo = Physics2D.Raycast(shootPoint.position, shootPoint.right);
            if (hitInfo)
            {
                HPEnemy  hpEnemy = hitInfo.transform.GetComponent<HPEnemy>();
                if(hpEnemy != null)
                {
                    hpEnemy.TakeDamage(laserDamage);
                }

            }
        }
    }
    void Bullet()
    {
        Instantiate(BulletPlasma, shootPoint.position, aimTransform.rotation);
        TimeBTWShoot = startTimeBTWShoot;
    }
}
