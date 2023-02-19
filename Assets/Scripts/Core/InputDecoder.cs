using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using TMPro;

public class InputDecoder
{
    public static List<Character> CharacterList = new List<Character> ();

    public static GameObject InterfaceElements = GameObject.Find("UI_Elements");
    private static GameObject Background = GameObject.Find("Background");
    private static Image BackgroundImage = Background.GetComponent<Image>();
    public static GameObject Portrait = GameObject.Find("Portrait");
    private static Image PortraitImage = Portrait.GetComponent<Image>();
    public static GameObject DialogBoxTextObject = GameObject.Find("DialogBoxText");
    public static GameObject NamePlateTextObject = GameObject.Find("NamePlateText");
    public static bool dialogMode = false;

    public static void ParseInputLine(string stringToParse)
    {
        string withOutTabs = stringToParse.Replace("\t", "");
        stringToParse = withOutTabs;

        if (stringToParse.StartsWith("\""))
        {
            Say(stringToParse);
        }
        
        string[] seperateString = { " ", "'", "\"", "(", ")"};
        string[] args = stringToParse.Split(seperateString, StringSplitOptions.RemoveEmptyEntries);
        foreach (Character character in CharacterList)
        {
            if (args[0] == character.shortName)
            {
                SplitToSay(stringToParse, character);
            }
        }

        if (args[0] == "show")
        {
            showImage(stringToParse);
        }

        if (args[0] == "clrscr")
        {
            ClearScene();
        }

        if (args[0] == "Character")
        {
            CreateNewCharacter(stringToParse);
        }
        if (args[0] == "ChangeEmotion")
        {
            foreach (Character character in CharacterList)
            {
                if (args[1] == character.shortName)
                {
                    character.sideImage = args[2];
                }
            }
        }
    }

    #region Say Stuff

    public static void SplitToSay (string stringToParse, Character character)
    {
        int toQuote = stringToParse.IndexOf("\"") + 1;
        int endQuote = stringToParse.Length - 1;
        string stringToOutput = stringToParse.Substring(toQuote, endQuote - toQuote);
        Say(character, stringToOutput, character.sideImage);
    }

    public static void Say(string what)
    {
        if (!InterfaceElements.activeInHierarchy) InterfaceElements.SetActive(true);
        //DialogBoxTextObject.GetComponent<TextMeshProUGUI>().text = what;
        dialogMode = true;
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().PutDialog(what);
    }

    public static void Say(Character character, string what, string sideImage)
    {
        if (!InterfaceElements.activeInHierarchy) InterfaceElements.SetActive(true);
        //DialogBoxTextObject.GetComponent<TextMeshProUGUI>().text = what;
        dialogMode = true;
        DialogBoxTextObject.GetComponent<DialogBoxTextTyper>().PutDialog(what);
        NamePlateTextObject.GetComponent<TextMeshProUGUI>().text = character.fullName;
        PortraitImage.sprite = Resources.Load<Sprite> ("images/" + character.shortName + '/' + sideImage);
    }

    

    #endregion

    #region Image Stuff

    public static void showImage(string stringToParse)
    {
        var imageToUse = new Regex(@"show (?<ImageFileName>[^.]+)");
        var matches = imageToUse.Match(stringToParse);
        string imageToShow = matches.Groups["ImageFileName"].ToString();

        BackgroundImage.sprite = Resources.Load<Sprite>("images/" + imageToShow);
    }

    public static void ClearScene()
    {
        BackgroundImage.sprite = null;
    }
    #endregion

    #region New Character

    public static void CreateNewCharacter(string stringToParse)
    {
        string newCharShortName = null;
        string newCharFullName = null;
        Color newCharColor = Color.white;
        string newCharSideImage = null;


        var characterExpression = new Regex(@"Character\((?<shortName>[a-zA-Zㄱ-ㅎ가-힣0-9_]+), (?<fullName>[a-zA-Zㄱ-ㅎ가-힣0-9_]+), color=(?<characterColor>[a-zA-Zㄱ-ㅎ가-힣0-9_]+), image=(?<sideImage>[a-zA-Zㄱ-ㅎ가-힣0-9_]+)\)");
        if (characterExpression.IsMatch(stringToParse))
        {
            var matches = characterExpression.Match(stringToParse);
            newCharShortName = matches.Groups["shortName"].ToString();
            newCharFullName = matches.Groups["fullName"].ToString();
            newCharColor = Color.clear; ColorUtility.TryParseHtmlString(matches.Groups["characterColor"].ToString(), out newCharColor);
            newCharSideImage = matches.Groups["sideImage"].ToString();
        }


        CharacterList.Add(new Character(newCharShortName, newCharFullName, newCharColor, newCharSideImage));
    }
    #endregion
}
