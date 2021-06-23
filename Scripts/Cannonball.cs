using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cannonball : MonoBehaviour
{
    Vector3 _initialPosition;
    private bool _wasShot;
    private float _timeStationary;

    [SerializeField] private float _launchPower = 500;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (_wasShot && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeStationary += Time.deltaTime;
        }

        if(transform.position.y > 10 || 
           transform.position.y < -10 || 
           transform.position.x > 20 || 
           transform.position.x < -20 || 
           _timeStationary > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }       
    }
    
    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sortingOrder = -2;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        Vector2 dirToInitPos = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(dirToInitPos * _launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _wasShot = true;
        GetComponent<LineRenderer>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
