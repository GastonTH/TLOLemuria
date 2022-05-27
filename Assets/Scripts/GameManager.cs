using System;
using Json_Serializer;
using UIScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public Heroe MyHeroe;
    public GameObject _uiPlayerUIElements;
    private bool _stateGame = false; //true = new game, false = continue game
    
    // info de la barra de vida
    public HealthBarController healthBarController;
    public ManaBarController manaBarController;
    public XPBarController xpBarController;
    public ActionBarController actionBarController;
    public CoinCountController coinCountController;
    public GameObject pausePanel;
    public GameObject confirmExit;

    private bool _isPaused;
    private Vector3 _lastPosition;
    private Vector3[] _spawnPoints = new []{new Vector3(4,-4,0), new Vector3(8,-4,0), new Vector3(12,-4,0),new Vector3(16,-4,0)};
    
    void Start()
    {
        SpawnMonsters();
        Time.timeScale = 1f;
        _isPaused = false;
        if (_stateGame)
        {
            //true --> crearemos un nuevo heroe desde 0
            FillInterface();
        }
        else
        {
            ContinueGame();
            // que el heroe inicialice un UI con sus constantes vitales
            // en una funcion que sea continuar partida, que descargue el json de la info del personaje
        }
    }

    private void SpawnMonsters()
    {
        for (int ii = 0; ii < _spawnPoints.Length; ii++)
        {
            Instantiate(Resources.Load("Characters/Bandits/HeavyBandit"), _spawnPoints[ii], Quaternion.identity);
            Debug.Log("enemigo insanciado en posicion: " + _spawnPoints[ii].x + " - " + _spawnPoints[ii].y);
        }
    }

    void Update()
    {

        if (Input.GetKeyDown("q"))
        {
            MyHeroe.CurrentVit -= 10;
            healthBarController.setHealth(MyHeroe.CurrentVit);
        }
        
        // pulsar el boton escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("boton escape");
            _isPaused = !_isPaused;
            if (_isPaused)
            {
                PauseGame();

            }
            else
            {
                ResumeGame();
            }
        }
        
        if (Input.GetKeyDown("n"))
        {
            MyHeroe.CurrentMana -= 10;
            manaBarController.setMana(MyHeroe.CurrentMana);
        }

        if (Input.GetKeyDown("b"))
        {
            MyHeroe.CurrentXp += 10;
            xpBarController.setXP(MyHeroe.CurrentXp);
            
            if (MyHeroe.CurrentXp >= MyHeroe.XpMax)
            {
                Levelup();
            }
            
            xpBarController.updateInfo(MyHeroe.Level, MyHeroe.XpMax, MyHeroe.CurrentXp);
            
        }
    }

    public void SetLastPosition(Vector3 v)
    {
        _lastPosition = v;
        //Debug.Log("last position - " + _lastPosition);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        confirmExit.SetActive(false);
        _uiPlayerUIElements.SetActive(true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        confirmExit.SetActive(false);
        _uiPlayerUIElements.SetActive(false);

        GameObject.Find("ButtonResume").GetComponent<Button>().onClick.AddListener(ResumeGame);
        GameObject.Find("ButtonExit").GetComponent<Button>().onClick.AddListener(() =>
        {
            pausePanel.SetActive(false);
            confirmExit.SetActive(true);
            GameObject.Find("ButtonExitYes").GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            });
            GameObject.Find("ButtonExitNo").GetComponent<Button>().onClick.AddListener(() =>
            {
                pausePanel.SetActive(true);
                confirmExit.SetActive(false);
            });
        });
    }

    private void Levelup()
    {
        // primero debe comprobar si se ha pasado de de la experiencia maxima
        // si se ha pasado, el restante debe ser experiencia para el siguiente nive
        
        // si no se ha pasado, el restante debe ser experiencia para el siguiente nivel, sino sera 0 y seguira igual       
        int restXp = MyHeroe.CurrentXp - MyHeroe.XpMax; // por ejemplo, si restXP es 120 - 100 de la total = 20 restante

        //Debug.Log("restante de experiencia = " + myHeroe.CurrentXp + " - " + myHeroe.XpMax + " = " +restXP);
        MyHeroe.CurrentXp = restXp; // ahora CurrentXp es 20
        
        // ahora aumentaremos el numero del nivel
        MyHeroe.Level++;

        UpgradeStats();
        
        // ahora actualizamos la barra de mana y vida y se decimos que cuando suba de nivel se cure la vida y rellene el mana
        healthBarController.setMaxHealth(MyHeroe.MaxVit, MyHeroe.MaxVit);
        manaBarController.setMaxMana(MyHeroe.MaxMana, MyHeroe.MaxMana);
        
        // test
        
        //List<ListPlayerSerializable> players = new List<ListPlayerSerializable>();

        //players = JsonUtility.FromJson<PlayerSerializer>(jsonFromDB.text);
        
        //var jsonFromDB = Resources.Load<TextAsset>("Files/test");
        //Debug.Log("json " + jsonFromDB.text);
        //PlayerSerializer player = JsonUtility.FromJson<PlayerSerializer>(jsonFromDB.text);
        // 2. crear el heroe con esos datos

    }

    private void UpgradeStats()
    {
        // cuando suba de nivel, tiene que subir la vida maximas, mana maximo y la experiencia maxima
        MyHeroe.XpMax += (int)Math.Ceiling(MyHeroe.XpMax * 0.1f); // aumentamos el xpMax en 10%
        MyHeroe.MaxVit += (int)Math.Ceiling(MyHeroe.MaxVit * 0.1f); // aumentamos la vida en 10%
        MyHeroe.MaxMana += (int)Math.Ceiling(MyHeroe.MaxMana * 0.1f); // aumentamos el mana en 10%
        //TODO molaria que cuando suba de nivel tambien aumente el daño de ataque, defensa, etc
    }

    private void ContinueGame()
    {

        // 1. buscar el heroe en la api
        var jsonFromDB = Resources.Load<TextAsset>("Files/test");
        //Debug.Log("json " + jsonFromDB.text);
        PlayerSerializer player = JsonUtility.FromJson<PlayerSerializer>(jsonFromDB.text);
        // 2. crear el heroe con esos datos
        MyHeroe = new Heroe(player);
        
        Object playerPrefab = null;
        
        switch (MyHeroe.GameClass)
        {
            case "Bandit":
                playerPrefab = Resources.Load("Characters/Bandits/BanditPlayer");
                break;
            case "Wizard":
                //playerPrefab = Resources.Load("Characters/Bandits/BanditPlayer");
                break;
            case "Warrior":
                //playerPrefab = Resources.Load("Characters/Bandits/BanditPlayer");
                break;
            case "Archer":
                //playerPrefab = Resources.Load("Characters/Bandits/BanditPlayer");
                break;
            case "MartialArtist":
                //playerPrefab = Resources.Load("Characters/Bandits/BanditPlayer");
                break;
            default:
                playerPrefab = Resources.Load("Characters/Bandits/BanditPlayer");
                break;
        }

        playerPrefab.name = "Player";
        Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        
        // jugador creado y ahora se lo pasaremos a la Camara
        GameObject.Find("Main Camera").GetComponent<CameraController2D>().target = GameObject.Find("Player(Clone)").transform;
        // rellenaremos el UI con los datos del heroe
        FillInterface();

    }

    private void FillInterface()
    {
        Debug.Log(MyHeroe.ToString());
        Debug.Log("MAX VIT" + MyHeroe.MaxVit);
        Debug.Log("COINS" + MyHeroe.Coins);
        
        // accediendo a las monedas y dandoles valor
        coinCountController.setCoinCount(MyHeroe.Coins);
        
        // accede a la barra de vida y la rellena con los datos del heroe
        healthBarController.setMaxHealth(MyHeroe.MaxVit, MyHeroe.CurrentVit);
        healthBarController.setHealth(MyHeroe.CurrentVit);
        
        // accede a la barra de mana y la rellena con los datos del heroe
        manaBarController.setMaxMana(MyHeroe.MaxMana, MyHeroe.CurrentMana);
        manaBarController.setMana(MyHeroe.CurrentMana);
        
        // accede a la barra de xp y la rellena con los datos del heroe
        xpBarController.updateInfo(MyHeroe.Level, MyHeroe.XpMax, MyHeroe.CurrentXp);
        
        // accede a la barra de acciones y la rellena con los datos del heroe
        //_actionBarController.setMaxActionTime(10); // aqui ira el cooldown del heroe, es decir el tiempo que tarda en dar un golpe
        //_actionBarController.setActionTime(10); 
        
    }
    
    /*private void SaveGame()
    {
        // 1. serializar el heroe
        // 2. guardar el json en la api
        
        // 1. serializar el heroe
        //string json = JsonUtility.ToJson(MyHeroe);
        //Debug.Log("json " + json);
        // 2. guardar el json en la api
        //Resources.Load<TextAsset>("Files/test");
        
    }
    */
    
}
