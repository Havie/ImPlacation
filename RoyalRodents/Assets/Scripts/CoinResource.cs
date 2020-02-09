using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinResource : MonoBehaviour
{
    int value = 1;
    bool active = false;

    private void Start()
    {
        StartCoroutine(PickUpDelay());
    }
    IEnumerator PickUpDelay()
    {

        yield return new WaitForSeconds(3.5f);
        active = true;
    }

    public bool isActive()
    {
        return active;
    }
}
