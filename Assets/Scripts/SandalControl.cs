using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandalControl : MonoBehaviour
{
    private bool _hit, _isGrounded;
    private Rigidbody2D _rb2d;
    private int _dashes;
    void Start()
    {
        _hit = false;
        _isGrounded = false;
        _rb2d = GetComponent<Rigidbody2D>();
        _dashes = 0;
    }

    void Update()
    {
        if(!_hit && !_isGrounded)
        {
            transform.Rotate(0, 0, -390 * Time.deltaTime);
        }

        if(Input.GetMouseButtonDown(0) && _dashes > 0)
        {
            Dash();
        }
    }
    
    private void Dash()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position;
        _rb2d.AddForce(direction * 100);
        _dashes--;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Mango"))
        {
            Debug.Log("Bateu na manga");
            _rb2d.AddForce(Vector3.up * 90);
            _hit = true;
            _dashes++;
        }else if(other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }

    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }    
    }

    
}
