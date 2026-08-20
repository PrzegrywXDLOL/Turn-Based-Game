using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turn_Based_Game
{
    internal class Player
    {
        public string name;
        public int hp;
        public int damage;
        public int heal;
        public int healsAvailable;
        public bool dead = false;
        public bool block = false;
        public int blocksAvailable;
        public bool attack = false;

        public Player() { }

        //function that will assign stats to player's character

        public void SetStats(string Name, int HP, int Damage, int Heal, int BlocksAvailable, int HealsAvailable)
        {
            name = Name;
            hp = HP;
            damage = Damage;
            heal = Heal;
            blocksAvailable = BlocksAvailable;
            healsAvailable = HealsAvailable;
        }

        //function that will deal damage to player's character

        public void DealDmg()
        {
            if (block)
            {
                Console.WriteLine("Damage blocked");
                block = false;
            }
            else
                hp -= damage;
            
            if (hp <= 0)
                dead = true;
        }

        //function that heals player's character if possible

        public void PlayerHeal()
        {
            if (healsAvailable > 0)
            {
                hp += heal;
                healsAvailable--;
            }
            else
                Console.WriteLine("No heals available");
        }

        //checking if player's character is dead

        public bool IsDead()
        {
            return dead;
        }

        //Turning on block for the next turn

        public void Block()
        {
            if (blocksAvailable > 0)
            {
                block = true;
                blocksAvailable--;
            }
            else
                Console.WriteLine("No blocks available");
            
        }

        public void Attack()
        {
            attack = true;
        }

        public bool IsAttacking()
        {
            return attack;
        }
    }
}
