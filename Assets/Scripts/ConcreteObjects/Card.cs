using UnityEngine;
using System.Collections;

public class Card
{
    public readonly string Name;
    public readonly float[] Attributes;

    public Card(string name, params float[] attributes)
    {
        Name = name;
        Attributes = attributes;
    }
}

