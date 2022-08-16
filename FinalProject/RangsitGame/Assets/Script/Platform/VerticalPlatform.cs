using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effector2D;
    public float waitingTime;

    private void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            effector2D.rotationalOffset = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                waitingTime = 0.5f;
            }
            if(waitingTime <= 0)
            {
                effector2D.rotationalOffset = 180f;
                waitingTime = 0.5f;
            }
            else
            {
                waitingTime -= Time.deltaTime;
            }
        }
    }
}
