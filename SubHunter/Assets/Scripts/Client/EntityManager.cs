using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private static EntityManager instance;
    public static EntityManager I { get { return EntityManager.instance; } }

    public List<Enemy>      Enemies;
    public List<Powerup>    Powerups;
    public List<Projectile> Projectiles;
    public List<Bomb>       Bombs;
    public List<Explosion>  Explosions;

    private void Start()
    {
        EntityManager.instance = this;

        this.Enemies     = new List<Enemy>();
        this.Powerups    = new List<Powerup>();
        this.Projectiles = new List<Projectile>();
        this.Bombs       = new List<Bomb>();
        this.Explosions  = new List<Explosion>();
    }

    private void Update()
    {
        if (! GameState.IsInPlayState())
            return;

        EnemyUpdate();
        PowerupUpdate();
        ProjectileUpdate();
        BombUpdate();
    }

    private void EnemyUpdate()
    {
        foreach (var enemy in this.Enemies)
        {
            if (! (enemy is Medusa))
                continue;

            if (Game.I.Ship.Box.Overlaps(enemy.Box))
            {
                enemy.ExplodeByBomb();
                Game.I.DieBreak();
                return;
            }
        }
    }

    private void PowerupUpdate()
    {
        foreach (var powerup in this.Powerups)
        {
            if (Game.I.Ship.Box.Overlaps(powerup.Box))
            {
                powerup.Effect();
                powerup.Destroy();
                continue;
            }
        }
    }

    private void ProjectileUpdate()
    {
        foreach (var projectile in this.Projectiles)
        {
            if (Game.I.Ship.Box.Overlaps(projectile.Box))
            {
                projectile.Destroy();
                Game.I.DieBreak();
                return;
            }
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

                enemy.ExplodeByBomb();
                bomb.Destroy();
            }
        }
    }

    private void ExplosionUpdate()
    {
        foreach (var explosion in this.Explosions)
        {
            foreach (var enemy in this.Enemies)
            {
                if (! enemy.Box.Overlaps(explosion.Box))
                    continue;

                enemy.ExplodeByExplosion(explosion.ComboIdx);
            }
        }
    }
}