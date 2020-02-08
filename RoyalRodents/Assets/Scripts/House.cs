using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House :MonoBehaviour
{
    private Sprite _built;
    private float _hitpoints = 50;



    public House() // calls BuildObjectConstructor by default
    {
        
    }



    // Start is called before the first frame update
    void Start()
    {
        _built = Resources.Load<Sprite>("TmpAssests/House");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float BuildingComplete()
    {
        this.transform.GetComponent<SpriteRenderer>().sprite = _built;
        return _hitpoints;
    }

   
}
