using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundEffect;

    public void PlaySoundEffect(){
      soundEffect.Play();
    }
}
