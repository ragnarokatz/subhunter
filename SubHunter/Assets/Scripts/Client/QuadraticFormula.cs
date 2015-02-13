using System;

public class QuadraticFormula
{
    private float a;
    private float b;
    private float c;

    public QuadraticFormula(float a)
    {
        this.a = a;
    }

    public QuadraticFormula(float a, float b)
    {
        this.a = a;
        this.b = b;
    }

    public QuadraticFormula(float a, float b, float c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public float CalculateY(float x)
    {
        return (float) (a * Math.Pow((double)x, 2) + b * x + c);
    }
}