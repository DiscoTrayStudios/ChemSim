using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator
{

    // H = Enthalpy, E = Sum of internal energy, P = Pressure, and V = Volume
    // Enthalpy Equation: H = E + PV
    // Change in Enthalpy: deltaH = deltaE + P(deltaV)

    public Calculator() {}

    public double[] CalculateChanges(Dictionary<string, int> inputNames, List<GameObject> output)
    {
        double totalInputH = 0;
        double totalInputG = 0;
        double totalInputS = 0;
        double totalOutputH = 0;
        double totalOutputG = 0;
        double totalOutputS = 0;
        foreach (string name in inputNames.Keys)
        {
            for (int i=0;i< inputNames[name];i++)
            {
                Debug.Log(name);
                totalInputH += GameManager.Instance.getMoleculedH(name);
                totalInputG += GameManager.Instance.getMoleculedG(name);
                totalInputS += GameManager.Instance.getMoleculedS(name);
                Debug.Log(totalInputH);
            }
        }
        foreach (GameObject g in output)
        {
            Debug.Log(g);
            totalOutputH += GameManager.Instance.getMoleculedH(g.name);
            totalOutputG += GameManager.Instance.getMoleculedG(g.name);
            totalOutputS += GameManager.Instance.getMoleculedS(g.name);
        }
        double dH = Math.Round(totalOutputH - totalInputH, 2);
        double dG = Math.Round(totalOutputG - totalInputG, 2);
        double dS = Math.Round(totalOutputS - totalInputS, 2);
        return new double[3] { dH, dG, dS };
    }
}
