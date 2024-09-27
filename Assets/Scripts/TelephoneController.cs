using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelephoneController : MonoBehaviour
{
    public float moveSpeed = 3;
    public GameObject Player;
    public static bool isPanningEnabled=false;
    
    private void Update()
    {
        //Connect with telephone hereeeeee
        if (isPanningEnabled) 
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");
            Vector3 newPosition = Player.transform.position + new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;

            // Apply constraintsss hereeeee


            Player.transform.position = newPosition;
        }


    }

}
