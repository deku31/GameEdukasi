using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum JenisPilihan
{
    BidangRuang,Huruf
}
public class GameManager : MonoBehaviour
{
    [SerializeField] JenisPilihan jenis = JenisPilihan.BidangRuang;

    [Header("UI")]
    public Text Header;
    public GameObject EndingPanel;
    public GameObject PanelPause;

    [Header("Bagian A")]
    
    public List<GameObject> CharacterPrefabsA;
    public List<GameObject> JawabanPrefab;
   

    [Header("Posisi")]
    [SerializeField]private Transform playerPosition;
    /* untuk posisi jawaban
     * 0=posisi kanan
     * 1=posisi kiri
     */
    public List<Transform> PosisijawabanBagA;
    public List<Transform> Endposisi;

    [SerializeField] List<bool> terisi;

    [SerializeField] PlayerMovement playerscript;
    private int randomA;
    public int RandomJawaban;//jawaban akan dirandom kanan atau kiri

    private void Awake()
    {
        RandomJawaban = Random.RandomRange(0, 2);
        for (int i = 0; i < terisi.Count; i++)
        {
            terisi[i] = false;
        }
    }
    void Start()
    {
        PanelPause.SetActive(false);
        Time.timeScale = 1;
        Header.enabled = false;
        EndingPanel.SetActive(false);
        if (jenis==JenisPilihan.BidangRuang)
        {
            randomA = Random.RandomRange(0, CharacterPrefabsA.Count);
            Instantiate(CharacterPrefabsA[randomA], playerPosition);
        }
        if (playerscript == null)
        {
            playerscript = FindObjectOfType<PlayerMovement>();
            print(RandomJawaban);
        }
        if (RandomJawaban==0)//jawaban kanan
        {
            Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[0]);
            terisi[0] = true;
        }
        else if(RandomJawaban > 0)
        {
            Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
            terisi[1] = true;
        }
        //================mengisi posisi yang kosong=======================================
        int randomR=Random.RandomRange(0,2);
        if (terisi[0] == false)
        {
            if (randomA==0)
            {
                Instantiate(JawabanPrefab[1+randomR], PosisijawabanBagA[0]);
            }
            else
            {
                Instantiate(JawabanPrefab[0], PosisijawabanBagA[0]);
            }
        }
        else
        {
            if (randomA == 0)
            {
                Instantiate(JawabanPrefab[randomA + 1], PosisijawabanBagA[1]);
            }
            else if(randomA>0)
            {
                Instantiate(JawabanPrefab[0], PosisijawabanBagA[1]);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (playerscript.benar==true)
        {
            Header.text = "Benar";
            Header.color = new Color32(250, 0, 255, 255);
        }
        else
        {
            Header.text = "salah";
            Header.color = new Color32(255, 0, 0, 255);
        }
    }
    public void Navigator(string NamaScene)//masukan nama scene yang di tuju
    {
        SceneManager.LoadScene(NamaScene);
    }
    public void Pause()
    {
        PanelPause.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        PanelPause.SetActive(false);
        Time.timeScale = 1;
    }
}
