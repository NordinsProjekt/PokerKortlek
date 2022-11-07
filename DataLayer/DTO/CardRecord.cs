﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DTO
{
    public sealed record CardRecord(int CardId,string Color, int Value)
    {
        public override string ToString()
        {
            return $"{Value} of {Color}";
        }
    }
}
