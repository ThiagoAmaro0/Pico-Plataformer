using System;
using System.Collections;
using AmaroDev.SceneManagement;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private float _finishDelay = 2;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement player))
        {
            StartCoroutine(NextLevel());
        }
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(_finishDelay);
        if (!SceneLoader.ScenesList.NextScene.Load())
        {
            SceneLoader.ScenesList.Menu.Load();
        }
    }
}