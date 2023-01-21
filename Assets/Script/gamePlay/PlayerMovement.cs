using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameManager gm;
    [SerializeField]Rigidbody2D player;
    Vector3 playerVelocity;
    [SerializeField] float moveSpeed=10f;
    public bool benar;
    [SerializeField] Vector2 target;
    Transform _terget;
    private bool isDragging;
    // Start is called before the first frame update
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        _terget = gameObject.transform;
        benar = false;
        if (player==null)
        {
            player = gameObject.AddComponent<Rigidbody2D>();
        }
    }

    public void OnMouseDown()
    {
        if (gm.playing==true)
        {
            isDragging = true;
            gameObject.transform.localScale=new Vector2(1f,1f);
            gm.sfx._sfx(1);
        }
    }
    public void OnMouseDrag()
    {
        if (isDragging == true)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.y = transform.position.y;
            transform.position = target;
        }
    }
    public void OnMouseUp()
    {
        isDragging = false;
        gameObject.transform.localScale = new Vector2(0.85f, 0.85f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag=="ground")
        {
            gm.sfx._sfx(2);
            print("tanah boy");
        }
        if (gm.RandomJawaban == 0)//jawaban kanan
        {
            if (collision.transform.tag == "Kanan")
            {
                benar = true;
                transform.position = collision.transform.position;
                gameObject.transform.localScale = new Vector2(0.85f, 0.85f);

                isDragging = false;
                gm.gameover();
            }
            else if(collision.transform.tag=="Kiri")
            {
                print("salah");
                gm.sfx._sfx(3);
                transform.position = collision.transform.position;
                gameObject.transform.localScale = new Vector2(0.85f, 0.85f);

                isDragging = false;
                gm.gameover();
            }
        }
        else //jawaban kiri
        {
            if (collision.transform.tag == "Kiri")
            {
                benar = true;
                print("Benar");
                transform.position = collision.transform.position;
                gameObject.transform.localScale = new Vector2(0.85f, 0.85f);

                isDragging = false;
                gm.gameover();
            }
            else if (collision.transform.tag == "Kanan")
            {
                print("salah");
                gm.sfx._sfx(3);
                transform.position = collision.transform.position;
                gameObject.transform.localScale = new Vector2(0.85f, 0.85f);

                isDragging = false;
                gm.gameover();
            }
            
        }
    }
}
