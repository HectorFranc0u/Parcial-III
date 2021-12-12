using System.ComponentModel.DataAnnotations;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace webAPI.Models
{   [Authorize]
    public class Furniture
    {
        
        //definicion de las propiedades
        [Key]
        
        public int FurId{get;set;}

        public string FurName{get;set;}
        
        public string FurType{get;set;}
    
        public string FurMaterial{get;set;}
    
        public int FurPrice{get;set;}
    }
}