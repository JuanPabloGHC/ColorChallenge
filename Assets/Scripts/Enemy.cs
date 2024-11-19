using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] Color[] colors;
    [SerializeField] Vector2 initialPosition;
    public int initialIndex;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    public float speed;

    [SerializeField] GController gameController;
    private int level;
    private int index;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = FindAnyObjectByType<GController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = gameController.Speed;
        rigidBody.MovePosition(new Vector2(transform.position.x, transform.position.y - speed));
        if (this.transform.position.y <= -6)
        {
            ChangeColor();
            gameController.CheckCount();
            this.transform.position = new Vector2(transform.position.x, 6f);
        }
    }

    private void ChangeColor()
    {
        index = Random.Range(1, level + 3);
        spriteRenderer.color = colors[index];
    }

    public void Restart()
    {
        if (gameController != null)
        {
            level = gameController.Level;
            speed = gameController.Speed;
        }
        transform.position = initialPosition;
        spriteRenderer.color = colors[initialIndex];
        index = initialIndex;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>().Index != index) 
        {
            gameController.GameOver();
        }
    }
}
