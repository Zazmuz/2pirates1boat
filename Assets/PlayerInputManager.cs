using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance; // Singleton for global access

    public GameInformation gameInformation;
    private SharedDeviceInputManager sharedDeviceInputManager;

    public List<GameObject> players = new List<GameObject>(); // Store active players

    void Awake(){
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start(){
        sharedDeviceInputManager = GetComponent<SharedDeviceInputManager>();
    }
    void Update()
    {
        if(gameInformation.GetPlayers().Count == 2){
            SceneChanger.ChangeScene("Game");
        }
    }

    // ✅ Called automatically when a player joins
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        GameObject player = playerInput.gameObject;
        Debug.Log($"✅");
        if (!players.Contains(player))
        {
            players.Add(player);
            gameInformation.AddPlayer(player); // Store in gameInformation too

            Debug.Log($"✅ Player {playerInput.playerIndex} joined. Total players: {players.Count}");
        }
    }

    // ✅ Remove player if they leave
    public void OnPlayerLeft(PlayerInput playerInput)
    {
        GameObject playerToRemove = playerInput.gameObject;
        
        if (players.Contains(playerToRemove))
        {
            players.Remove(playerToRemove);
        }
    }

    // ✅ Manually trigger player joining (e.g., when pressing a UI button)
    public void AddPlayerManually()
    {
        sharedDeviceInputManager.JoinPlayer(); // This will trigger OnPlayerJoined
    }
}
