using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using NUnit.Framework.Internal;
using System.Configuration;
using System.Drawing;

namespace Win11ThemeTest
{
    public class ButtonTest
    {
        private readonly Application? app;
        private readonly Window? window;
        public Window? btnWindow;
        readonly Button? testButton;
        readonly Button? button;
        readonly Button? disabledButton;

        public ButtonTest()
        {
            try
            {
                var appPath = ConfigurationManager.AppSettings["Testpath"];
                app = Application.Launch(appPath);
                using var automation = new UIA3Automation();
                window = app.GetMainWindow(automation);
                testButton = window.FindFirstDescendant(cf => cf.ByAutomationId("testbtn")).AsButton();
                Mouse.Click(testButton.GetClickablePoint());
                Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(2000));
                btnWindow = window.FindFirstDescendant(cf => cf.ByName("ButtonWindow")).AsWindow();
                button = btnWindow.FindFirstDescendant(cf => cf.ByAutomationId("btn")).AsButton();
                disabledButton = btnWindow.FindFirstDescendant(cf => cf.ByAutomationId("disbtn")).AsButton();
            }
            catch (Exception ex)
            {
                var filePath = ConfigurationManager.AppSettings["logpath"];
                if (filePath != null)
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    filePath = filePath + "log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";   //Text File Name
                    if (!File.Exists(filePath))
                    {
                        File.Create(filePath).Dispose();
                    }
                    using StreamWriter sw = File.AppendText(filePath);
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + "\nError Message:" + " " + ex.Message.ToString();
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(error);
                    sw.Flush();
                    sw.Close();
                }
                else
                {
                    throw new ArgumentNullException();
                }

            }
        }

        //test if button is available in window
        [Test]
        public void Button1_isButtonAvailable()
        {
            Assert.Multiple(() =>
            {
                Assert.That(btnWindow, Is.Not.Null);

                Assert.That(button, Is.Not.Null);
            });
        }

        //test if button is clicked
        [Test]
        public void Button2_isClicked()
        {
            Assert.That(button, Is.Not.Null);
            button.Click();
            Wait.UntilInputIsProcessed();
            Assert.That(btnWindow, Is.Not.Null);
            var popup = btnWindow.FindFirstDescendant(cf => cf.ByName("Button Clicked")).AsWindow();
            Assert.That(popup, Is.Not.Null);
            Button pBtn = btnWindow.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
            pBtn.Click();
        }

        //test if button clicked with enter key
        [Test]
        public void Button3_isClickableWithEnterKey()
        {
            Assert.That(button, Is.Not.Null);
            button.Focus();
            Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            Keyboard.Release(FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            Assert.That(btnWindow, Is.Not.Null);
            var popup = btnWindow.FindFirstDescendant(cf => cf.ByName("Button Clicked")).AsWindow();
            Assert.That(popup, Is.Not.Null);
            Button pBtn = btnWindow.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
            pBtn.Click();
        }

        //test if button clicked with space key
        [Test]
        public void Button4_isClickableWithSpaceKey()
        {
            Assert.That(button, Is.Not.Null);
            button.Focus();
            Keyboard.Press(FlaUI.Core.WindowsAPI.VirtualKeyShort.SPACE);
            Keyboard.Release(FlaUI.Core.WindowsAPI.VirtualKeyShort.SPACE);
            Wait.UntilInputIsProcessed();
            Assert.That(btnWindow, Is.Not.Null);
            var popup = btnWindow.FindFirstDescendant(cf => cf.ByName("Button Clicked")).AsWindow();
            Assert.That(popup, Is.Not.Null);
            Button pBtn = btnWindow.FindFirstDescendant(cf => cf.ByName("OK")).AsButton();
            pBtn.Click();
        }

        //test no action on mouse right click on button
        [Test]
        public void Button5_onMouseRightClick()
        {
            Assert.That(button, Is.Not.Null);
            button.RightClick();
            Assert.That(btnWindow, Is.Not.Null);
            var popup = btnWindow.FindFirstDescendant(cf => cf.ByName("Button Clicked")).AsWindow();
            Assert.That(popup, Is.Null);
        }

        //Test disabled button
        [Test]
        public void Button6_isDisabled()
        {
            Assert.That(disabledButton, Is.Not.Null);
            Assert.That(disabledButton.IsEnabled, Is.False);
        }

        //Test disabled button
        [Test]
        public void Button7_isDisabledClick()
        {
            Assert.That(disabledButton, Is.Not.Null);
            Assert.That(disabledButton.IsEnabled, Is.False);
            disabledButton.Click();
            var popup = disabledButton.FindFirstDescendant(cf => cf.ByName("Button Clicked")).AsWindow();
            Assert.That(popup, Is.Null);
        }

        #region UIApperancetestcases
        //test if button is available in window
        [Test]
        public void Button1_basicButtonHasWin11Theme()
        {
            Assert.Multiple(() =>
            {
                Assert.That(btnWindow, Is.Not.Null);
                Assert.That(button, Is.Not.Null);
            });
            string expectedPath = "D:\\Win11ThemeCode\\menuTest\\Win11ThemeSampleApp\\Win11ThemeTest\\Expected\\Button\\basicButton_screenshot.png";      
            string filePath = "D:\\Win11ThemeCode\\menuTest\\Win11ThemeSampleApp\\Win11ThemeTest\\Result\\Button\\" + "basicButton_screenshot.png";
            CaptureElementScreenshot(filePath);
            CompareImages(filePath, expectedPath);
        }

        [Test]
        public void Button1_buttonMouseHoverWin11Theme()
        {           
            Assert.Multiple(() =>
            {
                Assert.That(btnWindow, Is.Not.Null);

                Assert.That(button, Is.Not.Null);
            });
            Mouse.MoveTo(button.GetClickablePoint());
             string expectedPath = "D:\\Win11ThemeCode\\menuTest\\Win11ThemeSampleApp\\Win11ThemeTest\\Expected\\Button\\mouseHoverButton_screenshot.png";
            string filePath = "D:\\Win11ThemeCode\\menuTest\\Win11ThemeSampleApp\\Win11ThemeTest\\Result\\Button\\" + "mouseHoverButton_screenshot.png";
            CaptureElementScreenshot(filePath);
            CompareImages(filePath, expectedPath);
        }

        private void CaptureElementScreenshot(string filePath)
        {           
            var buttons = button;
            Wait.UntilInputIsProcessed();
            
            if (buttons != null)
            {
                // Get the bounding rectangle of the button
                var rect = buttons.BoundingRectangle;

                // Capture the screenshot of the button
                using (var bmp = new System.Drawing.Bitmap((int)rect.Width, (int)rect.Height))
                {
                    using (var g = System.Drawing.Graphics.FromImage(bmp))
                    {
                        g.CopyFromScreen(new System.Drawing.Point((int)rect.Left, (int)rect.Top), System.Drawing.Point.Empty, new System.Drawing.Size((int)rect.Width, (int)rect.Height));
                    }

                    // Optionally, save or process the captured screenshot                    
                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine($"Screenshot captured and saved to: {Path.GetFullPath(filePath)}");
                }
            }
            else
            {
                Console.WriteLine("Button not found.");
            }           
        }

        
        private void CompareImages(string resultPath, string expectedPath)
        {            
            // Load the PNG image files         
            using (var image1 = new Bitmap(expectedPath))
            using (var image2 = new Bitmap(resultPath))
            {
                // Compare the pixel values of the images
                bool areEqual = AreImagesEqual(image1, image2);

                // Determine if the images are identical
                if (areEqual)
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            //DeleteFileButton_Click();
        }

        private bool AreImagesEqual(Bitmap image1, Bitmap image2)
        {
            // Check if images have the same dimensions
            if (image1.Width != image2.Width || image1.Height != image2.Height)
            {
                return false;
            }

            // Compare pixel values of the images
            for (int x = 0; x < image1.Width; x++)
            {
                for (int y = 0; y < image1.Height; y++)
                {
                    if (image1.GetPixel(x, y) != image2.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void DeleteFileButton_Click()
        {
            string filePath = "D:\\Win11ThemeCode\\menuTest\\Win11ThemeSampleApp\\Win11ThemeTest\\Result\\button_screenshot.png"; // Replace with the path of the file you want to delete

            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);                   
                }
                else
                {
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");                
            }
        }

        #endregion
        //close windows
        [Test]
        public void Button91_closeWindows()
        {
            Assert.That(btnWindow, Is.Not.Null);
            btnWindow.Close();
            Wait.UntilInputIsProcessed();
            Assert.That(btnWindow.IsOffscreen);
            Wait.UntilInputIsProcessed();
            Assert.That(window, Is.Not.Null);
            window.Close();
            Assert.That(window.IsOffscreen);
        }

    }
}
