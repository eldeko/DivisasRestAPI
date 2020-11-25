using DivisasRESTAPI.Helpers;
using DivisasRESTAPI.Models.Liniers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace DivisasRESTAPI.Services
{
    public class LiniersService : ILiniersService
    {

        public CategoriaContainer GetLiniersDataAsync(string desde, string hasta)
        {

            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-AR");
            string specifier = "N";
            string command = "../app/BashScripts/getLiniers.sh " + " -d " + desde + " -h " + hasta;

            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();

            //
            // PROD ENV.
            pProcess.StartInfo.FileName = "bash";
            pProcess.StartInfo.WorkingDirectory = ".//";
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

            //
            //LOCAL TEST
            //string strOutput = LoadJson();


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

             container.FechaDesde = desde == null ? DateTime.Today : DateTime.Parse(desde, culture);

            container.FechaHasta =  hasta == null ? DateTime.Today : DateTime.Parse(hasta, culture);

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

            #region Convertidor a Tabla 
            //DESCOMENTAR PARA DEVOLVER TABLA HTML
            //StringBuilder sb = new StringBuilder();

            //using (Table table = new Table(sb))
            //{

            //    //table.AddHeaderRow();
            //    foreach (var categoria in container.Categorias)
            //    { table.StartTableBody();
            //        using (Helpers.Row row = table.AddHeaderRow())
            //        {

            //            row.AddCell(categoria.NombreCategoria);
            //            row.AddCell("");
            //            row.AddCell("Precio" + "<br/>" + "Minimo");
            //            row.AddCell("Precio" + "<br/>" + "Maximo");
            //            row.AddCell("Precio" + "<br/>" + "Promedio");
            //            row.AddCell("Mediana");
            //            row.AddCell("Cabezas");
            //            row.AddCell("Importe");
            //            row.AddCell("Kgs.");
            //            row.AddCell("Promedio" + "<br/>" + "Kgs.");

            //            foreach (var subcategoria in categoria.Subcategorias)
            //            {                        
            //                table.AddRow();
            //                row.AddCell("");
            //                    row.AddCell(subcategoria.NombreSubCategoria);
            //                row.AddCell(subcategoria.Minimo.ToString(specifier, culture).Insert(0, "$"));
            //                row.AddCell(subcategoria.Maximo.ToString(specifier, culture).Insert(0, "$"));
            //                row.AddCell(subcategoria.Promedio.ToString(specifier, culture).Insert(0, "$"));
            //                row.AddCell(subcategoria.Mediana.ToString(specifier, culture).Insert(0, "$"));
            //                row.AddCell(subcategoria.Cabezas.ToString());
            //                row.AddCell(subcategoria.Importe.ToString(specifier, culture).Insert(0, "$"));
            //                row.AddCell(subcategoria.Kgs.ToString(specifier, culture));
            //                row.AddCell(subcategoria.KgsProm.ToString(specifier, culture));
            //            }
            //            table.AddRow();
            //            row.AddCell("SUBTOTAL");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell(Math.Round(categoria.PrecioPromedioCategoria, 3).ToString(specifier, culture).Insert(0, "$"));
            //            row.AddCell("");
            //            row.AddCell(categoria.TotalCabezasCategoria.ToString());
            //            row.AddCell(Math.Round(categoria.TotalImporteCategoria, 3).ToString(specifier, culture).Insert(0, "$"));
            //            row.AddCell(Math.Round(categoria.TotalKgsCategoria, 3).ToString(specifier, culture));
            //            row.AddCell(Math.Round(categoria.KgsPromedioCategoria, 3).ToString(specifier, culture));
            //            table.AddRow();
            //            row.AddCell("<br/>");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell("");
            //            row.AddCell("");
            //        }
            //        table.EndTableBody();
            //    }
            //    table.StartTableBody();
            //    Row total = table.AddRow();
            //    total.AddCell("TOTAL");
            //    total.AddCell("");
            //    total.AddCell("");
            //    total.AddCell("");
            //    total.AddCell(Math.Round(container.TotalContainerPrecioPromedio, 3).ToString(specifier, culture).Insert(0, "$"));
            //    total.AddCell("");
            //    total.AddCell(container.TotalContainerCabezas.ToString());
            //    total.AddCell(Math.Round(container.TotalContainerImporte, 3).ToString(specifier, culture).Insert(0, "$"));
            //    total.AddCell(Math.Round(container.TotalContainerKgs, 3).ToString(specifier, culture));
            //    total.AddCell(Math.Round(container.TotalContainerKgsProm, 3).ToString(specifier, culture));
            //    table.EndTableBody();
            //}

            //string finishedTable = sb.ToString();
            #endregion
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

            container.TotalContainerKgs     = container.Categorias.Sum(x => x.TotalKgsCategoria);
            container.TotalContainerImporte = container.Categorias.Sum(x => x.TotalImporteCategoria);
            container.TotalContainerCabezas = container.Categorias.Sum(x => x.TotalCabezasCategoria);

            container.TotalContainerPrecioPromedio = container.TotalContainerImporte / container.TotalContainerKgs;
            container.TotalContainerKgsProm = container.TotalContainerKgs / container.TotalContainerCabezas;
        }

        //LOAD HARDCODED JSON FROM WINDOWS DISK.
        private string LoadJson()
        {
            using (StreamReader r = new StreamReader("c:\\intel\\json.json"))
            {
                string json = r.ReadToEnd();
                return json;
            }
        }
    }
}