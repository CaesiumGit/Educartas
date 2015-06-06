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
        name.text = cardData.Name;
        size.text = createSizeText(cardData.Size);
        speed.text = cardData.Speed + " Km/h";
        litter.text = cardData.LitterNumber + "";
        lifespan.text = cardData.LifeSpan + " anos";

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

    private string createSizeText(float size)
    {
        string sizeText = "Tamanho: ";
        if (size >= 100)
        {
            sizeText += size / 100 + " m";
        }
        else
        {
            sizeText += size + " cm";
        }

        return sizeText;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
