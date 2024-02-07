using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipImageSelector : MonoBehaviour
{
    [SerializeField] private Sprite[] shipColors;
    private int shipColorInt = 0;

    [SerializeField] private Image shipImage;

    private void Start()
    {
        shipColorInt = PlayerPrefs.GetInt("ShipColorInt", 0);
        shipImage.sprite = shipColors[shipColorInt];
    }

    public void NextShip()
    {
        shipColorInt = (shipColorInt + 1) % shipColors.Length;
        SetShipColor();
    }

    public void PreviousShip()
    {
        shipColorInt--;
        if (shipColorInt < 0)
        {
            shipColorInt = shipColors.Length - 1;
        }
        SetShipColor();
    }

    private void SetShipColor()
    {
        shipImage.sprite = shipColors[shipColorInt];
        PlayerPrefs.SetInt("ShipColorInt", shipColorInt);
    }
}
