using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    int a = 1; //�������� �̵����� ���������� �̵����� �Ǵ��ϴµ� �ʿ��� ����
    private float speed = 15f;

 
    void Update()
    {
        SpeedCar();
        Move();

    }

    private void Move()
    {

        if (transform.position.x < -100.0f) //���� ��ġ�� �·� �̵������� �ִ밪���� ������ �� ���ʿ��� ���������� �̵�����

        {
            a = 1;
            Vector3 monVec = new Vector3(a, 0, 0).normalized; //������
            transform.LookAt(transform.position + monVec);


        }

        else if (transform.position.x > 100.0f) //��� �̵������� �ִ밪���� ũ�� �������� �̵�

        {
            a = -1;
            Vector3 monVec = new Vector3(a, 0, 0).normalized; //����
            transform.LookAt(transform.position + monVec);
            a = 1;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime * a);

    }

    void SpeedCar() //car2�� car1�� �ٸ��� �ӵ��� ���� ������.
    {
        if (gameObject.tag == "car2")
        {
            speed = 25f;
        }
    }


}
