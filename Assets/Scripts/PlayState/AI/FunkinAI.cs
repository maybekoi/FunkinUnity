using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunkinAI : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "AiNote")
        {
            Destroy(gameObject);
        }
    }
}