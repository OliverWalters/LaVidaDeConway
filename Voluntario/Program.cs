using System.Runtime.CompilerServices;

namespace Voluntario
{
    internal class Program
    {
        static void Main()
        {
            //las matrices y valores comentados con un 0 al principio eran para comprobar que esta bien hecho con uno de los bucles infinitos existentes
            const int FILAS = 50;
            const int COLUMNAS = 50;

            bool[,] matriz = new bool[FILAS, COLUMNAS];

            for (int i = 0; i < FILAS; i++) // bucle para randomizar vivas o muertas
            {
                for (int j = 0; j < COLUMNAS; j++)
                {
                    Random random = new Random();
                    if (random.Next(0, 2) == 1)
                    {
                        matriz[i, j] = false;
                    }
                    else
                    {
                        matriz[i, j] = true;
                    }
                    // 0 matriz[i, j] = false;
                }
            }
            /* 0
            matriz[2,3] = true;
            matriz[3,4] = true;
            matriz[4,2] = true;
            matriz[4,3] = true;
            matriz[4,4] = true;*/
            while (true)
            {
                bool[,] matrizResult = new bool[FILAS, COLUMNAS];
                Console.SetCursorPosition(0, 0);
                MostrarMatriz(matriz);
                for (int i = 0; i < FILAS; i++)
                {
                    for (int j = 0; j < COLUMNAS; j++)
                    {
                        if (i == 0)//fila de arriba
                        {
                            if (j == 0) //esquina arriba izquierda
                            {
                                int vecinos = CuentaVecinos(matriz, i, j, 0, 0, 2, 2);
                                matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                            }
                            else if (j == COLUMNAS - 1) //esquina arriba derecha
                            {
                                int vecinos = CuentaVecinos(matriz, i, j, 0, -1, 2, 1);
                                matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                            }
                            else
                            {
                                int vecinos = CuentaVecinos(matriz, i, j, 0, -1, 2, 2);
                                matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                            }
                        }
                        else if (i == FILAS - 1) //fila de abajo
                        {
                            if (j == 0) //esquina abajo izquierda
                            {
                                int vecinos = CuentaVecinos(matriz, i, j, -1, 0, 1, 2);
                                matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                            }
                            else if (j == COLUMNAS - 1) //esquina abajo derecha
                            {
                                int vecinos = CuentaVecinos(matriz, i, j, -1, -1, 1, 1);
                                matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                            }
                            else
                            {
                                int vecinos = CuentaVecinos(matriz, i, j, -1, -1, 1, 2);
                                matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                            }
                        }
                        else if (j == 0) //columna izquierda
                        {
                            int vecinos = CuentaVecinos(matriz, i, j, -1, 0, 2, 2);
                            matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                        }
                        else if (j == COLUMNAS - 1) //columna derecha
                        {
                            int vecinos = CuentaVecinos(matriz, i, j, -1, -1, 2, 1);
                            matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                        }
                        else //cualquiera del centro
                        {
                            int vecinos = CuentaVecinos(matriz, i, j, -1, -1, 2, 2);
                            matrizResult[i, j] = ViveOMuere(matriz[i, j], vecinos);
                        }
                    }
                }
                matriz = matrizResult;
                Thread.Sleep(250);
                Console.Clear();
            }
        }
        static void MostrarMatriz(bool[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j])
                    {
                        Console.Write('0');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }
        }
        static bool ViveOMuere(bool celula, int contador)
        {
            if (celula && (contador == 2 || contador == 3)) // si esta vivo y tiene 2 o 3 vivos alrededor sigue vivo
            {
                celula = true;
            }
            else if (!celula && contador == 3) // si esta muerto y tiene 3 vivos alrededor vive
            {
                celula = true;
            }
            else
            {
                celula = false;
            }
            return celula;
        }
        static int CuentaVecinos(bool[,] matriz, int iContador, int jContador, int fila, int columna, int condicionFilas, int condicionColumnas)
        {
            int vivos = 0;
            int _columna = columna;//para que se resete d para que no solo lo haga la primera vez (linea 156)
            while (fila < condicionFilas)
            {
                while (_columna < condicionColumnas)
                {
                    bool a = matriz[iContador + fila, jContador + _columna];
                    if (iContador + fila == iContador && jContador + _columna == jContador) //da un valor erroneo al comparar el propio para no contarlo como relleno
                    {
                        a = false;
                    }

                    if (a) //cuenta los rellenos
                    {
                        vivos++;
                    }
                    _columna++;
                }
                _columna = columna;
                fila++;
            }
            return vivos;
        }
    }
}