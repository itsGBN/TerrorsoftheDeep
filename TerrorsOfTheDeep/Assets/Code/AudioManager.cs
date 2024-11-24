using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] AudioSource[] Audio;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this); }
    }

    // Play the sound of the Player Walking
    public void fishHookLaunch() { if (!Audio[0].isPlaying) { Audio[0].pitch = Random.Range(0.8f, 1.2f); Audio[0].Play(); } }
    public void fishReel() { if (!Audio[1].isPlaying) { Audio[1].pitch = Random.Range(0.8f, 1.2f); Audio[1].Play(); } }
    public void fishReelStop() { if (Audio[1].isPlaying) { Audio[1].Stop(); } }
    public void fishCaught() { if (!Audio[2].isPlaying) { Audio[2].pitch = Random.Range(0.8f, 1.2f); Audio[2].Play(); } }
    public void fishAddPoint() { if (!Audio[3].isPlaying) { Audio[3].pitch = Random.Range(0.8f, 1.2f); Audio[3].Play(); } }
}
