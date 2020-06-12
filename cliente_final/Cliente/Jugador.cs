using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliente
{
    public class Jugador
    {
        string nombre;
        int color;
        Ficha[] fichas = new Ficha[4];

        public Jugador(string nombre)
        {
            this.nombre = nombre;
        }

        public void SetColor(int color)
        {
            this.color = color;
        }

        public string GetNombre()
        {
            return nombre;
        }

        public int GetColor()
        {
            return this.color;
        }

        public Ficha [] GetFichas()
        {
            return this.fichas;
        }
    }
}
