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
    public void Glitch3() { if (!Audio[4].isPlaying) { Audio[4].pitch = Random.Range(0.5f, 1.5f); Audio[4].Play(); } }
    public void Glitch1() { if (!Audio[7].isPlaying) { Audio[4].pitch = Random.Range(0.8f, 1.2f); Audio[7].Play(); } }
    public void Glitch2() { if (!Audio[7].isPlaying) { Audio[8].pitch = Random.Range(0.8f, 1.2f); Audio[8].Play(); } }
    public void MusicOne() { if (!Audio[5].isPlaying) { Audio[5].Play(); } }
    public void MusicOneStop() { if (Audio[5].isPlaying) { Audio[5].Stop(); } }
    public void AmbieneceOne() { if (!Audio[6].isPlaying) { Audio[6].Play(); } }
    public void AmbienceOneStop() { if (Audio[6].isPlaying) { Audio[6].Stop(); } }
    public void fishLosePoint() { if (!Audio[9].isPlaying) { Audio[9].pitch = Random.Range(0.8f, 1.2f); Audio[9].Play(); } }
    public void MusicTwo() { if (!Audio[10].isPlaying) { Audio[10].Play(); } }
    public void MusicTwoStop() { if (Audio[10].isPlaying) { Audio[10].Stop(); } }
    public void MusicThree() { if (!Audio[11].isPlaying) { Audio[11].Play(); } }
    public void MusicThreeStop() { if (Audio[11].isPlaying) { Audio[11].Stop(); } }
    public void MusicChaos() { if (!Audio[12].isPlaying) { Audio[12].Play(); } }
    public void MusicChaosStop() { if (Audio[12].isPlaying) { Audio[12].Stop(); } }
    public void MusicMenu() { if (!Audio[13].isPlaying) { Audio[13].Play(); } }
    public void MusicMenuStop() { if (Audio[13].isPlaying) { Audio[13].Stop(); } }
    public void NotSafe() { if (!Audio[14].isPlaying) { Audio[14].Play(); } }

    public void NeverLeave() { if (!Audio[15].isPlaying) { Audio[15].Play(); } }


}
