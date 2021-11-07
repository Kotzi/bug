using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameController : MonoBehaviour
{
    public AntsNestController zone2AntsNestController;
    public AntsNestController zone3AntsNestController;
    public CinemachineVirtualCamera zone3Camera;
    private PlayerController playerController;
    private CharacterController2D characterController;

    void Awake()
    {
        playerController = Object.FindObjectOfType<PlayerController>();
        characterController = Object.FindObjectOfType<CharacterController2D>();
        characterController.OnLandEvent.AddListener(onPlayerLanded);
        characterController.OnJumpEvent.AddListener(onPlayerJump);
    }

    void onPlayerLanded()
    {
        zone3AntsNestController.changeAntsMode(false);
    }

    void onPlayerJump()
    {
        if (playerController.hasPlant())
        {
            zone3AntsNestController.changeAntsMode(true);
        }
    }
    
    public void changeAntsRoute(bool shouldBeOnGround)
    {
        zone3AntsNestController.changeAntsRoute(shouldBeOnGround);
    }

    public void onZone2Detected()
    {
        zone2AntsNestController.active = true;
    }

    public void onZone3Detected()
    {
        zone3Camera.Priority = 1000;
        zone2AntsNestController.active = false;
        zone3AntsNestController.active = true;
    }

    public void onZone4Detected()
    {
        zone3Camera.Priority = 1;
        zone3AntsNestController.active = false;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
