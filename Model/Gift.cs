using System.ComponentModel.DataAnnotations;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace webAPI.Models
{
    public class Gift
    {
        
        //definicion de las propiedades
        [Key]
        
        public int GiftID{get;set;}

        public string GiftName{get;set;}

        public string GiftType{get;set;}
    }
}