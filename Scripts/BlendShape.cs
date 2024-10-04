using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShape
{
    private string name;
    private int id;

    public BlendShape(string name, int id) { 
        this.name = name;
        this.id = id;
    }

    public string Name() { 
        return name;
    }

    public int Id() { 
        return id;
    }
}
