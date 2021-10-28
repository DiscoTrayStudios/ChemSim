using System;
using System.Collections.Generic;


// This holds all the information regarding reactants and their values 
public class MoleculeValuesTable
{
    private Dictionary<string, Molecule> reactantValues;
    private Molecule H2OL = new Molecule("H<sub>2</sub>O(l)", -285.8, 69.9, -237.1);
    private Molecule H2OG = new Molecule("H<sub>2</sub>O(g)", -241.8, 188.8, -228.6);
    private Molecule CO2 = new Molecule("CO<sub>2</sub>", -393.5, 213.8, -394.4);
    private Molecule HCl = new Molecule("HCl", -167.5, -131.2, 53.06);
    private Molecule NaOH = new Molecule("NaOH", -425.6, 64.5, -379.5);
    private Molecule NH3 = new Molecule("NH<sub>3</sub>", -46.1, 192.5, -16.5);
    private Molecule NH4 = new Molecule("NH<sub>2</sub><sup>+</sup>", -132.5, 113.4, -79.3);
    private Molecule OH = new Molecule("OH<sup>-</sup>", -229.9, -10.5, -157.3);
    private Molecule H2SO4 = new Molecule("H<sub>2</sub>SO<sub>4</sub>", -909.3, 20.1, -744.5);
    private Molecule Na2SO4Aq = new Molecule("Na<sub>2</sub>SO<sub>4</sub>(aq)", -1389.5, 138.1, -1268.4);
    private Molecule Na2SO4S = new Molecule("Na<sub>2</sub>SO<sub>4</sub>(s)", -1384.5, 149.5, -1266.83);
    private Molecule Na = new Molecule("Na<sup>+</sup>", -240.1, 59.0, -261.9);
    private Molecule SO4 = new Molecule("SO<sub>4</sub><sup>2-</sup>", -909.3, 20.1, -744.6);
    private Molecule N2 = new Molecule("N<sub>2</sub>", 0, 191.5, 0);
    private Molecule H2 = new Molecule("H<sub>2</sub>", 0, 130.6, 0);
    private Molecule NaCl = new Molecule("NaCl", -407.3, 115.5, -393.1);
    private Molecule H2CO3 = new Molecule("H<sub>2</sub>CO<sub>3</sub>", -677.14, -56.9, -527.9);

    public MoleculeValuesTable()
    {
        reactantValues = new Dictionary<string, Molecule>();
        reactantValues.Add("H<sub>2</sub>O(l)", H2OL);
        reactantValues.Add("H<sub>2</sub>O(g)", H2OG);
        reactantValues.Add("CO<sub>2</sub>", CO2);
        reactantValues.Add("HCl", HCl);
        reactantValues.Add("H<sub>2</sub>SO<sub>4</sub>", H2SO4);
        reactantValues.Add("NaOH", NaOH);
        reactantValues.Add("NH<sub>3</sub>", NH3);
        reactantValues.Add("N<sub>2</sub>", N2);
        reactantValues.Add("Na<sub>2</sub>SO<sub>4</sub>(aq)", Na2SO4Aq);
        reactantValues.Add("Na<sub>2</sub>SO<sub>4</sub>(s)", Na2SO4S);
        reactantValues.Add("H<sub>2</sub>", H2);
        reactantValues.Add("NH<sub>2</sub><sup>+</sup>", NH4);
        reactantValues.Add("OH<sup>-</sup>", OH);
        reactantValues.Add("Na<sup>+</sup>", Na);
        reactantValues.Add("SO<sub>4</sub><sup>2-</sup>", SO4);
        reactantValues.Add("NaCl", NaCl);
        reactantValues.Add("H<sub>2</sub>CO<sub>3</sub>", H2CO3);
    }
    public double getMoleculedH(string name) {
        try
        {
            return reactantValues[name].get_dH();
        } catch (KeyNotFoundException) {
            return 0;
        }
    }
    public double getMoleculedS(string name) {
        try
        {
            return reactantValues[name].get_dS();
        }
        catch (KeyNotFoundException)
        {
            return 0;
        }
    }
    public double getMoleculedG(string name) {
        try
        {
            return reactantValues[name].get_dG();
        } catch (KeyNotFoundException) 
        {
            return 0;
        }
    }

    public Dictionary<string, Molecule> getReactionValues()
    {
        return reactantValues;
    }
}
