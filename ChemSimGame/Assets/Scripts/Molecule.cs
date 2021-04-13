using System;

public class Molecule
{
	private string name;
	private double dH;
	private double dS;
	private double dG;

	public Molecule(string name, double dH, double dS, double dG)
	{
		this.name = name;
		this.dH = dH;
		this.dS = dS;
		this.dG = dG;
	}

	public string get_name() { return name; }
	public double get_dH() { return dH; }
	public double get_dS() { return dS; }
	public double get_dG() { return dG; }
}
