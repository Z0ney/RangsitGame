using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public GameObject shootPos1;
    public GameObject shootPos2;
    public GameObject shootPos3;
    public GameObject laser;
    public GameObject laser2;
    public GameObject laser3;
    public float distance;
    public LayerMask whatIsLayer;
    public int Skill;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       Skill = Random.Range(0,2);
        print(Skill);
    }
    public void Skill1()
    {
        laser.SetActive(true);
        RaycastHit2D hitInfo = Physics2D.Raycast(shootPos1.transform.position, Vector2.down, distance, whatIsLayer);
        if (hitInfo.collider.tag != "Platform")
        {
            float distanceDir = shootPos1.transform.position.y - hitInfo.point.y;
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, 2 * distanceDir, laser.transform.localScale.z);
        }
        else
        {
            laser.transform.localScale = new Vector3(laser.transform.localScale.x, 2 * 7.63806f, laser.transform.localScale.z);
        }

    }
    public void Skill2()
    {
        laser2.SetActive(true);
        laser3.SetActive(true);
        RaycastHit2D hitInfo2 = Physics2D.Raycast(shootPos2.transform.position, Vector2.down, distance, whatIsLayer);
        RaycastHit2D hitInfo3 = Physics2D.Raycast(shootPos3.transform.position, Vector2.down, distance, whatIsLayer);
        
        float distanceDir2 = shootPos2.transform.position.y - hitInfo2.point.y;
        float distanceDir3 = shootPos3.transform.position.y - hitInfo3.point.y;
        laser2.transform.localScale = new Vector3(laser.transform.localScale.x, 2 * distanceDir2, laser.transform.localScale.z);
        laser3.transform.localScale = new Vector3(laser.transform.localScale.x, 2 * distanceDir3, laser.transform.localScale.z);
    }
    public void FinishSkill()
    {
        laser.SetActive(false);
        laser2.SetActive(false);
        laser3.SetActive(false);
    }
}
