using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public JSONManager jsonManager; // Reference to the JSONManager script

    private void Start()
    {
        // Ensure the JSONManager script is assigned
        if (jsonManager == null)
        {
            Debug.LogError("JSONManager reference is not set in SceneLoader script.");
            return;
        }

        // Call the UpdateLeaderboard() function from JSONManager
        jsonManager.UpdateLeaderboard();
    }
}
