﻿using System.Collections.Generic;

namespace AnuncioWeb.Models.DTO
{
    public abstract class BaseDTO
    {
        public List<LinkDTO> Links { get; set; }
    }
}
