using UnityEngine;

public class PlayerIDManager : MonoBehaviour
{
    [SerializeField] private string playerID = "Player_0001";  // Default Player id

    private static string storedPlayerID;  

    void Awake()
    {
        storedPlayerID = playerID;
        Debug.Log("Player ID set to: " + storedPlayerID);
    }

    public static string GetPlayerID()
    {
        return storedPlayerID;
    }
}
