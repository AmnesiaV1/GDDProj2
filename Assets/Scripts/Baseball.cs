using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baseball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            //Todo: set up game manager.
            //GameObject gm = GameObject.FindWithTag("GameController");
            //gm.GetComponent<GameManager>().WinGame();
            Debug.Log("Victory!");
        }
    }
}