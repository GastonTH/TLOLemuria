using System;
using System.Collections;
using System.Collections.Generic;
using Json_Serializer;
using UIScripts;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public Heroe myHeroe;
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
        }
        else
        {
            ContinueGame();
            // que el heroe inicialice un UI con sus constantes vitales
            // en una funcion que sea continuar partida, que descargue el json de la info del personaje
        }
        //FillInterface();
    }

    void Update()
    {
        
        if (Input.GetKeyDown("q"))
        {
            myHeroe.CurrentVit -= 10;
            healthBarController.setHealth(myHeroe.CurrentVit);
        }
        
        if (Input.GetKeyDown("n"))
        {
            myHeroe.CurrentMana -= 10;
            manaBarController.setMana(myHeroe.CurrentMana);
        }

        if (Input.GetKeyDown("b"))
        {
            myHeroe.CurrentXp += 10;
            xpBarController.setXP(myHeroe.CurrentXp);
            
            if (myHeroe.CurrentXp >= myHeroe.XpMax)
            {
                levelup();
            }
            
            xpBarController.updateInfo(myHeroe.Level, myHeroe.XpMax, myHeroe.CurrentXp);

        }
    }

    private void levelup()
    {
        // primero debe comprobar si se ha pasado de de la experiencia maxima
        // si se ha pasado, el restante debe ser experiencia para el siguiente nive
        
        // si no se ha pasado, el restante debe ser experiencia para el siguiente nivel, sino sera 0 y seguira igual       
        int restXP = myHeroe.CurrentXp - myHeroe.XpMax; // por ejemplo, si restXP es 120 - 100 de la total = 20 restante

        //Debug.Log("restante de experiencia = " + myHeroe.CurrentXp + " - " + myHeroe.XpMax + " = " +restXP);
        myHeroe.CurrentXp = restXP; // ahora CurrentXp es 20
        
        // ahora aumentaremos el numero del nivel
        myHeroe.Level++;

        // cuando suba de nivel, tiene que subir la vida maximas, mana maximo y la experiencia maxima
        myHeroe.XpMax += (int)Math.Ceiling(myHeroe.XpMax * 0.1f); // aumentamos el xpMax en 10%
        myHeroe.MaxVit += (int)Math.Ceiling(myHeroe.MaxVit * 0.1f); // aumentamos la vida en 10%
        myHeroe.MaxMana += (int)Math.Ceiling(myHeroe.MaxMana * 0.1f); // aumentamos el mana en 10%
        
        // ahora actualizamos la barra de mana y vida
        healthBarController.setMaxHealth(myHeroe.MaxVit, myHeroe.CurrentVit);
        manaBarController.setMaxMana(myHeroe.MaxMana, myHeroe.CurrentMana);
        
    }

    private void ContinueGame()
    {

        // 1. buscar el heroe en la api
        var jsonFromDB = Resources.Load<TextAsset>("Files/test");
        //Debug.Log("json " + jsonFromDB.text);
        PlayerSerializer player = JsonUtility.FromJson<PlayerSerializer>(jsonFromDB.text);
        // 2. crear el heroe con esos datos
        myHeroe = new Heroe(player);
        
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

        Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
        FillInterface();

    }
    
    // esta funcion busca el UI dentro de la escena y lo rellena con los datos del heroe
    private void FillInterface()
    {
        // accede a la barra de vida y la rellena con los datos del heroe
        healthBarController.setMaxHealth(myHeroe.MaxVit, myHeroe.CurrentVit);
        healthBarController.setHealth(myHeroe.CurrentVit);
        
        // accede a la barra de mana y la rellena con los datos del heroe
        manaBarController.setMaxMana(myHeroe.MaxMana, myHeroe.CurrentMana);
        manaBarController.setMana(myHeroe.CurrentMana);
        
        // accede a la barra de xp y la rellena con los datos del heroe
        xpBarController.updateInfo(myHeroe.Level, myHeroe.XpMax, myHeroe.CurrentXp);
        // accede a la barra de acciones y la rellena con los datos del heroe
        //_actionBarController.setMaxActionTime(10); // aqui ira el cooldown del heroe, es decir el tiempo que tarda en dar un golpe
        //_actionBarController.setActionTime(10); 
    }
    
    private void SaveGame()
    {
        // 1. serializar el heroe
        // 2. guardar el json en la api
    }
    
}
