using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region Interact_functions
    //When player presses button to interact with door, it opens. Decrement player keycount
    public void Interact()
    {
        Destroy(this.gameObject);
    }
    #endregion
}
