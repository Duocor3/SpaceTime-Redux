using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressLoop : MonoBehaviour {

    // declares variables and such
    public string filePath;
    public bool nextOrPrev;

    public Button nextButton;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Text shipName;
    public static int[] currentImage = { 0, 0, 0 };

    private string[] images = new string[10];
    private int whichImage;
    private int max;
    private int min;
    private int increment;
    private int numberOfImages;

    private Image mImage;
    private Sprite spriteShown;

    // Use this for initialization
    void Start () {
        // decides which value of currentimage to use
        if (filePath == @"Icons\PlayerOneLoop\")
        {
            whichImage = 0;
        } else if (filePath == @"Icons\PlayerTwoLoop\")
        {
            whichImage = 1;
        } else
        {
            whichImage = 2;
        }

        // gets all the images inside of "Icons"
        DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Tsegazeab\Desktop\Coding Projects\Spacetime Redux\Assets\Resources\" + filePath);
        FileInfo[] info = dir.GetFiles("*.*");

        // attaches a method to a button for when it's pressed
        nextButton.onClick.AddListener(changeImage);

        // gets the image from the game object
        mImage = GetComponent<Image>();

        // pushes the image file names into the array
        foreach (FileInfo f in info)
        {
            if (f.Extension == ".png") {
                images[currentImage[whichImage]] = f.Name.Substring(0, f.Name.Length - 4);
                currentImage[whichImage]++;
            }
        }

        // resets currentImage and sets the max and min of the number of files
        numberOfImages = currentImage[whichImage] - 1;
        currentImage[whichImage] = 0;
        max = numberOfImages;
        min = 0;
        increment = 1;
    }

    // uses the currentImage variable to display the right image
    void changeImage()
    {
        // checks to see if it should switch forward or backward
        if (nextOrPrev) {
            max = numberOfImages;
            min = 0;
            increment = 1;
        } else {
            max = 0;
            min = numberOfImages;
            increment = -1;
        }

        // adds 1 to currentImage, resets if it hits max
        if (currentImage[whichImage] == max)
        {
            currentImage[whichImage] = min;
        } else {
            currentImage[whichImage] = currentImage[whichImage] + increment;
        }

        // sets the name of the ship
        // this is a bad way to do it, but im tired and lazy
        if (whichImage != 2)
        {
            if (currentImage[whichImage] == 0)
            {
                shipName.text = "Random";
            }
            else if (currentImage[whichImage] == 1)
            {
                shipName.text = "Inferno<size=9><color=#A0C7D9>\nClass: Cruiser\nWeapon: Laser\nAbility: Time Stop\n</color></size>";
            }
            else if (currentImage[whichImage] == 2)
            {
                shipName.text = "Spectre<size=9><color=#A0C7D9>\nClass: Fighter\nWeapon: Railgun\nAbility: Force Field\n</color></size>";
            }
            else if (currentImage[whichImage] == 3)
            {
                shipName.text = "Colossus<size=9><color=#A0C7D9>\nClass: Dreadnought\nWeapon: Minigun\nAbility: Ghost\n</color></size>";
            }
            else if (currentImage[whichImage] == 4)
            {
                shipName.text = "Ballistic<size=9><color=#A0C7D9>\nClass: Battleship\nWeapon: Homing Missles\nAbility: Repulse\n</color></size>";
            }
            else
            {
                shipName.text = "Blaster<size=9><color=#A0C7D9>\nClass: Frigate\nWeapon: Shotgun\nAbility: Teleport\n</color></size>";
            }
        } else
        {
            if (currentImage[whichImage] == 0)
            {
                shipName.text = "Random";
            } else if (currentImage[whichImage] == 1)
            {
                shipName.text = "<size=13>Stronghold</size>";
            } else if (currentImage[whichImage] == 2)
            {
                shipName.text = "<size=13>Redline</size>";
            } else if (currentImage[whichImage] == 3)
            {
                shipName.text = "<size=13>Borg</size>";
            }
            else if (currentImage[whichImage] == 4)
            {
                shipName.text = "<size=13>Potshot</size>";
            }
            else if (currentImage[whichImage] == 5)
            {
                shipName.text = "<size=13>Nexus</size>";
            }
        }

        // plays the button sound
        audioSource.clip = audioClip;
        audioSource.Play();

        //changes image based on what the current image is
        mImage.sprite = Resources.Load<Sprite>(@filePath + images[currentImage[whichImage]]);
    }
}
