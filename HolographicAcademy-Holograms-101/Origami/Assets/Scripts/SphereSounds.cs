using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSounds : MonoBehaviour
{
    AudioSource audioSource = null;
    AudioClip impaceClip = null;
    AudioClip rollineClip = null;
    bool rolling = false;

	// Use this for initialization
	void Start ()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 20f;
        
        impaceClip = Resources.Load<AudioClip>("Impact");
        rollineClip = Resources.Load<AudioClip>("Rolling");
	}

    void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.magnitude >= 0.1f)
        {
            audioSource.clip = impaceClip;
            audioSource.Play();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
        if (!rolling && rigid.velocity.magnitude >= 0.01f)
        {
            rolling = true;
            audioSource.clip = rollineClip;
            audioSource.Play();
        }
        else if (rolling && rigid.velocity.magnitude < 0.01f)
        {
            rolling = false;
            audioSource.Stop();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(rolling)
        {
            rolling = false;
            audioSource.Stop();
        }
    }
}
