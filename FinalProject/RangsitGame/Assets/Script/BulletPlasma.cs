using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlasma : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public LayerMask whatisLayer;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", lifeTime) ;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatisLayer);
        if (hitInfo.collider != null)
        {
            if(hitInfo.collider.tag == "Enemy")
            {
                //TakeDamage
                hitInfo.collider.GetComponent<HPEnemy>().TakeDamage(damage);
            }
            DestroyBullet();
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
