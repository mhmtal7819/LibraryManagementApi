﻿using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class LibConfig : IEntityTypeConfiguration<Libs>
    {

        public void Configure(EntityTypeBuilder<Libs> builder)
        {
            builder.HasData(
               new Libs { Id = 1, CategoryId = 1, Title = "Karagöz ve Hacivat", Price = 75 },
                new Libs { Id = 2, CategoryId = 2, Title = "Mesnevi", Price = 175 },
                new Libs { Id = 3, CategoryId = 1, Title = "Devlet", Price = 375 }
               );
        }
    }
}
