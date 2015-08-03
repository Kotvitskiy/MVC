using AutoMapper;
using Store.Business.Entities;
using Store.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Mvc.Components.AutoMapper
{
    public class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            ConfigureBookMapping();
        }

        private static void ConfigureBookMapping()
        {
            Mapper.CreateMap<BookItem, BookItemViewModel>();
        } 
    }
}