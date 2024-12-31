﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace denemeodev
{
    public class Ogrenci
    {

        public int OgrenciId { get; set; }  
        public string Adi { get; set; }     
        public string Soyadi { get; set; }  
        public string Numara { get; set; }  
        public int SinifId { get; set; }    
        public virtual Sinif Sinif { get; set; } 
        public virtual ICollection<OgrenciDers> OgrenciDersleri { get; set; }


    }
}
