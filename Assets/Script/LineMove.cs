using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMove : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager = null;
    public float speed = 5f;
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (-transform.position.x > gameManager.MaxPosition.x)
            Destroy(gameObject);
    }
    public void Down()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }
}
