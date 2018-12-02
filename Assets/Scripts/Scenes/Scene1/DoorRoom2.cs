using Assets.Scripts.Shared.Enums;
using Shared.Enums;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.Scene1
{
    public class DoorRoom2 : MonoBehaviour
    {
        public Animator transitionAnimation;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameCollisionType.PLAYER))
                StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            transitionAnimation.SetTrigger(AnimationTriggerType.SCREEN_FADE_END);
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene((int)SceneType.LEVEL2);
        }
    }
}
