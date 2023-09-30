using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausepanel : MonoBehaviour
{
    private Animator anim;
    public GameObject button;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Retry()
    {   
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Pause()
    {
        
        anim.SetBool("isPause", true);
        button.SetActive(false);
    }

    

    public void Resume()
    {
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd()
    {
        button.SetActive(true);
    }


}
