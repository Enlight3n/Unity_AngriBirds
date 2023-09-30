using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public List<bird> birds;
    public List<pig> pig;
    public static Gamemanager _instance;

    private Vector3 originPos; //初始化的位置

    public GameObject win;
    public GameObject lose;

    public GameObject[] stars;

    private int starsNum = 0;

    private int totalNum = 10;

    private void Awake() 
    {
        _instance = this;
        if(birds.Count >0)
        {
            originPos = birds[0].transform.position;
        }
        
    }

    private void Start() 
    {
        Initialized();
    }
    private void Initialized()
    {
        for(int i = 0; i< birds.Count; i++)
        {
            if(i == 0)
            {
                birds[i].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

     
    public void NextBird()
    {
        if(pig.Count > 0)
        {
            if(birds.Count > 0)
            {
                Initialized();
            }
            else
            {
                //输了
                lose.SetActive(true);
            }
        }
        else
        {
            //赢了
            win.SetActive(true);
        }
    }
    public void ShowStars()
    {
        StartCoroutine("show");
    }

    IEnumerator show()
    {
        for(;starsNum < birds.Count + 1;starsNum++)
        {
            if(starsNum>= stars.Length){
                break;
            }
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);

        }
    }

    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);

    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SaveData(){
        if(starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }


        int sum = 0;

        for(int i=1; i<=totalNum; i++) {
            sum+= PlayerPrefs.GetInt("level" + i.ToString());
        }

        PlayerPrefs.SetInt("totalNum",sum);
    }
}