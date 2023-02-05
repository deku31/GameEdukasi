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
    public GameObject PanelJawabBenar;
    public GameObject PanelJawabSalah;
    public GameObject PResult;
    public Text TotalSoalTxt;
    public Text BenarTxt;
    public Text SalahTxt;
    public Text HighscoreTxt;


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
    public int alffabetId;//id alfabet

    bool pause=false;
    public bool playing = false;

    int idcondition;//menentukan kondisi benar atau salah

    string _scene;


    public int skor;
    public int JSSoal;//jumlah seluruh soal
    public int JSoalBenar=0;//skor benar
    public int JSoalSalah=0;//skor salah


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
        InstanceObj();
    }
    public void InstanceObj()
    {
        RandomJawaban = Random.RandomRange(0, 2);

        randomA = Random.RandomRange(0, CharacterPrefabsA.Count);
        Instantiate(CharacterPrefabsA[randomA], playerPosition);

        if (playerscript == null)
        {
            playerscript = FindObjectOfType<PlayerMovement>();
        }

        if (jenis == JenisPilihan.Huruf)
        {

            if (RandomJawaban == 0)//jawaban kanan
            {
                Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[0]);
                alffabetId = randomA;
                terisi[0] = true;
            }
            else if (RandomJawaban > 0)
            {
                Instantiate(JawabanPrefab[randomA], PosisijawabanBagA[1]);
                alffabetId = randomA;
                terisi[1] = true;
            }
            //================mengisi posisi yang kosong=======================================
            int randomR = Random.RandomRange(0, JawabanPrefab.Count);
            if (terisi[0] == false)
            {
                if (randomA == 0)
                {
                    Instantiate(JawabanPrefab[randomA + randomR], PosisijawabanBagA[0]);
                }
                else if (randomA > 0)
                {
                    if (randomR != randomA)
                    {
                        Instantiate(JawabanPrefab[randomR], PosisijawabanBagA[0]);
                    }
                    else
                    {
                        randomR = Random.RandomRange(randomA, JawabanPrefab.Count);
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
                    if (randomR != randomA)
                    {
                        Instantiate(JawabanPrefab[randomR], PosisijawabanBagA[1]);
                    }
                    else
                    {
                        randomR = Random.RandomRange(randomA, JawabanPrefab.Count);
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
            if (pause == false)
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

        skor = (JSSoal - JSoalBenar) / 100;
        print(skor);
        if (playerscript.benar == true)
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
        sfx.bgm.volume = 0.6f;
        pause = true;
        PanelPause.SetActive(true);
    }
    public void correctAnsware()
    {
        idcondition = 0;
        StartCoroutine(showConditionPanel(0.8f));
        playing = false;

    }
    public void incorrectAnsware()
    {
        idcondition = 1;
        StartCoroutine(showConditionPanel(0.8f));
        playing = false;
    }
    public void Resume()
    {
        sfx._sfx(0);
        sfx.bgm.volume = 1f;
        pause = false;
        PanelPause.SetActive(false);
    }
    public void nextQuestion()
    {
        sfx.bgm.volume = 1f;
        PanelJawabBenar.SetActive(false);
        PanelJawabSalah.SetActive(false);
        playing = true;
        if (playing==true)
        {
            playerPosition.transform.DetachChildren();
            foreach (var item in PosisijawabanBagA)
            {
                item.transform.DetachChildren();
            }
            for (int i = 0; i < terisi.Count; i++)
            {
                terisi[i] = false;
            }
            InstanceObj();
        }
    }

    public void _Presult()
    {
        sfx._sfx(0);
        sfx.bgm.Stop();
        PResult.SetActive(true);
        TotalSoalTxt.text = JSSoal.ToString() ;
        BenarTxt.text = JSoalBenar.ToString();
        SalahTxt.text = JSoalSalah.ToString();
        HighscoreTxt.text = skor.ToString();
        playing = false;
    }

    public void gameover()
    {
        playing = false;
        StartCoroutine(ShowGemeover(0.5f));
    }

    IEnumerator showConditionPanel(float time)
    {
        yield return new WaitForSeconds(time);
        if (idcondition ==0)
        {
            PanelJawabBenar.SetActive(true);
        }
        else
        {
            PanelJawabSalah.SetActive(true);
        }
        StopCoroutine(showConditionPanel(0));
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
