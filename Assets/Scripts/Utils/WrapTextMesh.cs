using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WrapTextMesh : MonoBehaviour
{
    public TextMesh Text;
    public float MaxWitdh;
    // Use this for initialization
    void Start()
    {
        //wrapText();pra caralho!";
        //Text.text = "TExto super \n grande 
        wrapText(Text, 0.455f, 0.045f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void wrapText(TextMesh textObject, float maxWidth, float maxHeight)
    {
        var textRenderer = textObject.GetComponent<Renderer>();
        var words = textObject.text.Trim().Split(' ');
        textObject.text = "";
        string lastString = "";
        Debug.Log(words.Length);
        for (var i = 0; i < words.Length; i++)
        {
            string nextWord = words[i] + " ";
            textObject.text += nextWord;
            var textSize = textRenderer.bounds.size.x;
            if (textSize > maxWidth)
            {
                textObject.text = lastString + "\n" + nextWord;
            }
            else
            {
                lastString = textObject.text;
            }
        }

        while (textRenderer.bounds.size.x > maxWidth)
        {
            textObject.fontSize--;
        }

        Debug.Log(textRenderer.bounds.size.y);
        while (textRenderer.bounds.size.y > maxHeight)
        {
            textObject.fontSize--;
        }

    }
}
