using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TestingApplication.Models
{
    public partial class UIProperties : Control
    {
        public static readonly DependencyProperty BorderThicknessProperty =
        DependencyProperty.Register(
            "BorderThickness",
            typeof(Thickness),
            typeof(TextBox));

        public Thickness BorderThickness
        {
            get => (Thickness)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
    }
}