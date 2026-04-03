using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class ScoreBoard : MonoBehaviour
    {
        public Friend friend;
        public TextMeshProUGUI text;
        public float curr;
        public float t = 0;
        public AudioClip applause;
        public AudioSource source;
        private void OnEnable()
        {
            curr = 0;
            t = 0;
            Invoke(nameof(PlaySound), 2f);
        }

        public void LateUpdate()
        {
            if (curr < friend.score)
            {
                t += Time.deltaTime;
                curr = Mathf.Lerp(0, friend.score, t/2f);
                text.text = curr.ToString("0");
            }
        }

        public void PlaySound()
        {
            source.PlayOneShot(applause);
        }
    }
}