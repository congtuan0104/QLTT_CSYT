﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QLTT_CSYT
{
    /// <summary>
    /// Interaction logic for Admin_Dashboard.xaml
    /// </summary>
    public partial class Admin_Dashboard : Window
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void btnPH2_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            for (int i = 0; i < Application.Current.Windows.Count - 1; i++)
                Application.Current.Windows[i].Close();           
            menu.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
