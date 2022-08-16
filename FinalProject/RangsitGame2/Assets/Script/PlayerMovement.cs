using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region
    public float speedMoveX;
    Vector2 moveDir;
    Rigidbody2D rb2D;
    public float jumpForce;
    public Transform feetPos;
    private bool isReadyToJump = false;
    public float checkRadius;
    public LayerMask Ground;
    private float jumpTimeCounter;
    private bool isJump;
    #endregion
    #region limitMove
    private Vector3 limitMaxVector;
    private Vector3 limitMinVector;
    private Vector3 viewPos;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        limitMinVector = GameObject.Find("LimitMin").transform.position;
        limitMaxVector = GameObject.Find("LimitMax").transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        isReadyToJump = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground);
        if (isReadyToJump == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            jumpTimeCounter = .35f;
            isJump = true;
            rb2D.velocity = Vector2.up * jumpForce;
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && isJump == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb2D.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJump = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
        {
            isJump = false;
        }
    }
    private void FixedUpdate()
    {
        if(transform.position.x >= limitMaxVector.x)
        {
            transform.position = new Vector3(limitMaxVector.x,transform.position.y,transform.position.z);
        }else if(transform.position.x <= limitMinVector.x)
        {
            transform.position = new Vector3(limitMinVector.x, transform.position.y, transform.position.z);

        }
        moveDir.x = Input.GetAxisRaw("Horizontal");
        rb2D.velocity = new Vector2(moveDir.x * speedMoveX * Time.deltaTime, rb2D.velocity.y);

    }
}
