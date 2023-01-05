using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JenisPilihan
{
    BidangRuang,Huruf
}
public class GameManager : MonoBehaviour
{
    [SerializeField] JenisPilihan jenis = JenisPilihan.BidangRuang;
    public List<GameObject> CharacterPrefabs;
    [SerializeField]private Transform playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (jenis==JenisPilihan.BidangRuang)
        {
            Instantiate(CharacterPrefabs[0], playerPosition);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
