using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    #region Interact_functions
    //If player collides with key, increment player key count (can be done in playercontroller)
    public void Interact()
    {
        //Todo: just skeleton, subject to change.
        Destroy(this.gameObject);
    }
    #endregion
}
