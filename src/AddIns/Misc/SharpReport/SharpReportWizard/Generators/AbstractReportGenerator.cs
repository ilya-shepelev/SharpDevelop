//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

using ICSharpCode.Core;
	
using SharpReport;
using SharpReportCore;
	
using SharpQuery;
using SharpQuery.Collections;
using SharpQuery.Connection;
using SharpQuery.SchemaClass;
	/// <summary>
	/// Abstract Class for all ReportGenerators
	/// </summary>
	/// <remarks>
	/// 	created by - Forstmeier Peter
	/// 	created on - 07.09.2005 14:21:07
	/// </remarks>
namespace ReportGenerator {	
	
	public class AbstractReportGenerator : IReportGenerator {
		private ReportModel reportModel;
		private ReportGenerator reportGenerator;
		private Properties customizer;
		private SharpReportManager manager;
		
		public AbstractReportGenerator() {
		}
		
		public AbstractReportGenerator(Properties customizer,ReportModel reportModel){
			this.customizer = customizer;
			this.reportModel = reportModel;
			if (reportModel == null) {
				throw new ArgumentNullException("reportModel");
			}
			reportGenerator = (ReportGenerator)customizer.Get("Generator");
			manager = new SharpReportManager();
		}
		
		#region ReportGenerator.IReportGenerator interface implementation
		public virtual void GenerateReport() {
			throw new NotImplementedException("must be overriden");
		}
		
		#endregion
		
		
		
		//TODO Change these function to using SharpQuery
		protected DataTable GenerateFieldsTable(ReportModel reportModel) {
			if (reportModel == null) {
				throw new ArgumentNullException("reportModel");
			}
			if (reportModel.ReportSettings.ConnectionString.Length == 0) {
				throw new ArgumentException("CreateOLEDB Connection : No ConnectionString");
			}
			OleDbConnection connection = null;
			OleDbCommand command = null;
			try {
				
				connection = new OleDbConnection(reportModel.ReportSettings.ConnectionString);
				connection.Open();
				
				if (connection == null) {
					throw new ArgumentNullException("AbstractReportGenerator:GenerateFieldsTable <connection");
				}
				command = connection.CreateCommand();
				
				command.CommandText = reportModel.ReportSettings.CommandText;
				command.CommandType = reportModel.ReportSettings.CommandType;
				
				// If needed Add some parameters
				if (reportModel.ReportSettings.SqlParametersCollection != null &&
				    reportModel.ReportSettings.SqlParametersCollection.Count > 0) {
					int rpc = reportModel.ReportSettings.SqlParametersCollection.Count;
					OleDbParameter oleDBPar = null;
					SqlParameter rpPar;
					for (int i = 0;i < rpc ;i++) {
						rpPar = (SqlParameter)reportModel.ReportSettings.SqlParametersCollection[i];
						System.Console.WriteLine("{0} {1} {2}",rpPar.ParameterName,rpPar.DataType,rpPar.DefaultValue);
						
						
						if (rpPar.DataType != System.Data.DbType.Binary) {
							oleDBPar = new OleDbParameter(rpPar.ParameterName,
							                              rpPar.DataType);
							oleDBPar.Value = rpPar.DefaultValue;
						} else {
							System.Console.WriteLine("binary");
							oleDBPar = new OleDbParameter(rpPar.ParameterName,
							                              System.Data.DbType.Binary);
						}
						oleDBPar.Direction = rpPar.ParameterDirection;
						command.Parameters.Add(oleDBPar);
						
					}
				}
			} catch (Exception e) {
				throw e;
			}
			OleDbDataReader reader = null;
			DataTable schemaTable = null;
			try {
				if (connection.State != ConnectionState.Open) {
					connection.Open();
				}
				reader = command.ExecuteReader(CommandBehavior.KeyInfo);
				schemaTable = reader.GetSchemaTable();
				return schemaTable;
			} catch (Exception e) {
				
				throw e;
			} finally {
				if (reader != null) {
					reader.Close();
				}
				
				connection.Close();
			}
		}
		
	
		
		protected void BuildStandartSections () {
			foreach (ReportSection section in this.reportModel.SectionCollection) {
				section.Size = new Size (section.Size.Width,
				                         SharpReportCore.GlobalValues.DefaultSectionHeight);
			}
		}
		
		#region Properties
		
		public Properties Customizer {
			get {
				return customizer;
			}
		}
		public ReportGenerator ReportGenerator {
			get {
				return reportGenerator;
			}
		}
		public ReportModel ReportModel {
			get {
				return reportModel;
			}
		}
		public SharpReportManager Manager {
			get {
				return manager;
			}
		}
		
		#endregion
		
	}
}
