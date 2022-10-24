using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class Goal : MonoBehaviour
    {
        protected void OnTriggerEnter2D(Collider2D collision)
        {
            Player target = collision.gameObject.GetComponentInParent<Player>();
            if (target != null)
            {
                var timeLeft = CountdownTimer.Instance.CurrentTime;
                PlayerPrefs.SetFloat("timeRemaining", timeLeft);
                PlayerPrefs.SetInt("gameWon", 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene("End Screen");
            }
        }
    }
}
