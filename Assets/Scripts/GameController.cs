using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private AntsNestController antsNestController;
    private PlayerController playerController;
    private CharacterController2D characterController;

    void Awake()
    {
        antsNestController = Object.FindObjectOfType<AntsNestController>();
        playerController = Object.FindObjectOfType<PlayerController>();
        characterController = Object.FindObjectOfType<CharacterController2D>();
        characterController.OnLandEvent.AddListener(onPlayerLanded);
        characterController.OnJumpEvent.AddListener(onPlayerJump);
    }

    void onPlayerLanded()
    {
        antsNestController.changeAntsMode(false);
    }

    void onPlayerJump()
    {
        if (playerController.hasPlant())
        {
            antsNestController.changeAntsMode(true);
        }
    }

    public void changeAntsRoute(bool shouldBeOnGround)
    {
        antsNestController.changeAntsRoute(shouldBeOnGround);
    }
}
