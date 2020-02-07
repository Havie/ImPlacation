using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : BuildableObject
{



    public House() // calls BuildObjectConstructor by default
    {

    }

    public House(string s) : base(s) // will call the second BuildableObject constructor that takes in a string
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        eState = BuildingState.Building;
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void BuildSomething() 
    {
        base.BuildSomething(); //replaces super
        Debug.Log("Heard build in House");
    }
}
