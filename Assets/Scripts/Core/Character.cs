using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string shortName;
    public string fullName;
    public Color color;
    public string emotion;
    public float textSpeed;
    public AudioClip voice;

    public Character(string shortNameInput, string fullNameInput, Color colorInput, string emotionInput, float textSpeed, string voice)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.emotion = emotionInput;
        this.textSpeed = textSpeed;
        this.voice = Resources.Load("Sound/Voice/" + voice) as AudioClip;
        checkNames();
    }

    public Character(string shortNameInput, string fullNameInput, Color colorInput, string emotionInput, float textSpeed)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.emotion = emotionInput;
        this.textSpeed = textSpeed;
        checkNames();
    }

    public Character(string shortNameInput, string fullNameInput, Color colorInput, string emotionInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.emotion = emotionInput;
        this.textSpeed = 0.04f;
        
        checkNames();
    }

    public Character(string shortNameInput, string fullNameInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = Color.white;
        this.emotion = "default";
        this.textSpeed = 0.04f;

        checkNames();
    }

    public Character(string shortNameInput, string fullNameInput, Color colorInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.emotion = "default";
        this.textSpeed = 0.06f;

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
 