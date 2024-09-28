using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketBall : MonoBehaviour
{
    int randNum;
    [SerializeField] float _speed = 10;
    [SerializeField] Transform _ball;
    [SerializeField] Transform _arms;
    [SerializeField] Transform _posOverhead;
    [SerializeField] Transform _posDribble;
    [SerializeField] Transform _target;
    [SerializeField] Transform _targetMiss1;
    [SerializeField] Transform _targetMiss2;
    [SerializeField] Transform _targetMiss3;
    [SerializeField] Transform _targetStart;

    private Animator animator; 
    private bool _isBallInHands = true;
    private bool _isBallFlying = false;
    private float _t = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += Vector3.ClampMagnitude(direction,1)  * _speed * Time.deltaTime;
        transform.LookAt(transform.position + direction);
        animator.SetFloat("speed", Vector3.ClampMagnitude(direction,1).magnitude);

        if (_isBallInHands)
        {   
            _ball.GetComponent<Collider>().isTrigger = true;
            if (Input.GetKey(KeyCode.Space))
            {
                _ball.position = _posOverhead.position;
                
                transform.LookAt(_target.position);
            }
            else
            {
                _ball.position = _posDribble.position + Vector3.up * Mathf.Abs((Mathf.Sin(Time.time * 5))/ 11);
            }   

            if (Input.GetKeyUp(KeyCode.Space))
            {
                _isBallInHands = false;
                _isBallFlying = true;
                _t = 0;
                _targetStart.position = _posOverhead.position;
                randNum = Random.Range(1,5);
                _ball.GetComponent<Collider>().isTrigger = false;

            }
        }

        if (_isBallFlying)
        {
            _t += Time.deltaTime;
            float duration = 1;
            float t01 = _t / duration;
            
            if (randNum == 1)
            {
                Vector3 a = _targetStart.position;
                Vector3 b = _target.position;
                Vector3 pos = Vector3.Lerp(a, b, t01);
                Vector3 arc = Vector3.up * 2 * (Mathf.Sin(t01 * 3.14f)) / 4;
                _ball.position = pos + arc;
            }
            else if (randNum == 2)
            {
                Vector3 a = _targetStart.position;
                Vector3 b = _targetMiss1.position;
                Vector3 pos = Vector3.Lerp(a, b, t01);
                Vector3 arc = Vector3.up * 2 * (Mathf.Sin(t01 * 3.14f)) / 4;
                _ball.position = pos + arc;
            }
            else if (randNum == 3)
            {
                Vector3 a = _targetStart.position;
                Vector3 b = _targetMiss2.position;
                Vector3 pos = Vector3.Lerp(a, b, t01);
                Vector3 arc = Vector3.up * 2 * (Mathf.Sin(t01 * 3.14f)) / 4;
                _ball.position = pos + arc;
            }
            else if (randNum == 4)
            {
                Vector3 a = _targetStart.position;
                Vector3 b = _targetMiss3.position;
                Vector3 pos = Vector3.Lerp(a, b, t01);
                Vector3 arc = Vector3.up * 2 * (Mathf.Sin(t01 * 3.14f)) / 4;
                _ball.position = pos + arc;
            }
            
 
            if (t01 >= 1)
            {
                _isBallFlying = false;
                _ball.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (!_isBallInHands && !_isBallFlying && other.gameObject.tag == "ball")
        {
            Debug.Log("fscdfwefs");
            _isBallInHands = true;
            _ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
