using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeInvisibeFin : MonoBehaviour
{

    public Renderer r;

    void Start()
    {
        r = GetComponent<Renderer>();
        r.enabled = false;
    }

    void Update()
    {
        
    }
}
