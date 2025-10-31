﻿using Alma.Domain.Entities;
using Alma.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alma.Application.DTOs.Campanha
{
    public class NovaCampanhaDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal MetaValor { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string ImagemUrl { get; set; }
    }
}
