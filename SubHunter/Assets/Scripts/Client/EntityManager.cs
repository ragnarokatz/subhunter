using System;
using System.Collections.Generic;
using UnityEngine;
using SubHunter.Powerup;

public class EntityManager : MonoBehaviour
{
    private static EntityManager instance;
    public static EntityManager I { get { return EntityManager.instance; } }

    public Transform EnemyParent;
    public Transform ProjectileParent;
    public Transform PowerupParent;

    [HideInInspector] public List<Enemy>      Enemies;
    [HideInInspector] public List<Powerup>    Powerups;
    [HideInInspector] public List<Projectile> Projectiles;
    [HideInInspector] public List<Bomb>       Bombs;

    private void Start()
    {
        EntityManager.instance = this;

        this.Enemies     = new List<Enemy>();
        this.Powerups    = new List<Powerup>();
        this.Projectiles = new List<Projectile>();
        this.Bombs       = new List<Bomb>();
    }

    private void Update()
    {
        if (! GameState.IsInPlayState())
            return;

        EnemyUpdate();
        PowerupUpdate();
        ProjectileUpdate();
        BombUpdate();
        ComboUpdate();
    }

    private void EnemyUpdate()
    {
        foreach (var enemy in this.Enemies)
        {
            if (! (enemy is Medusa))
                continue;

            if (Game.I.Ship.Box.Overlaps(enemy.Box))
            {
                enemy.Explode();
                Game.I.DieBreak();
                return;
            }
        }
    }

    private void PowerupUpdate()
    {
        foreach (var powerup in this.Powerups)
        {
            if (! Game.I.Ship.Box.Overlaps(powerup.Box))
                continue;

            powerup.Effect();
            powerup.Destroy();
        }
    }

    private void ProjectileUpdate()
    {
        foreach (var projectile in this.Projectiles)
        {
            if (! Game.I.Ship.Box.Overlaps(projectile.Box))
                continue;

            projectile.Destroy();
            Game.I.DieBreak();
            return;
        }
    }

    private void BombUpdate()
    {
        foreach (var bomb in this.Bombs)
        {
            foreach (var enemy in this.Enemies)
            {
                if (! enemy.Box.Overlaps(bomb.Box))
                    continue;

                enemy.Explode();
                bomb.Destroy();
            }
        }
    }

    // May need to be optimized for performance
    private void ComboUpdate()
    {
        foreach (var enemy in this.Enemies)
        {
            if (! enemy.IsExploding)
                continue;

            foreach (var target in this.Enemies)
            {
                if (target.IsExploding)
                    continue;

                if (target.Box.Overlaps(enemy.Box))
                    target.Explode(enemy.ComboIdx);
            }
        }
    }
}