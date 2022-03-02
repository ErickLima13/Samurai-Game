using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("Atributes")]
    public float health;
    public float speed;
    public float jumpForce;
    public float atkRadius;
    public float direction;

    public bool isDead;

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public Transform firePoint;
    public LayerMask enemyLayer;
    public Image healthBar;
    public GameController gc;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip sfx;

    private bool isJumping;
    private bool isAttacking;

    void Update()
    {
        if (isDead == false)
        {
            Jump();
            onAttack();
        }

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping == false)
            {
                anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
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
            hit.GetComponent<FlightEnemy>().onHit();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firePoint.position, atkRadius);
    }

    IEnumerator OnAttacking()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public void OnHit(float damage)//faz o player sofrer dano
    {
        if (!isDead)
        {
            anim.SetTrigger("hit");
            health -= damage;
            healthBar.fillAmount = health / 100;
            GameOver();
        }
    }

    void GameOver()
    {
        if (health <= 0)
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
        }
    }
}

