using System;
using System.Collections.Generic;
using System.Linq;

namespace LQPractica1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Ejemplo base
            /*
            //Ej 1
            string[] niveles = { "Basico", "Intermedio", "Avanzado" };
            Int16 max = 6;
            var nc = niveles.Count();
            Console.WriteLine(nc);

            //Ej 2 no LINQ
            foreach(var n in niveles)
            {
                if (n.Length > 6)
                    Console.WriteLine(n);
            }

            //Ej 2 con LINQ
            IEnumerable<string> ns = niveles.Where(n => n.Length > max);
                //.OrderBy(n => n); //para Ej 4-1
            foreach(var n in ns)
            {
                Console.WriteLine(n);
            }
            #endregion

            #region Ejemplo Sintaxis consulta
            //Ej 3
            var qn =
            from nivel in niveles
            where nivel.Length > max
            //orderby nivel ascending //Para ej 4-1
            select nivel;
            */
            #endregion

            #region Encadenamiento de operadores
            List<Empleado> empleados = new List<Empleado>()
            {
                new Empleado {
                    Nombre = "Daniela",
                    Apellido = "Pérez",//5
                    Departamento = Departamento.Desarrollo
                },
                new Empleado {
                    Nombre = "José",
                    Apellido = "Lima Rico",//9
                    Departamento = Departamento.Admin
                },
                 new Empleado {
                    Nombre = "Fernanda",
                    Apellido = "Vega Valle",//10
                    Departamento = Departamento.Desarrollo
                },
                  new Empleado {
                    Nombre = "Fabiola",
                    Apellido = "Cortés Vázquez",//14
                    Departamento = Departamento.Desarrollo
                },
                   new Empleado {
                    Nombre = "Mónica",
                    Apellido = "Correa",//6
                    Departamento = Departamento.Soporte
                },
            };

            //Parte 0
            var em = empleados
                .Where(u => u.Departamento == Departamento.Desarrollo
                            && u.Nombre.ToLower().Contains("f"));
            var emOrdered = em.OrderBy(u => u.Id);

            //Parte 1
            var filtro = empleados
                .Where(u => u.Departamento == Departamento.Desarrollo
                            && u.Nombre.ToLower().Contains("f"))
                .OrderBy(u => u.Id)
                .Select(u => new //Parte 2
                {
                    u.Id,
                    u.Nombre,
                    InicialAp = u.Apellido.Substring(0, 1),
                    Depto = u.Departamento.ToString()
                });
            var encabezado = string.Format("{0,-40} {1,-10} {2,-10} {3}",
                            "ID", "Nombre", "Apellido", "Departamento");
            Console.WriteLine(encabezado);
            foreach (var f in filtro)
            {
                string fila = string.Format("{0,-40} {1,-10} {2,-10} {3}",
                    f.Id, f.Nombre, f.InicialAp, f.Depto);
                Console.WriteLine(fila);
            }
            #endregion

            #region Practica
            //Method syntax
            var filtroB = empleados
               .Where(e => (e.Departamento == Departamento.Desarrollo ||
                            e.Departamento == Departamento.Soporte)
                           && e.Apellido.ToLower().StartsWith("c"))
               .OrderByDescending(e => e.Nombre)
               .Select(e => new //Parte 2
               {
                   e.Nombre,
                   e.Apellido,
                   Depto = e.Departamento
               });
            var encabezadoB = string.Format("{0,-40} {1,-10} {2}",
                            "Nombre", "Apellido", "Codigo Depto");
            Console.WriteLine(encabezadoB);
            #region Ej inmediata Pte 1
            //Ej 1
            var filtroArr = filtroB.ToArray();
            //Ej 2
            var p = filtroB.FirstOrDefault();
            Console.WriteLine("/** Primero **/");
            Console.WriteLine(p.Nombre);
            #endregion
            #region Ej diferida
            /** Para 2.4 Ejecucion diferida **/
            Console.WriteLine("/** Ej. Diferida **/");
            var empleadosNuevos = new List<Empleado>
            {
                new Empleado
                {
                    Nombre = "Fabricio",
                    Apellido = "Cordero",//7
                    Departamento = Departamento.Desarrollo
                },
                new Empleado
                {
                    Nombre = "Julia",
                    Apellido = "Lombardo",//8
                    Departamento = Departamento.Admin
                },
            };
            empleados.AddRange(empleadosNuevos);

            /** Para 2.4 Ejecucion diferida **/
            #endregion

            foreach (var f in filtroB) //cambiar filtroArr por filtroArr en Ej Diferida
            {
                string fila = string.Format("{0,-40} {1,-10} {2}",
                    f.Nombre, f.Apellido, f.Depto);
                Console.WriteLine(fila);
            }
            //Query expression
            var qe = from e in empleados
                     where (e.Departamento == Departamento.Desarrollo ||
                            e.Departamento == Departamento.Soporte)
                            && e.Apellido.ToLower().StartsWith("c")
                     orderby e.Nombre descending
                     select new
                     {
                         e.Nombre,
                         e.Apellido,
                         Depto = e.Departamento
                     };
            Console.WriteLine();
            foreach (var f in qe)
            {
                string fila = string.Format("{0,-40} {1,-10} {2}",
                    f.Nombre, f.Apellido, f.Depto);
                Console.WriteLine(fila);
            }
            #endregion

            #region Subconsultas
            Console.WriteLine("/** Subconsultas **/");
            //Ej 1
            var subq = empleados.Where(e => e.Apellido.Split().LastOrDefault().StartsWith("V"));
            //Ej 1,2
            foreach (var f in subq)
            {
                string fila = string.Format("{0,-40} {1,-10} {2,-10} {3}",
                    f.Id, f.Nombre, f.Apellido, f.Departamento);
                Console.WriteLine(fila);
            }

            #endregion

            #region Práctica Subconsultas
            
            Console.WriteLine("/** Subconsultas Práctica **/");
            var nsubq = empleados.Where(e =>
                                e.Nombre.Length == empleados.OrderBy(eb => eb.Apellido.Length)
                                                            .Select(eb => eb.Apellido.Length)
                                                            .First());
            foreach (var f in nsubq)
            {
                string fila = string.Format("{0,-10} {1}",
                    f.Nombre, f.Apellido);
                Console.WriteLine(fila);
            }

            var nsubqB = from e in empleados
                        where e.Nombre.Length ==
                            (from eb in empleados
                             orderby eb.Apellido.Length
                             select eb.Apellido.Length).First()
                        select e;
            
            foreach (var f in nsubqB)
            {
                string fila = string.Format("{0,-10} {1}",
                    f.Nombre, f.Apellido);
                Console.WriteLine(fila);
            }

            //Reduccion
            var nsubqC = from e in empleados
                         where e.Nombre.Length == empleados.OrderBy(eb => eb.Apellido.Length).First().Apellido.Length
                         select e;
            foreach (var f in nsubqC)
            {
                string fila = string.Format("{0,-10} {1}",
                    f.Nombre, f.Apellido);
                Console.WriteLine(fila);
            }
            #endregion
        }
    }
}
