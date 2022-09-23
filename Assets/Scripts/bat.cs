using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //bat lives for .25 secs
        Destroy(this.gameObject, 0.25f);
    }
}
