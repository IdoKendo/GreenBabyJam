using Assets.Scripts.Shared.Enums;
using Assets.Scripts.Shared.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance = null;
    public static Player_Manager PlayerManager = null;

    // Awake is always called before any Start functions
    public void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        PlayerManager = new Player_Manager();
    }

    public void NewGame()
    {

    }

    // Use this for initialization
    void Start ()
    {
        SceneManager.LoadScene((int)SceneType.LEVEL1);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
