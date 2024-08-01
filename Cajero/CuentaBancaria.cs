﻿using System;
using System.Collections.Generic;
using System.IO;

namespace CajeroAutomatico
{
    public class CuentaBancaria
    {
        public string Usuario { get; private set; }
        public string Clave { get; private set; }
        public double Saldo { get; private set; }
        private static string archivoUsuarios = "C:\\Users\\Windows\\source\\repos\\Cajero\\Cajero\\bin\\Debug\\Listas.txt";
        private string archivoMovimientosUsuario;

        public CuentaBancaria(string usuario, string clave, double saldo)
        {
            Usuario = usuario;
            Clave = clave;
            Saldo = saldo;
            archivoMovimientosUsuario = $"{usuario}_Movimientos.txt";
        }

        public bool ValidarCredenciales(string usuario, string clave)
        {
            return Usuario == usuario && Clave == clave;
        }


        public void CambiarClave(string nuevaClave)
        {
            Clave = nuevaClave;
            List<string> lineas = new List<string>();
            string[] usuarios = File.ReadAllLines(archivoUsuarios);
            foreach (string linea in usuarios)
            {
                string[] datos = linea.Split(",");
                if (datos[0] == Usuario)
                {
                    datos[1] = nuevaClave;
                    Console.WriteLine("Contraseña cambiada exitosamente.");
                }
                lineas.Add(string.Join(",", datos));
            }
            File.WriteAllLines(archivoUsuarios, lineas);
        }
    }
}
