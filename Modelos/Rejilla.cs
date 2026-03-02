using System;

namespace IPC2_Proyecto1_2020XXXX.Modelos
{
    public class Rejilla
    {
        public int M { get; set; }
        public Celda[,] Matriz { get; set; }

        public Rejilla(int m)
        {
            M = m;
            Matriz = new Celda[m, m];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < m; j++)
                    Matriz[i, j] = new Celda();
        }

        public Rejilla ProximoPeriodo()
        {
            Rejilla siguiente = new Rejilla(M);
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    int infectados = ContarVecinos(i, j);
                    bool esta = Matriz[i, j].Infectada;
                    if (esta)
                    {
                        siguiente.Matriz[i, j].Infectada = infectados == 2 || infectados == 3;
                    }
                    else
                    {
                        siguiente.Matriz[i, j].Infectada = infectados == 3;
                    }
                }
            }
            return siguiente;
        }

        private int ContarVecinos(int fila, int col)
        {
            int cont = 0;
            for (int di = -1; di <= 1; di++)
            {
                for (int dj = -1; dj <= 1; dj++)
                {
                    if (di == 0 && dj == 0) continue;
                    int ni = fila + di;
                    int nj = col + dj;
                    if (ni >= 0 && ni < M && nj >= 0 && nj < M)
                    {
                        if (Matriz[ni, nj].Infectada) cont++;
                    }
                }
            }
            return cont;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    sb.Append(Matriz[i, j].Infectada ? '1' : '0');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
