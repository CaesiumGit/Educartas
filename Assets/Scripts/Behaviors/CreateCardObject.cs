using UnityEngine;
using System.Collections;

public class CreateCardObject : MonoBehaviour
{
    public GameObject DefaultCard;

    // Use this for initialization
    void Start()
    {
        createCards();
    }

    private void createCards()
    {
        var cardsData = CardDatabase.GetCards();

        for (int i = 0; i < cardsData.Length; i++)
        {
            var cardData = cardsData[i];

            setCard(cardData);
        }
    }

    private GameObject setCard(AnimalCardData cardData)
    {
        var cardObject = Instantiate(DefaultCard, Vector3.zero, DefaultCard.transform.rotation) as GameObject;

        cardObject.name = cardData.ScientificName;
        Transform textChild = cardObject.transform.FindChild("text");
        //configura os textos
        TextMesh scientificName = textChild.FindChild("scientific_name").GetComponent<TextMesh>();
        TextMesh name = textChild.FindChild("name").GetComponent<TextMesh>();
        TextMesh size = textChild.FindChild("size").GetComponent<TextMesh>();
        TextMesh speed = textChild.FindChild("speed").GetComponent<TextMesh>();
        TextMesh litter = textChild.FindChild("litter").GetComponent<TextMesh>();
        TextMesh lifespan = textChild.FindChild("lifespan").GetComponent<TextMesh>();

        scientificName.text = cardData.ScientificName;
        wrapText(scientificName, 0.455f, 0.1f);
        name.text = cardData.Name;
        wrapText(name, 0.6f, 0.15f);
        float width = 0.45f;
        size.text = "Tamanho: " + createSizeText(cardData.Size);
        wrapAttributeText(size, width);
        speed.text = "Velocidade: " + cardData.Speed + " Km/h";
        wrapAttributeText(speed, width);
        litter.text = "Ninhada: " + cardData.LitterNumber + "";
        wrapAttributeText(litter, width);
        lifespan.text = "Tempo de vida: " + cardData.LifeSpan + " anos";
        wrapAttributeText(lifespan, width);

        //configura sprites
        Transform spriteChild = cardObject.transform.FindChild("sprites");

        SpriteRenderer bg = spriteChild.transform.FindChild("bg").GetComponent<SpriteRenderer>();
        SpriteRenderer animalImage = spriteChild.transform.FindChild("animal_image").GetComponent<SpriteRenderer>();
        SpriteRenderer terrainSymbol = spriteChild.transform.FindChild("terrain_symbol").GetComponent<SpriteRenderer>();

        bg.sprite = cardData.ClassBG;
        animalImage.sprite = cardData.AnimalImage;
        terrainSymbol.sprite = cardData.TerrainSymbol;

        return cardObject;
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

    private void wrapAttributeText(TextMesh textObject, float maxWidth)
    {
        var textRenderer = textObject.GetComponent<Renderer>();
        while (textRenderer.bounds.size.x > maxWidth)
        {
            textObject.fontSize--;
        }
    }

    private string createSizeText(float size)
    {
        string sizeText = "";
        if (size >= 100)
        {
            sizeText += System.Math.Round(size / 100, 1) + " m";
        }
        else
        {
            sizeText += size + " cm";
        }

        return sizeText;
    }

}
