using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    #region Interact_functions
    public void Interact(GameObject player)
    {
        player.GetComponent<PlayerController>().keyCount += 1;
        Destroy(this.gameObject);
    }
    #endregion
}
