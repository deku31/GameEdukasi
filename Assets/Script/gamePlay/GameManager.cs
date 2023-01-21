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
    [SerializeField] JenisPilihan jenis = JenisPilihan.Huruf;

    public SoundManager sfx;

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

    bool pause=false;
    public bool playing = false;

    string _scene;

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
        StartCoroutine(WaitingTIme(0.5f));
        sfx = FindObjectOfType<SoundManager>();

        PanelPause.SetActive(false);
        Time.timeScale = 1;
        EndingPanel.SetActive(false);
        randomA = Random.RandomRange(0, CharacterPrefabsA.Count);
        Instantiate(CharacterPrefabsA[randomA], playerPosition);

        if (playerscript == null)
        {
            playerscript = FindObjectOfType<PlayerMovement>();
            print(RandomJawaban);
        }

        if (jenis==JenisPilihan.Huruf)
        {
            
            if (RandomJawaban == 0)//jawaban kanan
            {
                Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[0]);
                terisi[0] = true;
            }
            else if (RandomJawaban > 0)
            {
                Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
                terisi[1] = true;
            }
            //================mengisi posisi yang kosong=======================================
            int randomR = Random.RandomRange(0,JawabanPrefab.Count);
            if (terisi[0] == false)
            {
                if (randomA == 0)
                {
                    Instantiate(JawabanPrefab[randomA+randomR], PosisijawabanBagA[0]);
                }
                else if(randomA>0)
                {
                    if (randomR!=randomA)
                    {
                        Instantiate(JawabanPrefab[randomR], PosisijawabanBagA[0]);
                    }
                    else
                    {
                        randomR = Random.RandomRange(randomA,JawabanPrefab.Count);
                        Instantiate(JawabanPrefab[randomR], PosisijawabanBagA[0]);
                    }
                }
            }
            else
            {
                if (randomA == 0)
                {
                    Instantiate(JawabanPrefab[randomA + 1], PosisijawabanBagA[1]);
                }
                else if (randomA > 0)
                {
                    if (randomR!=randomA)
                    {
                        Instantiate(JawabanPrefab[randomR], PosisijawabanBagA[1]);
                    }
                    else
                    {
                        randomR = Random.RandomRange(randomA,JawabanPrefab.Count);
                        Instantiate(JawabanPrefab[randomR], PosisijawabanBagA[1]);
                    }
                }
            }
        }
        sfx.audio.volume = 1f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause==false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
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
        _scene = NamaScene;
        sfx._sfx(0);
        StartCoroutine(ChangeScene(0.8f));
    }
    public void Transisi(string namaScene)
    {
        _scene = namaScene;
        StartCoroutine(ChangeScene(1f));
    }
    public void Pause()
    {
        sfx._sfx(0);
        pause = true;
        PanelPause.SetActive(true);
    }
    public void Resume()
    {
        sfx._sfx(0);
        pause = false;
        PanelPause.SetActive(false);
    }

    public void gameover()
    {
        playing = false;
        StartCoroutine(ShowGemeover(0.5f));
    }

    IEnumerator WaitingTIme(float time)
    {
        yield return new WaitForSeconds(time);
        playing = true;
        StopCoroutine(WaitingTIme(0));
    }
    IEnumerator ShowGemeover(float time)
    {
        yield return new WaitForSeconds(time);
        EndingPanel.SetActive(true);
        StopAllCoroutines();
    }
    IEnumerator ChangeScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(_scene);
        StopAllCoroutines();
    }

}
