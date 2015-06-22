using UnityEngine;
using System;
using System.Collections.Generic;

public class Game
{
    public List<Projectile> projectiles;
    public List<Enemy>      enemies;

    //===============================Game controls============================
    public static void StartGame(int level)
    {
        Player.I.StartNewGame(level);
    }

    public static void EndGame()
    {
        HighscoreConfig.TrySubmitHighscore();
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
        Game.PlayerShip = shipGO.GetComponent<Sub>();
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