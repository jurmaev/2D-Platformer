using UnityEngine;

public class MusicControl : MonoBehaviour
{
    private static MusicControl _instance;

    private void Awake() 
    {
        DontDestroyOnLoad(gameObject); 

        if (_instance == null) 
        {
            _instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}