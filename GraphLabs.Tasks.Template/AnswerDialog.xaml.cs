using System.Windows;
using System.Windows.Controls;

namespace GraphLabs.Tasks.Template
{
    public partial class AnswerDialog : ChildWindow
    {
        private readonly bool _inters;

        public AnswerDialog(bool b)
        {
            InitializeComponent();

            _inters = b;

            label.Name = _inters ? "OK!" : "Совместите графы";

            ans_positive.Checked += (s, a) =>
            {
                label.Name = _inters ? "OK!" : "Совместите графы";
                textBox.IsReadOnly = true;
            };
            ans_negative.Checked += (s, a) =>
            {
                label.Name = "Опишите причину в поле ввода";
                textBox.IsReadOnly = false;
            };
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (
                ans_positive.IsChecked.HasValue && ans_positive.IsChecked.Value && _inters ||
                ans_negative.IsChecked.HasValue && ans_negative.IsChecked.Value && textBox.Text.Length != 0)
            {
                //TODO: Выдать текст ответа

                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

