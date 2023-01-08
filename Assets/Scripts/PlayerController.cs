using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStatus
    {
        Normal,
        Attack1,
        Attack2,
    }

    public float moveSpeed = 5.0f;
    public float attackSpeed1 = 5.0f;
    public GameObject scythePrefab;
    public float backForce = 1f;

    private PlayerStatus status = PlayerStatus.Normal;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isMoving = false;
    private int secWeapon = 0;
    private bool isUnbeatable = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Movement control

        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float dy = Input.GetAxis("Vertical") * moveSpeed;
        float dx = Input.GetAxis("Horizontal") * moveSpeed;

        if (dx < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (dx > 0)
        {
            spriteRenderer.flipX = false;

        }

        isMoving = dx != 0 || dy != 0;
        // Make it move 10 meters per second instead of 10 meters per frame...
        GetComponent<Rigidbody2D>().transform.Translate(dx * Time.deltaTime, dy * Time.deltaTime, 0);

        animator.SetFloat("dx", dx);
        animator.SetFloat("dy", dy);
        animator.SetBool("isHorizontal", (dx != 0));
        animator.SetBool("isMoving", isMoving);

        // Attack control (Mouse Click)

        if (status == PlayerStatus.Normal && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Attack1());
        }
        if (secWeapon != 0 && status == PlayerStatus.Normal && Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Attack2());

        }

    }
    public Vector3 getPosition()
    {
        return transform.position;
    }

    private IEnumerator Attack1()
    {
        status = PlayerStatus.Attack1;
        animator.SetBool("isAttack1", true);
        float offset;
        GameObject scythe = Instantiate(scythePrefab, transform.position, Quaternion.identity);
        if (spriteRenderer.flipX == true)
        {
            scythe.transform.localScale = new Vector3(-1f, 1f, 1f);
        } else
        {
            scythe.transform.localScale = new Vector3(1f, 1f, 1f);

        }
        Debug.Log(scythe.transform.localScale);
        //Debug.Log(scythe.transform.position);

        while(true)
        {
            //Debug.Log(scythe.transform.rotation.z);
            scythe.transform.position = transform.position;
            if (spriteRenderer.flipX == true)
            {
                scythe.transform.localScale = new Vector3(-1f, 1f, 1f);
                scythe.transform.GetChild(0).Rotate(0.0f, 0.0f, -attackSpeed1 * 0.01f);
                yield return new WaitForSeconds(0.01f);
                if (scythe.transform.GetChild(0).rotation.z * 360 > 250)
                {
                    break;
                }
            }
            else
            {
                scythe.transform.localScale = new Vector3(1f, 1f, 1f);
                scythe.transform.GetChild(0).Rotate(0.0f, 0.0f, -attackSpeed1 * 0.01f);
                yield return new WaitForSeconds(0.01f);
                if (scythe.transform.GetChild(0).rotation.z * 360 < -250)
                {
                    break;
                }
            }

            
        }

        Destroy(scythe);
        status = PlayerStatus.Normal;
        animator.SetBool("isAttack1", false);

    }
    private IEnumerator Attack2()
    {
        status = PlayerStatus.Attack2;
        animator.SetBool("isAttack2", true);


        yield return new WaitForSeconds(1.0f);
        status = PlayerStatus.Normal;
        animator.SetBool("isAttack2", false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("hurted");
        if (!isUnbeatable && collision.collider.tag == "Enemy")
        {
            Vector3 backDir = collision.collider.transform.position - transform.position;
            backDir = -backDir.normalized;
            StartCoroutine(hurted(backDir));
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

    }

    private IEnumerator hurted(Vector3 backDir)
    {
        //GetComponent<Rigidbody2D>().AddForce(backDir * backForce);

        isUnbeatable = true;
        spriteRenderer.color = Color.red;
        //Debug.Log(spriteRenderer.color);
        yield return new WaitForSeconds(0.3f);
        //Debug.Log(spriteRenderer.color);
        spriteRenderer.color = Color.white;


        yield return new WaitForSeconds(0.05f);
        isUnbeatable = false;
    }

}


