using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliente
{
    public class Casilla
    {
        int numero;
        string tipo;
        int x;
        int y;
        int x1;
        int y1;
        int x2;
        int y2;

        public Casilla(int numero,string tipo,int x,int y,int x1, int y1,int x2 ,int y2)
        {
            this.numero = numero;
            this.tipo = tipo;
            this.x = x;
            this.y = y;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }

    }
}
