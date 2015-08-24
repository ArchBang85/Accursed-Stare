using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityWell : GravityObject {
    private int startingCost = 200;
    private int energyCost = 2;
    public void setEnergyCost(int cost)
    {
        energyCost = cost;
    }
    public int getEnergyCost()
    {
        return energyCost;
    }

    public int getStartingCost()
    {
        return startingCost;
    }






    // on mouse click destroy



}
