using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Dtos.Character
{
    public class GetCharacterDTO
    {
        public string Name { get; set; } = "Fredo";

        public int HitPoints { get; set; } = 50;

        public int Strength { get; set; } = 100;

        public int Defense { get; set; } = 10;

        public int Intelligence { get; set; } = 10;

        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}