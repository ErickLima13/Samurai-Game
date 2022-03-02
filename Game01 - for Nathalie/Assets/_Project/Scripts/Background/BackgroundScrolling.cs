using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public GameObject player;

    public float speed;

    void Update()
    {
        InfiniteBackground();
    }

    private void InfiniteBackground()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(player.GetComponent<Player>().direction * speed * Time.deltaTime, 0);
    }
}
