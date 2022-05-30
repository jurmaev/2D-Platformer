using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public static MusicControl instance;

    private void Awake() 
    {
        DontDestroyOnLoad(gameObject); 

        if (instance == null) 
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}