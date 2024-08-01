using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace CajeroAutomatico
{
    class Program
    {
        public static string usuarioActual;
        private static string archivoUsuarios = "C:\\Users\\Windows\\source\\repos\\Cajero\\Cajero\\bin\\Debug\\Listas.txt";
        private static string archivoMovimientosUsuario ;

        public static void Main(string[] args)
        {
            Inicio();

        }

        public static void Inicio()
        {
            Console.WriteLine("Ingrese ID de cuenta ");
            string usuario = Console.ReadLine();
            Console.WriteLine("ingrese Contraseña");
            string clave = Console.ReadLine();


            if (ValidarCredenciales(usuario, clave))
            {
                usuarioActual = usuario;
                Menu();
            }
            else
            {
                Console.WriteLine("ID de cuenta o Contraseña incorrecta, Intentelo de nuevo");

            }
        }

        private static bool ValidarCredenciales(string usuario, string clave)
        {
            string[] usuarios = File.ReadAllLines(archivoUsuarios);
            foreach (string linea in usuarios)
            {
                string[] datos = linea.Split(',');
                if (datos[0] == usuario && datos[1] == clave  )
                {
                    return true;
                }
            }
            return false;
        }

        private static void Menu()
        {
            while (true)
            {
                Console.WriteLine("1. Retirar");
                Console.WriteLine("2. Depositar");
                Console.WriteLine("3. Cambiar clave");
                Console.WriteLine("4. Consultar saldo");
                Console.WriteLine("5. Ver Movimientos");
                Console.WriteLine("6. Salir");

                int operacion = int.Parse(Console.ReadLine());

                switch (operacion)
                {
                    case 1:
                        RetirarDinero();
                        break;
                    case 2:
                        DepositarDinero();
                        break;
                    case 3:
                        CambioClave();
                        break;
                    case 4:
                        ConsultarSaldo();
                        break;
                    case 5:
                        VerMovimientos();
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del programa...");
                        return;
                     default:
                        Console.WriteLine("Opcion no valida. Intenta de nuevo");
                        break;
                        
                }



            }

        }
        private static void ConsultarSaldo()
        {
            string[] usuarios = File.ReadAllLines(archivoUsuarios);
            foreach (string linea in usuarios)
            {
                string[] datos = linea.Split(',');
                if (datos[0] == usuarioActual)
                {
                    Console.WriteLine($"su saldo es: { datos[2]}");
                }
            }
        }

        private static void RetirarDinero()
        {
            Console.WriteLine("Ingrese la cantidad a retirar");
            double cantidad = Convert.ToDouble(Console.ReadLine());

            RegistrarMovimiento(usuarioActual, $"Retiro de ${cantidad}");

            List<string> lineas = new List<string>();
            string[] usuarios = File.ReadAllLines(archivoUsuarios);
            foreach(string linea in usuarios)
            {
                string[] datos = linea.Split(',');
                if (datos[0] == usuarioActual)
                {
                    double saldo = Convert.ToDouble(datos[2]);
                    if (cantidad <= saldo)
                    {
                        saldo -= cantidad;
                        datos[2] = saldo.ToString();
                        Console.WriteLine($"Ha retidardo {cantidad}. Su nuevo saldo es {saldo}");
                    }
                    else
                    {
                        Console.WriteLine("Saldo insuficiente");
                    }
                }
                lineas.Add(string.Join(",", datos));
            }
            File.WriteAllLines(archivoUsuarios, lineas);
        }


        private static void DepositarDinero()
        {
            Console.WriteLine("Ingrese la cantidad a depositar:");
            double cantidad = Convert.ToDouble(Console.ReadLine());

            RegistrarMovimiento(usuarioActual, $"Deposito de ${cantidad}");

            List<string> lineas = new List<string>();
            string[] usuarios = File.ReadAllLines(archivoUsuarios);
            foreach (string linea in usuarios)
            {
                string[] datos = linea.Split(',');
                if (datos[0] == usuarioActual)
                {
                    double saldo = Convert.ToDouble(datos[2]);
                    saldo += cantidad;
                    datos[2] = saldo.ToString();
                    Console.WriteLine($"Ha depositado {cantidad}. Su nuevo saldo es {saldo}");
                }
                lineas.Add(string.Join(",", datos));
            }
            File.WriteAllLines(archivoUsuarios, lineas);
        }

        private static void CambioClave()
        {
            Console.WriteLine("Ingrese su nueva contrasena");
            string nuevaClave = Console.ReadLine();

            List<string> lineas = new List<string>();
            string[] usuarios = File.ReadAllLines(archivoUsuarios);
            foreach (string linea in usuarios)
            {
                string[] datos = linea.Split(",");
                if (datos[0] == usuarioActual)
                {
                    datos[1] = nuevaClave;
                    Console.WriteLine("Contrasena cambiada exitosamente.");
                }
                lineas.Add(string.Join(",", datos));
            }
            File.WriteAllLines(archivoUsuarios, lineas);
        }

        private static void RegistrarMovimiento(string usuario, string movimiento)
        {
            archivoMovimientosUsuario = $"{usuario}_Movimientos.txt";

            if (!File.Exists(archivoMovimientosUsuario))
            {
                using (StreamWriter escriba = File.CreateText(archivoMovimientosUsuario))
                {
                    escriba.WriteLine($"{DateTime.Now} - {movimiento} - {usuarioActual}");
                }
            }

            using (StreamWriter escriba = File.AppendText(archivoMovimientosUsuario))
            {
                escriba.WriteLine($"{DateTime.Now} - {movimiento} - {usuarioActual}");
            }
        }

        private static void VerMovimientos()
        {
            archivoMovimientosUsuario = $"{usuarioActual}_Movimientos.txt";

            if (File.Exists(archivoMovimientosUsuario))
            {
                string[] movimientos = File.ReadAllLines (archivoMovimientosUsuario);
                foreach (string movimiento in movimientos)
                {
                    Console.WriteLine (movimiento);
                }
            }
            else
            {
                Console.WriteLine("No hay movimientos para mostrar");
            }
        }
    }
    
}