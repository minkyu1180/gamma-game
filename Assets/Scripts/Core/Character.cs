using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string shortName;
    public string fullName;
    public Color color;
    public string sideImage;

    public Character(string shortNameInput, string fullNameInput, Color colorInput, string sideImageInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.sideImage = sideImageInput;

        checkNames();
    }

    public Character(string shortNameInput, string fullNameInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = Color.white;
        this.sideImage = null;

        checkNames();
    }

    public Character(string shortNameInput, string fullNameInput, Color colorInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.sideImage = null;

        checkNames();
    }

    public void checkNames()
    {
        if (this.fullName == null)
        {
            throw new InvalidPropertyException("Full Name must contain a string");
        }
        if (this.shortName == null)
        {
            throw new InvalidPropertyException("Full Name must contain a string");
        }
    }
}
 