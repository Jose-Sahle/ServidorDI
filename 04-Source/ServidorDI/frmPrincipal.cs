using System;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DirecTV.DI.Classes;

namespace ServidorDI
{
	/// <summary>
	/// Tela inicial de configuração e inicialização do Servidor DI
	/// </summary>
	public class FormServidorDI : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabControl tbServidor;
		private System.Windows.Forms.TabPage tbpConfiguracao;
		private System.Windows.Forms.TabPage tbpOperacao;
		private System.Windows.Forms.GroupBox grpConexao;
		private System.Windows.Forms.Button cmdFechar;
		private System.Windows.Forms.GroupBox grpEstatisticos;
		private System.Windows.Forms.GroupBox grpGerais;
		private System.Windows.Forms.TextBox txtTamanhoMaximoBuffer;
		private System.Windows.Forms.Label lblTamanhoBuffer;
		private System.Windows.Forms.TextBox txtConexoesSimultaneas;
		private System.Windows.Forms.Label lblConexoesSimultaneas;
		private System.Windows.Forms.GroupBox grpCoenxao;
		private System.Windows.Forms.Label lblPorta;
		private System.Windows.Forms.Label lblIPServidor;
		private System.Windows.Forms.ListView lswConexoes;
		private System.Windows.Forms.TextBox txtConexoesPorIP;
		private System.Windows.Forms.Label lblConexoesPorIP;
		private System.Windows.Forms.Button cmdIniciar;
		private System.Windows.Forms.ColumnHeader Porta;
		private System.Windows.Forms.ColumnHeader IPCliente;
		private System.Windows.Forms.ColumnHeader BytesEnviados;
		private System.Windows.Forms.ColumnHeader BytesRecebidos;
		private System.Windows.Forms.ColumnHeader TempoConexao;
		private System.Windows.Forms.Button cmdSalvar;
		private System.Windows.Forms.TextBox txtPorta;
		private System.Windows.Forms.TextBox txtIPServidor;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormServidorDI()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormServidorDI));
			this.tbServidor = new System.Windows.Forms.TabControl();
			this.tbpConfiguracao = new System.Windows.Forms.TabPage();
			this.tbpOperacao = new System.Windows.Forms.TabPage();
			this.lswConexoes = new System.Windows.Forms.ListView();
			this.grpConexao = new System.Windows.Forms.GroupBox();
			this.cmdFechar = new System.Windows.Forms.Button();
			this.grpEstatisticos = new System.Windows.Forms.GroupBox();
			this.grpGerais = new System.Windows.Forms.GroupBox();
			this.txtTamanhoMaximoBuffer = new System.Windows.Forms.TextBox();
			this.lblTamanhoBuffer = new System.Windows.Forms.Label();
			this.txtConexoesSimultaneas = new System.Windows.Forms.TextBox();
			this.lblConexoesSimultaneas = new System.Windows.Forms.Label();
			this.grpCoenxao = new System.Windows.Forms.GroupBox();
			this.lblPorta = new System.Windows.Forms.Label();
			this.lblIPServidor = new System.Windows.Forms.Label();
			this.txtConexoesPorIP = new System.Windows.Forms.TextBox();
			this.lblConexoesPorIP = new System.Windows.Forms.Label();
			this.txtPorta = new System.Windows.Forms.TextBox();
			this.txtIPServidor = new System.Windows.Forms.TextBox();
			this.cmdIniciar = new System.Windows.Forms.Button();
			this.Porta = new System.Windows.Forms.ColumnHeader();
			this.IPCliente = new System.Windows.Forms.ColumnHeader();
			this.BytesEnviados = new System.Windows.Forms.ColumnHeader();
			this.BytesRecebidos = new System.Windows.Forms.ColumnHeader();
			this.TempoConexao = new System.Windows.Forms.ColumnHeader();
			this.cmdSalvar = new System.Windows.Forms.Button();
			this.tbServidor.SuspendLayout();
			this.tbpConfiguracao.SuspendLayout();
			this.tbpOperacao.SuspendLayout();
			this.grpConexao.SuspendLayout();
			this.grpGerais.SuspendLayout();
			this.grpCoenxao.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbServidor
			// 
			this.tbServidor.Controls.Add(this.tbpConfiguracao);
			this.tbServidor.Controls.Add(this.tbpOperacao);
			this.tbServidor.Location = new System.Drawing.Point(6, 6);
			this.tbServidor.Name = "tbServidor";
			this.tbServidor.SelectedIndex = 0;
			this.tbServidor.Size = new System.Drawing.Size(388, 284);
			this.tbServidor.TabIndex = 0;
			// 
			// tbpConfiguracao
			// 
			this.tbpConfiguracao.Controls.Add(this.cmdSalvar);
			this.tbpConfiguracao.Controls.Add(this.grpConexao);
			this.tbpConfiguracao.Location = new System.Drawing.Point(4, 22);
			this.tbpConfiguracao.Name = "tbpConfiguracao";
			this.tbpConfiguracao.Size = new System.Drawing.Size(380, 258);
			this.tbpConfiguracao.TabIndex = 0;
			this.tbpConfiguracao.Text = "Configuração";
			// 
			// tbpOperacao
			// 
			this.tbpOperacao.Controls.Add(this.grpEstatisticos);
			this.tbpOperacao.Controls.Add(this.lswConexoes);
			this.tbpOperacao.Location = new System.Drawing.Point(4, 22);
			this.tbpOperacao.Name = "tbpOperacao";
			this.tbpOperacao.Size = new System.Drawing.Size(380, 258);
			this.tbpOperacao.TabIndex = 1;
			this.tbpOperacao.Text = "Operação";
			// 
			// lswConexoes
			// 
			this.lswConexoes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.Porta,
																						  this.IPCliente,
																						  this.BytesEnviados,
																						  this.BytesRecebidos,
																						  this.TempoConexao});
			this.lswConexoes.FullRowSelect = true;
			this.lswConexoes.GridLines = true;
			this.lswConexoes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lswConexoes.Location = new System.Drawing.Point(8, 12);
			this.lswConexoes.Name = "lswConexoes";
			this.lswConexoes.Size = new System.Drawing.Size(364, 102);
			this.lswConexoes.TabIndex = 0;
			// 
			// grpConexao
			// 
			this.grpConexao.Controls.Add(this.grpCoenxao);
			this.grpConexao.Controls.Add(this.grpGerais);
			this.grpConexao.Location = new System.Drawing.Point(6, 8);
			this.grpConexao.Name = "grpConexao";
			this.grpConexao.Size = new System.Drawing.Size(360, 218);
			this.grpConexao.TabIndex = 0;
			this.grpConexao.TabStop = false;
			// 
			// cmdFechar
			// 
			this.cmdFechar.Location = new System.Drawing.Point(316, 294);
			this.cmdFechar.Name = "cmdFechar";
			this.cmdFechar.TabIndex = 1;
			this.cmdFechar.Text = "Fechar";
			this.cmdFechar.Click += new System.EventHandler(this.cmdFechar_Click);
			// 
			// grpEstatisticos
			// 
			this.grpEstatisticos.Location = new System.Drawing.Point(10, 120);
			this.grpEstatisticos.Name = "grpEstatisticos";
			this.grpEstatisticos.Size = new System.Drawing.Size(358, 130);
			this.grpEstatisticos.TabIndex = 1;
			this.grpEstatisticos.TabStop = false;
			this.grpEstatisticos.Text = "Estatísticos";
			// 
			// grpGerais
			// 
			this.grpGerais.Controls.Add(this.txtConexoesPorIP);
			this.grpGerais.Controls.Add(this.lblConexoesPorIP);
			this.grpGerais.Controls.Add(this.txtTamanhoMaximoBuffer);
			this.grpGerais.Controls.Add(this.lblTamanhoBuffer);
			this.grpGerais.Controls.Add(this.txtConexoesSimultaneas);
			this.grpGerais.Controls.Add(this.lblConexoesSimultaneas);
			this.grpGerais.Location = new System.Drawing.Point(14, 24);
			this.grpGerais.Name = "grpGerais";
			this.grpGerais.Size = new System.Drawing.Size(328, 98);
			this.grpGerais.TabIndex = 4;
			this.grpGerais.TabStop = false;
			this.grpGerais.Text = "Gerais";
			// 
			// txtTamanhoMaximoBuffer
			// 
			this.txtTamanhoMaximoBuffer.Location = new System.Drawing.Point(230, 66);
			this.txtTamanhoMaximoBuffer.Name = "txtTamanhoMaximoBuffer";
			this.txtTamanhoMaximoBuffer.Size = new System.Drawing.Size(48, 20);
			this.txtTamanhoMaximoBuffer.TabIndex = 7;
			this.txtTamanhoMaximoBuffer.Text = "";
			// 
			// lblTamanhoBuffer
			// 
			this.lblTamanhoBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTamanhoBuffer.Location = new System.Drawing.Point(44, 72);
			this.lblTamanhoBuffer.Name = "lblTamanhoBuffer";
			this.lblTamanhoBuffer.Size = new System.Drawing.Size(178, 14);
			this.lblTamanhoBuffer.TabIndex = 6;
			this.lblTamanhoBuffer.Text = "Tamanho Máximo do Buffer";
			// 
			// txtConexoesSimultaneas
			// 
			this.txtConexoesSimultaneas.Location = new System.Drawing.Point(230, 22);
			this.txtConexoesSimultaneas.Name = "txtConexoesSimultaneas";
			this.txtConexoesSimultaneas.Size = new System.Drawing.Size(48, 20);
			this.txtConexoesSimultaneas.TabIndex = 5;
			this.txtConexoesSimultaneas.Text = "";
			// 
			// lblConexoesSimultaneas
			// 
			this.lblConexoesSimultaneas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblConexoesSimultaneas.Location = new System.Drawing.Point(44, 28);
			this.lblConexoesSimultaneas.Name = "lblConexoesSimultaneas";
			this.lblConexoesSimultaneas.Size = new System.Drawing.Size(178, 14);
			this.lblConexoesSimultaneas.TabIndex = 4;
			this.lblConexoesSimultaneas.Text = "Total de Conexões Simultãneas";
			// 
			// grpCoenxao
			// 
			this.grpCoenxao.Controls.Add(this.txtIPServidor);
			this.grpCoenxao.Controls.Add(this.txtPorta);
			this.grpCoenxao.Controls.Add(this.lblIPServidor);
			this.grpCoenxao.Controls.Add(this.lblPorta);
			this.grpCoenxao.Location = new System.Drawing.Point(16, 130);
			this.grpCoenxao.Name = "grpCoenxao";
			this.grpCoenxao.Size = new System.Drawing.Size(328, 74);
			this.grpCoenxao.TabIndex = 5;
			this.grpCoenxao.TabStop = false;
			this.grpCoenxao.Text = "Conexão";
			// 
			// lblPorta
			// 
			this.lblPorta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPorta.Location = new System.Drawing.Point(42, 24);
			this.lblPorta.Name = "lblPorta";
			this.lblPorta.Size = new System.Drawing.Size(130, 14);
			this.lblPorta.TabIndex = 5;
			this.lblPorta.Text = "Porta de Comunicação";
			// 
			// lblIPServidor
			// 
			this.lblIPServidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblIPServidor.Location = new System.Drawing.Point(42, 46);
			this.lblIPServidor.Name = "lblIPServidor";
			this.lblIPServidor.Size = new System.Drawing.Size(130, 14);
			this.lblIPServidor.TabIndex = 6;
			this.lblIPServidor.Text = "IP do Servidor";
			// 
			// txtConexoesPorIP
			// 
			this.txtConexoesPorIP.Location = new System.Drawing.Point(230, 44);
			this.txtConexoesPorIP.Name = "txtConexoesPorIP";
			this.txtConexoesPorIP.Size = new System.Drawing.Size(48, 20);
			this.txtConexoesPorIP.TabIndex = 9;
			this.txtConexoesPorIP.Text = "";
			// 
			// lblConexoesPorIP
			// 
			this.lblConexoesPorIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblConexoesPorIP.Location = new System.Drawing.Point(44, 50);
			this.lblConexoesPorIP.Name = "lblConexoesPorIP";
			this.lblConexoesPorIP.Size = new System.Drawing.Size(178, 14);
			this.lblConexoesPorIP.TabIndex = 8;
			this.lblConexoesPorIP.Text = "Total de Conexões por IP";
			// 
			// txtPorta
			// 
			this.txtPorta.Location = new System.Drawing.Point(182, 20);
			this.txtPorta.Name = "txtPorta";
			this.txtPorta.Size = new System.Drawing.Size(48, 20);
			this.txtPorta.TabIndex = 8;
			this.txtPorta.Text = "";
			// 
			// txtIPServidor
			// 
			this.txtIPServidor.Location = new System.Drawing.Point(182, 42);
			this.txtIPServidor.Name = "txtIPServidor";
			this.txtIPServidor.Size = new System.Drawing.Size(110, 20);
			this.txtIPServidor.TabIndex = 9;
			this.txtIPServidor.Text = "";
			// 
			// cmdIniciar
			// 
			this.cmdIniciar.Location = new System.Drawing.Point(8, 294);
			this.cmdIniciar.Name = "cmdIniciar";
			this.cmdIniciar.TabIndex = 2;
			this.cmdIniciar.Text = "Iniciar";
			this.cmdIniciar.Click += new System.EventHandler(this.cmdIniciar_Click);
			// 
			// Porta
			// 
			this.Porta.Text = "Porta";
			// 
			// IPCliente
			// 
			this.IPCliente.Text = "IP Cliente";
			// 
			// BytesEnviados
			// 
			this.BytesEnviados.Text = "Bytes Enviados";
			// 
			// BytesRecebidos
			// 
			this.BytesRecebidos.Text = "BytesRecebidos";
			// 
			// TempoConexao
			// 
			this.TempoConexao.Text = "Tempo da Conexão";
			// 
			// cmdSalvar
			// 
			this.cmdSalvar.Location = new System.Drawing.Point(276, 230);
			this.cmdSalvar.Name = "cmdSalvar";
			this.cmdSalvar.TabIndex = 2;
			this.cmdSalvar.Text = "Salvar";
			this.cmdSalvar.Click += new System.EventHandler(this.cmdSalvar_Click);
			// 
			// FormServidorDI
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(400, 319);
			this.Controls.Add(this.cmdIniciar);
			this.Controls.Add(this.cmdFechar);
			this.Controls.Add(this.tbServidor);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormServidorDI";
			this.Text = "Servidor DI";
			this.Load += new System.EventHandler(this.FormServidorDI_Load);
			this.tbServidor.ResumeLayout(false);
			this.tbpConfiguracao.ResumeLayout(false);
			this.tbpOperacao.ResumeLayout(false);
			this.grpConexao.ResumeLayout(false);
			this.grpGerais.ResumeLayout(false);
			this.grpCoenxao.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormServidorDI());
		}

		#region Declarações
			private Servidor mServidor = null;
		#endregion

		#region Eventos
			private void FormServidorDI_Load(object sender, System.EventArgs e)
			{
				InstanciarObjetoServidor();

				CarregarControles();			
			}

			private void cmdSalvar_Click(object sender, System.EventArgs e)
			{
				SalvarControles();			
			}

			private void cmdIniciar_Click(object sender, System.EventArgs e)
			{
				if(mServidor.Estado == Servidor.TipoEstado.Parado)
					IniciarServidor();
				else if(mServidor.Estado == Servidor.TipoEstado.Ativado)
					PararServidor();
			}

			private void cmdFechar_Click(object sender, System.EventArgs e)
			{

			}
		#endregion

		#region Métodos Privados
			/// <summary>
			/// Ativa ou desativa os controles
			/// </summary>
			private void AtivarDesativarControles(bool pValor)
			{
				cmdFechar.Enabled = pValor;

				txtConexoesSimultaneas.Enabled	= pValor;
				txtTamanhoMaximoBuffer.Enabled	= pValor;
				txtConexoesPorIP.Enabled		= pValor;
				txtPorta.Enabled				= pValor;
				txtIPServidor.Enabled			= pValor;
			}

			/// <summary>
			/// Função que captura individualmente cada chave do arquivo de configuração.
			/// Caso haja um erro na leitura do arquivo de configuração, a função retornará
			/// uma string vazia.
			/// </summary>
			/// <param name="pChave">Nome de chave do arquivo de configuração</param>
			/// <returns>Valor contido na chave do arquivo de configuração</returns>
			private string CarregarConfig(string pChave)
			{
				string aRetorno = "";

				try
				{					
					aRetorno = ConfigurationSettings.AppSettings[pChave].ToString();
				}
				catch
				{
					aRetorno = "";
				}				

				return aRetorno;
			}

			/// <summary>
			/// Carrega os valores do arquivo de configuração para os controles
			/// </summary>
			private void CarregarControles()
			{
				txtConexoesSimultaneas.Text = CarregarConfig("Conexoes Simultaneas");
				txtTamanhoMaximoBuffer.Text = CarregarConfig("Tamanho do Buffer");
				txtConexoesPorIP.Text		= CarregarConfig("Conexoes por IP");
				txtPorta.Text				= CarregarConfig("Porta");
				txtIPServidor.Text			= CarregarConfig("IP do Servidor");
			}

			/// <summary>
			/// Inicia o serviço de servidor do objeto
			/// </summary>
			private void IniciarServidor()
			{
				mServidor.MaximoConexoesSimultaneas	= Convert.ToInt32(txtConexoesSimultaneas.Text);
				mServidor.ConexoesPorIP				= Convert.ToInt32(txtConexoesPorIP.Text);
				mServidor.IPServidor				= txtIPServidor.Text;
				mServidor.Porta						= Convert.ToInt32(txtPorta.Text);
				mServidor.TamanhoMaximoBuffer		= Convert.ToInt32(txtTamanhoMaximoBuffer.Text);

				//mServidor.Enfileiramento

				if (!mServidor.Iniciar())
				{
					MessageBox.Show("Não foi possível iniciar o servidor!\n\nErro Retornado:\n\n" + mServidor.Erro);
				}
				else
				{
					cmdIniciar.Text = "Finalizar";
					AtivarDesativarControles(false);
				}
			}

			/// <summary>
			/// Instancia o objeto de servidor.
			/// Caso o objeto não seja instanciado o botão iniciar não será ativado
			/// </summary>
			private void InstanciarObjetoServidor()
			{
				try
				{
					mServidor = new DirecTV.DI.Classes.Servidor();
					cmdIniciar.Enabled = true;
				}
				catch
				{
					cmdIniciar.Enabled = false;
				}
			}

			/// <summary>
			/// Encerra o serviço do servidor
			/// </summary>
			private void PararServidor()
			{
				mServidor.Parar();
				cmdIniciar.Text = "Iniciar";				
				AtivarDesativarControles(true);
			}

			/// <summary>
			/// Salva uma chave específica no arquivo de configuração
			/// </summary>
			/// <param name="pChave">Nome da chave do arquivo de configuração</param>
			/// <param name="pValor">Valor a ser salvo</param>
			/// <returns>Retorna verdadeiro se bem sucedido ou falso se não</returns>
			private bool SalvarConfig(string pChave, string pValor)
			{
				bool aRetorno = false;

				try
				{					
					ConfigurationSettings.AppSettings.Set(pChave, pValor);
					aRetorno = true;
				}
				catch(Exception e)
				{
					MessageBox.Show(e.Message);
					aRetorno = false;
				}				

				return aRetorno;
			}

			/// <summary>
			/// Salva todos os controles no arquivo de configuração
			/// </summary>
			private void SalvarControles()
			{
				if (!SalvarConfig("Conexoes Simultaneas", txtConexoesSimultaneas.Text))
					return;

				if (!SalvarConfig("Tamanho do Buffer", txtTamanhoMaximoBuffer.Text))
					return;

				if (!SalvarConfig("Conexoes por IP", txtConexoesPorIP.Text))
					return;

				if (!SalvarConfig("Porta", txtPorta.Text))
					return;

				if (!SalvarConfig("IP do Servidor", txtIPServidor.Text))
					return;
			}
		#endregion
	}
}
