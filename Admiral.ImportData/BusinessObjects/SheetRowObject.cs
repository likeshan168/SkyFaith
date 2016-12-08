using System.Collections.Generic;
using System.Drawing;
using System.Text;
using DevExpress.Spreadsheet;
using DevExpress.Xpo;

namespace Admiral.ImportData
{
    public class SheetRowObject
    {
        public SheetRowObject(SheetContext context)
        {
            this.context = context;
        }

        SheetContext context;

        public int Row { get; set; }

        public XPBaseObject Object { get; set; }

        public Row RowObject { get; set; }

        StringBuilder message = new StringBuilder();
        List<Cell> errorCells = new List<Cell>();
        public void AddErrorMessage(string msg, Cell errorCell)
        {
            HasError = true;
            message.Append(msg);
            errorCells.Add(errorCell);

            errorCell.FillColor = Color.Red;
            errorCell.Font.Color = Color.White;
            var s = errorCell.Worksheet;
            var tipCell = s.Cells[errorCell.RowIndex, 0];
            tipCell.Value = tipCell.DisplayText + "\n" + msg;
        }

        public void AddErrorMessage(string msg, IEnumerable<string> properties)
        {
            RowObject[0].Value = RowObject[0].DisplayText + "\n" + msg + "´íÎó×Ö¶Î:" + string.Join(",", properties);
            foreach (var item in properties)
            {
                var cell = context.GetErrorCellByPropertyName(item, Row);
                cell.FillColor = Color.Red;
                cell.Font.Color = Color.White;
            }
        }

        public bool HasError { get; private set; }

        public string GetErrorMessage()
        {
            return message.ToString();
        }

    }
}