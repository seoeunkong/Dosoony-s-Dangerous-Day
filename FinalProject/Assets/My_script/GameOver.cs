using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text GameOver_txt;
    GameObject gameobject;
    GameObject gameobject2;
    bool over1;
    bool over2;
    public bool goal;
    float dietime;

    void Start()
    {
        gameobject = GameObject.Find("player");
        gameobject2 = GameObject.Find("GameDirector_final"); //UI
    }

    void Update()
    {
        player a = gameobject.GetComponent<player>();
        TimeManager b = gameobject2.GetComponent <TimeManager>();
        over1 = a.dead; //게이지 다 씀
        goal = a.goal; //성공여부
        dietime = b.time; //시간 다 씀
        if (over1 == true)
        {
            SceneManager.LoadScene(2); //gameover씬으로 이동
            b.gameObject.SetActive(false); //죽으면 타이머 기능 끄기
        }
        else if (goal==true)
        {
            SceneManager.LoadScene(3);//goal씬으로 이동
        }
        else if (dietime <= 0)
        {
            SceneManager.LoadScene(2); //gameover씬으로 이동
            b.gameObject.SetActive(false); //죽으면 타이머 기능 끄기
        }

        GameOver_txt.transform.position = a.transform.position + new Vector3(0, 5, 10); //gameover 문구의 위치 지정
    }
}
