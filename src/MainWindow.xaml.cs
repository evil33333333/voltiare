using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.VisualBasic.CompilerServices;
using WpfAnimatedGif;

namespace Voltaire_Swapper
{
	// Token: 0x02000004 RID: 4
	public partial class MainWindow : Window
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000022CC File Offset: 0x000004CC
		private static string getUUID()
		{
			string arg = "localhost";
			ManagementScope managementScope = new ManagementScope(string.Format("\\\\{0}\\root\\CIMV2", arg), null);
			managementScope.Connect();
			ObjectQuery query = new ObjectQuery("SELECT UUID FROM Win32_ComputerSystemProduct");
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(managementScope, query);
			string result = string.Empty;
			foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				result = managementObject["UUID"].ToString();
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002370 File Offset: 0x00000570
		private static string ksmk(string originalText, string hashKey)
		{
			string result = string.Empty;
			byte[] bytes = Encoding.UTF8.GetBytes(originalText);
			using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
			{
				byte[] key = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(hashKey));
				using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider
				{
					Key = key,
					Mode = CipherMode.ECB,
					Padding = PaddingMode.PKCS7
				})
				{
					ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
					byte[] array = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
					result = Convert.ToBase64String(array, 0, array.Length);
				}
			}
			return result;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002424 File Offset: 0x00000624
		private static string ksmkde(string originalText, string hashKey)
		{
			string empty = string.Empty;
			try
			{
				string result = string.Empty;
				byte[] array = Convert.FromBase64String(originalText);
				using (MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider())
				{
					byte[] key = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(hashKey));
					using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider
					{
						Key = key,
						Mode = CipherMode.ECB,
						Padding = PaddingMode.PKCS7
					})
					{
						ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
						byte[] bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
						result = Encoding.UTF8.GetString(bytes);
					}
				}
				return result;
			}
			catch
			{
			}
			return empty;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024F0 File Offset: 0x000006F0
		private static void starrt()
		{
			MainWindow._AREvt = new AutoResetEvent(false);
			ThreadStart start = new ThreadStart(MainWindow.Scan1);
			new Thread(start)
			{
				IsBackground = true,
				Priority = ThreadPriority.Lowest
			}.Start();
			start = new ThreadStart(MainWindow.Scan2);
			new Thread(start)
			{
				IsBackground = true,
				Priority = ThreadPriority.Lowest
			}.Start();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002558 File Offset: 0x00000758
		private static void Scan1()
		{
			if (Directory.Exists("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\Process Hacker 2"))
			{
				MainWindow.work("Process Hacker 2 folder");
			}
			if (Directory.Exists("C:\\Users\\" + Environment.UserName + "\\AppData\\Roaming\\dnSpy"))
			{
				MainWindow.work("dnSpy folder");
			}
			if (Directory.Exists("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Programs\\Fiddler"))
			{
				MainWindow.work("Fiddler folder");
			}
			if (Directory.Exists("C:\\ProgramData\\HTTPDebuggerPro"))
			{
				MainWindow.work("HTTP Debugger folder");
			}
			ServiceController serviceController = ServiceController.GetServices().FirstOrDefault((ServiceController s) => s.ServiceName == "HTTPDebuggerPro");
			if (serviceController != null)
			{
				MainWindow.work("HTTP Debugger Services");
			}
			MainWindow._AREvt.WaitOne(10000, true);
			MainWindow.Scan1();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000263C File Offset: 0x0000083C
		private static void Scan2()
		{
			try
			{
				List<string> list = new List<string>(new string[]
				{
					"Process Hacker",
					"HTTP Debugger",
					"dnSpy",
					"Fiddler Everywhere",
					"NetLimiter",
					"MegaDumper",
					"Exeinfo",
					"ExtremeDumper",
					"KsDumper",
					"charles",
					"brute",
					"ollydbg",
					"ilspy",
					"HxD",
					"dump",
					"Progress Telerik Fiddler Web Debugger",
					"Wireshark",
					"Fiddler",
					"Wireshark",
					"dumpcap",
					"dnSpy-x86",
					"cheatengine-x86_64",
					"HTTPDebuggerUI",
					"Procmon",
					"Procmon64",
					"Procmon64a",
					"ProcessHacker",
					"x32dbg",
					"x64dbg",
					"DotNetDataCollector32",
					"DotNetDataCollector64"
				});
				try
				{
					foreach (object obj in list)
					{
						Process[] processes = Process.GetProcesses();
						foreach (Process process in processes)
						{
							if (process.MainWindowTitle.Contains(Conversions.ToString(RuntimeHelpers.GetObjectValue(obj))) || process.ProcessName.Contains(Conversions.ToString(RuntimeHelpers.GetObjectValue(obj))))
							{
								string what = Conversions.ToString(RuntimeHelpers.GetObjectValue(obj));
								MainWindow.work(what);
							}
						}
					}
				}
				finally
				{
				}
				GC.Collect();
				Thread.Sleep(500);
			}
			catch
			{
				Environment.Exit(0);
			}
			MainWindow._AREvt.WaitOne(10000, true);
			MainWindow.Scan2();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000287C File Offset: 0x00000A7C
		private static void work(string what)
		{
			MainWindow.crack(what);
			Directory.CreateDirectory("C:/ProgramData/x");
			Environment.Exit(0);
			MainWindow.boom();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000028A8 File Offset: 0x00000AA8
		private static void crack(string procname)
		{
			string userName = Environment.UserName;
			string text = new WebClient
			{
				Proxy = null
			}.DownloadString("https://api.ipify.org/?format=text");
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string data = string.Concat(new string[]
					{
						string.Concat(new string[]
						{
							"{\"content\":null,\"embeds\":[{\"title\":\"There's a PC that has been destroyed\",\"description\":\"IP : [ ",
							MainWindow.ipadress,
							" - ",
							text,
							" ]"
						}),
						"",
						string.Concat(new string[]
						{
							"\\nUsername : [ ",
							MainWindow.usernamem,
							" ]\\nPC name : [ ",
							userName,
							" ]\\nUsed tool : [ ",
							procname,
							" ]"
						}),
						"\",\"color\":8128006,\"footer\":{\"text\":\"By @DE4TOOLS",
						" \"},\"thumbnail\":{\"url\":\"\"}}]}"
					});
					webClient.Headers.Add("content-type", "application/json");
					webClient.UploadString(MainWindow.linkw, data);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020D6 File Offset: 0x000002D6
		private static void boom()
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000029BC File Offset: 0x00000BBC
		private static void usernamee()
		{
			try
			{
				MainWindow.usernamem = File.ReadAllText("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\User.txt");
			}
			catch
			{
				MainWindow.usernamem = "@anonymous";
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002A08 File Offset: 0x00000C08
		private static void DBG()
		{
			if (Directory.Exists("C:/ProgramData/x"))
			{
				Process.Start(new ProcessStartInfo("cmd.exe", "/c START CMD /C \"COLOR C && TITLE mbasec Protection && ECHO [mbasec] Please contact support, you have been banned && TIMEOUT 10\""));
				Process.GetCurrentProcess().Kill();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002A40 File Offset: 0x00000C40
		private static bool IsAdministrator()
		{
			WindowsIdentity current = WindowsIdentity.GetCurrent();
			WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
			return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002A68 File Offset: 0x00000C68
		private static void Admin()
		{
			if (!MainWindow.IsAdministrator())
			{
				Process.Start(new ProcessStartInfo("cmd.exe", "/c START CMD /C \"COLOR C && TITLE MBASec Protection && ECHO [MBASec] - Run it as admin && TIMEOUT 10\""));
				Process.GetCurrentProcess().Kill();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private static string ShowDialog()
		{
			Form form = new Form
			{
				Width = 160,
				Height = 100,
				FormBorderStyle = FormBorderStyle.FixedToolWindow,
				StartPosition = FormStartPosition.CenterScreen
			};
			System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox
			{
				Left = 10,
				Top = 7,
				Width = 126,
				Text = "@"
			};
			System.Windows.Forms.Button button = new System.Windows.Forms.Button
			{
				Text = "Login",
				Left = 10,
				Width = 126,
				Top = 30,
				DialogResult = System.Windows.Forms.DialogResult.OK
			};
			button.Click += delegate(object sender, EventArgs e)
			{
				File.WriteAllText("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\User.txt", textBox.Text);
			};
			form.Controls.Add(textBox);
			form.Controls.Add(button);
			form.AcceptButton = button;
			form.Text = "Type your username";
			form.BackColor = System.Drawing.Color.Black;
			form.ForeColor = System.Drawing.Color.White;
			button.FlatStyle = FlatStyle.Flat;
			textBox.BorderStyle = BorderStyle.FixedSingle;
			textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			textBox.BackColor = System.Drawing.Color.Black;
			textBox.ForeColor = System.Drawing.Color.White;
			form.ShowIcon = false;
			form.ShowInTaskbar = false;
			form.MaximizeBox = false;
			form.MinimizeBox = false;
			if (File.Exists("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\User.txt"))
			{
				textBox.Text = File.ReadAllText("C:\\Users\\" + Environment.UserName + "\\AppData\\Local\\Temp\\User.txt");
			}
			else
			{
				textBox.Text = "@";
			}
			return (form.ShowDialog() == System.Windows.Forms.DialogResult.OK) ? textBox.Text : "@";
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002C5C File Offset: 0x00000E5C
		private void ch()
		{
			try
			{
				string originalText = new WebClient().DownloadString(MainWindow.linka);
				string input = MainWindow.ksmkde(originalText, MainWindow.mbapass);
				MainWindow.userr = MainWindow.ShowDialog();
				MainWindow.usernamee();
				MainWindow.find = Regex.Match(input, MainWindow.userr + ":(.*?)]").Groups[1].Value;
				MainWindow.txt = string.Concat(new string[]
				{
					"[",
					MainWindow.userr,
					":",
					MainWindow.final,
					"]"
				});
				if (Operators.CompareString(MainWindow.find, "", false) == 0)
				{
					Process.Start(new ProcessStartInfo("cmd.exe", "/c START CMD /C \"COLOR C && TITLE MBASec Protection && ECHO [MBASec] - Not activated, make sure you are using the correct PC or your membership has expired && TIMEOUT 10\""));
					File.WriteAllText("Serial.txt", MainWindow.txt);
					Process.Start("Serial.txt");
					Process.GetCurrentProcess().Kill();
				}
				else if (MainWindow.find.Contains(MainWindow.second))
				{
					this.check();
				}
				else
				{
					Process.Start(new ProcessStartInfo("cmd.exe", "/c START CMD /C \"COLOR C && TITLE MBASec Protection && ECHO [MBASec] - Not activated, make sure you are using the correct PC or your membership has expired && TIMEOUT 10\""));
					File.WriteAllText("Serial.txt", MainWindow.txt);
					Process.Start("Serial.txt");
					Process.GetCurrentProcess().Kill();
				}
			}
			catch
			{
				Environment.Exit(0);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002DC0 File Offset: 0x00000FC0
		private void check()
		{
			base.Opacity = 100.0;
			ServicePointManager.UseNagleAlgorithm = false;
			ServicePointManager.Expect100Continue = false;
			ServicePointManager.CheckCertificateRevocationList = false;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ThreadPool.SetMaxThreads(int.MaxValue, int.MaxValue);
			ServicePointManager.DefaultConnectionLimit = int.MaxValue;
			try
			{
				MainWindow.SettingsFile = File.ReadAllLines("Settings.txt").ToList<string>();
			}
			catch (Exception)
			{
				System.Windows.MessageBox.Show("Settings.txt is missing!", "", MessageBoxButton.OK, MessageBoxImage.Hand);
				Environment.Exit(1);
			}
			try
			{
				MainWindow.SwapperName = MainWindow.SettingsFile[0].Trim().Split(new string[]
				{
					"||"
				}, StringSplitOptions.None)[1].Trim();
				MainWindow.Bio = MainWindow.SettingsFile[1].Trim().Split(new string[]
				{
					"||"
				}, StringSplitOptions.None)[1].Trim();
				MainWindow.MsgBoxContext = MainWindow.SettingsFile[2].Trim().Split(new string[]
				{
					"||"
				}, StringSplitOptions.None)[1].Trim();
				MainWindow.ImagePath = MainWindow.SettingsFile[3].Trim().Split(new string[]
				{
					"||"
				}, StringSplitOptions.None)[1].Trim();
				string text = MainWindow.SettingsFile[4].Trim().Split(new string[]
				{
					"||"
				}, StringSplitOptions.None)[1].Trim();
				MainWindow.WebHook = MainWindow.SettingsFile[5].Trim().Split(new string[]
				{
					"||"
				}, StringSplitOptions.None)[1].Trim();
				if (text.ToLower().Contains("black"))
				{
					MainWindow.AppTheme = MainWindow.Themes.Black;
				}
				else if (text.ToLower().Contains("white"))
				{
					MainWindow.AppTheme = MainWindow.Themes.White;
				}
				else if (text.ToLower().Contains("blue"))
				{
					MainWindow.AppTheme = MainWindow.Themes.Blue;
				}
				else if (text.ToLower().Contains("purple"))
				{
					MainWindow.AppTheme = MainWindow.Themes.Purple;
				}
				else if (text.ToLower().Contains("gray"))
				{
					MainWindow.AppTheme = MainWindow.Themes.Gray;
				}
				this.SetTheme();
			}
			catch (Exception)
			{
				System.Windows.MessageBox.Show("Something into \"Settings.txt\" is missing!", "", MessageBoxButton.OK, MessageBoxImage.Hand);
				Environment.Exit(1);
			}
			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.UriSource = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + MainWindow.ImagePath);
			bitmapImage.EndInit();
			ImageBehavior.SetAnimatedSource(this.PictureBox, bitmapImage);
			this.NameLbl.Text = MainWindow.SwapperName;
			new Thread(new ThreadStart(this.Counter)).Start();
			new Thread(new ThreadStart(this.RSCounter)).Start();
			new Thread(new ThreadStart(this.SendDiscordMsg)).Start();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000030D4 File Offset: 0x000012D4
		[STAThread]
		public void Maina()
		{
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
			Ping ping = new Ping();
			PingReply pingReply = ping.Send("8.8.8.8");
			if (Operators.CompareString(pingReply.Status.ToString(), "Success", false) == 0)
			{
				this.check();
			}
			else
			{
				Process.Start(new ProcessStartInfo("cmd.exe", "/c START CMD /C \"COLOR C && TITLE MBASec Protection && ECHO [MBASec] - Turn on the internet and try again && TIMEOUT 10\""));
				Process.GetCurrentProcess().Kill();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000020D8 File Offset: 0x000002D8
		public MainWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003148 File Offset: 0x00001348
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			base.Opacity = 0.0;
			this.Maina();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000020CE File Offset: 0x000002CE
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000316C File Offset: 0x0000136C
		private void ThreadsTxt_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				MainWindow.Threads = Convert.ToInt32(this.ThreadsTxt.Text);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021E0 File Offset: 0x000003E0
		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Environment.Exit(1);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000031A4 File Offset: 0x000013A4
		[DebuggerStepThrough]
		private void StartBtn_Click(object sender, RoutedEventArgs e)
		{
			MainWindow.<StartBtn_Click>d__58 <StartBtn_Click>d__ = new MainWindow.<StartBtn_Click>d__58();
			<StartBtn_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<StartBtn_Click>d__.<>4__this = this;
			<StartBtn_Click>d__.sender = sender;
			<StartBtn_Click>d__.e = e;
			<StartBtn_Click>d__.<>1__state = -1;
			<StartBtn_Click>d__.<>t__builder.Start<MainWindow.<StartBtn_Click>d__58>(ref <StartBtn_Click>d__);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000031EC File Offset: 0x000013EC
		[DebuggerStepThrough]
		public Task<bool> AppIGLogin()
		{
			MainWindow.<AppIGLogin>d__59 <AppIGLogin>d__ = new MainWindow.<AppIGLogin>d__59();
			<AppIGLogin>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<AppIGLogin>d__.<>4__this = this;
			<AppIGLogin>d__.<>1__state = -1;
			<AppIGLogin>d__.<>t__builder.Start<MainWindow.<AppIGLogin>d__59>(ref <AppIGLogin>d__);
			return <AppIGLogin>d__.<>t__builder.Task;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003230 File Offset: 0x00001430
		[DebuggerStepThrough]
		public Task<bool> WebIGLogin()
		{
			MainWindow.<WebIGLogin>d__60 <WebIGLogin>d__ = new MainWindow.<WebIGLogin>d__60();
			<WebIGLogin>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<WebIGLogin>d__.<>4__this = this;
			<WebIGLogin>d__.<>1__state = -1;
			<WebIGLogin>d__.<>t__builder.Start<MainWindow.<WebIGLogin>d__60>(ref <WebIGLogin>d__);
			return <WebIGLogin>d__.<>t__builder.Task;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003274 File Offset: 0x00001474
		[DebuggerStepThrough]
		public Task<bool> AppCheckBlock()
		{
			MainWindow.<AppCheckBlock>d__61 <AppCheckBlock>d__ = new MainWindow.<AppCheckBlock>d__61();
			<AppCheckBlock>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<AppCheckBlock>d__.<>4__this = this;
			<AppCheckBlock>d__.<>1__state = -1;
			<AppCheckBlock>d__.<>t__builder.Start<MainWindow.<AppCheckBlock>d__61>(ref <AppCheckBlock>d__);
			return <AppCheckBlock>d__.<>t__builder.Task;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000032B8 File Offset: 0x000014B8
		[DebuggerStepThrough]
		public Task<bool> WebCheckBlock()
		{
			MainWindow.<WebCheckBlock>d__62 <WebCheckBlock>d__ = new MainWindow.<WebCheckBlock>d__62();
			<WebCheckBlock>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<WebCheckBlock>d__.<>4__this = this;
			<WebCheckBlock>d__.<>1__state = -1;
			<WebCheckBlock>d__.<>t__builder.Start<MainWindow.<WebCheckBlock>d__62>(ref <WebCheckBlock>d__);
			return <WebCheckBlock>d__.<>t__builder.Task;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000032FC File Offset: 0x000014FC
		[DebuggerStepThrough]
		public Task App_ChangeUsername(string session, string url, string number, string uuid, string user, string bio, string name, string email, string userid, string deviceid)
		{
			MainWindow.<App_ChangeUsername>d__63 <App_ChangeUsername>d__ = new MainWindow.<App_ChangeUsername>d__63();
			<App_ChangeUsername>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<App_ChangeUsername>d__.<>4__this = this;
			<App_ChangeUsername>d__.session = session;
			<App_ChangeUsername>d__.url = url;
			<App_ChangeUsername>d__.number = number;
			<App_ChangeUsername>d__.uuid = uuid;
			<App_ChangeUsername>d__.user = user;
			<App_ChangeUsername>d__.bio = bio;
			<App_ChangeUsername>d__.name = name;
			<App_ChangeUsername>d__.email = email;
			<App_ChangeUsername>d__.userid = userid;
			<App_ChangeUsername>d__.deviceid = deviceid;
			<App_ChangeUsername>d__.<>1__state = -1;
			<App_ChangeUsername>d__.<>t__builder.Start<MainWindow.<App_ChangeUsername>d__63>(ref <App_ChangeUsername>d__);
			return <App_ChangeUsername>d__.<>t__builder.Task;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003390 File Offset: 0x00001590
		[DebuggerStepThrough]
		public Task Web_ChangeUsername(string session, string url, string number, string uuid, string user, string bio, string name, string email, string userid, string deviceid)
		{
			MainWindow.<Web_ChangeUsername>d__64 <Web_ChangeUsername>d__ = new MainWindow.<Web_ChangeUsername>d__64();
			<Web_ChangeUsername>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<Web_ChangeUsername>d__.<>4__this = this;
			<Web_ChangeUsername>d__.session = session;
			<Web_ChangeUsername>d__.url = url;
			<Web_ChangeUsername>d__.number = number;
			<Web_ChangeUsername>d__.uuid = uuid;
			<Web_ChangeUsername>d__.user = user;
			<Web_ChangeUsername>d__.bio = bio;
			<Web_ChangeUsername>d__.name = name;
			<Web_ChangeUsername>d__.email = email;
			<Web_ChangeUsername>d__.userid = userid;
			<Web_ChangeUsername>d__.deviceid = deviceid;
			<Web_ChangeUsername>d__.<>1__state = -1;
			<Web_ChangeUsername>d__.<>t__builder.Start<MainWindow.<Web_ChangeUsername>d__64>(ref <Web_ChangeUsername>d__);
			return <Web_ChangeUsername>d__.<>t__builder.Task;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003424 File Offset: 0x00001624
		public static string RanChars(int Length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] array = new byte[4];
				while (Length-- > 0)
				{
					rngcryptoServiceProvider.GetBytes(array);
					uint num = BitConverter.ToUInt32(array, 0);
					stringBuilder.Append("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789"[(int)(num % (uint)"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789".Length)]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000034A4 File Offset: 0x000016A4
		public static string RanNums(int Length)
		{
			StringBuilder stringBuilder = new StringBuilder();
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] array = new byte[4];
				while (Length-- > 0)
				{
					rngcryptoServiceProvider.GetBytes(array);
					uint num = BitConverter.ToUInt32(array, 0);
					stringBuilder.Append("123456789"[(int)(num % (uint)"123456789".Length)]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003524 File Offset: 0x00001724
		[DebuggerStepThrough]
		public Task TasksManager(string session, string url, string number, string uuid, string user, string bio, string name, string email, string userid, string deviceid)
		{
			MainWindow.<TasksManager>d__68 <TasksManager>d__ = new MainWindow.<TasksManager>d__68();
			<TasksManager>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<TasksManager>d__.<>4__this = this;
			<TasksManager>d__.session = session;
			<TasksManager>d__.url = url;
			<TasksManager>d__.number = number;
			<TasksManager>d__.uuid = uuid;
			<TasksManager>d__.user = user;
			<TasksManager>d__.bio = bio;
			<TasksManager>d__.name = name;
			<TasksManager>d__.email = email;
			<TasksManager>d__.userid = userid;
			<TasksManager>d__.deviceid = deviceid;
			<TasksManager>d__.<>1__state = -1;
			<TasksManager>d__.<>t__builder.Start<MainWindow.<TasksManager>d__68>(ref <TasksManager>d__);
			return <TasksManager>d__.<>t__builder.Task;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000035B8 File Offset: 0x000017B8
		public void Counter()
		{
			try
			{
				for (;;)
				{
					System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
					{
						this.GoodTxt.Text = MainWindow.Good.ToString();
						this.BadTxt.Text = MainWindow.Bad.ToString();
						this.RSTxt.Text = "R/S: " + MainWindow.RS.ToString();
						Thread.Sleep(10);
					}));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000035FC File Offset: 0x000017FC
		public void RSCounter()
		{
			StringBuilder GoodBefore = new StringBuilder("0");
			for (;;)
			{
				GoodBefore.Clear();
				GoodBefore.Append(MainWindow.Good.ToString());
				Thread.Sleep(1000);
				System.Windows.Application.Current.Dispatcher.Invoke(new ThreadStart(delegate()
				{
					MainWindow.RS = MainWindow.Good - Convert.ToInt32(GoodBefore.ToString());
				}), DispatcherPriority.Background, Array.Empty<object>());
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003670 File Offset: 0x00001870
		public void SetTheme()
		{
			switch (MainWindow.AppTheme)
			{
			case MainWindow.Themes.Black:
				this.MainGrid.Background = System.Windows.Media.Brushes.Black;
				foreach (System.Windows.Controls.TextBox textBox in this.MainStackPanel.Children.OfType<System.Windows.Controls.TextBox>())
				{
					textBox.Foreground = System.Windows.Media.Brushes.White;
				}
				this.NameLbl.Foreground = System.Windows.Media.Brushes.White;
				this.StartBtn.Foreground = System.Windows.Media.Brushes.White;
				break;
			case MainWindow.Themes.White:
				this.MainGrid.Background = System.Windows.Media.Brushes.White;
				foreach (System.Windows.Controls.TextBox textBox2 in this.MainStackPanel.Children.OfType<System.Windows.Controls.TextBox>())
				{
					textBox2.Foreground = System.Windows.Media.Brushes.Black;
				}
				this.NameLbl.Foreground = System.Windows.Media.Brushes.Black;
				this.StartBtn.Foreground = System.Windows.Media.Brushes.Black;
				break;
			case MainWindow.Themes.Blue:
				this.MainGrid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(4, 1, 48));
				foreach (System.Windows.Controls.TextBox textBox3 in this.MainStackPanel.Children.OfType<System.Windows.Controls.TextBox>())
				{
					textBox3.Foreground = System.Windows.Media.Brushes.White;
				}
				this.NameLbl.Foreground = System.Windows.Media.Brushes.White;
				this.StartBtn.Foreground = System.Windows.Media.Brushes.White;
				break;
			case MainWindow.Themes.Purple:
				this.MainGrid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(37, 0, 37));
				foreach (System.Windows.Controls.TextBox textBox4 in this.MainStackPanel.Children.OfType<System.Windows.Controls.TextBox>())
				{
					textBox4.Foreground = System.Windows.Media.Brushes.White;
				}
				this.NameLbl.Foreground = System.Windows.Media.Brushes.White;
				this.StartBtn.Foreground = System.Windows.Media.Brushes.White;
				break;
			case MainWindow.Themes.Gray:
				this.MainGrid.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(162, 162, 162));
				foreach (System.Windows.Controls.TextBox textBox5 in this.MainStackPanel.Children.OfType<System.Windows.Controls.TextBox>())
				{
					textBox5.Foreground = System.Windows.Media.Brushes.Black;
				}
				this.NameLbl.Foreground = System.Windows.Media.Brushes.Black;
				this.StartBtn.Foreground = System.Windows.Media.Brushes.Black;
				break;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003968 File Offset: 0x00001B68
		public Task WhenSwapped(string user, string session)
		{
			try
			{
				System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
				{
					if (MainWindow.Tries <= 3)
					{
						this.StartBtn.Content = "Fucked";
						File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + user + "_Info.txt", new string[]
						{
							"Username: " + user,
							"SessionID: " + session,
							string.Format("Date and Time: {0}", DateTime.Now)
						});
						System.Windows.MessageBox.Show(MainWindow.MsgBoxContext + user, Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButton.OK, MessageBoxImage.Asterisk);
					}
				}));
			}
			catch (Exception)
			{
			}
			return Task.CompletedTask;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000039CC File Offset: 0x00001BCC
		public void SendDiscordMsg()
		{
			for (;;)
			{
				try
				{
					if (MainWindow.Tries >= 1)
					{
						try
						{
							System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate()
							{
								string data = string.Concat(new string[]
								{
									"{\r\n                                  \"content\": null,\r\n                                  \"embeds\": [\r\n                                    {\r\n                                      \"description\": \"NEW USERNAME SWAPPED !\\n[ # ] - USERNAME : [@",
									this.TargetTxt.Text,
									"](https://www.instagram.com/",
									this.TargetTxt.Text,
									")\\n[ # ] - ATTEMPTS : ",
									this.GoodTxt.Text,
									"\",\r\n                                      \"color\": 15857402\r\n                                    }\r\n                                  ],\r\n                                  \"attachments\": []\r\n                                }"
								});
								using (WebClient webClient = new WebClient())
								{
									webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
									webClient.UploadString("https://discordapp.com/api/webhooks/979317428104339488/44W8XzqBj0VJ3Zq7g-k1B5OsFhPsWisC0MZXT6FIoHo_16IMXLJFnkCQm-8pdc3PkZZp", data);
								}
								using (WebClient webClient2 = new WebClient())
								{
									webClient2.Headers[HttpRequestHeader.ContentType] = "application/json";
									webClient2.UploadString(MainWindow.WebHook, data);
								}
								MainWindow.Tries = 0;
							}));
						}
						catch (Exception)
						{
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x04000005 RID: 5
		public static MainWindow.Themes AppTheme = MainWindow.Themes.Black;

		// Token: 0x04000006 RID: 6
		public static List<string> SettingsFile = new List<string>();

		// Token: 0x04000007 RID: 7
		public static string SwapperName = "#Voltaire";

		// Token: 0x04000008 RID: 8
		public static string Bio = "Swapped By Voltaire";

		// Token: 0x04000009 RID: 9
		public static string MsgBoxContext = "Swapped Succesfully: @";

		// Token: 0x0400000A RID: 10
		public static string WebHook = "";

		// Token: 0x0400000B RID: 11
		public static string ImagePath = "image.jpg";

		// Token: 0x0400000C RID: 12
		public static string OldUserid = "00000000";

		// Token: 0x0400000D RID: 13
		public static string OldUser = "xxxx";

		// Token: 0x0400000E RID: 14
		public static string OldFullName = "Voltaire...";

		// Token: 0x0400000F RID: 15
		public static string OldEmail = "xxxxxxxxxxx@gmail.com";

		// Token: 0x04000010 RID: 16
		public static string OldPhone = "+xxx";

		// Token: 0x04000011 RID: 17
		public static string OldBio = "...";

		// Token: 0x04000012 RID: 18
		public static string OldUrl = "https://user.com";

		// Token: 0x04000013 RID: 19
		public static string UUid = Guid.NewGuid().ToString();

		// Token: 0x04000014 RID: 20
		public static string DeviceID = MainWindow.RanChars(16);

		// Token: 0x04000015 RID: 21
		public static int Good = 0;

		// Token: 0x04000016 RID: 22
		public static int Bad = 0;

		// Token: 0x04000017 RID: 23
		public static int RS = 0;

		// Token: 0x04000018 RID: 24
		public static int Threads = 10;

		// Token: 0x04000019 RID: 25
		public static int Tries = 0;

		// Token: 0x0400001A RID: 26
		public static bool Running = false;

		// Token: 0x0400001B RID: 27
		private static string ipadress = Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault((IPAddress ip) => ip.AddressFamily == AddressFamily.InterNetwork).ToString();

		// Token: 0x0400001C RID: 28
		private static string userr;

		// Token: 0x0400001D RID: 29
		private static string find;

		// Token: 0x0400001E RID: 30
		private static string txt;

		// Token: 0x0400001F RID: 31
		private static string uid = MainWindow.getUUID();

		// Token: 0x04000020 RID: 32
		private static string first = MainWindow.uid;

		// Token: 0x04000021 RID: 33
		private static string usernamem = "@anonymous";

		// Token: 0x04000022 RID: 34
		private static string mbapass = "DE4TOOLS@Voltaire";

		// Token: 0x04000023 RID: 35
		private static string linka = "http://00.aba.vg/Voltaire.txt";

		// Token: 0x04000024 RID: 36
		private static string linkw = "https://discord.com/api/webhooks/961886136899342346/wckn9srZp8tmxYlT7q9jdoLnn712eywFB1vCeIx9oRmwsEzMgTPeyIkFN0avWPOhuvh6";

		// Token: 0x04000025 RID: 37
		private static string second = MainWindow.ksmk(MainWindow.first, MainWindow.mbapass);

		// Token: 0x04000026 RID: 38
		private static string final = MainWindow.ksmk(MainWindow.second, MainWindow.mbapass);

		// Token: 0x04000027 RID: 39
		private static AutoResetEvent _AREvt;

		// Token: 0x04000028 RID: 40
		private static readonly RandomNumberGenerator rng = new RNGCryptoServiceProvider();

		// Token: 0x02000005 RID: 5
		public enum Themes
		{
			// Token: 0x04000037 RID: 55
			Black,
			// Token: 0x04000038 RID: 56
			White,
			// Token: 0x04000039 RID: 57
			Blue,
			// Token: 0x0400003A RID: 58
			Purple,
			// Token: 0x0400003B RID: 59
			Gray
		}
	}
}
