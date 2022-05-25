using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu_inicial
{

    public class InitMenuController : MonoBehaviour
    {
        public void NewGame()
        {
            // cambiar de escena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void ContinueGame()
        {
            // 1o - comprobar si has iniciado sesion
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    
        public void Quit()
        {
            // salir del juego
            Debug.Log("Saliendo");
            Application.Quit();
        }
    }
}
