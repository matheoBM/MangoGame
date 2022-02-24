using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MangoControl : MonoBehaviour
{
    private HingeJoint2D _hingeJoint;

    void Start()
    {
        _hingeJoint = GetComponent<HingeJoint2D>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Sandal"))
        {
            _hingeJoint.enabled = false;
        }else if(other.gameObject.CompareTag("Player"))
        {
            PlayerPoints.AddPoints();
            Destroy(gameObject);
            GenerateMango.SubtractMango();
        }
    }
}
