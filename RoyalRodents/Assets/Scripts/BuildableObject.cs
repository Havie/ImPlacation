using System.Collections;
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

    private SpriteRenderer sr;
    private SpriteRenderer srNotify;
    private SpriteRenderer srWorker;
    private UIBuildMenu _BuildMenu;

    private float hitpoints;

    protected enum BuildingState { Available, Idle, Building, Built };

    protected enum BuildingType { House, Farm, Tower, Wall, TownCenter}




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
        _animator = GetComponentInChildren<Animator>();
        


        GameObject o=GameObject.FindGameObjectWithTag("BuildMenu");
        Debug.Log(o.ToString());
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
            Debug.Log("Heard Mouse");
                Vector3 MouseRaw = Input.mousePosition;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                    if (hit.collider != null)
                    {
                   // Debug.Log(hit.collider.gameObject.name);

                    if ( hit.collider.gameObject.GetComponent<BuildableObject>())
                    {
                        Debug.Log("Matched");
                        if (eState == BuildingState.Built)
                        {

                        }
                        if (eState == BuildingState.Available || eState==BuildingState.Idle)
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


                            eState = BuildingState.Idle;
                            _BuildMenu.showMenu(true, MouseRaw);
                        }

                    }
                else
                {
                    Debug.Log("NotGameObject2:" + gameObject + " __ hit:" + hit.collider.gameObject);
                    if (_BuildMenu.isActive())
                        _BuildMenu.showMenu(false, MouseRaw);
                }


            }
            else
            {
                Debug.Log("NotGameObject1");
                if (_BuildMenu.isActive())
                    _BuildMenu.showMenu(false, MouseRaw);
            }


        }
    }

    public virtual void BuildSomething()
    {
        Debug.Log("Heard build at" );
    }


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something In Collider Range");
    }


}
