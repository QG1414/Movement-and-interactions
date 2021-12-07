using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellObject : MonoBehaviour
{
    public float SmellTime = 30f;
    private void Update()
    {
        SmellTime -= Time.deltaTime;
        if (SmellTime <= 0)
            Destroy(gameObject);
    }



}
