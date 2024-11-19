using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Color[] colors;
    private SpriteRenderer spriteRenderer;

    private AudioSource audioSource;

    private int index;
    public int Index { get { return index; } }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor(int index)
    {
        audioSource.Play();
        spriteRenderer.color = colors[index];
        this.index = index;
    }

    public void Restart()
    {
        spriteRenderer.color = colors[0];
        index = 0;
    }

}
