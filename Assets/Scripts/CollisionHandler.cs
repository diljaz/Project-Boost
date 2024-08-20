using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delaySpeed = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled){return;}

        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break; 
            default:
                StartCrashSequence();
                break;
        } 
    }

        void StartCrashSequence()
        {
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel",delaySpeed);
            crashParticles.Play();
            audioSource.Stop();
            audioSource.PlayOneShot(crashSound);
        }

        void StartSuccessSequence()
        {
            isTransitioning = true;
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel",delaySpeed);
            successParticles.Play();
            audioSource.Stop();
            audioSource.PlayOneShot(successSound);
        }
        void ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        void LoadNextLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex);
        }
}

