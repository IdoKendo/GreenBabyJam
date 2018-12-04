using Assets.Scripts.Shared.Enums;
using Assets.Scripts.Shared.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance = null;
    public static Player_Manager playerManager = null;
    public static Sound_Manager soundManager = null;

    // Awake is always called before any Start functions
    public void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        playerManager = new Player_Manager();
    }

    public void NewGame()
    {

    }

    // Use this for initialization
    void Start ()
    {
        SceneManager.LoadScene((int)SceneType.MAIN_MENU);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
