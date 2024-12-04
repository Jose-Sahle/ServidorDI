using System;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace DirecTV.DI.Classes
{
	/// <summary>
	/// Classe que encapsula o socket
	/// </summary>
	public class ClienteSocket
	{
		#region Declarações
			private Socket mSocketCliente = null;

			private Thread mThreadEscuta  = null;
			private Thread mThreadEscrita = null;

			private bool mPararThreadEscuta  = false;
			private bool mPararThreadEscrita = false;

			private bool mMarcadoParaExclusao = false;
			private int  mTamanhoMaximoBuffer = 1;
			private string mErro = "";

			private object mEnfileiramento = null;
		#endregion

		#region Métodos Público
			/// <summary>
			/// Construtor da classe. 
			/// </summary>
			/// <param name="pSocketCliente">Socket que efetuará a conexão</param>
			public ClienteSocket(Socket pSocketCliente)
			{
				mTamanhoMaximoBuffer = 1024; // 1K

				mSocketCliente = pSocketCliente;

				mMarcadoParaExclusao = false;
			}

			/// <summary>
			/// Destrutor da classe
			/// </summary>
			~ClienteSocket()
			{
				PararSocket();
			}

			/// <summary>
			/// Inicia a thread que efetuará a escuta do socket
			/// </summary>
			public void InicarThreadDeEscuta()
			{
				mThreadEscuta = new Thread(new ThreadStart(SocketListenerThreadStart));
				mThreadEscuta.Start();

				mPararThreadEscuta = false;
				mMarcadoParaExclusao = false;
			}
			
			/// <summary>
			/// Inicia a thread que efetuará a escrita do socket
			/// </summary>
			public void IniciarThreadDeEscrita()
			{
				mThreadEscrita = new Thread(new ThreadStart(SocketWriterThreadStart));
				//mThreadEscrita.Start();

				mPararThreadEscrita = false;
				mMarcadoParaExclusao = false;
			}

			public void PararSocket()
			{
				if (mSocketCliente != null)
				{
					// Variável de controle para que as thread de escuta e escrita finalizem
					mPararThreadEscrita = true;
					mPararThreadEscuta  = true;

					// Fecha o socket
					mSocketCliente.Close(); 

					// Determina 1 segundo para que as threads sejam finalizadas naturalmente
					if (mThreadEscrita.IsAlive)
						mThreadEscrita.Join(1000); 

					mThreadEscuta.Join(1000);

					// Se as threads não pararam espontãneamente, serão derrubadas
					if (mThreadEscuta.IsAlive)
						mThreadEscuta.Abort();

					if (mThreadEscrita.IsAlive)
						mThreadEscrita.Abort();

					// Limpa os objetos
					mThreadEscuta  = null;
					mThreadEscrita = null;
					mSocketCliente = null;

					mMarcadoParaExclusao = true;
				}
			}
		#endregion

		#region Métodos Privados
			/// <summary>
			/// Thread que utilizada para ficar escutando no socket
			/// </summary>
			private void SocketListenerThreadStart()
			{
				int aTamanhoBufferLido = 0;

				Byte [] aBuffer = new Byte[mTamanhoMaximoBuffer];

				while (!mPararThreadEscuta)
				{
					try
					{
						aTamanhoBufferLido = mSocketCliente.Receive(aBuffer, mTamanhoMaximoBuffer, System.Net.Sockets.SocketFlags.None);
						GravarPacote(ref aBuffer, aTamanhoBufferLido);
					}
					catch (SocketException se)
					{	
						mErro = se.Message;
						mPararThreadEscuta   = true;
						mMarcadoParaExclusao = true;
					}
				}
			}
			
			/// <summary>
			/// Thread utilizada para ficar escrevendo no socket
			/// </summary>
			private void SocketWriterThreadStart()
			{
				Byte [] aBuffer = new Byte[mTamanhoMaximoBuffer];
				
				while (!mPararThreadEscrita)
				{
					try
					{
						LerPacote(ref aBuffer);
						mSocketCliente.Send(aBuffer);
					}
					catch (SocketException se)
					{	
						mErro = se.Message;
						mPararThreadEscrita  = true;
						mMarcadoParaExclusao = true;
					}
				}
			}

			/// <summary>
			/// Grava os dados do pacote no objeto que foi passado como referência 
			/// para tratamento de filas de pacote.
			/// </summary>
			/// <param name="pBuffer">Pacote que foi lido pelo socket</param>
			/// <param name="pTamanhoBufferLido">Tamanho do buffer lido</param>
			private void GravarPacote(ref Byte[] pBuffer, int pTamanhoBufferLido)
			{
				//mEnfileiramento.Adicionar("FilaDeEntrada", pBuffer, pTamanhoBufferLido);
			}

			/// <summary>
			/// Lê os dados do objeto de enfileiramento de pacotes de saída
			/// </summary>
			/// <param name="pBuffer"></param>
			private void LerPacote(ref Byte[] pBuffer)
			{
				//mEnfileiramento.Retirar("FilaDeEntrada", pBuffer);
			}
		#endregion

		#region Propriedades
			/// <summary>
			/// Indica se o objeto ClienteSocket deve ser excluído
			/// </summary>
			public bool MarcadoParaExclusao
			{
				get{return mMarcadoParaExclusao;}
				set{mMarcadoParaExclusao = value;}
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
			/// Retorna o último erro ocorrido no objeto
			/// </summary>
			public string Erro
			{
				get{return mErro;}
			}

			/// <summary>
			/// Representa o objeto para enfileiramento de mensagens.
			/// 
			/// Valor Padrão: null
			/// 
			/// O objeto de enfileiramento deverá possuir três métodos:
			/// 
			/// bool Adicionar(string pNomeFila, ref byte[] pMensagem, int pBufferLido) 
			/// bool Retirar(string pNomeFila)
			/// bool Iniciar()
			/// </summary>
			public object Enfileiramento
			{
				get{return mEnfileiramento;}
				set{mEnfileiramento = value;}
			}
		#endregion
	}
}
