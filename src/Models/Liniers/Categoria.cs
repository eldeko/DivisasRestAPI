using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DivisasRESTAPI.Models.Liniers
{
    public class CategoriaContainer
    {
        public List<Categoria> Categorias { get; set; }

        public CategoriaContainer()
        {
            this.Categorias = new List<Categoria>();
        }
    }

    public class Categoria
    {
        public string NombreCategoria { get; set; }
        public decimal PrecioPromedioCategoria { get; set; }
        public decimal TotalCabezasCategoria { get; set; }
        public decimal TotalImporteCategoria { get; set; }
        public decimal TotalKgsCategoria { get; set; }
        public decimal KgsPromedioCategoria { get; set; }

        public List<SubCategoria> Subcategorias { get; set; }

        public Categoria()
        {
            this.Subcategorias = new List<SubCategoria>();
        }
    }

    public class SubCategoria
    {
        public string NombreSubCategoria { get; set; }
        public string NombreCategoria { get; set; }
        public decimal Minimo { get; set; }
        public decimal Maximo { get; set; }
        public decimal Promedio { get; set; }
        public decimal Mediana { get; set; }
        public int Cabezas { get; set; }
        public decimal Importe { get; set; }
        public decimal Kgs { get; set; }
        public decimal KgsProm { get; set; }
    }
}
