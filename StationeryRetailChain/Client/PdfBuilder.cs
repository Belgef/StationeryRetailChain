using StationeryRetailChain.Shared.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf.Interactive;

namespace StationeryRetailChain.Client
{
    public class PdfBuilder
    {
        public static MemoryStream CreateReceipt(Receipt item, IEnumerable<StockProduct> stock)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            PdfBrush solidBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            RectangleF bounds = new RectangleF(0,90, graphics.ClientSize.Width, 30);
            graphics.DrawRectangle(solidBrush, bounds);
            PdfFont subHeadingFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 14);
            PdfTextElement element = new PdfTextElement("Receipt #" + item.ReceiptNumber.ToString(), subHeadingFont);
            element.Brush = PdfBrushes.White;
            PdfLayoutResult result = element.Draw(page, new PointF(10, bounds.Top + 8));
            string currentDate = "Purchase date: " + item.PurchaseDate.ToString("dd/MM/yyyy HH:mm:ss");
            SizeF textSize = subHeadingFont.MeasureString(currentDate);
            PointF textPosition = new PointF(graphics.ClientSize.Width - textSize.Width - 10, result.Bounds.Y);
            graphics.DrawString(currentDate, subHeadingFont, element.Brush, textPosition);
            PdfFont timesRoman = new PdfStandardFont(PdfFontFamily.TimesRoman, 10);

            element = new PdfTextElement("Purchase receipt", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 25));

            PdfPen linePen = new PdfPen(new PdfColor(126, 151, 173), 0.70f);
            PointF startPoint = new PointF(0, result.Bounds.Bottom + 3);
            PointF endPoint = new PointF(graphics.ClientSize.Width, result.Bounds.Bottom + 3);
            graphics.DrawLine(linePen, startPoint, endPoint);

            element = new PdfTextElement($"Seller: {item.Seller.EmployeeName}", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 5));

            element = new PdfTextElement($"Shop address: {item.Seller.Shop.StreetAddress}, {item.Seller.Shop.City.CityName}, {item.Seller.Shop.City.State.StateName}", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 5));

            element = new PdfTextElement($"Customer: {item.Customer.CustomerName}", timesRoman);
            element.Brush = new PdfSolidBrush(new PdfColor(126, 155, 203));
            result = element.Draw(page, new PointF(10, result.Bounds.Bottom + 5));

            PdfGrid grid = new PdfGrid();
            grid.DataSource = item.Items.Select(e => new { Name = stock.Where(e2 => e.StockProductId == e2.StockProductId).First().StationeryProduct.Name, SellQuantity = e.SellQuantity, SellPrice = e.SellPrice, TotalPrice = e.SellPrice * e.SellQuantity }).AsEnumerable();
            PdfGridCellStyle cellStyle = new PdfGridCellStyle();
            cellStyle.Borders.All = PdfPens.White;
            PdfGridRow header = grid.Headers[0];
            PdfGridCellStyle headerStyle = new PdfGridCellStyle();
            headerStyle.Borders.All = new PdfPen(new PdfColor(126, 151, 173));
            headerStyle.BackgroundBrush = new PdfSolidBrush(new PdfColor(126, 151, 173));
            headerStyle.TextBrush = PdfBrushes.White;
            headerStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 14f, PdfFontStyle.Regular);
            for (int i = 0; i < header.Cells.Count; i++)
            {
                if (i == 0 || i == 1)
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle);
                else
                    header.Cells[i].StringFormat = new PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle);
            }
            header.ApplyStyle(headerStyle);
            cellStyle.Borders.Bottom = new PdfPen(new PdfColor(217, 217, 217), 0.70f);
            cellStyle.Font = new PdfStandardFont(PdfFontFamily.TimesRoman, 12f);
            cellStyle.TextBrush = new PdfSolidBrush(new PdfColor(131, 130, 136));
            //Creates the layout format for grid
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            // Creates layout format settings to allow the table pagination
            layoutFormat.Layout = PdfLayoutType.Paginate;
            //Draws the grid to the PDF page.
            PdfGridLayoutResult gridResult = grid.Draw(page, new RectangleF(new PointF(0, result.Bounds.Bottom + 40), new SizeF(graphics.ClientSize.Width, graphics.ClientSize.Height - 100)), layoutFormat);
            
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            stream.Position = 0;
            return stream;
        }
    }
}
