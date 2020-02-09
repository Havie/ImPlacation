﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterControllerTMP controller;
    private float _moveSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

    private bool _AttackDelay;
    private bool _isAttacking;
    private float _damage;
    // Start is called before the first frame update
    void Start()
    {
        _moveSpeed= this.GetComponent<PlayerStats>()._Move_Speed;
        _damage = this.GetComponent<PlayerStats>()._AttackDamage;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        // move our character
        controller.Move(horizontalMove *Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void Attack()
    {
        if (!_AttackDelay)
        {

            _isAttacking = true;

            // _Animator.SetTrigger("Attack");

   

                StartCoroutine(AttackEnd());
        }

    }
    IEnumerator AttackEnd()
    {

        _AttackDelay = true;
        yield return new WaitForSeconds(0.1f);

        //Add to the starting pos so we dont target ourself
        Vector3 _startPos = this.transform.position;
        Vector3 _ourDir = Vector2.left;
        if (this.transform.GetComponent<CharacterControllerTMP>())
        {
            //This is Another Hack that needs fixing
            bool _facingRight = this.transform.GetComponent<CharacterControllerTMP>().m_FacingRight;
            if (_facingRight)
            {
                _startPos += new Vector3(1, 0, 0);
                _ourDir = -Vector2.left;
            }
            else
                _startPos -= new Vector3(1, 0, 0);
        }
        Debug.LogError(_startPos);
        //Define a Layer mask to Ignore all items ON that layer.
        int _LayerMask = ~(LayerMask.GetMask("Default"));
        RaycastHit2D hit = Physics2D.Raycast(_startPos, _ourDir, 0.75f, _LayerMask);

        //Drawing a Ray doesnt work?
       //Debug.DrawRay(_startPos, _ourDir, Color.red);

        Debug.Log("Hit Dis:" + hit.distance);
        if (hit.collider != null)
        {
            Debug.Log("Found :" + hit.collider.gameObject.name);
            AIController ai = hit.collider.GetComponent<AIController>();
            if (ai)
            {
                ai.Damage(_damage);
            }
        }

        yield return new WaitForSeconds(0.85f);
        _isAttacking = false;
        _AttackDelay = false;
    }

}
