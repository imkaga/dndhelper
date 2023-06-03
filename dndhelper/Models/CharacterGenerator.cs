using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
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
        // Nazwa postaci
        [Required]
        [DisplayName("Nazwa postaci")]
        public string Name { get; set; }
        // Rasa
        [Required]
        [DisplayName("Rasa")]
        public RaceEnum? Race { get; set; }
        
        // Siła
        [Range(2, 10)]
        [DisplayName("Siła")]
        public int Strength { get; set; }
        
        // Zręczność
        [Range(2, 10)]
        [DisplayName("Zręczność")]
        public int Dexterity { get; set; }
        
        // Inteligencja
        [Range(2, 10)]
        [DisplayName("Inteligencja")]
        public int Intelligence { get; set; }
        
        // Szczęście
        [Range(2, 10)]
        [DisplayName("Szczęście")]
        public int Luck { get; set; }
    }
}
