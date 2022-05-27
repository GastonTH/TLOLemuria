using UnityEngine;

public interface CommonActions
{
    void TakeDamage(int dmg);
    void DoDamage();
    void Run();
    void Jump();
    Vector3 WhoIsUrLastPosition();
}