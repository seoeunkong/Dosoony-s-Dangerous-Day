using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float time;
    public Text Timer;
    GameObject gameobject1;
    GameObject gameobject2;
    bool goal;

    void Start()
    {
        gameobject1 = GameObject.Find("player");
        gameobject2 = GameObject.Find("GameDirector_final"); //UI
        time = 60; //시간제한 50초
        
    }

    void Update()
    {
        GameOver b = gameobject2.GetComponent<GameOver>();
        goal = b.goal;
        if (goal == false) // 게임 중
        {
            time -= Time.deltaTime;
            Timer.text = "남은 시간: " + Mathf.Round(time) + "초";
            Timer.transform.position = gameobject1.transform.position + new Vector3(13, 19, 0);  //타이머 문구 위치 지정
        }
       

    }
}
