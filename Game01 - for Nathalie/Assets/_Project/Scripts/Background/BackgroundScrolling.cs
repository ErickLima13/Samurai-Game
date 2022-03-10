using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Choices
{
    Player,
    Menu
}

public class BackgroundScrolling : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public GameObject player;

    public float speed;

    public Choices choice;

    void Update()
    {
        InfiniteBackground();
    }

    private void InfiniteBackground()
    {
        switch (choice)
        {
            case Choices.Player:
                meshRenderer.material.mainTextureOffset += new Vector2(player.GetComponent<Player>().direction * speed * Time.deltaTime, 0);
                break;
            case Choices.Menu:
                meshRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
                break;
        }
    }
}
