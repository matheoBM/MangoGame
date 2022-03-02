using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private bool _hasSandal;
    private GameObject _sandal;
    private Animator _animator;
    private int _health;
    private float _time;

    [SerializeField] private float _speed, _sandalSpeed;
    [SerializeField] private GameObject _sandalPrefab;
    [SerializeField] private Transform _shotPointCenter;
    [SerializeField] private MangometerControl _meterControl;
    [SerializeField] private GameObject _gameOverMenu;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("HasSandal", true);
        _rb2d = GetComponent<Rigidbody2D>();
        _hasSandal = true;
        _speed = 10;
        _sandalSpeed = 500;
        _health = 100;
        _time = 0;
    }

    void Update()
    {
        //Get mouse direction
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        _shotPointCenter.right = direction;


        if(Input.GetMouseButtonDown(0) && _hasSandal)
        {
            ThrowSandal(direction);
            _hasSandal = false;
            _animator.SetBool("HasSandal", _hasSandal);
        }else if(Input.GetMouseButton(1) && !_hasSandal)
        {
            CallSandal();
        }
        _time += Time.deltaTime;
        if (_time >= 0.3)
        {
            _health -= 1;
            _time = 0;
            _meterControl.GetComponent<MangometerControl>().SetHealthValue(_health);
            if(_health <= 0) 
            {
                //TODO: Add an '_canMove' variable to control when the player can move
                _gameOverMenu.SetActive(true);
            }
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
        _sandal.GetComponent<Rigidbody2D>().AddForce(_sandalSpeed * direction);
        _sandal.GetComponent<Rigidbody2D>().gravityScale = 1f;
        _sandal.GetComponent<BoxCollider2D>().enabled = true;
        
    }

    private void CallSandal()
    {
        Vector2 sandalPosition = _sandal.transform.position;
        Vector2 direction = ((Vector2)transform.position - sandalPosition).normalized;
        _sandal.GetComponent<Rigidbody2D>().AddForce(30 * direction);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Sandal"))
        {
            Destroy(_sandal);
            _hasSandal = true;
            _animator.SetBool("HasSandal", true);
        }else if (other.gameObject.CompareTag("Mango"))
        {
            _health += 5;
            if(_health > 100) _health = 100;   
        }
    }

}
