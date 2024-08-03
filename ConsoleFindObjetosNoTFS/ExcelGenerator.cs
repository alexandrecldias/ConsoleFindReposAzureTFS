using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace ConsoleFindObjetosNoTFS
{
    public class ExcelGenerator
    {
        /// <summary>
        /// Gera um arquivo Excel a partir de um DataTable.
        /// </summary>
        /// <param name="dataTable">O DataTable com os dados a serem exportados.</param>
        /// <param name="filePath">O caminho completo onde o arquivo Excel será salvo.</param>
        public void GenerateExcelFile(DataTable dataTable, string filePath)
        {
            // Verifica se o DataTable não é nulo e contém dados
            if (dataTable == null || dataTable.Rows.Count == 0)
                throw new ArgumentException("O DataTable está vazio ou nulo.");

            // Cria um novo workbook
            using (var workbook = new XLWorkbook())
            {
                // Adiciona uma nova worksheet
                var worksheet = workbook.Worksheets.Add("Dados");

                // Adiciona os nomes das colunas
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                }

                // Adiciona as linhas de dados
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = dataTable.Rows[i][j]?.ToString() ?? string.Empty;
                    }
                }

                // Ajusta as colunas ao conteúdo
                worksheet.Columns().AdjustToContents();

                // Salva o arquivo no caminho especificado
                workbook.SaveAs(filePath);
            }
        }
    }
}
