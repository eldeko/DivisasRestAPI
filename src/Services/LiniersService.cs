using DivisasRESTAPI.Models.Liniers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DivisasRESTAPI.Services
{
    public class LiniersService : ILiniersService
    {

        public CategoriaContainer GetLiniersDataAsync(string desde, string hasta)
        {

            string command = "../app/BashScripts/getLiniers.sh " + " -d " + desde + " -h " + hasta;

            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            pProcess.StartInfo.FileName = "bash";
         //   pProcess.StartInfo.WorkingDirectory = ".\\src\\src\\BashScripts";
            pProcess.StartInfo.CreateNoWindow = false;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.RedirectStandardInput = true;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.Start();
            pProcess.StandardInput.WriteLine("bash");
            pProcess.StandardInput.WriteLine(command);
            pProcess.StandardInput.Flush();
            pProcess.StandardInput.Close();

            string strOutput = pProcess.StandardOutput.ReadToEnd();

            string jsonSubCategorias = null;

            foreach (var lines in strOutput.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (lines.Contains("SubCategoria"))
                {
                    jsonSubCategorias = lines;
                }
            }

            var result = JsonConvert.DeserializeObject<Categoria>(jsonSubCategorias);

            List<string> ListasCategorias = new List<string>();


            foreach (var subCategoria in result.Subcategorias)
            {
                if (!ListasCategorias.Contains(subCategoria.NombreCategoria.ToString()))
                {
                    ListasCategorias.Add(subCategoria.NombreCategoria.ToString());
                }
            }

            var container = new CategoriaContainer();

            foreach (var categoria in ListasCategorias)
            {
                container.Categorias.Add(new Categoria() { NombreCategoria = categoria });
            }

            foreach (var categoriaLoop in container.Categorias)
            {
                foreach (var subcategoriaLoop in result.Subcategorias)
                {
                    if (subcategoriaLoop.NombreCategoria == categoriaLoop.NombreCategoria)
                    {
                        categoriaLoop.Subcategorias.Add(subcategoriaLoop);
                    }
                }
            }

            ProcessTotalsAndAvgs(container);

            return container;
        }

        private void ProcessTotalsAndAvgs(CategoriaContainer container)
        {
            foreach (var actualCategoria in container.Categorias)
            {
                actualCategoria.TotalKgsCategoria = actualCategoria.Subcategorias.Sum(x => x.Kgs);
                actualCategoria.TotalImporteCategoria = actualCategoria.Subcategorias.Sum(x => x.Importe);
                actualCategoria.TotalCabezasCategoria = actualCategoria.Subcategorias.Sum(x => x.Cabezas);

                actualCategoria.PrecioPromedioCategoria = actualCategoria.TotalImporteCategoria / actualCategoria.TotalKgsCategoria;
                actualCategoria.KgsPromedioCategoria = actualCategoria.TotalKgsCategoria / actualCategoria.TotalCabezasCategoria;
            }
        }
    }
}