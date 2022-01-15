using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour {

    // declares variables
    public string ActionName;

    public CanvasGroup canvasGroup;
    public Button button;
    public Text header;
    public Text text;

    public bool isCredits;

    private bool isActive;

	// Use this for initialization
	void Start () {
        // couldn't figure out how to use the UI to add a script to a button
        button.onClick.AddListener(RunMethod);
    }

    void RunMethod()
    {
        if (ActionName == "Play Game")
        {
            // gets the player choice values - well, not anymore
            
            // loads the game scene for the play button
            SceneManager.LoadScene("StrongholdMap");
        }
        else if (ActionName == "Help Screen Border")
        {
            // enables or disables all the images like a toggle
            if (canvasGroup.alpha == 0f)
            {
                isActive = true;
                canvasGroup.alpha = 1f;
            }
            else
            {
                isActive = false;
                canvasGroup.alpha = 0f;
            }

            if (isCredits)
            {
                header.text = "Credits";
                text.text = "Inspired from Spacetime v3.37 on Scratch\n" +
                            "Music: Chiptune Loop 36 by Mnarg\n" +
                            "Everything else is made by me :)";
            } else
            {
                header.text = "Help";
                text.text = "Player One moves using WASD\n" +
                            "Player Two moves using IJKL/Arrow Keys\n" +
                            "Player One fires its weapon using C and uses its ability with V\n" +
                            "Player Two fires its weapon using O and uses its ability with P";
            }

            canvasGroup.blocksRaycasts = isActive;
        }
    }
}