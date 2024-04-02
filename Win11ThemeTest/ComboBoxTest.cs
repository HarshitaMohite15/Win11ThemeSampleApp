﻿using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA3;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win11ThemeTest
{
    public class comboBoxTests
    {
        Application app;
        Window mainWindow;
        Button cmbBoxButton;
        Window comboWindow;
        ComboBox comboBox;
        ComboBox comboBoxBind;
        ComboBox comboBoxBind2;
        //UIA3Automation automation = new UIA3Automation();
        public comboBoxTests()
        {
            try
            {
                var appPath = ConfigurationManager.AppSettings["Testpath"];
                app = Application.Launch(appPath);

                using (var automation = new UIA3Automation())
                {
                    mainWindow = app.GetMainWindow(automation);
                    cmbBoxButton = mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("cmbBoxButton")).AsButton();
                    Mouse.Click(cmbBoxButton.GetClickablePoint());
                    Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(500));
                    comboWindow = mainWindow.FindFirstDescendant(cf => cf.ByName("ComboBoxWindow")).AsWindow();

                    comboBox = comboWindow.FindFirstDescendant(cf => cf.ByAutomationId("comboBoxList")).AsComboBox();
                    comboBoxBind = comboWindow.FindFirstDescendant(cf => cf.ByAutomationId("comboBoxBind")).AsComboBox();
                    comboBoxBind2 = comboWindow.FindFirstDescendant(cf => cf.ByAutomationId("comboBoxBind2")).AsComboBox();
                }
            }
            catch (Exception ex)
            {
                var filepath = ConfigurationManager.AppSettings["logpath"];
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath = filepath + "log_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error = "Log Written Date:" + " " + DateTime.Now.ToString() + "\nError Message:" + " " + ex.Message.ToString();
                    sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                    sw.WriteLine("-------------------------------------------------------------------------------------");
                    sw.WriteLine(error);
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        [Test]
        public void cb_findComboBox()
        {
            Assert.IsNotNull(comboWindow);            
            Assert.IsNotNull(comboBox);
            Assert.IsNotNull(comboBoxBind);
            Assert.IsNotNull(comboBoxBind2);
        }

        [Test]
        public void cb1_defaultSelectedItem()
        {
            Assert.That(comboBox.SelectedItem, Is.Not.Null);           
        }

        [Test]
        public void cb1_isNull()
        {
            Assert.That(comboBox, Is.Not.Null);           
        }

        [Test]
        public void cb1_readEditable()
        {
            Assert.That(comboBox.IsReadOnly, Is.False);
            Assert.That(comboBox.IsEditable, Is.False);            
        }

        [Test]
        public void cb2_select()
        {
            comboBox.Select("Red");
            Assert.That(comboBox.SelectedItem.Name, Is.EqualTo("Red"));            
        }

        //Check if the drop-down is open by clicking on both the drop-down & the drop-down Arrow.
        [Test]       
        public void cb2_comboExpandCollapse()
        {                   
            comboBox.Expand();
            Assert.That(comboBox.ExpandCollapseState, Is.EqualTo(ExpandCollapseState.Expanded));
            comboBox.Collapse();
            Assert.That(comboBox.ExpandCollapseState, Is.EqualTo(ExpandCollapseState.Collapsed));            
        }
        [Test]
        public void cb2_IsEditable()
        {           
            Assert.That(comboBoxBind.IsEditable, Is.True);            
        }

        [Test]
        public void cb3_editableTextTest()
        {          
            Assert.That(comboBoxBind, Is.Not.Null);
            Assert.That(comboBoxBind.IsEditable, Is.True);
            comboBoxBind.EditableText = "10";
            Assert.That(comboBoxBind.SelectedItem, Is.Not.Null);
            Assert.That(comboBoxBind.SelectedItem.Text, Is.EqualTo("10"));
        }

        //Check whether the dropdown is clickable or not.
        [Test]
        public void cb4_mouseClick()
        {
            Mouse.MoveTo(comboBox.GetClickablePoint());
            Mouse.Click();
            Assert.That(comboBox.ExpandCollapseState, Is.EqualTo(ExpandCollapseState.Expanded));         
          
        }
        [Test]
        public void cb4_mouseSelectClick()
        {
            Mouse.MoveTo(comboBox.GetClickablePoint());
            Mouse.Click();
            
            Mouse.MoveTo(comboBox.Items[0].GetClickablePoint());
            Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(1500));
            Mouse.LeftClick();
            Wait.UntilInputIsProcessed(TimeSpan.FromMilliseconds(1000));
            Assert.That(comboBox.SelectedItem.Name, Is.EqualTo("Green"));                    
        }

        [Test]
        public void z_Cleanup()
        {
            comboWindow.Close();
            Assert.IsTrue(comboWindow.IsOffscreen);
            mainWindow.Close();
            Assert.IsTrue(mainWindow.IsOffscreen);
        }
    }
}