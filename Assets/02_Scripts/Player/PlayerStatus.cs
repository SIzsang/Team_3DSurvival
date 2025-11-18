using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus
{
    public float MoveSpeed;
    private float moveSpeed;

    public PlayerStatus(PlayerStatusData data)
    {
        
    }


    IEnumerator NaturalRecovery()
    {
        yield return new WaitForSeconds(1f);
    }
    
}
