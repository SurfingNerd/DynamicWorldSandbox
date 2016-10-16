﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicWorldSandbox.Model
{
    public class Creature
    {
        public Creature(bool ApplyNewUniqueId = true)
        {
            UniqueID = s_currentCreatureID++;
        }

        private static long s_currentCreatureID;

        public long UniqueID;
        public int Birthday;
        public List<Buff> CurrentBuffs = new List<Buff>();
        public CreatureStats CreatureStats  = new CreatureStats();

        public CreatureType CreatureType;
    }


    public class CreatureType
    {
        public CreatureType(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public virtual Creature CreateNewCreature()
        {
            Creature result = new Creature();

            ApplyNewBuffs(result);


            return result;
        }

        protected virtual void ApplyNewBuffs(Creature result)
        {
            
        }
    }

    public class CreatureStats
    {
        public double Intelligence;


        public double Agility;


        public double Strength;


        public double Stamina;

    }

    /// <summary>
    /// a buff or debuff that is changing some attributes or behaviors
    /// </summary>
    public class Buff
    {

    }
}
