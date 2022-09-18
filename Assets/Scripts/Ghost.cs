using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    #region Movement_variables
    [SerializeField]
    private float moveSpeedScaler;
    [SerializeField]
    private float moveSpeedCapacity;
    float moveSpeed;
    #endregion

    #region Attack_variables
    [SerializeField]
    private float attackRadius;
    #endregion

    #region Physics_components
    Rigidbody2D GhostRB;
    #endregion

    #region Targeting_variables
    [SerializeField]
    private GameObject player;
    #endregion

    #region Unity_functions
    private void Awake()
    {
        //grabs the ghost rigidbody
        GhostRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }
    #endregion

    #region Movement_functions
    //move towards the player
    private void Move()
    {
        //gets vector between ghost and player
        Vector2 direction = player.transform.position - transform.position;

        //movespeed decreases as the ghost gets closer to the player but maxs out at Capactiy.
        moveSpeed = Mathf.Max(direction.magnitude / moveSpeedScaler, moveSpeedCapacity);

        GhostRB.velocity = direction.normalized * moveSpeed;
    }
    #endregion

    #region Attack_functions
    /* private void Attack()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackRadius, Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                //function to end game (doesn't necessarily have to be in this script)
                EndGame();
            }
        }
    } */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            //function to end the game (doesn't necessarily have to be in this script)
            EndGame();
        }
    }
    #endregion

    #region Game_functions
    public void EndGame()
    {
        //Todo
        return;
    }
    #endregion
}
