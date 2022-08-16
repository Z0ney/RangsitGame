using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpPlayer : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject spriteRendererGun;
    private PlayerAttack PlayerAttack;
    private PlayerMovement PlayerMovement;
    private bool isDie;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space  ) && isDie)
        {
            SceneManager.LoadScene("Level1");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DamageToDie")
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerAttack.enabled = false;
        PlayerMovement.enabled = false;
        spriteRenderer.enabled = false;
        spriteRendererGun.SetActive(false);
        isDie = true;
    }
}
