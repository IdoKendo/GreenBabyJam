using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    // Awake is always called before any Start functions
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void NewGame()
    {

    }

    // Use this for initialization
    void Start ()
    {
        SceneManager.LoadScene(1);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
