﻿using covid19_api.Models;
using Microsoft.EntityFrameworkCore;

namespace covid19_api.Data
{
    public class DataContext:DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<CountryData> CountryDatas => Set<CountryData>();
        public DbSet<Case> Cases => Set<Case>();


    }
}