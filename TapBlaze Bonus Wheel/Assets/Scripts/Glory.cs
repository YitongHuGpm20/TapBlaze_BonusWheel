using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).transform.Rotate(0, 0, 0.1f);
        transform.GetChild(1).transform.Rotate(0, 0, -0.1f);
    }
}
