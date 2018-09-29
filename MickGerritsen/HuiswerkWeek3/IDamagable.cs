using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable {
    int health { get; set; }
    void TakeDamage(int amount);
}
