using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Vector3 position;
    public CapsuleCollider capsuleCollider;
    


    Animator animator;
    float movementSpeed = 17f;

    
    Rigidbody rigid;
    bool jDown;
  
    bool DDown;
    bool isDodge;
    bool trampolineActive;
    bool isLand;
   
   
    
    GameObject nearitem;
    GameObject equipweapon;
   
    
    public int energy;
    
   

    public ParticleSystem p_system;
    int a;

    public bool dead;
    public bool goal;

    void Awake()
    {
        energy = 100; //�÷��̾��� �⺻ ������(ü��)�� 100���� ������.
        rigid = GetComponent<Rigidbody>(); //�߷� ����(������ �� �̿�)
        animator = GetComponentInChildren<Animator>();//�ִϸ��̼�
        capsuleCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>(); //�ݶ��̴� ����� �����߰��� �����ϱ� ���� �ʿ�
        dead = false; // ü�� Ȥ�� �ð��� �� ������ ���� �Ǵ��ϱ� ���� ����
        goal = false; // ���� ������ �������� ���� �Ǵ� ����
    }

   

    void FixedUpdate()  //Update() �Լ��� �̿��ϸ� �÷��̾� �̵� ���߿� ī�޶� ���� ���� FixedUpdate() ���
    {
      
        Movement();
        Interation();
        ParticleStart();
            charcter();
            Jump();
            Dodge();
        trampolineJump();
        ChageCollider();
        GameOver();
       
        
        

    }
    

    
    void Movement() //������Ʈ ������
    {
        position.x += movementSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal"); //����Ű ��,�Ʒ��� �÷��̾��� x��ǥ�� ������
        position.z += movementSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical"); // ����Ű ������, �������� �÷��̾��� z��ǥ�� ������
        Vector3 moveVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized; //�ε巴�� �����̱� ���� normalized ���

        transform.position = position;
        transform.LookAt(transform.position + moveVec); //�����̴� ���⿡ ���� �÷��̾� �ü��� ���� �� �ֵ��� ����.

    }


    void charcter() //������Ʈ �ִϸ��̼�
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0) //���� �������� �ʴ´ٸ� isRun(�޸��� �ִϸ��̼�)�� ��Ȱ��ȭ��Ű�� �⺻ �ִϸ��̼Ǹ�(ldie)�� Ȱ��ȭ��Ŵ
        {
            animator.SetBool("isRun", false);
        }
        else //�����δٸ� isRun(�޸��� �ִϸ��̼�) Ȱ��ȭ��Ŵ.
            animator.SetBool("isRun", true);
    }

    private void OnTriggerEnter(Collider other) //������ �Ա�
    {
            nearitem = other.gameObject; //���� �������� nearitem������ ����
    }

    private void OnTriggerExit(Collider other)
    {
            nearitem = null;  //���� ������ ������ nearitem ������ �����
    }
    
    void Jump() //����
    {
        jDown = Input.GetButtonDown("Jump"); //�����̽��ٸ� ������ true�� �޾���.

        if (jDown&&rigid.velocity.y<5) //���ѹ� �����ϴ� ���� �����ϱ� ���� rigid.velocity.y<5��� ���� �������� ��. ó���� rigid.velocity.y=0���� �Ϸ��� ���� �� ������ ���ϴ� ��쵵 �߻��Ͽ� ���� ���ݰ� ���� ������.
        {
            rigid.AddForce(Vector3.up * 1000, ForceMode.Impulse);//������ ���� �̵� ����
            animator.SetTrigger("DoJump"); //�����ϴ� �ִϸ��̼� Ȱ��ȭ
            animator.SetBool("IsJump", true); //�����ϴ� �ִϸ��̼� Ȱ��ȭ
           
        }
        else
            animator.SetBool("IsJump", false);

    }

    void Dodge() //������
    {
        DDown = Input.GetButtonDown("Fire3"); //��shift�� ������ �����Ⱑ Ȱ��ȭ

        if (DDown )
        {
            movementSpeed *= 2;
            animator.SetTrigger("DoDodge"); //������ �ִϸ��̼� �۵�
            isDodge = true; 

            Invoke("DodgeOut", 0.4f); //�ð����� �ΰ� �����⸦ ������ �Լ��� �ҷ���
        }
        
    }

    void DodgeOut() // ������ ������ ���� �����ϱ� ���� ����
    {
        movementSpeed *= 0.5f;
        isDodge = false;
    }

    void ChageCollider() //�������Ҷ� �ݶ��̴� ũ�� ����
    {
        if (isDodge == true) //�����⸦ �Ѵٴ� ������ ������ �Ǹ� �ݶ��̴��� ũ�⸦ 2�� ����(������ ����:4)
        {
            capsuleCollider.height = 2;
        }
        else
        {
            capsuleCollider.height = 4;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "car1") //��ֹ� �ڵ����� ����� ��
        {
            
            animator.SetTrigger("isDie"); // ��ֹ��� �������� �� �ִϸ��̼� �۵�
            Damage(); //energy(ü��)�� �����ϴ� �Լ��� �ҷ���
        }
        else if (collision.gameObject.tag == "car2") // ��ֹ� �ڵ����� ����� ��. ��, car1�� �ٸ��� car2�� �ӵ��� �� ����. ���� �±׸� ������� ��.
        {

            animator.SetTrigger("isDie");
            Damage();
        }
        else if (collision.gameObject.tag == "jumping") //���� ������ ����� ��
        {
            trampolineActive = true; //���� ������ ��Ҵ����� ���� �Ǵ��� ���� ������ ������.
        }
        else if (collision.gameObject.tag == "obstacle1") //�ڵ��� �̿��� ��� ��ֹ��鿡 ����� ��
        {

            animator.SetTrigger("isDie");
            Damage();

        }
        else if (collision.gameObject.tag == "Finish") //��������(Finish)�� ����� ��
        {

            goal = true;

        }


    }

   
    void trampolineJump() //������ �� ���� ���ִ� �Լ�
    {
        if (trampolineActive == true) //���ε����� ������ trampolineActive�� true�� ����.
        {
            rigid.AddForce(Vector3.up * 1500, ForceMode.Impulse); //�⺻�������� ���� �ٰ� ����
            animator.SetBool("IsJump", true);
            animator.SetTrigger("DoJump");

            trampolineActive = false;
        }
    }

    public void Damage() //ü�� ���� �Ծ��� ���� �Լ�
    {
        if (energy > 0) //���� ü���� 0�̰ų� ������ ���̻��� ����� ���� ����.
        {
            
            energy -= 30;
        }
       
    }

   
    void Interation() //������ ��ȣ�ۿ�
    {
        if (nearitem != null) // �÷��̾ �������� �Ծ ���� �ִ� nearitem�� �� ���� �ƴϿ��� �� �Լ��� ����
        {
            if (energy < 100) //ü���� 100�̻��̸� ���̻��� ����� ���� ����
            {
                energy += 20;

                if (energy > 100) //���� ü���� �ʱ⿣ 100���Ͽ����� �������� ��� 20�� �߰��Ǿ� 100�̻��� �Ǹ� 100���� �ʱ�ȭ��
                {
                    energy = 100;
                }
            }
            Destroy(nearitem); //�������� ������ ����.
        }
       
    }

    

    void ParticleStart() //�������� �Ծ��� ���� ��ƼŬ ȿ��
    {
       
        if (nearitem != null)
        {
            if (nearitem.tag == "energy") //nearitem�� ü���̶��
            {
                 GetComponent<ParticleSystem>().Play();
               
            }
        }
    }


    void GameOver() //ü���� 0�϶��� ���ӿ����� �Լ�
    {
        if (energy <= 0)
        {
            dead = true; //���� ��ũ��Ʈ�� �ѱ� ���� ����. 
        }
       
    }


}
