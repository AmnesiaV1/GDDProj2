using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    #region Animation_variables
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //gets the currDireciton of the player when they attack
        Vector2 currDirection = player.GetComponent<PlayerController>().direcitonAnim;

        float x = currDirection.x;
        float y = currDirection.y;

        Debug.Log(x);
        Debug.Log(y);

        //Set animator to the value of exactly when the player attacks
        anim.SetFloat("dirX", x);
        anim.SetFloat("dirY", y);

        //bat lives for 0.5 secs :(
        Destroy(this.gameObject, 0.25f);
    }
}
