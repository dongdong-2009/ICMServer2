using Microsoft.Office.Interop.Excel;
using System.Windows.Controls;

namespace ICMServer.Services
{
    public class PrintService : IPrintService
    {
        public void Print(DataGrid grid)
        {
            if (grid == null)
                return;

            Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
            Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)xla.ActiveSheet;

            xla.Visible = true;

            for (int i = 0; i < grid.Columns.Count; ++i)
            {
                ws.Cells[1, i + 1].Font.Bold = true;
                ws.Cells[1, i + 1] = grid.Columns[i].Header;
            }

            for (int i = 0; i < grid.Items.Count; ++i)
            {
                for (int j = 0; j < grid.Columns.Count; ++j)
                {
                    //if (grid.Columns[i].GetType().ToString() == "System.Windows.Controls.DataGridTextColumn")
                        ws.Cells[i + 2, j + 1] = (grid.Columns[j].GetCellContent(grid.Items[i]) as TextBlock).Text;
                }
            }
        }
    }
}
