﻿using Assets.Scripts.Shared.Enums;
using Shared.Enums;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Scenes.Scene1
{
    public class DoorRoom1 : MonoBehaviour
    {
        public Animator transitionAnimation;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameCollisionType.PLAYER))
                StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            var player = FindObjectOfType<Player>();
            player.Save(SceneType.LEVEL1);

            transitionAnimation.SetTrigger(AnimationTriggerType.SCREEN_FADE_END);
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene((int)SceneType.LEVEL1);
        }
    }
}
