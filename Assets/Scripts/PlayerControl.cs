using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private bool _hasSandal;
    private GameObject _sandal;
    private Animator _animator;
    [SerializeField] private float _speed, _mangoSpeed;
    [SerializeField] private GameObject _sandalPrefab;
    [SerializeField] private Transform _shotPointCenter;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("HasSandal", true);
        _rb2d = GetComponent<Rigidbody2D>();
        _hasSandal = true;
        _speed = 10;
        _mangoSpeed = 500;
    }

    void Update()
    {
        //Get mouse direction
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        _shotPointCenter.right = direction;

        //if(!_hasSandal) _sandal.transform.Rotate(0, 0, -300 * Time.deltaTime);

        if(Input.GetMouseButtonDown(0) && _hasSandal)
        {
            ThrowSandal(direction);
            _hasSandal = false;
            _animator.SetBool("HasSandal", _hasSandal);
        }else if(Input.GetMouseButton(1) && !_hasSandal)
        {
            CallSandal();
        }

    }
    
    private void FixedUpdate()
    {
        float direction = Input.GetAxis("Horizontal");
        Movement(new Vector2(direction, 0));
        FlipCaracter(direction);
        _animator.SetFloat("Speed", Mathf.Abs(direction));
    }

    private void Movement(Vector2 direction)
    {
        _rb2d.velocity = _speed * direction;
    }

    /// <summary>
    /// Flip the caracter if the direction has changed
    /// </summary>
    /// <param name="direction">New direction of the player</param>
    private void FlipCaracter(float direction)
    {
        if(Mathf.Sign(direction) != Mathf.Sign(transform.localScale.x) && direction != 0)
            transform.localScale = new Vector3(-1*transform.localScale.x, 1, 1);
    }

    /// <summary>
    /// Throw the sandal
    /// </summary>
    /// <param name="direction">Direction of the mouse</param>
    private void ThrowSandal(Vector2 direction)
    {
        Vector2 position = (Vector2)_shotPointCenter.position + (direction * 2);
        _sandal = Instantiate(_sandalPrefab, position, Quaternion.identity);
        _sandal.GetComponent<Rigidbody2D>().AddForce(_mangoSpeed * direction);
        _sandal.GetComponent<Rigidbody2D>().gravityScale = 1f;
        _sandal.GetComponent<BoxCollider2D>().enabled = true;
        
    }

    private void CallSandal()
    {
        Vector2 sandalPosition = _sandal.transform.position;
        Vector2 direction = ((Vector2)transform.position - sandalPosition).normalized;
        _sandal.GetComponent<Rigidbody2D>().AddForce(20 * direction);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Sandal"))
        {
            Destroy(_sandal);
            _hasSandal = true;
            _animator.SetBool("HasSandal", true);
        }
    }

}
