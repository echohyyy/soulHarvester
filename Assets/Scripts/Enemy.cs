using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3;
    public float backForce = 1f;


    private bool isUnbeatable = false;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = GameObject.Find("Player").GetComponent<PlayerController>().getPosition() - transform.position;
        direction = direction.normalized;
        transform.Translate(direction * speed * Time.deltaTime);

    }
    private void OnColliderEnter2D(Collision2D collision)
    {
        Debug.Log("enemy hurted");
        if (!isUnbeatable && collision.collider.tag == "Weapon")
        {
            //Vector3 tmp = collision.collider.transform.position;
            Vector3 tmp = GameObject.Find("Player").transform.position;
            Vector3 backDir =  - transform.position;
            backDir = -backDir.normalized;
            StartCoroutine(hurted(backDir));
        }

    }

    private IEnumerator hurted(Vector3 backDir)
    {

        isUnbeatable = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(backDir.x * backForce, backDir.y * backForce);
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        spriteRenderer.color = Color.red;
        Debug.Log(spriteRenderer.color);
        yield return new WaitForSeconds(0.3f);
        Debug.Log(spriteRenderer.color);
        spriteRenderer.color = Color.white;


        yield return new WaitForSeconds(0.05f);
        isUnbeatable = false;
    }
}
