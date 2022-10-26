using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MinigameGameplayScreen : MonoBehaviour
{
    [SerializeField] protected MinigameStartScreen startScreen;
    [SerializeField] protected TextMeshProUGUI scoreboard;
    protected readonly List<GameObject> removables = new();

    protected virtual void OnEnable()
    {
        foreach (GameObject go in removables)
        {
            Destroy(go);
        }
        UpdateScore();
    }

    protected abstract void UpdateScore();

    public void GameOver()
    {
        if (startScreen)
        {
            startScreen.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
