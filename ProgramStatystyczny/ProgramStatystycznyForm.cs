using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProgramStatystyczny
{
    public partial class ProgramStatystycznyForm : Form
    {
        int[] tablicaLiczb;


        public ProgramStatystycznyForm()
        {
            InitializeComponent();
        }

        private void btnGeneruj_Click(object sender, EventArgs e)
        {
            int min = int.Parse(txtMin.Text);
            int max = int.Parse(txtMax.Text);
            int ilosc = int.Parse(txtIlosc.Text);

            int[] wylosowaneLiczby = GenerujCiagLiczb(min, max, ilosc);

            tablicaLiczb = wylosowaneLiczby;

            txtCiagN.Text = string.Join(" ", wylosowaneLiczby);

            cbxCiagN.DataSource = wylosowaneLiczby;
        }
        int[] GenerujCiagLiczb(
            int minimalnaLiczba,
            int maksymalnaLiczba,
            int iloscLiczb)
        {
            int[] tablica = new int[iloscLiczb];
            Random generator = new Random();

            for (int i = 0; i < tablica.Length; i++)
            {
                tablica[i] = generator.Next(minimalnaLiczba, maksymalnaLiczba);
            }

            return tablica;
        }

        private void btnSortowanieB_Click(object sender, EventArgs e)
        {
            txtCiagB.Clear();

            int[] kopiaDlaSortowaniaBabelkowego = (int[])tablicaLiczb.Clone();

            sortujBabelkowo(kopiaDlaSortowaniaBabelkowego);

            txtCiagB.Text = string.Join(" ", kopiaDlaSortowaniaBabelkowego);

        }
        void sortujBabelkowo(int[] tablicaLiczb)
        {
            for (int i = 0; i < tablicaLiczb.Length; i++)
            {
                for (int j = i + 1; j < tablicaLiczb.Length; j++)
                {
                    if (tablicaLiczb[i] > tablicaLiczb[j])
                    {
                        int temp = tablicaLiczb[i];

                        tablicaLiczb[i] = tablicaLiczb[j];

                        tablicaLiczb[j] = temp;
                    }
                }
            }
        }

        void sortujPrzezWstawianie(int[] tablicaLiczb)
        {
            for (int i = 0; i < tablicaLiczb.Length; i++)
            {
                int j = i;
                var temp = tablicaLiczb[j];
                while (j > 0 && tablicaLiczb[j - 1] > temp)
                {
                    tablicaLiczb[j] = tablicaLiczb[j - 1];
                    j--;
                }
                tablicaLiczb[j] = temp;
            }
        }

        private void btnSortowanieW_Click(object sender, EventArgs e)
        {
            txtCiagW.Clear();

            int[] kopiaDlaSortowania = (int[])tablicaLiczb.Clone();

            sortujPrzezWstawianie(kopiaDlaSortowania);

            txtCiagW.Text = string.Join(" ", kopiaDlaSortowania);
        }
        void sortujQuicksort(int[] tablicaLiczb, int L, int R)
        {
            int i = L;
            int j = R;
            int pv = tablicaLiczb[(L + R) / 2];

            while (i <= j)
            {
                while (tablicaLiczb[i] < pv)
                    i++;
                while (tablicaLiczb[j] > pv)
                    j--;

                if (i <= j)
                {
                    int tmp = tablicaLiczb[i];
                    tablicaLiczb[i++] = tablicaLiczb[j];
                    tablicaLiczb[j--] = tmp;
                }
            }
            if (j > L)
            {
                sortujQuicksort(tablicaLiczb, L, j);
            }
            if (i < R)
            {
                sortujQuicksort(tablicaLiczb, i, R);
            }
        }

        private void btnSortowanieQ_Click(object sender, EventArgs e)
        {
            txtCiagQ.Clear();

            int[] kopiaDlaSortowania = (int[])tablicaLiczb.Clone();

            sortujQuicksort(kopiaDlaSortowania, 0, kopiaDlaSortowania.Length - 1);

            txtCiagQ.Text = string.Join(" ", kopiaDlaSortowania);
        }
        int znajdźPozycjęWCiągu(int[] tablicaliczb, int liczbaszukana)
        {
            int i;
            for ( i = 0; i < tablicaliczb.Length; i++)
            {
               if(tablicaliczb[i] == liczbaszukana)
                {
                    break;
                }
                else
                {
                    continue;
                }
               
            }
            return i;
            
            
            
        }

        private void btnSzukanie_Click(object sender, EventArgs e)
        {

            int szukana = int.Parse(txtSzukana.Text);
            int[] kopiaDlaSortowania = (int[])tablicaLiczb.Clone();
            sortujPrzezWstawianie(kopiaDlaSortowania);
            int pozycjaCiąg = znajdźPozycjęWCiągu(kopiaDlaSortowania, szukana);
            pozycjaCiąg = pozycjaCiąg + 1;
            txtPozycja.Text = Convert.ToString(pozycjaCiąg);
        }
        double średnia(int[] tablica) 
        {
            int y = 0;
            double średniaWynik = 0;
            for (int i =0; i < tablica.Length; i++) 
            {
                y = y + tablica[i];
                średniaWynik = y / tablica.Length;
            }
            return średniaWynik;

        }
        double mediana(int[] tablica) 
        {
            Array.Sort(tablica);

            if (tablica.Length % 2 == 0)
                return (tablica[tablica.Length / 2 - 1] + tablica[tablica.Length / 2]) / 2;
            else
                return tablica[tablica.Length / 2];
        }
        int dominanta(int[] tablica) 
        {
            int[] ciąg = new int[tablica.Length];
            for (int i = 0; i < tablica.Length; i++) 
            {
                int y = 0;
                for (int k = 0; k < tablica.Length; k++) 
                {
                    if (tablica[i] == tablica[k]) 
                    {
                        y = y + 1;
                    }
                }
                ciąg[i] = y;
            }
            int poz = znajdźPozycjęWCiągu(ciąg, ciąg.Max());
            return tablica[poz];
        }

        private void txtWylicz_Click(object sender, EventArgs e)
        {
            int[] kopiaDlaObliczeń = (int[])tablicaLiczb.Clone();
            double średniaOblicz = średnia(kopiaDlaObliczeń);
            txtSrednia.Text = Convert.ToString(średniaOblicz);
            double medianaOblicz = mediana(kopiaDlaObliczeń);
            txtMediana.Text = Convert.ToString(medianaOblicz);
            int dominantaOblicz = dominanta(kopiaDlaObliczeń);
            txtDominanta.Text = Convert.ToString(dominantaOblicz);
        }
    }
}

