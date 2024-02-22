using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    int a = 1; //왼쪽으로 이동할지 오른쪽으로 이동할지 판단하는데 필요한 변수
    private float speed = 15f;

 
    void Update()
    {
        SpeedCar();
        Move();

    }

    private void Move()
    {

        if (transform.position.x < -100.0f) //지금 위치가 좌로 이동가능한 최대값보다 작으면 젤 왼쪽에서 오른쪽으로 이동시작

        {
            a = 1;
            Vector3 monVec = new Vector3(a, 0, 0).normalized; //오른쪽
            transform.LookAt(transform.position + monVec);


        }

        else if (transform.position.x > 100.0f) //우로 이동가능한 최대값보다 크면 왼족으로 이동

        {
            a = -1;
            Vector3 monVec = new Vector3(a, 0, 0).normalized; //왼쪽
            transform.LookAt(transform.position + monVec);
            a = 1;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime * a);

    }

    void SpeedCar() //car2는 car1과 다르게 속도를 높게 설정함.
    {
        if (gameObject.tag == "car2")
        {
            speed = 25f;
        }
    }


}
