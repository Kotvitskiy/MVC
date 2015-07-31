using AutoMapper;
using BookStore.Business.Entities;
using BookStore.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Mvc.Components.AutoMapper
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