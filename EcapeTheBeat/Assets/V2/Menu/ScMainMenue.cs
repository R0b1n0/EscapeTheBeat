using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenue : MonoBehaviour
{
    [SerializeField] AudioClip m_Clip;
    AudioSource m_Source;
    Animator animator;
    bool gameStarting;
    float ogVol;
    bool pressedStarted;


    void Start()
    {
        animator = GetComponent<Animator>();
        m_Source = GetComponent<AudioSource>();
        ogVol = m_Source.volume;
    }

    private void Update()
    {
        if (gameStarting)
        {
            m_Source.volume -= ogVol*Time.deltaTime;
        }
    }

    public void StartGame()
    {
        if (!pressedStarted)
        {
            pressedStarted = true;
            animator.SetBool("start", true);
            m_Source.PlayOneShot(m_Clip);
        }

    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("V2");
    }
    public void LowerMusicVolume()
    {
        gameStarting = true;
    }
}
