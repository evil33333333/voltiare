using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Voltaire_Swapper
{
	// Token: 0x02000003 RID: 3
	public partial class AuthWindow : Window
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		public AuthWindow()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			base.DragMove();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021E0 File Offset: 0x000003E0
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

		// Token: 0x06000007 RID: 7 RVA: 0x000020D6 File Offset: 0x000002D6
		private void SignInBtn_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}
