using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HP_Controller : MonoBehaviour
{
    GameObject director;
    public Slider slHP;
    float value;

    void Start()
    {
       director = GameObject.Find("player");
        value = 100.0f;
        slHP.value = value;


    }


    void Update()
    {

        HpGauge();

    }

    public void HpGauge()
    {
        player a=director.GetComponent<player>();
        slHP.value = a.energy; //체력값 갖고 오기
        slHP.transform.position = a.transform.position+new Vector3(0,7,0); //hp 위치 지정
    }

   
}
