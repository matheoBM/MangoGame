using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    private static int _points;
    void Start()
    {
        _points = 0;
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Adds one point to the point counter
    /// </summary>
    public static void AddPoints()
    {
        _points++;
        Debug.Log("Pontuação atual: " + _points);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Mango"))
        {

        }    
    }
}
