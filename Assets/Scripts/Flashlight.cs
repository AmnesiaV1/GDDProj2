using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;  //Should give access to light2d scripts

public class Flashlight : MonoBehaviour
{
    //Rotates light attached to this object based on player's direction
    #region Direction_variables
    [SerializeField]
    private float rotationSpeed;
    PlayerController player;
    #endregion

    #region Unity_functions
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        //Rotation script snippet obtained from this time-stamped video: https://youtu.be/gs7y2b0xthU?t=359
        if (player.currDirection != Vector2.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, player.currDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
    #endregion
}
