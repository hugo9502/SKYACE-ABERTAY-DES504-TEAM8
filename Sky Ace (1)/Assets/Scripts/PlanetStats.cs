using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet", menuName = "Planet", order = 1)]
public class PlanetStats : ScriptableObject {
    public string planetName;
    [Range(0.5f, 5.0f)]
    public float size;
    [Range(0.5f, 10.0f)]
    public float gravityRadiusMultiplier;
    [Range(0.25f, 1.5f)]
    public float orbitSpeed;
    public bool firePlanet;
    public bool icePlanet;
    public bool isStart;
    public bool isGoal;
    public string lore;
}
