
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DoAn1.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DoAn1.Models.CSDL>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}