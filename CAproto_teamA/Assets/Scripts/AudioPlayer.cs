using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioSource audioSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSound = GetComponent<AudioSource>();
    }

    public void ClickSound()
	{
        audioSound.PlayOneShot(audioSound.clip);

    }
}
