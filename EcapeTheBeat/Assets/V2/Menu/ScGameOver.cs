using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScGameOver : MonoBehaviour
{
    public static ScGameOver Instance;

    [SerializeField] AudioClip winMusic;
    [SerializeField] AudioClip loseMusic;

    AudioSource myAudio;
    Animator myAnimator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
    }

    public void PlayerDead()
    {
        myAudio.clip = loseMusic;
        myAudio.Play();
        ShowMenu();
    }

    public void PlayerWon()
    {
        myAudio.clip = winMusic;
        myAudio.Play();
        ShowMenu();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GiveUp()
    {
        SceneManager.LoadScene("mainMenu");
    }

    private void ShowMenu()
    {
        myAnimator.SetBool("gameOver", true);
    }
}
