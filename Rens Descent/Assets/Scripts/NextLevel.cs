using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int nextLevel;
    public ScoreManager _scoreManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player")) SceneManager.LoadScene(nextLevel);
        _scoreManager.resetTimer();

    }
}
