using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cliente
{
    public class Tablero
    {
        List<Casilla> casillas;

        public Tablero()
        {
            this.casillas = new List<Casilla>();
        }

        public void AñadirCasilla(Casilla casilla)
        {
            this.casillas.Add(casilla);
        }

        public Casilla GetCasilla(int num)
        {
            return this.casillas.ElementAt<Casilla>(num-1);
        }
    }
}
