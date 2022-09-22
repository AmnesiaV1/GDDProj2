using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //bat lives for 0.5 secs :(
        Destroy(this.gameObject, 0.25f);
    }
}
