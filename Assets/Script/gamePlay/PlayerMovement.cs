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
    public bool salah=false;
    [SerializeField] Vector2 target;
    Transform _terget;
    private bool isDragging;
    int id = 0;

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
            player = gameObject.GetComponent<Rigidbody2D>();
        }
    }
    private void Update()
    {
        if (gameObject.transform.parent==null)
        {
            Destroy(gameObject);
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
        }
        if (isDragging==true)
        {
            if (gm.RandomJawaban == 0)//jawaban kanan
            {
                if (collision.transform.tag == "Kanan")
                {
                    benar = true;
                    id = 0;
                }
                else if (collision.transform.tag == "Kiri")
                {
                    salah = true;
                    id = 1;
                }
            }
            else //jawaban kiri
            {
                if (collision.transform.tag == "Kiri")
                {
                    benar = true;
                    id = 1;
                }
                else if (collision.transform.tag == "Kanan")
                {
                    salah = true;
                    id = 0;
                }
            }
        }
        else if(isDragging==false)
        {
            if (benar == true)
            {
                gm.sfx.bgm.volume = 0.2f;
                gm.sfx._alfabet(gm.alffabetId);

                Vector3 defaultscale = new Vector3(0.85f, 0.85f, 1);
                gameObject.transform.localScale = new Vector3(0.85f, 0.85f,1);
                if (gameObject.transform.localScale == defaultscale)
                {
                    transform.position = gm.PosisijawabanBagA[id].position;
                }
                gm.JSoalBenar += 1;
                gm.JSSoal += 1;
                gm.correctAnsware();
            }
            else if (salah == true)
            {
                gm.sfx.bgm.volume = 0.2f;
                gm.sfx._sfx(3);
                Vector3 defaultscale = new Vector3(0.85f, 0.85f,1);
                gameObject.transform.localScale = new Vector3(0.85f, 0.85f, 1);
                if (gameObject.transform.localScale==defaultscale)
                {
                    transform.position = gm.PosisijawabanBagA[id].position;
                }
                gm.JSoalSalah += 1;
                gm.JSSoal += 1;
                gm.incorrectAnsware();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDragging==true)
        {
            if (collision.transform.tag == "Kanan")
            {
                if (benar == true)
                {
                    benar = false;

                }
            }
            else if (collision.transform.tag == "Kiri")
            {
                if (salah == true)
                {
                    salah = false;

                }
            }
        }
       
    }
}
