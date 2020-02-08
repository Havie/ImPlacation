using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public Animator _Animator;
    public GameObject _target;
    public float _MovementSpeed=2f;

    public float _health = 25f;
    public float _damage = 3f;

    private float minDistance = 1f;
    private bool _FacingRight;
    private bool _isAttacking;
    private bool _AttackDelay;



    // Start is called before the first frame update
    void Start()
    {
        _Animator = this.GetComponent<Animator>();
        if (!_Animator)
            Debug.LogError("AI Controller Missing Animator");
    }

    // Update is called once per frame
    void Update()
    {
       if(_target)
        {
                Vector3 _goalPos;
                _goalPos = new Vector3(_target.transform.position.x,0,0);
            if (Mathf.Abs(transform.position.x - _goalPos.x) >= minDistance)
            {
                MoveToTarget(_goalPos);
            }
            else
                Attack();
        }

    }


    public void MoveToTarget(Vector3 pos)
    {
        if (!_isAttacking)
        {

            //Debug.Log("Goal Pos =" + pos);
            transform.position += pos.normalized * Time.deltaTime * _MovementSpeed;
            _Animator.SetBool("IsMoving", true);
        }
            if (pos.x > 0 && !_FacingRight)
            {
                Flip();
            }
            // Otherwise if the input is moving  left and the AI is facing right...
            else if (pos.x < 0 && _FacingRight)
            {
                Flip();
            }
        

    }
    public void Attack()
    {
        if (!_AttackDelay)
        {

            _isAttacking = true;

            _Animator.SetTrigger("Attack");

            _target.GetComponent<PlayerStats>().Damage(_damage);
            StartCoroutine(AttackEnd());
        }

    }
    IEnumerator AttackEnd()
    {
        _AttackDelay = true;
        yield return new WaitForSeconds(1.5f);
        _isAttacking = false;
        _AttackDelay = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<PlayerMovement>())
        {
            _target = collision.gameObject;
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _FacingRight = !_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
