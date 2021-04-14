using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{

    // H = Enthalpy, E = Sum of internal energy, P = Pressure, and V = Volume
    // Enthalpy Equation: H = E + PV
    // Change in Enthalpy: deltaH = deltaE + P(deltaV)

    public int deltaE;
    public int P;
    public int deltaV;

    int calcEnthalpy()
    {
        int deltaH = deltaE + P * (deltaV);
        return deltaH;
    }

}
