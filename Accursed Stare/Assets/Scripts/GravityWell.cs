using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityWell : GravityObject {



    private int energyCost;
    public void setEnergyCost(int cost)
    {
        energyCost = cost;
    }
    public int getEnergyCost(int cost)
    {
        return energyCost;
    }


}
