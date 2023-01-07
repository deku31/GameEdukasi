using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameManager gm;
    [SerializeField]Rigidbody2D player;
    Vector3 playerVelocity;
    [SerializeField]float gravitasi=10f;
    [SerializeField] float moveSpeed=10f;
    public bool benar;

    // Start is called before the first frame update
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        benar = false;
        player = gameObject.AddComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(Horizontal, 0, vertical);

        if (move.magnitude!=0)
        {
            player.MovePosition(player.transform.position + move * Time.deltaTime * moveSpeed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gm.RandomJawaban==0)//jawaban kanan
        {
            if (collision.transform.tag == "Kanan")
            {
                benar = true;
                print("Benar");
            }
            else
            {
                print("salah");
            }
            Time.timeScale = 0;
            gm.Header.enabled = true;

        }
        else //jawaban kiri
        {
            if (collision.transform.tag == "Kiri")
            {
                benar = true;
                print("Benar");
            }
            else
            {
                print("salah");
            }
            gm.Header.enabled = true;

            Time.timeScale = 0;
        }

    }
}
