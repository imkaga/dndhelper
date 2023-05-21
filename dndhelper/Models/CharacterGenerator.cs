using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace dndhelper.Models
{
    public class CharacterGenerator
    {
        // Enum reprezentujący dostępne rasy
        public enum RaceEnum
        {
            Human,
            Elf,
            Dwarf,
            Orc
        }

        // właściwości
        [HiddenInput(DisplayValue = false)]
        public string UserId { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public RaceEnum Race { get; set; }
        [Range(2, 10)]
        public int Strength { get; set; }
        [Range(2, 10)]
        public int Dexterity { get; set; }
        [Range(2, 10)]
        public int Intelligence { get; set; }
        [Range(2, 10)]
        public int Luck { get; set; }
    }
}
