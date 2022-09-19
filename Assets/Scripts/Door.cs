using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region Interact_functions
    //When player presses button to interact with door, it opens. Decrement player keycount
    public void Interact(GameObject player)
    {
        if (player.GetComponent<PlayerController>().keyCount > 0)
        {
            player.GetComponent<PlayerController>().keyCount -= 1;
            Destroy(this.gameObject);
        } else
        {
            Debug.Log("Missing key!");
        }
    }
    #endregion
}
