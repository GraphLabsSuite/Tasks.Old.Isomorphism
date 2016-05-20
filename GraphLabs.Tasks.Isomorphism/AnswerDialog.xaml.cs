using System.Windows;
using System.Windows.Controls;

namespace GraphLabs.Tasks.Isomorphism
{
    public partial class AnswerDialog : ChildWindow
    {
        private readonly bool _inters;

        public AnswerDialog(bool b)
        {
            InitializeComponent();

            _inters = b;
            OKButton.IsEnabled = _inters;

            infoBlock.Text = _inters ? "OK!" : "Совместите графы";

            ans_positive.Checked += (s, a) =>
            {
                infoBlock.Text = _inters ? "OK!" : "Совместите графы";
                textBox.IsReadOnly = true;
                OKButton.IsEnabled = _inters;
            };
            ans_negative.Checked += (s, a) =>
            {
                infoBlock.Text = "Опишите причину в поле ввода";
                textBox.IsReadOnly = false;
                OKButton.IsEnabled = textBox.Text.Length > 0;
            };
            textBox.TextChanged += (sender, args) =>
            {
                OKButton.IsEnabled = textBox.Text.Length > 0;
            };
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (ans_positive.IsChecked.HasValue && ans_positive.IsChecked.Value && _inters)
            {
                DialogResult = true;
            }
            if (ans_negative.IsChecked.HasValue && ans_negative.IsChecked.Value && textBox.Text.Length > 0)
            {
                DialogResult = true;
            }
        }

        public string Message => textBox.Text;

        public bool Answer => ans_positive.IsChecked.HasValue && ans_positive.IsChecked.Value;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

