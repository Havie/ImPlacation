using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableObject : MonoBehaviour
{
    public Sprite _statedefault;
    public Sprite _stateBuilding;
    public Sprite _stateDamaged;
    public Sprite _stateDestroyed;
    public Sprite _onHover;
    public Sprite _notification;
    public GameObject _NotificationObject;
    public Sprite _buildingHammer;

    public Animator _animator;

    [SerializeField]
    private BuildingState eState;

    private SpriteRenderer sr;
    private SpriteRenderer srNotify;

    private enum BuildingState { Available, Idle, Building };

    // Start is called before the first frame update
    void Start()
    {
        sr = this.transform.GetComponent<SpriteRenderer>();
        srNotify = _NotificationObject.transform.GetComponent<SpriteRenderer>();
        sr.sprite = _statedefault;

        eState=BuildingState.Available;
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        switch(eState)
        {
            case BuildingState.Available:
                {
                    srNotify.sprite = _notification;
                    srNotify.enabled = true;
                    _animator.SetBool("Notify", true);
                    _animator.SetBool("Building", false);
                    break;
                }
            case BuildingState.Building:
                {
                    srNotify.sprite = _buildingHammer;
                    srNotify.enabled = true;
                    _animator.SetBool("Building", true);
                    break;
                }
            case BuildingState.Idle:
                {
                    srNotify.enabled = false;
                    _animator.SetBool("Notify", false);
                    _animator.SetBool("Building", false);
                    break;
                }
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                    if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    eState = BuildingState.Idle;
                }
            }
        }
    }

}
