using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliente
{
    public class Ficha
    {
        int color;
        int pos;

        public Ficha(int color,int pos)
        {
            this.color = color;
            this.pos = pos;
        }
        
        public void SetPos(int pos)
        {
            this.pos = pos;
        }
    }
}
