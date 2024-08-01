using System;

namespace CajeroAutomatico
{
    class Program
    {
        static CuentaBancaria cuenta;

        static void Main(string[] args)
        {
            Inicio();
        }

        public static void Inicio()
        {
            Console.WriteLine("Ingrese ID de cuenta: ");
            string usuario = Console.ReadLine();
            Console.WriteLine("Ingrese Contraseña: ");
            string clave = Console.ReadLine();

            if (cuenta == null)
            {
                string[] usuarios = System.IO.File.ReadAllLines("C:\\Users\\Windows\\source\\repos\\Cajero\\Cajero\\bin\\Debug\\Listas.txt");
                foreach (string linea in usuarios)
                {
                    string[] datos = linea.Split(',');
                    if (datos[0] == usuario && datos[1] == clave)
                    {
                        cuenta = new CuentaBancaria(datos[0], datos[1], Convert.ToDouble(datos[2]));
                        break;
                    }
                }
            }

            if (cuenta != null && cuenta.ValidarCredenciales(usuario, clave))
            {
                Menu();
            }
            else
            {
                Console.WriteLine("ID de cuenta o Contraseña incorrecta, Intentelo de nuevo");
            }
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
                        Console.WriteLine("Ingrese la cantidad a retirar:");
                        double cantidadRetiro = Convert.ToDouble(Console.ReadLine());
                        cuenta.Retirar(cantidadRetiro);
                        break;
                    case 2:
                        Console.WriteLine("Ingrese la cantidad a depositar:");
                        double cantidadDeposito = Convert.ToDouble(Console.ReadLine());
                        cuenta.Depositar(cantidadDeposito);
                        break;
                    case 3:
                        Console.WriteLine("Ingrese su nueva contrasena:");
                        string nuevaClave = Console.ReadLine();
                        cuenta.CambiarClave(nuevaClave);
                        break;
                    case 4:
                        cuenta.ConsultarSaldo();
                        break;
                    case 5:
                        cuenta.VerMovimientos();
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
    }
}