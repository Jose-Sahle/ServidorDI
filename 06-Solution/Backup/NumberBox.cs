using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.ComponentModel;

public class NumberBox : TextBox
{
	#region Enumerações
		/// <summary>
		/// Define o tipo do controle. Se vai tratar números inteiros ou com casas decimais
		/// </summary>
		public enum NumberBoxTypes
		{
			Number,
			Currency
		}
	#endregion

	#region Campos
		private decimal mMaxValorNumero	= 2147483648;
		private decimal mMinValorNumero	= -2147483648;
		private decimal mMaxValorMoeda	= 9000000000;
		private decimal mMinValorMoeda	= -9000000000;

		private string mDecimalSeparator;
		private string mThousandSeparator;

		private bool mAllowThousand;
		private bool mShowZeroDecimals; 
		private bool mAllowNull;

		private int mDecimalSize;
		private int mSelectionPos;

		private NumberBoxTypes mNumberType;

		private object textValue;

		private decimal mMinValue;
		private decimal mMaxValue;

		private CultureInfo language;
	#endregion

	#region Propriedades
		/// <summary>
		/// Configura ou retorna a quantidade de casas decimais do controle
		/// </summary>
		public int DecimalSize
		{
			get{return mDecimalSize;}
			set
			{
				mDecimalSize = value;
				this.FormatarValor(SubstituirSeparador(base.Text));
			}
		}

		/// <summary>
		/// Indica se será usado ou não separador de milhar
		/// </summary>
		public bool HasThousandSeparator
		{
			get{return mAllowThousand;}
			set
			{
				mAllowThousand = value;
				this.FormatarValor(SubstituirSeparador(base.Text));
			}
		}

		/// <summary>
		/// Determina se os zeros decimais serão exibidos ou não.
		/// </summary>
		public bool ShowZeroDecimals
		{
			get{return mShowZeroDecimals;}		
			set
			{
				mShowZeroDecimals = value;
				this.FormatarValor(SubstituirSeparador(base.Text));
			}
		}

		public NumberBoxTypes NumberType
		{
			get{return mNumberType;}
			set
			{
				mNumberType = value;

				if(mNumberType == NumberBoxTypes.Number)
				{
					mDecimalSize = 0;
					mMinValue = mMinValorNumero;
					mMaxValue = mMaxValorNumero;
				}
				else
				{
					mMinValue = mMinValorMoeda;
					mMaxValue = mMaxValorMoeda;
				}

				this.CapturarSeparadores();
				this.FormatarValor(SubstituirSeparador(base.Text));
			}
		}

		/// <summary>
		/// Determina se o controle permitirá 'null'
		/// </summary>
		public bool AllowNull
		{
			get{return mAllowNull;}
			set
			{
				mAllowNull = value;
				this.FormatarValor(SubstituirSeparador(base.Text));
			}
		}

		
	#endregion
}

/*
using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;

namespace DirecTV.Classes.Controles
{
	/// <summary>
	/// Summary description for NumberBox.
	/// </summary>
	public class NumberBox : System.ComponentModel.Component
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public NumberBox(System.ComponentModel.IContainer container)
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public NumberBox()
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
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
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}
*/