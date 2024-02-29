using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using TestingApplication;
using TestingApplication.Models;

namespace Win11ThemeTest
{
    [TestFixture]
    public class TextTest
    {
        private Application app;
        private Window mainWindow;
        private Window textWindow;
        TextBox textBox;
        TextBox disabledTextBox;
        Button txtButton;

        public TextTest()
        {
            app = Application.Launch(@"..\\..\\..\\..\\TestingApplication\\bin\\Debug\\net9.0-windows\\win-x64\\TestingApplication.exe");

            using (var automation = new UIA3Automation())
            {
                mainWindow = app.GetMainWindow(automation);
                txtButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("txtBoxButton")).AsButton();
                Mouse.Click(txtButton.GetClickablePoint());
                Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(500));
                textWindow = mainWindow.FindFirstDescendant(cf => cf.ByName("TextWindow")).AsWindow();
                textBox = textWindow.FindFirstDescendant(cf => cf.ByAutomationId("tbTxt")).AsTextBox();
                var textBox1 = textWindow.FindFirstDescendant(cf => cf.ByAutomationId("tbTxt")).AsTextBox();
                disabledTextBox = textWindow.FindFirstDescendant(cf => cf.ByAutomationId("tbTxt_disabled")).AsTextBox();
            }
        }

        [Test]
        public void TextSelection()
        {
            UIProperties properties = new UIProperties();
            var borderThickness = properties.BorderThickness;

            Console.WriteLine();
            textBox.Enter("Hello World!");
            Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(500));
            textWindow = mainWindow.FindFirstDescendant(cf => cf.ByName("TextWindow")).AsWindow();
        }
    }
}
