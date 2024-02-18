/*
 * Created by Carlos Martinez
 * 
 * This script contains data used for kobold-type enemies in the game.
 * Includes Health, Attack Power, and Movement Speed
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class KoboldData : ScriptableObject
{
    public int health = 20; // Enemy Health
    public int attack = 1; // Enemy Attack Power
    public float speed = 2f; // Enemy Movement Speed
}
