using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startdirector : MonoBehaviour
{

    void FixedUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("first_gamescene");
        }
    }
}
