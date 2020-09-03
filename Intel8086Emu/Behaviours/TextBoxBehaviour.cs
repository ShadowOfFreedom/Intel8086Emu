using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Intel8086Emu.Behaviours
{
    public static class TextBoxBehaviour
    {
        public static bool GetKeepCursorPosition(DependencyObject obj)
        {
            return (bool)obj.GetValue(KeepCursorPositionProperty);
        }

        public static void SetKeepCursorPosition(DependencyObject obj, bool value)
        {
            obj.SetValue(KeepCursorPositionProperty, value);
        }

        public static readonly DependencyProperty KeepCursorPositionProperty =
            DependencyProperty.RegisterAttached("KeepCursorPosition", typeof(bool), typeof(TextBoxBehaviour), new UIPropertyMetadata(false, KeepCursorPosition));


        public static int GetPreviousCaretIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(PreviousCaretIndexProperty);
        }

        public static void SetPreviousCaretIndex(DependencyObject obj, int value)
        {
            obj.SetValue(PreviousCaretIndexProperty, value);
        }

        public static readonly DependencyProperty PreviousCaretIndexProperty =
            DependencyProperty.RegisterAttached("PreviousCaretIndex", typeof(int), typeof(TextBoxBehaviour), new UIPropertyMetadata(0));


        public static string GetPreviousTextValue(DependencyObject obj)
        {
            return (string)obj.GetValue(PreviousTextValueProperty);
        }

        public static void SetPreviousTextValue(DependencyObject obj, string value)
        {
            obj.SetValue(PreviousTextValueProperty, value);
        }

        public static readonly DependencyProperty PreviousTextValueProperty =
            DependencyProperty.RegisterAttached("PreviousTextValue", typeof(string), typeof(TextBoxBehaviour), new UIPropertyMetadata(null));

        private static void KeepCursorPosition(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.PreviewKeyDown += textBox_PreviewKeyDown;
                textBox.TextChanged += textBox_TextChanged;
                textBox.Unloaded += textBox_Unloaded;
            }
            else
            {
                throw new ArgumentException("KeepCursorPosition only available for textboxes");
            }
        }

        static void textBox_Unloaded(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.PreviewKeyDown -= textBox_PreviewKeyDown;
            textBox.TextChanged -= textBox_TextChanged;
            textBox.Unloaded -= textBox_Unloaded;
        }


        static void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var previousIndex = GetPreviousCaretIndex(textBox);
            var previousText = GetPreviousTextValue(textBox);

            var previousLen = !string.IsNullOrEmpty(previousText) ? previousText.Length : 0;
            if (textBox != null)
            {
                var currentLen = textBox.Text.Length;
                var change = (currentLen - previousLen);

                var newCharIndex = Math.Max(1, (previousIndex + change));

                Debug.WriteLine("Text Changed Previous Caret Pos : {0}", previousIndex);
                Debug.WriteLine("Text Changed Change : {0}", change);
                Debug.WriteLine("Text Changed New Caret Pos : {0}", newCharIndex);

                textBox.CaretIndex = Math.Max(newCharIndex, previousIndex);
            }

            SetPreviousCaretIndex(textBox, textBox.CaretIndex);
            SetPreviousTextValue(textBox, textBox.Text);
        }

        static void textBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                Debug.WriteLine("Key Preview Caret Pos : {0}", textBox.CaretIndex);
                Debug.WriteLine("------------------------");
                SetPreviousCaretIndex(textBox, textBox.CaretIndex);
                SetPreviousTextValue(textBox, textBox.Text);
            }
        }
    }
}
