using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Threading;
using System.Timers;

namespace Insta_scrape
{
    public partial class Form1 : Form
    {
        private List<UserInfo> users = new List<UserInfo>();
        private UserInfo userInfo = new UserInfo();
        private IWebDriver driver;
        private string LogFilePath;

        public Form1()
        {
            InitializeComponent();
            LogFilePath = Directory.GetCurrentDirectory() + "\\Log.txt";
            var LogFileStream = new FileStream(LogFilePath, FileMode.OpenOrCreate);
            LogFileStream.Close();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            userInfo.Username = textBox_Username.Text;
            FirefoxOptions options = new FirefoxOptions();
            options.AddArguments("--headless");
            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            driver = new FirefoxDriver(driverService, options);
            driver.Url = @"https://www.picuki.com/profile/" + userInfo.Username;
            userInfo.PostCount = driver.FindElement(By.ClassName("total_posts")).Text;
            userInfo.FollowerCount = driver.FindElement(By.ClassName("followed_by")).Text;
            userInfo.FollowingCount = driver.FindElement(By.ClassName("follows")).Text;
            users.Add(userInfo);
            LogTimer();
        }

        private void LogTimer()
        {
            System.Timers.Timer t = new System.Timers.Timer(TimeSpan.FromMinutes(Convert.ToDouble(interval_input.Value)).TotalMilliseconds);
            t.AutoReset = true;
            t.Elapsed += new System.Timers.ElapsedEventHandler(CheckForChanges);
            t.Start();
        }

        private void LogFileWrite(string text)
        {
            using (StreamWriter log = new StreamWriter(LogFilePath, true))
            {
                log.WriteLine(text);
            }
        }

        private void CheckForChanges(object sender, ElapsedEventArgs e)
        {
            foreach (UserInfo user in users)
            {
                driver.Url = @"https://www.picuki.com/profile/" + userInfo.Username;
                string newpc = driver.FindElement(By.ClassName("total_posts")).Text, newfc = driver.FindElement(By.ClassName("followed_by")).Text, newf = driver.FindElement(By.ClassName("follows")).Text;
                if (userInfo.PostCount != newpc)
                {
                    LogFileWrite(DateTime.Now + string.Format(" User {0} Post count changed from {1} to {2}", userInfo.Username, userInfo.PostCount, newpc));
                    userInfo.PostCount = newpc;
                }
                if (userInfo.FollowerCount != newfc)
                {
                    LogFileWrite(DateTime.Now + string.Format(" User {0} Follower count changed from {1} to {2}", userInfo.Username, userInfo.PostCount, newfc));
                    userInfo.PostCount = newfc;
                }
                if (userInfo.FollowingCount != newf)
                {
                    LogFileWrite(DateTime.Now + string.Format(" User {0} Following count changed from {1} to {2}", userInfo.Username, userInfo.PostCount, newf));
                    userInfo.PostCount = newf;
                }
            }
            LogTimer();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            //Currently takes 4 seconds for the exception to be handled, not sure why, minor issue for now.
            try
            {
                if (!String.IsNullOrEmpty(driver.CurrentWindowHandle))
                {
                    driver.Quit();
                }
            }
            catch { }
            this.Close();
        }

        //Test button, removed for publish
        //private void button1_Click(object sender, EventArgs e)
        //{
        //    users[0].PostCount = "0";
        //}

        private void Form1_Resize(object sender, EventArgs e)
        {
            //if the form is minimized
            //hide it from the task bar
            //and show the system tray icon
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(driver.CurrentWindowHandle))
                {
                    driver.Quit();
                }
            }
            catch { }
        }
    }
}