using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Controller : MonoBehaviour //Rotator ��ũ��Ʈ
{
    public float speed = 3f; //�ӵ��� 3f�� ������.


    
    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime / 0.03f, Space.Self); //�ڱ� �ڽ��� z�� �������� ȸ����.
    }

}
