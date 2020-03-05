using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;

    // create list of stat modifyers
    //for example, if this damage state, then modifyer can change this stat, buff or nerf
    private List<int> modifyers = new List<int>();

    public int GetValue()
    {
        // calculate all modifier to our stat
        int finalValue = baseValue;
        //lambda for quik loop of all modifier and add it to final
        modifyers.ForEach(x => finalValue += x);

        return finalValue;
    }

    public void AddModifier(int modifier) {
        if (modifier != 0)
            modifyers.Add(modifier);
    }

    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
            modifyers.Remove(modifier);
    }


}
