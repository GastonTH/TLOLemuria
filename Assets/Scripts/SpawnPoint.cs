using UnityEngine;

public class SpawnPoint
{
     public Vector3 position; // define la posicion que tendra el spawnpoint
     public string name; // define quien debera ser spawneado en ese spawnpoint
     public string type; // monstruo o boss
     public bool isAlive; // si esta vivo o muerto
     public string alias; // para buscarlo en un futuro y que resucite
     
     public SpawnPoint(Vector3 position, string name, string alias)
     {
          this.position = position;
          this.name = name;
          isAlive = false; // por defecto estara muerto, hasta que el script los resucite
          this.alias = alias;
     }
}