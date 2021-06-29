using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostersController : MonoBehaviour
{
    [SerializeField] private GameObject[] boostersSerialization;

    // static variables
    public static float chanceOnBooster = 20f;
    private static float boosterDuration = 15f; // seconds
    public static GameObject[] boosters;
    private static Image[] boostersImage;
    private static CharacterAnimatorController _animatorController;

    //enum order: damage, speed, ore, .....(extra can be added)
    public enum BoosterTypes
    {
        damage = 0,
        speed = 1,
        ore = 2,
    }

    private void Awake()
    {
        _animatorController = FindObjectOfType<CharacterAnimatorController>();

        boosters = boostersSerialization;
        boostersImage = new Image[boosters.Length];
    }

    private void Update()
    {
        for (var index = 0; index < boosters.Length; index++)
        {
            if (!boostersImage[index])
                continue;
            
            if (boostersImage[index].fillAmount > 0)
                boostersImage[index].fillAmount -= Time.deltaTime / boosterDuration;
            else
            {
                boostersImage[index] = null;
                boosters[index].SetActive(false);
                    
                if (index == (int)BoosterTypes.speed)
                    _animatorController.DecreaseAnimationSpeed();
            }
        }
    }

    public static void GetBooster(BoosterTypes boosterType)
    {
        var index = (int) boosterType;

        boosters[index].SetActive(true);
        boostersImage[index] = (boosters[index].GetComponent<Image>());
        boostersImage[index].fillAmount = 1f;
        
        if (boosterType == BoosterTypes.speed)
            _animatorController.IncreaseAnimationSpeed(1.5f);
    }
}