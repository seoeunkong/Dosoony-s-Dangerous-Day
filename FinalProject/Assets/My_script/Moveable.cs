using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public float distance = 5f; //장애물을 이동하는 거리
    public float speed = 5f;
    public float offset = 2.0f; //출발할때의 위치 수정하기 위해 

    private bool isForward = true; //움직임을 완료했으면
    private Vector3 startPos;
    GameObject wall;


    void Start()
    {
        startPos = transform.position;

        transform.position += Vector3.right * offset;

    }

   
    void Update()
    {
        if (gameObject.tag == "Wall")
        {
            if (isForward)
            {
                if (transform.position.x < startPos.x + distance) //오른쪽으로 이동 
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                }
                else
                    isForward = false;
            }
            else
            {
                if (transform.position.x > startPos.x) //왼쪽으로 이동 
                {
                    transform.position -= Vector3.right * Time.deltaTime * speed;
                }
                else
                    isForward = true;
            }
        }

        else
        {


            if (isForward)
            {
                if (transform.position.x > startPos.x - distance) //왼쪽으로 이동
                {
                    transform.position -= Vector3.right * Time.deltaTime * speed;
                    
                }

                else
                    isForward = false;
            }
            else
            {
                if (transform.position.x < startPos.x) //오른쪽으로 이동
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                    
                }

                else
                    isForward = true;
            }
            
        }

        }
    }



               