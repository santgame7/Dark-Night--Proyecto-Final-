using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Creditos : MonoBehaviour
{
    
    void Start()
    {
        Invoke("WaitToEnd", 30);
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

    }

    public void WaitToEnd()
    {
        SceneManager.LoadScene("Menu");
    }
}
