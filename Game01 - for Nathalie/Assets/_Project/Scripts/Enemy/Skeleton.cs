using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skeleton : MonoBehaviour
{
    public float speed;
    public float distance;
    public float distanceAttack;
    public float direction;
    public float rageHealth;

    public bool isDead;
    public bool isRight;
    public bool isAttack;

    public Status status;

    public LayerMask playerLayer;

    public Transform target;
    public Transform groundCheck;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private SpawnEnemy spawnEnemy;

    private void Initialization()
    {
        spawnEnemy = GameObject.Find("SpawnManager").GetComponent<SpawnEnemy>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = 1f;
        rageHealth = status.health / 2;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        Attack();
        RageMode();
    }

    private void Patrol()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D ground = Physics2D.Raycast(groundCheck.position, Vector2.down, distance);
        Debug.DrawRay(groundCheck.position, Vector2.down * distance, Color.red);

        if (!ground.collider)
        {
            if (isRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
                direction = 1f;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
                direction = -1f;
            }
        }

    }

    private void RageMode()
    {
        if(status.health == rageHealth)
        {
            speed = 3f;
            spriteRenderer.color = Color.red;
            animator.Play("AttackRage");
        }
    }

    private void Attack()
    {
        RaycastHit2D playerTarget = Physics2D.Raycast(target.position, Vector2.right * direction, distanceAttack,playerLayer);
        Debug.DrawRay(target.position, Vector2.right * distanceAttack * direction, Color.white);

        if (playerTarget.collider && !isAttack)
        {
            StartCoroutine(OnAttacking());
            isAttack = true;
        }
    }

    public void onHit() 
    {
        if (status.health <= 0)
        {
            isDead = true;
            speed = 0f;
            animator.SetTrigger("Death");
            spawnEnemy.isBoss = false;
            Destroy(gameObject, 0.5f);  
        }
    }

    private IEnumerator OnAttacking()
    {
        animator.SetBool("isAttack",true);
        speed = 0;
        
        yield return new WaitForSeconds(2f);
        animator.SetBool("isAttack", false);

        isAttack = false;
        speed = 2;
    }
    
}
