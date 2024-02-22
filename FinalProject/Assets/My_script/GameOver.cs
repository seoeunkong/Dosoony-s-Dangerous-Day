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
        over1 = a.dead; //������ �� ��
        goal = a.goal; //��������
        dietime = b.time; //�ð� �� ��
        if (over1 == true)
        {
            SceneManager.LoadScene(2); //gameover������ �̵�
            b.gameObject.SetActive(false); //������ Ÿ�̸� ��� ����
        }
        else if (goal==true)
        {
            SceneManager.LoadScene(3);//goal������ �̵�
        }
        else if (dietime <= 0)
        {
            SceneManager.LoadScene(2); //gameover������ �̵�
            b.gameObject.SetActive(false); //������ Ÿ�̸� ��� ����
        }

        GameOver_txt.transform.position = a.transform.position + new Vector3(0, 5, 10); //gameover ������ ��ġ ����
    }
}
