using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerPos;
    public float smoothing;
    public Vector2 minPos;
    public Vector2 maxPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != playerPos.position)
        {
            Vector3 PlayerPos = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);
            PlayerPos.x = Mathf.Clamp(PlayerPos.x, minPos.x, maxPos.x);
            PlayerPos.y = Mathf.Clamp(PlayerPos.y, minPos.y, maxPos.y);
            transform.position = Vector3.Lerp(transform.position, PlayerPos, smoothing);
        }
    }
}
