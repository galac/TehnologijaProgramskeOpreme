using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPOZdejPaZares
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /* HttpCookie cookies = Request.Cookies["banningCookie"];
             if (cookies != null)
             {
                 if (cookies.Value == "yes")
                 {
                     Response.Redirect("BlokiranIp.aspx");
                 }
             }*/
        }

            
        public static DataTable GetData(MySqlCommand cmd)
        {
            DataTable dt = new DataTable();
            string connectionString = "server=slavnik.fri.uni-lj.si;user id=mh6946;persistsecurityinfo=True;database=t8_2015;password=j7vukcgreh";
            MySqlConnection con = new MySqlConnection(connectionString);
            MySqlDataAdapter sda = new MySqlDataAdapter();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                sda.Dispose();
                con.Dispose();
            }
        }

        //public static void exportToPdf(String strQuery, HttpResponse response) {

        //    MySqlCommand cmd = new MySqlCommand(strQuery);
        //    DataTable dt = _Default.GetData(cmd);
        //    GridView GridView1 = new GridView();

        //    GridView1.AllowPaging = false;
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //    response.ContentType = "application/pdf";
        //    response.AddHeader("content-disposition", "attachment;filename=DataTable.pdf");
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);
        //    GridView1.RenderControl(hw);
        //    StringReader sr = new StringReader(sw.ToString());
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //    PdfWriter.GetInstance(pdfDoc, response.OutputStream);
        //    pdfDoc.Open();
        //    htmlparser.Parse(sr);
        //    pdfDoc.Close();
        //    response.Write(pdfDoc);
        //    response.End();
        //}

        public static void exportToPdf(DataTable dt, HttpResponse response, String podnaslov, int[] widths)
        {
            GridView GridView1 = new GridView();
            //Podpora šumnikov
            BaseFont bfBold = BaseFont.CreateFont("c:/windows/fonts/arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont bfNormal = BaseFont.CreateFont("c:/windows/fonts/arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);


            GridView1.DataSource = dt;
            GridView1.DataBind();

            GridView1.AllowPaging = false;
            GridView1.AutoGenerateColumns = true;
            int n = dt.Columns.Count;
            Debug.Print("" + n + " column od gridview: " + GridView1.Columns.Count);
            if (dt.Columns.Count <= 0)
            {
                Debug.Print("jeba");
            }
            else
            {

                PdfPTable table = new PdfPTable(dt.Columns.Count+1);
                //int[] widths = new int[dt.Columns.Count];
                table.SetWidths(widths);
                Phrase pFirst = new Phrase("Št.");
                pFirst.Font.Size = 15;
                PdfPCell cellFirst = new PdfPCell(pFirst);
                //Color c = ColorTranslator.FromHtml("#008000");
                //BaseColor color = new BaseColor(red, green, blue); // or red, green, blue, alpha
                cellFirst.BackgroundColor = BaseColor.LIGHT_GRAY;
                cellFirst.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cellFirst);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    //PdfPHeaderCell cell = new PdfPHeaderCell(new Phrase(GridView1.Rows[0].Cells[i].Text.ToString));
                    //widths[i] = (int)GridView1.Columns[i].ItemStyle.Width.Value;
                    //PdfPCell cell = new PdfPCell(new Phrase(dt.Rows[0].ItemArray[i].ToString()));
                    //Phrase p = new Phrase(dt.Columns[i].ColumnName, new iTextSharp.text.Font(bfNormal, 12));
                    //p.Font.Size = 15;
                    PdfPCell cell = new PdfPCell(new Phrase(dt.Columns[i].ColumnName, new iTextSharp.text.Font(bfNormal, 12)));
                    //BaseColor color = new BaseColor(red, green, blue); // or red, green, blue, alpha
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    //PdfPCell cellk = new PdfPCell(new Phrase("Locations"));
                }
                //table.SetWidths(widths);

                //Transfer rows from GridView to table
                int count = 0;
                BaseColor alternateRowColor = new BaseColor(210, 228, 223);
                foreach (DataRow dr in dt.Rows)
                {
                    PdfPCell countCell = new PdfPCell(new Phrase((count+1) + "."));
                    if (count % 2 == 1)
                    {
                        countCell.BackgroundColor = alternateRowColor;
                    }
                    countCell.PaddingBottom = 4;
                    countCell.PaddingLeft = 5;
                    countCell.PaddingRight = 5;
                    countCell.PaddingTop = 4;
                    countCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(countCell);
                    foreach (DataColumn dc in dt.Columns)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dr[dc].ToString(), new iTextSharp.text.Font(bfNormal, 12)));
                        if (count % 2 == 1)
                        {
                            cell.BackgroundColor = alternateRowColor;
                        }
                        cell.PaddingBottom = 4;
                        cell.PaddingLeft = 5;
                        cell.PaddingRight = 5;
                        cell.PaddingTop = 4;
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                    count++;
                }


                    //Create the PDF Document
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 130f, 0f);
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, response.OutputStream);
                    ITextEvents ite = new ITextEvents();
                    ite.Header = podnaslov;
                    pdfWriter.PageEvent = ite;
                    pdfDoc.Open();
                    pdfDoc.Add(table);
                    pdfDoc.Close();
                    response.ContentType = "application/pdf";
                    response.AddHeader("content-disposition", "attachment;" +
                                                   "filename=FRI_izpis.pdf");
                    response.Cache.SetCacheability(HttpCacheability.NoCache);
                    response.Write(pdfDoc);
                    response.End();

                    //response.ContentType = "application/pdf";
                    //response.AddHeader("content-disposition", "attachment;filename=DataTable.pdf");
                    //StringWriter sw = new StringWriter();
                    //HtmlTextWriter hw = new HtmlTextWriter(sw);
                    //GridView1.RenderControl(hw);
                    //StringReader sr = new StringReader(sw.ToString());
                    //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    //pdfDoc.AddHeader("Header", "testni header, deluje?");
                    //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    //PdfWriter.GetInstance(pdfDoc, response.OutputStream);
                    //pdfDoc.Open();
                    //htmlparser.Parse(sr);
                    //pdfDoc.Close();
                    //response.Write(pdfDoc);
                    //response.End();
                
            }
        }



        public static void exportToCsv(DataTable dt, HttpResponse response)
        {
            response.Clear();
            response.Buffer = true;
            response.AddHeader("content-disposition", "attachment;filename=DataTable.csv");
            response.Charset = "";
            response.ContentType = "application/text";
            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            response.Output.Write(sb.ToString());
            response.Flush();
            response.End();
        }
    }

}