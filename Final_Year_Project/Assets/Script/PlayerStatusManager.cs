using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{
    // Start is called before the first frame update


    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //player hit by enemy
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            GameObject.Find("MainChar").GetComponent<PlayerInfo>().healthPoint -= 1;
            Debug.Log("Hitted");
        }
    }
}
