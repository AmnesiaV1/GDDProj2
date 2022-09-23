using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region Animation_variables
    [SerializeField]
    private GameObject openDoorAnimation;
    #endregion

    #region Interact_functions
    //When player presses button to interact with door, it opens. Decrement player keycount
    public void Interact(GameObject player)
    {
        if (player.GetComponent<PlayerController>().keyCount > 0)
        {
            player.GetComponent<PlayerController>().keyCount -= 1;
            StartCoroutine(OpenDoor());
        } else
        {
            Debug.Log("Missing key!");
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.1f);
        //Spawn open door, hide closed door
        Destroy(this.GetComponent<BoxCollider2D>());
        GameObject openDoor = Instantiate(openDoorAnimation, new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), Quaternion.identity);
        GetComponent<Renderer>().enabled = false;

        //Delete both items
        yield return new WaitForSeconds(1f);
        Destroy(openDoor);
        Destroy(this.gameObject);
    }
    #endregion
}
