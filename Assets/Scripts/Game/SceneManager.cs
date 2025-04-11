using System;
using UnityEngine;

namespace Platformer
{
    public class SceneManager : MonoBehaviour
    {
        public void LoadMainMenu() =>
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    
        public void LoadLevel(int LevelNumber) =>
			UnityEngine.SceneManagement.SceneManager.LoadScene("Level" + Convert.ToString(LevelNumber));
	}
}
