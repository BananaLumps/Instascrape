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
using System.Threading;
using System.Timers;
using InstagramApiSharp.API;
using InstagramApiSharp.Classes;
using InstagramApiSharp.API.Builder;
using Microsoft.VisualBasic;

namespace Insta_scrape
{
    public partial class Form1 : Form
    {
        private List<UserInfo> users = new List<UserInfo>();
        private UserInfo userInfo = new UserInfo();
        private string LogFilePath;
        private IInstaApi _instaApi;

        public Form1()
        {
            InitializeComponent();
            LogFilePath = Directory.GetCurrentDirectory() + "\\Log.txt";
            var LogFileStream = new FileStream(LogFilePath, FileMode.OpenOrCreate);
            LogFileStream.Close();
        }

        private async void button_start_Click(object sender, EventArgs e)
        {
            await InstaLogin();
            userInfo.Username = textBox_Username.Text;
            var info = await _instaApi.UserProcessor.GetUserInfoByUsernameAsync(userInfo.Username);
            userInfo.PostCount = info.Value.MediaCount.ToString();
            userInfo.FollowerCount = info.Value.FollowerCount.ToString();
            userInfo.FollowingCount = info.Value.FollowingCount.ToString();
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

        private async void CheckForChanges(object sender, ElapsedEventArgs e)
        {
            foreach (UserInfo user in users)
            {
                var info = await _instaApi.UserProcessor.GetUserInfoByUsernameAsync(user.Username);
                string newpc = info.Value.MediaCount.ToString(), newfc = info.Value.FollowerCount.ToString(), newf = info.Value.FollowingCount.ToString();
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
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private async Task InstaLogin()
        {
            var userSession = new UserSessionData
            {
                UserName = Interaction.InputBox("Please enter your Instagram Username", "Username", ""),
                Password = Interaction.InputBox("Please enter your Instagram Password", "Password", "")
            };
            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .Build();
            const string stateFile = "state.bin";
            try
            {
                // load session file if exists
                if (File.Exists(stateFile))
                {
                    using (var fs = File.OpenRead(stateFile))
                    {
                        _instaApi.LoadStateDataFromStream(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            if (!_instaApi.IsUserAuthenticated)
            {
                // login
                var logInResult = await _instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                {
                    MessageBox.Show("Login Failed - Error:" + logInResult.Info.Message);
                    return;
                }
            }
            // save session in file
            var state = _instaApi.GetStateDataAsStream();

            using (var fileStream = File.Create(stateFile))
            {
                state.Seek(0, SeekOrigin.Begin);
                state.CopyTo(fileStream);
            }
        }
    }
}