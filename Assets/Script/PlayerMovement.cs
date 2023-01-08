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
    [SerializeField] Vector3 target;
    Transform _terget;
    // Start is called before the first frame update
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        _terget = gameObject.transform;
        benar = false;
        player = gameObject.AddComponent<Rigidbody2D>();
        player.isKinematic = true;
        target = transform.position;
    }
    private void Update()
    {
#if UNITY_ANDROID
        if (Input.GetMouseButton(0)&&Time.timeScale>0)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            target.y = transform.position.y;
        }
        _terget.position = target;
#endif
    }
    void FixedUpdate()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(Horizontal, 0, vertical);
#if UNITY_STADALONE_WIN
        
        if (move.magnitude != 0)
        {
            player.MovePosition(player.transform.position + move * Time.deltaTime * moveSpeed);
        }
#endif
#if UNITY_ANDROID
        player.MovePosition(Vector2.Lerp(transform.position, target, moveSpeed));
#endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gm.RandomJawaban == 0)//jawaban kanan
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
            gm.Header.enabled = true;
            gm.EndingPanel.SetActive(true);
            Time.timeScale = 0;
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
            gm.EndingPanel.SetActive(true);

            Time.timeScale = 0;
        }

    }
}
