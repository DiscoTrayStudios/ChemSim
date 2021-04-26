using System;
using System.Collections.Generic;

public class MoleculeValuesTable
{
    private Dictionary<string, Molecule> reactantValues;
    private Molecule H2OL = new Molecule("H2O (L)", -285.8, 69.9, -237.2);
    private Molecule H2OG = new Molecule("H2O (G)", -241.8, 188.8, -228.6);
    private Molecule CO2 = new Molecule("CO2", -393.5, 213.8, -394.4);
    private Molecule HCl = new Molecule("HCl", -92.3, 186.9, -95.3);
    private Molecule NaOH = new Molecule("NaOH", -425.6, 64.5, -379.5);
    private Molecule NH3 = new Molecule("NH3", -46.1, 192.5, -16.5);
    private Molecule NH4 = new Molecule("NH4+", -132.5, 113.4, -79.3);
    private Molecule OH = new Molecule("OH-", -229.9, -10.5, -157.3);
    private Molecule H2SO4 = new Molecule("H2SO4", -909.3, 20.1, -744.5);
    private Molecule Na2SO4Aq = new Molecule("Na2SO4 (Aq)", -1389.5, 138.1, -1268.4);
    private Molecule Na2SO4S = new Molecule("Na2SO4 (S)", -1384.5, 149.5, -1266.83);
    private Molecule Na = new Molecule("Na+", -240.1, 59.0, -261.9);
    private Molecule SO4 = new Molecule("SO42-", -909.3, 20.1, -744.6);
    private Molecule N2 = new Molecule("N2", 0, 191.5, 0);
    private Molecule H2 = new Molecule("H2", 0, 130.6, 0);

    public MoleculeValuesTable()
    {
        reactantValues = new Dictionary<string, Molecule>();
        reactantValues.Add("H2O (L)", H2OL);
        reactantValues.Add("H2O (G)", H2OG);
        reactantValues.Add("CO2", CO2);
        reactantValues.Add("HCl", HCl);
        reactantValues.Add("NaOH", NaOH);
        reactantValues.Add("NH3", NH3);
        reactantValues.Add("NH4+", NH4);
        reactantValues.Add("OH-", OH);
        reactantValues.Add("H2SO4", H2SO4);
        reactantValues.Add("Na2SO4 (Aq)", Na2SO4Aq);
        reactantValues.Add("Na2SO4 (S)", Na2SO4S);
        reactantValues.Add("Na+", Na);
        reactantValues.Add("SO42-", SO4);
        reactantValues.Add("N2", N2);
        reactantValues.Add("H2", H2);
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
}
