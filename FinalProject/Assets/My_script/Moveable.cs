using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public float distance = 5f; //��ֹ��� �̵��ϴ� �Ÿ�
    public float speed = 5f;
    public float offset = 2.0f; //����Ҷ��� ��ġ �����ϱ� ���� 

    private bool isForward = true; //�������� �Ϸ�������
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
                if (transform.position.x < startPos.x + distance) //���������� �̵� 
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                }
                else
                    isForward = false;
            }
            else
            {
                if (transform.position.x > startPos.x) //�������� �̵� 
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
                if (transform.position.x > startPos.x - distance) //�������� �̵�
                {
                    transform.position -= Vector3.right * Time.deltaTime * speed;
                    
                }

                else
                    isForward = false;
            }
            else
            {
                if (transform.position.x < startPos.x) //���������� �̵�
                {
                    transform.position += Vector3.right * Time.deltaTime * speed;
                    
                }

                else
                    isForward = true;
            }
            
        }

        }
    }



               