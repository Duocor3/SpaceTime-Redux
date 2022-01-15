using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private int[] shipChoice = { 0, 0, 0 };

    public int[] ShipChoice()
    {

        shipChoice[0] = ButtonPressLoop.currentImage[0];
        shipChoice[1] = ButtonPressLoop.currentImage[2];
        shipChoice[2] = ButtonPressLoop.currentImage[1];

        return shipChoice;
    }
}
