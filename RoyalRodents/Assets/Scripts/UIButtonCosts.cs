using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIButtonCosts : MonoBehaviour
{
    public int cost;
    public int currentGold;
    public TextMeshProUGUI text;

    public Color bad = Color.red;
    public Color good = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        text=  this.gameObject.transform.GetComponent<TextMeshProUGUI>();
        UpdateCosts();
    }

     void Update()
    {
        
    }

    public void UpdateCosts()
    {
        currentGold = GameManager.Instance.gold;
        text.text = currentGold.ToString() + "/" + cost;
        if (currentGold < cost)
        {
            text.color = bad;
        }
        else
            text.color = good;
    }
}
