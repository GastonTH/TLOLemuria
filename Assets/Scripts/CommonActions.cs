using UnityEngine;

public interface CommonActions
{
    void TakeDamage(int dmg);
    void DoDamage(int dmg);
    void Run();
    void Jump();
    Vector3 WhoIsUrLastPosition();
}