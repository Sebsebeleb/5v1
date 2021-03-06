﻿using System;
using System.Collections;

using UnityEngine;

namespace BBG
{
    using BBG.Actor;

    public class TurnManager : MonoBehaviour
    {

        public static int BossCounter = 60;
        public GameObject ZoneSelectionMenu;

        private GameObject _player;
        private SkillBehaviour _playerSkills;
        private EffectHolder _playerEffects;


        /// <summary>
        /// When the boss counter is at 10 turns left, show this popup to remind player
        /// </summary>
        [SerializeField]
        private GameObject bossCounterReminderPrefab;

        void Awake()
        {
            this._player = GameObject.FindWithTag("Player");
            this._playerSkills = this._player.GetComponent<SkillBehaviour>();
            this._playerEffects = this._player.GetComponent<EffectHolder>();
        }


        public void UseTurn()
        {
            this.StartCoroutine(this.processTurn());
        }

        private IEnumerator processTurn()
        {
            this.UpdatePlayer();

            while (AnimationManager.IsAnimating())
            {
                yield return 0;
            }

            if (BossCounter == 10 + 1)
            {
                var v = Instantiate(this.bossCounterReminderPrefab);

            }

            //First we countdown the boss stuff
            if (BossCounter > 0)
            {
                BossCounter--;
                if (BossCounter <= 0)
                {
                    this.InitBoss();
                }
            }


            // TODO: Sort it properly (unless it already is)
            foreach (Actor.Actor enemy in GridManager.TileMap.GetAll())
            {
                while (AnimationManager.IsAnimating())
                {
                    yield return 0;
                }

                if (!enemy) continue;

                enemy.countdown.Countdown();

            }

            EventManager.Notify(Events.OnTurn, null);

            // Check if we defeated the zone
            if (this.ZoneWon())
            {
                //TG



                //Non-TG
                /* 
            ZoneSelectionMenu.SetActive(true);
            ZoneSelectionMenu.GetComponent<Zone.ZoneSelectionScreen>().PopulateZones();
            */
            }
        }

        private void UpdatePlayer()
        {

            // Uugh this is kinda dirty...
            this._playerSkills.CountDown();
            this._playerEffects.OnTurn();

            //Update player's resources
            this.RegenResources();


        }


        /// <summary>
        /// Regenerates the characters resources based on regeneration rates
        /// </summary>
        private void RegenResources()
        {
            Actor.Actor.Player.CurrentMana += Actor.Actor.Player.ManaRegen.Value;
            Actor.Actor.Player.CurrentMana = Math.Min(Actor.Actor.Player.CurrentMana, Actor.Actor.Player.MaxMana.Value);

            if (Actor.Actor.Player.damagable.CurrentHealth <= 0)
            {

            }
        }


        private void InitBoss()
        {
            foreach (Actor.Actor enemy in GridManager.TileMap.GetAll())
            {
                EnemyManager.KillEnemy(enemy);
            }

            EnemyManager.SpawnBoss();
        }

        // The zone has been won if 1) the boss has been reached and 2) all enemies are dead
        private bool ZoneWon()
        {

            if (BossCounter > 0)
            {
                return false;
            }

            foreach (var enemy in GridManager.TileMap.GetAll())
            {
                if (enemy.tag != "Corpse")
                {
                    return false;
                }
            }

            return true;
        }
    }
}
