using Shared.Enums;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private GameObject healthBar;
    [SerializeField] private Image bar;
   
    private Player player;

    private const float healthBarXPosition = -0.1f;
    private const float healthBarYPosition = 1.32f;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();
        healthBar = GameObject.Find(GameObjectType.HealthBar);
      //  HealthBarPosition();
    }

    private void HealthBarPosition()
    {
        RectTransform healthBarRect = healthBar.transform as RectTransform;
        healthBarRect.anchoredPosition = Vector2.zero;
        healthBarRect.sizeDelta = Vector2.zero;
        healthBarRect.anchorMax = new Vector2(healthBarXPosition, healthBarYPosition);
    }

    // Update is called once per frame
    void Update ()
    {
        ChangeHealt();
    }

    public void ChangeHealt()
    {
        float normilizedHealth = player.Health / player.MaxHealth;

        normilizedHealth = Mathf.Clamp(normilizedHealth, 0, 1);
        bar.fillAmount = normilizedHealth;
    }
}
