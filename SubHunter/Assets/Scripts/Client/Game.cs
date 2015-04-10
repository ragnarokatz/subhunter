using UnityEngine;
using System;
using System.Collections.Generic;

public class Game
{
    //===============================Game controls============================
    public static void StartGame()
    {
        StartLevel(1);
        Player.Clip = 5;
        Player.Lives = 4;
        Player.Score = 0;
    }

    public static void EndGame()
    {
        Highscore.TrySubmitHighscore();
    }

    private static bool isActive;
    public static void StopLevel()
    {
        Game.isActive = false;
    }

    public static void StartLevel(int level)
    {
        Player.Level = level;
        Player.Completion = 0;

        Game.isActive = true;

        InitShip();
    }

    public static bool IsActive()
    {
        return Game.isActive;
    }

    private static void InitShip()
    {
        var ship = Resources.Load<GameObject>("Ship");
        var shipGO = GameObject.Instantiate(ship) as GameObject;
        shipGO.transform.position = new Vector3(0f, Dimensions.WATER, 0f);
        Game.PlayerShip = shipGO.GetComponent<Ship>();
    }

    public static void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    //================================Game entities==============================
    public static Ship PlayerShip;
    private static List<Enemy> enemies = new List<Enemy>();
    private static List<Projectile> projectiles = new List<Projectile>();

    public static void AddEnemy(Enemy enemy)
    {
        Game.enemies.Add(enemy);
    }

    public static void RemoveEnemy(Enemy enemy)
    {
        Game.enemies.Remove(enemy);
    }

    public static void AddProjectile(Projectile projectile)
    {
        Game.projectiles.Add(projectile);
    }

    public static void RemoveProjectile(Projectile projectile)
    {
        Game.projectiles.Remove(projectile);
    }

    public static void CheckExplosion(Enemy source)
    {
        foreach (var enemy in Game.enemies)
            if (source.IsColliding(enemy))
                source.TriggerExplode(enemy);
    }

    //==================================Combo=====================================
    private const int COMBO_LIM = 10;
    private static Dictionary<int, int> combos = new Dictionary<int, int>(Game.COMBO_LIM);

    private static int nextIdx;

    public static int StartCombo()
    {
        var idx = Game.nextIdx;
        if (Game.combos.ContainsKey(idx))
            Game.combos[idx] = 1;
        else
            Game.combos.Add(idx, 1);

        UpdateNextIdx();

        return idx;
    }

    public static int ChainCombo(int idx)
    {
        System.Diagnostics.Debug.Assert(Game.combos.ContainsKey(idx));

        var chain = Game.combos[idx];
        var newChain = chain + 1;
        Game.combos[idx] = newChain;

        return newChain;
    }

    private static void UpdateNextIdx()
    {
        Game.nextIdx = (Game.nextIdx + 1) % COMBO_LIM;
    }
    //=============================================================================
}