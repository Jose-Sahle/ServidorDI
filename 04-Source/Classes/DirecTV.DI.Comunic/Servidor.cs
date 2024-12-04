using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections;

namespace DirecTV.DI.Classes
{
	/// <summary>
	/// Classe para tratar comunicação TCP/IP.
	/// </summary>
	public class Servidor
	{
		#region Enumerações
			/// <summary>
			/// Determina o estado do servidor
			/// 
			/// 1 - Parado
			/// 2 - Ativado
			/// 3 - Iniciando
			/// 4 - Finalizando
			/// </summary>
			public enum TipoEstado
			{
				Parado,
				Ativado,
				Iniciando,
				Finalizando
			}
		#endregion

		#region Declarações
			// Dados de funcionamento do servidor
			private int mMaximoConexoesSimultaneas;
			private int mConexoesPorIP;
			private object mEnfileiramento;
			private int mTamanhoMaximoBuffer;
			private TipoEstado mEstado;

			// Dados de conexão
			private TcpListener mServidor = null;
			private IPAddress mIPServidor = null;
			private int mPorta;

			// Controle da Thread de escuta na porta de comunicação
			private Thread mServerThread = null;
			private bool   mPararServidor;

			// Controle da Thread de limpeza dos sockets pendentes de exclusão
			private Thread mLimparClientSocket = null;
			private bool   mPararLimpezaSocket;

			// Dados genéricos
			private string mErro = "";
			private ArrayList mListaSocketCliente = null;
		#endregion

		#region Métodos Públicos
			/// <summary>
			/// Contrutor da classe que inicializa as variáveis de configuração com valores padrão
			/// </summary>
			public Servidor()
			{				
				mMaximoConexoesSimultaneas = 1;
				mEnfileiramento = null;
				mPorta = 5000;
				mIPServidor = IPAddress.Parse("127.0.0.1"); 

				mConexoesPorIP = 1;

				mTamanhoMaximoBuffer = 1024; //1K

				mPararServidor = false;
				mPararLimpezaSocket = false;

				mEstado = TipoEstado.Parado;
			}

			/// <summary>
			/// Destrutor da classe
			/// </summary>
			~Servidor()
			{
				Parar();
			}

			/// <summary>
			/// Inicia o Servidor
			/// </summary>
			/// <returns></returns>
			public bool Iniciar()
			{
				bool aSucesso = false;
				
				try
				{
					mEstado = TipoEstado.Iniciando;

					IPEndPoint aServerPort = new IPEndPoint(mIPServidor, mPorta);
					mServidor = new TcpListener(aServerPort);

					if (mServidor != null)
					{
						mPararServidor = false;
						mPararLimpezaSocket = false;

						mListaSocketCliente = new ArrayList();

						mServidor.Start();

						// Inicia a thread que receberá as solicitações do socket
						mServerThread = new Thread(new ThreadStart(ServerThreadStart));
						mServerThread.Start();

						// Inicia a thread que cuidará da exclusão dos sockets não mais utilizados
						mLimparClientSocket = new Thread(new ThreadStart(LimparClientSocket));
						mLimparClientSocket.Start();

						aSucesso = true;
						mEstado = TipoEstado.Ativado;
					}
					else
					{
						mErro = "Impossível iniciar o servidor. 'TcpListener' falhou!";
						mEstado = TipoEstado.Parado;
					}
				}
				catch(Exception e)
				{
					mErro = e.Message;
					mEstado = TipoEstado.Parado;
				}
				
				return aSucesso;
			}

			/// <summary>
			/// Para o processo do servidor
			/// </summary>
			public void Parar()
			{
				if (mServidor != null)
				{
					mEstado = TipoEstado.Finalizando;
					// É importante parar o servidor antes de fazer qualquer limpeza de objetos
					
					/* Parando a thread de exclusão de sockets */
					mPararLimpezaSocket = true;
					mLimparClientSocket.Join(1000); // Espera 1 segundo para a thread ser finalizada
					if(mLimparClientSocket.IsAlive) // Se a thread ainda estiver ativa, será derrubada
						mLimparClientSocket.Abort();
					mLimparClientSocket = null;
					
					/* Parando o servidor TCP/IP */
					mPararServidor = true;
					mServidor.Stop();		
					mServerThread.Join(1000); // Espera por 1 segundo para a thread parar. 					
					if (mServerThread.IsAlive) // Se a thread ainda estiver ativa, será derrubada.
						mServerThread.Abort();
					mServerThread = null;

					// Libera o objeto
					mServidor = null;

					// Para a comunicação com todos os clientes
					PararTodosSockets();

					mEstado = TipoEstado.Parado;
				}
			}
		#endregion

		#region Métodos Privados
			private void EfetuarLimpezaClienteSocket()
			{
				ArrayList aExcluidos = new ArrayList();

				// Verifica se existe algum socket marcado para exclusão,
				// então coloca-o em uma lista separada e depois elimina 
				// da lista principal
				lock(mListaSocketCliente)
				{
					// Verifica quais estão marcados, adiciona-o na lista de excluidos
					// E para o socket e libera os objetos
					foreach(ClienteSocket aClienteSocket in mListaSocketCliente)
					{
						if(aClienteSocket.MarcadoParaExclusao)
						{
							aExcluidos.Add(aClienteSocket);
							aClienteSocket.PararSocket();
						}
					}

					// Exclui todos os objetos que foram marcados para exclusão da lista
					// principal
					for (int aContador = 0; aContador < aExcluidos.Count;aContador++)
					{
						mListaSocketCliente.Remove(aExcluidos[aContador]);
					}
				} // lock(mListaSocketCliente)

				aExcluidos.Clear();
				aExcluidos = null;
			}

			/// <summary>
			/// Thread que ficará escutando a porta identificada na propriedade Porta
			/// </summary>
			private void ServerThreadStart()
			{
				Socket aSocket = null;
				ClienteSocket aClienteSocket = null;

				while(!mPararServidor)
				{
					try
					{
						// Fica esperando algum cliente requisitar uma conexão com o servidor
						aSocket = mServidor.AcceptSocket();

						// Verifica quantas conexões estão ativas
						lock(mListaSocketCliente)
						{
							// Limpa os sockets que estão marcados pra exclusão
							EfetuarLimpezaClienteSocket();

							// Verifica se já se atingiu a quantidade máxima de conexões
							if (mListaSocketCliente.Count == mMaximoConexoesSimultaneas)
							{
								aSocket.Close();
								continue;
							}

							aClienteSocket = new ClienteSocket(aSocket);
						}

						// Cria um objeto de controle do socket cliente
						lock(aClienteSocket)
						{

							aClienteSocket.TamanhoMaximoBuffer = mTamanhoMaximoBuffer;
							aClienteSocket.Enfileiramento = mEnfileiramento;
						}

						lock(mListaSocketCliente)
						{
							mListaSocketCliente.Add(aClienteSocket);
						}

						lock(aClienteSocket)
						{
							aClienteSocket.InicarThreadDeEscuta();
							aClienteSocket.IniciarThreadDeEscrita();
						}
					}
					catch(Exception e)
					{
						mErro = e.Message;
						mPararServidor = true;
					}
				}
			}

			/// <summary>
			/// Thread encarregada de apagar as refências de sockets marcados para exclusão
			/// </summary>
			private void LimparClientSocket()
			{
				while (!mPararLimpezaSocket)
				{
					EfetuarLimpezaClienteSocket();

					Thread.Sleep(1000);

				} // while(!mPararLimpezaSocket)
			}

			private void PararTodosSockets()
			{
				// Faz a chamada de todos os clientes sockets
				foreach(ClienteSocket aClienteSocket in mListaSocketCliente)
					aClienteSocket.PararSocket();
				
				mListaSocketCliente.Clear();
				mListaSocketCliente = null;
			}
		#endregion

		#region Propriedades
			/// <summary>
			/// Determina a quantidade de conexões simultâneas que serão aceitas.
			/// 
			/// Valor Padrão: 1
			/// </summary>
			public int MaximoConexoesSimultaneas
			{
				get{return mMaximoConexoesSimultaneas;}
				set{mMaximoConexoesSimultaneas = value;}
			}

			/// <summary>
			/// Representa o objeto para enfileiramento de mensagens.
			/// 
			/// Valor Padrão: null
			/// 
			/// O objeto de enfileiramento deverá possuir três métodos:
			/// 
			/// bool AdicionarFila(string pNomeFila, ref byte[] pMensagem, int pBufferLido) 
			/// bool RetirarFila(string pNomeFila)
			/// bool Iniciar()
			/// </summary>
			public object Enfileiramento
			{
				get{return mEnfileiramento;}
				set{mEnfileiramento = value;}
			}

			/// <summary>
			/// Determina o tamanho máximo do buffer de comunicação
			/// 
			/// Valor Padrão: 1
			/// </summary>
			public int TamanhoMaximoBuffer
			{
				get{return mTamanhoMaximoBuffer;}
				set{mTamanhoMaximoBuffer = value;}
			}

			/// <summary>
			/// Porta utilizada para a troca de informação.
			/// 
			/// Valor Padrão: 5000
			/// </summary>
			public int Porta
			{
				get{return mPorta;}
				set{mPorta = value;}
			}

			/// <summary>
			/// Endereço IP que ficará ouvindo (O proprio computador)
			/// 
			/// Valor Padrão: 127.0.0.1 (localhost)
			/// </summary>
			public string IPServidor
			{
				get{return mIPServidor.ToString();}
				set
				{
					mIPServidor = IPAddress.Parse(value);
				}
			}

			public int ConexoesPorIP
			{
				get{return mConexoesPorIP;}
				set{mConexoesPorIP = value;}
			}

			// Propriedades de leitura

			/// <summary>
			/// Contém o último erro ocorrido na classe
			/// </summary>
			public string Erro
			{
				get{return mErro;}				
			}

			/// <summary>
			/// Quantidade de conexões (sockets) ativos
			/// </summary>
			public int ConexoesAtivas
			{
				get
				{
					int aRetorno;

					lock(mListaSocketCliente)
					{
						aRetorno = mListaSocketCliente.Count;
					}

					return aRetorno;
				}
			}

			/// <summary>
			/// Retorna o estado atual do objeto servidor que pode ser:
			/// 1 - Parado
			/// 2 - Ativado
			/// 3 - Iniciando
			/// 4 - Finalizando
			/// </summary>
			public TipoEstado Estado
			{
				get{return mEstado;}
			}
		#endregion
	}
}
