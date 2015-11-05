

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using StockTraderRI.Infrastructure.Behaviors;
using System;

namespace StockTraderRI.Infrastructure
{
    public static class ReturnKey
    {
        /// <summary>
        /// Command to execute on return key event.
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
            "Command",
            typeof(ICommand),
            typeof(ReturnKey),
            new PropertyMetadata(OnSetCommandCallback));

        /// <summary>
        /// Default text to set to the TextBox once the Command has been executed
        /// </summary>
        public static readonly DependencyProperty DefaultTextAfterCommandExecutionProperty = DependencyProperty.RegisterAttached(
            "DefaultTextAfterCommandExecution",
            typeof(string),
            typeof(ReturnKey),
            new PropertyMetadata(OnSetDefaultTextAfterCommandExecutionCallback));

        private static readonly DependencyProperty ReturnCommandBehaviorProperty = DependencyProperty.RegisterAttached(
            "ReturnCommandBehavior",
            typeof(ReturnCommandBehavior),
            typeof(ReturnKey),
            null);

        /// <summary>
        /// Sets the <see cref="string"/> to set to the TextBox once the Command has been executed.
        /// </summary>
        /// <param name="textBox">TextBox dependency object on which the default text will be set after the Command has been executed.</param>
        /// <param name="defaultText">Default text to set.</param>
        public static void SetDefaultTextAfterCommandExecution(TextBox textBox, string defaultText)
        {
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }

            textBox.SetValue(DefaultTextAfterCommandExecutionProperty, defaultText);
        }

        /// <summary>
        /// Retrieves the default text set to the <see cref="TextBox"/> after the Command has been executed.
        /// </summary>
        /// <param name="textBox">TextBox dependency object on which the default text will be set after the Command has been executed.</param>
        /// <returns>Default text to set.</returns>
        public static string GetDefaultTextAfterCommandExecution(TextBox textBox)
        {
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }

            return textBox.GetValue(DefaultTextAfterCommandExecutionProperty) as string;
        }

        /// <summary>
        /// Sets the <see cref="ICommand"/> to execute on the return key event.
        /// </summary>
        /// <param name="textBox">TextBox dependency object to attach command</param>
        /// <param name="command">Command to attach</param>
        public static void SetCommand(TextBox textBox, ICommand command)
        {
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }

            textBox.SetValue(CommandProperty, command);
        }

        /// <summary>
        /// Retrieves the <see cref="ICommand"/> attached to the <see cref="TextBox"/>.
        /// </summary>
        /// <param name="textBox">TextBox containing the Command dependency property</param>
        /// <returns>The value of the command attached</returns>
        public static ICommand GetCommand(TextBox textBox)
        {
            if (textBox == null)
            {
                throw new ArgumentNullException("textBox");
            }

            return textBox.GetValue(CommandProperty) as ICommand;
        }

        private static void OnSetDefaultTextAfterCommandExecutionCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = dependencyObject as TextBox;
            if (textBox != null)
            {
                ReturnCommandBehavior behavior = GetOrCreateBehavior(textBox);
                behavior.DefaultTextAfterCommandExecution = e.NewValue as string;
            }
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = dependencyObject as TextBox;
            if (textBox != null)
            {
                ReturnCommandBehavior behavior = GetOrCreateBehavior(textBox);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static ReturnCommandBehavior GetOrCreateBehavior(TextBox textBox)
        {
            ReturnCommandBehavior behavior = textBox.GetValue(ReturnCommandBehaviorProperty) as ReturnCommandBehavior;
            if (behavior == null)
            {
                behavior = new ReturnCommandBehavior(textBox);
                textBox.SetValue(ReturnCommandBehaviorProperty, behavior);
            }

            return behavior;
        }
    }
}