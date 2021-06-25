using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundFaster : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    private float speed = 0.6f;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SoundSpeed());
    }
    public void LowSpeed(){
        speed = 0.6f;
        StartCoroutine(SoundSpeed());
    }
    public void Speed(){
        speed = 1f;
        StartCoroutine(SoundSpeed());

    }
    private IEnumerator SoundSpeed(){
        audioSource.pitch = speed;
        yield return null;
    }
}
