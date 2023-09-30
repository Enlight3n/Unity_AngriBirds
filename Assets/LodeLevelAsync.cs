using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LodeLevelAsync : MonoBehaviour
{
  
    void Start()
    {
        Screen.SetResolution(1024,576,false); 
        Invoke("Load",2);
    }

  
    void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
