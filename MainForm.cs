/*
 * Created by SharpDevelop.
 * User: alkom
 * Date: 29.04.2019
 * Time: 22:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using MoonSharp.Interpreter;

namespace bytelua
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{	
		public MainForm()
		{
			InitializeComponent();
		}
		
		void ObfuscateCode()
		{
			string scriptCode = @"    
				x = '';
				
				bin = x;
				local fb = '';
				local dec
				
				function compileScript()
					for i = 1, string.len(bin) do
						dec, _ = ('\\%3d'):format(bin:sub(i,i):byte()):gsub(' ', '0');
						fb = fb..dec;
					end
					
					return fb;
				end
			";
			
			Script script = new Script();    
			script.DoString(scriptCode);
			
			script.Globals["x"] = "loadstring(\"" + textBox1.Text + "\")";
			script.Globals["bin"] = script.Globals["x"];
			
			DynValue res = script.Call(script.Globals["compileScript"]);
			
			textBox1.Text = "loadstring(\"" + res.String + "\")()";
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			ObfuscateCode();
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			textBox1.Text = "_G['setTimer'](function()\r\n\tlocal i = 1\r\n\t_G['outputChatBox']('Hello world '..x)\r\n\ti = i +1\r\nend,1000,5)";
		}
		
		void Button3Click(object sender, EventArgs e)
		{
			textBox1.Text = "_G['addEventHandler']('onPlayerChat', root, function(msg, type)\r\n\tprint(_G['getPlayerName'](source), msg, type)\r\nend)";
		}
		
		void Button4Click(object sender, EventArgs e)
		{
			textBox1.Text = "_G['bindKey']('n','down',function(k,ks)\r\n\t_G['outputChatBox']('Your name: '.._G['getPlayerName'](_G['localPlayer']))\r\nend)\r\n\r\n_G['addEventHandler']('onClientResourceStart', resourceRoot, function()\r\n\t_G['outputChatBox']('Press b key')\r\nend)";
		}
	}
}
