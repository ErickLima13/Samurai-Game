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
    public float health;

    public bool isDead;
    public bool isRight;
    public bool isAttack;

    public Status status;

    public LayerMask playerLayer;

    public Transform target;
    public Transform groundCheck;

    public Attributes attributesBoss;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private SpawnEnemy spawnEnemy;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attack;
    

    private void Initialization()
    {
        spawnEnemy = GameObject.Find("SpawnManager").GetComponent<SpawnEnemy>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        direction = 1f;
        rageHealth = status.health / 2;
        health = attributesBoss.health;
        speed = attributesBoss.speed;
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
        if(status.health < rageHealth && !isDead)
        {
            speed = 3f;
            spriteRenderer.color = Color.red;
            animator.Play("AttackRage");
            audioSource.PlayOneShot(attack);
        }
    }

    private void Attack()
    {
        RaycastHit2D playerTarget = Physics2D.Raycast(target.position, Vector2.right * direction, distanceAttack,playerLayer);
        Debug.DrawRay(target.position, Vector2.right * distanceAttack * direction, Color.white);

        if (playerTarget.collider && !isAttack)
        {
            audioSource.PlayOneShot(attack);
            StartCoroutine(OnAttacking());
            isAttack = true;
            
        }

        
    }

    public void onHit() 
    {
        RageMode();

        if (status.health <= 0)
        {
            isDead = true;
            speed = 0f;
            animator.SetTrigger("Death");
            spawnEnemy.isBoss = false;
            StartCoroutine(Died());
        }
    }

    IEnumerator Died()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }

    private IEnumerator OnAttacking()
    {
        animator.SetBool("isAttack",true);
        
        speed = 0;
        
        yield return new WaitForSeconds(2f);
        animator.SetBool("isAttack", false);

        isAttack = false;
        speed = attributesBoss.speed;
    }

    private void OnEnable()
    {
        if (isDead)
        {
            status.health = health + spawnEnemy.bossCount;
            health = status.health;
            attributesBoss.speed = Random.Range(1f,5f);
            speed = attributesBoss.speed;
            rageHealth = health / 2;
            spriteRenderer.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            isDead = false;
            isAttack = false;
        }
    }

}
