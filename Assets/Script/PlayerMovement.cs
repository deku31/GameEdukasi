using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]CharacterController player;
    Vector3 playerVelocity;
    [SerializeField]float gravitasi=10f;
    [SerializeField] float moveSpeed=10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        
        if (Horizontal!=0)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            player.Move(move * Time.deltaTime * moveSpeed);
            print("halo");
        }
       
    }
}
