using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationSpeed = 50f;
    [SerializeField] AudioClip mainEngine;
    AudioSource audioSource;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            thrusting();
        }
        else
        {
            stopThrusting();
        }
    }

    void thrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
    }

    void stopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
    void ProcessRotation()
    {
         if (Input.GetKey(KeyCode.A))
        {
            leftRotation();       
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rightRotation();   
        }

        else
        {
            stopRotation();
        }
    }

    void leftRotation()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }  
    }

    void rightRotation()
    {
        ApplyRotation(-rotationSpeed); 
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }
    void stopRotation()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
    }
}
