using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour
{
    public int value = 5;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip potion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            collision.gameObject.GetComponent<Status>().health += value;
            collision.gameObject.GetComponent<Player>().healthBar.fillAmount += value/ collision.gameObject.GetComponent<Status>().health;
            audioSource.PlayOneShot(potion);
            Destroy(gameObject, 0.3f);
        }
    }
}
