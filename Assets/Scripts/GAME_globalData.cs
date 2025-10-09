using UnityEngine;

public class GAME_globalData : MonoBehaviour
{
    public static GAME_globalData instance;
    public bool quickGameTransition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
