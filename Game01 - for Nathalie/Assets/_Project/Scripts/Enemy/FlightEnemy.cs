using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightEnemy : MonoBehaviour
{
    public float damage;
    public float speed = 2f;
    public float stopDistance;
    public float range;

    public bool isRight;

    public Status status;

    public Rigidbody2D rig;

    public Animator anim;

    private GameObject potionPrefab;

    private bool isDead;

    private Transform player;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bite;

    private void Initialization()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        potionPrefab = Resources.Load<GameObject>("PotionHealth");
    }

    void Start()
    {
        Initialization();
    }

    void Update()
    {
        Flip();
        Attack();
    }

    private void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (isDead == false)
        {
            if (isRight)
            {
                rig.velocity = new Vector2(speed, rig.velocity.y);
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                rig.velocity = new Vector2(-speed, rig.velocity.y);
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        else
        {
            speed = 0f;
        }
    }

    private void Flip()
    {
        float playerPos = transform.position.x - player.position.x;

        if (playerPos > 0)
        {
            isRight = false;
        }
        else
        {
            isRight = true;
        }
    }

    private void Attack()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= stopDistance)// executado quando o inimigo enxerga o player
        {
            speed = 0f;
            anim.SetInteger("transition", 1);
        }
        else if(distance > stopDistance)
        {
            anim.SetInteger("transition",0);
            speed = 2f;
        }
    } 
    
    public void onHit()
    {
        if (status.health <= 0)
        {
            damage = 0;
            isDead = true;

            range = Random.Range(0, 20);

            if(range % 5 == 0)
            {
                Instantiate(potionPrefab, transform.position, transform.rotation);
                print(range);
            }

            speed = 0f;
            anim.SetTrigger("death");
            Destroy(gameObject, 0.5f);
            rig.gravityScale = 2f;
        }
    }

    public void Damage()
    {
        player.GetComponent<Player>().OnHit(damage);
        audioSource.PlayOneShot(bite);
    }
}

