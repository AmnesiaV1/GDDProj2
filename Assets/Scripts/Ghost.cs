using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    #region Movement_variables
    [SerializeField]
    [Tooltip("The distance from the player when the ghosts starts speeding up.")]
    private float speedUpDistance;
    [SerializeField]
    [Tooltip("Scale factor that determins how much the ghost will speed up when close enough to the player.")]
    private float moveSpeedScaler;
    [SerializeField]
    [Tooltip("Constant speed outside of range of player")]
    private float moveSpeedOutside;
    [SerializeField]
    private float maxSpeed;
    float moveSpeed;
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

        if (direction.magnitude > speedUpDistance)
        {
            //if the ghost is outside of the speed up radius, then the ghost has a constant speed.
            moveSpeed = moveSpeedOutside;
        }
        else
        {
            //As the ghost gets close, its speed will increase.
            moveSpeed = Mathf.Min(maxSpeed, 1 / (direction.magnitude / moveSpeedScaler));
        }

        GhostRB.velocity = direction.normalized * moveSpeed;
    }
    #endregion

    #region Attack_functions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<GameManager>().GameOver();
        }
    }
    #endregion
}
