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
        player = gameObject.AddComponent<Rigidbody2D>();
        player.isKinematic = true;
        target = transform.position;

    }
    private void Update()
    {
#if UNITY_ANDROID

        if (isDragging==true)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
            target.y = transform.position.y;
            player.MovePosition(Vector2.Lerp(transform.position, target, moveSpeed*Time.deltaTime));
        }
#endif
    }

    public void OnMouseDown()
    {
        isDragging = true;
    }
    public void OnMouseUp()
    {
        isDragging = false;
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
