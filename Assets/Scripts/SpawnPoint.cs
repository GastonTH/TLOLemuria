using UnityEngine;

public class SpawnPoint
{
     public Vector3 position; // define la posicion que tendra el spawnpoint
     public string name; // define quien debera ser spawneado en ese spawnpoint
     public string type; // monstruo o boss
     public bool isAlive; // si esta vivo o muerto
     
     public SpawnPoint(Vector3 position, string name)
     {
          this.position = position;
          this.name = name;
          isAlive = true;
     }
}