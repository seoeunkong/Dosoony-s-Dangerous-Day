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
        time = 60; //�ð����� 50��
        
    }

    void Update()
    {
        GameOver b = gameobject2.GetComponent<GameOver>();
        goal = b.goal;
        if (goal == false) // ���� ��
        {
            time -= Time.deltaTime;
            Timer.text = "���� �ð�: " + Mathf.Round(time) + "��";
            Timer.transform.position = gameobject1.transform.position + new Vector3(13, 19, 0);  //Ÿ�̸� ���� ��ġ ����
        }
       

    }
}
