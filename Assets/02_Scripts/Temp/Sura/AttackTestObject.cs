using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTestObject : MonoBehaviour,ICombatable
{

    public void TakePhysicalDamage( int damage )
    {
        Debug.Log("아야! " + damage);
    }
}
