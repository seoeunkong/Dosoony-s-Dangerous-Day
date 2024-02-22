using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Controller : MonoBehaviour //Rotator 스크립트
{
    public float speed = 3f; //속도를 3f로 설정함.


    
    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.03f, Space.Self); //자기 자신의 z축 기준으로 회전함.
    }

}
