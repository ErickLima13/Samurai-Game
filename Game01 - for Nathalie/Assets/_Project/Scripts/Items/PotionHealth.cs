using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionHealth : MonoBehaviour
{
    public int value = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            collision.gameObject.GetComponent<Status>().health += value;
            collision.gameObject.GetComponent<Player>().healthBar.fillAmount += value;
            Destroy(gameObject, 0.1f);
        }
    }
}
