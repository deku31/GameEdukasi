using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> sfx;
    public AudioSource audio;
    public AudioSource bgm;
    // Start is called before the first frame update
    public void _sfx(int id)
    {
        audio.PlayOneShot(sfx[id]);
    }
}
