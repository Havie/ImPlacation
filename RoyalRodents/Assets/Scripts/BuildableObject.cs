﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildableObject : MonoBehaviour, IDamageable<float>
{
    public Sprite _statedefault;
    public Sprite _stateBuilding;
    public Sprite _stateDamaged;
    public Sprite _stateDestroyed;
    public Sprite _onHover;
    public Sprite _notification;
    public Sprite _emptyWorker;
    public Sprite _Worker;
    public Sprite _buildingHammer;

    public GameObject _NotificationObject;
    public GameObject __WorkerObject;

    public Animator _animator;


    [SerializeField]
    protected BuildingState eState;

    [SerializeField]
    protected BuildingType eType;

    private SpriteRenderer sr;
    private SpriteRenderer srNotify;
    private SpriteRenderer srWorker;
    private UIBuildMenu _BuildMenu;

    private float hitpoints;
    private GameObject _currentBuilding;

    protected enum BuildingState { Available, Idle, Building, Built };

    protected enum BuildingType { House, Farm, Tower, Wall, TownCenter, Vacant}




    //interface stuff
    public void Damage(float damageTaken)
    {
        if (hitpoints - damageTaken > 0)
            hitpoints -= damageTaken;
        else
            hitpoints = 0;
    }

    public BuildableObject()
    {

    }

    public BuildableObject(string custom)
    {
        //this is a secondary constructor, see House class
    }


    // Start is called before the first frame update
    void Start()
    {
        sr = this.transform.GetComponent<SpriteRenderer>();
        sr.sprite = _statedefault;

        srNotify = _NotificationObject.transform.GetComponent<SpriteRenderer>();
        srWorker = __WorkerObject.transform.GetComponent<SpriteRenderer>();
        srWorker.sprite = _emptyWorker;

        eState =BuildingState.Available;
        eType = BuildingType.Vacant;
        _animator = GetComponentInChildren<Animator>();
        


        GameObject o=GameObject.FindGameObjectWithTag("BuildMenu");
        _BuildMenu = o.GetComponent<UIBuildMenu>();
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
                    srWorker.enabled = false;
                    _animator.SetBool("Notify", true);
                    _animator.SetBool("Building", false);
                    break;
                }
            case BuildingState.Building:
                {
                    srNotify.sprite = _buildingHammer;
                    srWorker.sprite = _emptyWorker;
                    srNotify.enabled = true;
                    srWorker.enabled = true;
                    _animator.SetBool("Building", true);
                    break;
                }
            case BuildingState.Idle:
                {
                    srNotify.enabled = false;
                    srWorker.enabled = false;
                    _animator.SetBool("Notify", false);
                    _animator.SetBool("Building", false);
                    break;
                }
            case BuildingState.Built:
                {
                    srNotify.enabled = false;
                    srWorker.enabled = true;
                    _animator.SetBool("Notify", false);
                    _animator.SetBool("Building", false);
                    break;
                }
        }



             if (Input.GetMouseButtonDown(0))
         {
           

                            //ALL OF THIS IS TEST
                            /* GameObject o = GameObject.FindGameObjectWithTag("Canvas");
                             RectTransform CanvasRect = o.GetComponent<RectTransform>();
                             Vector2 WorldObject_ScreenPosition = new Vector2(
                             ((mousePos.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
                             ((mousePos.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

                             Vector2 localpoint;
                             RectTransform rectTransform = _BuildMenu.getRect();
                             Canvas canvas = o.GetComponent<Canvas>();
                             RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, canvas.worldCamera, out localpoint);
                             Vector2 normalizedPoint = Rect.PointToNormalized(rectTransform.rect, localpoint);
                             Debug.Log("Normalized :  " +normalizedPoint);


                             Vector2 pos;
                             RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
                             Vector2 newPos2D_Cav = canvas.transform.TransformPoint(pos);

                             Debug.Log("Mouse2d" + mousePos2D);
                             Debug.Log("WorldObj:" + WorldObject_ScreenPosition);
                             Debug.Log("Mouse:" + MouseRaw);
                             Debug.Log("attempt:" + newPos2D_Cav);
                             // UI_Element.anchoredPosition = WorldObject_ScreenPosition;
                             //END OF TESTS
                             */

                            //now you can set the position of the ui element
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something In Collider Range");
    }

    public void imClicked(Vector2 loc)
    {
        if (eState == BuildingState.Built)
        {

        }
        if (eState == BuildingState.Available || eState == BuildingState.Idle)
        {
            eState = BuildingState.Idle;
            _BuildMenu.showMenu(true, loc);
        }
        else
        {
            eState = BuildingState.Idle;
            _BuildMenu.showMenu(true, loc);
        }
  

    }


    public virtual void BuildSomething(string type)
    {
        Debug.Log("Time to Build Something type=" + type);
        switch (type)
        {
            case ("house"):
                this.gameObject.AddComponent<House>();
                eType = BuildingType.House;
                Debug.Log("Made a house");
                break;

            case null:
                break;
        }
        _BuildMenu.showMenu(false, Vector3.zero);

    }



}