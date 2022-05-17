using System;
using Json_Serializer;
using UIScripts;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public Heroe MyHeroe;
    private GameObject _uiPlayerUIElements;
    private bool _stateGame = false; //true = new game, false = continue game
    
    // info de la barra de vida
    public HealthBarController healthBarController;
    public ManaBarController manaBarController;
    public XPBarController xpBarController;
    public ActionBarController actionBarController;
    
    void Start()
    {
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

    void Update()
    {
        
        if (Input.GetKeyDown("q"))
        {
            MyHeroe.CurrentVit -= 10;
            healthBarController.setHealth(MyHeroe.CurrentVit);
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
        
        switch (player.gameClass)
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
