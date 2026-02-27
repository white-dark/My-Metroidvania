using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : Entity_Combat
{
    public bool PerformCounterAttack()
    {
        foreach(var tar in GetDetectedColliders())
        {
            ICounterable counterable = tar.GetComponent<ICounterable>();

            if(counterable != null)
            {
                if (counterable.CanBeCountered())
                {
                    counterable.HandleCounter();
                    return true;
                }
            }
        }
        return false;
    }
}
