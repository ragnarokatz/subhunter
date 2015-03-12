using UnityEngine;
using System;
using System.Collections.Generic;

public class MainGame
{
    public static void StartGame()
    {
    }

    public static void EndGame()
    {
    }

    private bool isActive;
    public static void StopLevel()
    {
        this.isActive = false;
    }

    public static void StartLevel()
    {
        this.isActive = true;
    }

    public static bool IsActive()
    {
        return this.isActive;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    private List<Enemy> enemies;
    private List<Projectile> projectiles;

    public void AddEnemy(Enemy enemy)
    {
        this.enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        this.enemies.Remove(enemy);
    }

    public void AddProjectile(Projectile projectile)
    {
        this.projectiles.Add(projectile);
    }

    public void RemoveProjectile(Projectile projectile)
    {
        this.projectiles.Remove(projectile);
    }
}