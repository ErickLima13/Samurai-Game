using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("Atributes")]
    public float speed;
    public float jumpForce;
    public float atkRadius;
    public float direction;
    public float recoveryTime;
    public float damage;

    public bool isDead;
    public bool noDamage;
    public bool isGrounded;

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public Transform firePoint;
    public LayerMask enemyLayer;
    public Image healthBar;
    public GameController gc;
    public SpriteRenderer SpriteRenderer;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip sfx;
    public AudioClip takeDamage;

    private bool isJumping;
    private bool isAttacking;

    private Status status;

    private void Initialization()
    {
        status = GetComponent<Status>();
    }
    private void Start()
    {
        Initialization();
    }

    void Update()
    {
        if (isDead == false)
        {
            Jump();
            onAttack();
        }
    }

    private void LateUpdate()
    {
        anim.SetFloat("yVelocity", rig.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping == false)
            {
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                isGrounded = false;
            }
        }
    }

    void onAttack()//metódo do ataque
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isAttacking = true;
            anim.SetInteger("transition", 3);
            audioSource.PlayOneShot(sfx);
            StartCoroutine(OnAttacking());
        }
    }

    public void Damage()
    {
        Collider2D hit = Physics2D.OverlapCircle(firePoint.position, atkRadius, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Status>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firePoint.position, atkRadius);
    }

    IEnumerator OnAttacking()
    {
        yield return new WaitForSeconds(0.6f);
        isAttacking = false;
    }

    public void OnHit(float value)//faz o player sofrer dano
    {
        if (!noDamage && !isDead)
        {
            StartCoroutine(HitDelay());
            anim.SetTrigger("hit");
            status.health -= value;
            healthBar.fillAmount = status.health / 100;
            audioSource.PlayOneShot(takeDamage);
            GameOver();
        }
    }

    private IEnumerator HitDelay()
    {
        noDamage = true;
        yield return new WaitForSeconds(recoveryTime);

        for (int i = 0; i < 2; i++)
        {
            SpriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            SpriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }

        noDamage = false;
    }

    void GameOver()
    {
        if (status.health <= 0)
        {
            anim.SetTrigger("die");
            isDead = true;
            gc.ShowGameOver();
        }
    }

    //É chamado pela física do jogo
    void FixedUpdate()
    {
        if (!isDead)
        {
            onMove();
        }
    }

    void onMove()
    {
        direction = Input.GetAxisRaw("Horizontal");//variável que armazena o input  horizontal

        rig.velocity = new Vector2(direction * speed, rig.velocity.y);//move o player na direção do input

        if (direction > 0 && isJumping == false && isAttacking == false)
        {
            transform.eulerAngles = new Vector2(0, 0);//Passa o valor de 0,0 no rotation do player
            anim.SetInteger("transition", 1);
        }

        if (direction < 0 && isJumping == false && isAttacking == false)
        {
            transform.eulerAngles = new Vector2(0, 180);//Passa o valor de 0,180 no rotation do player
            anim.SetInteger("transition", 1);
        }

        if (direction == 0 && isJumping == false && isAttacking == false)
        {
            anim.SetInteger("transition", 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //se a condição for atendida player está tocando o chão
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            isGrounded = true;
        }
    }
}

