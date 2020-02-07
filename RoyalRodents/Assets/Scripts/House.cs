using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House :MonoBehaviour
{
    public Sprite _building;
    private Sprite _built;
    private float _hitpoints;



    public House() // calls BuildObjectConstructor by default
    {
        
    }



    // Start is called before the first frame update
    void Start()
    {
        _building = Resources.Load<Sprite>("TmpAssests/House");
        this.transform.GetComponent<SpriteRenderer>().sprite = _building;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
}
