using iTextSharp.text;
using iTextSharp.text.pdf;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace SSSNP_SNSOP.Core.BuilderPdfGenerator
{
    public class BuilderPdfTemplate
    {
        BuilderPdfStyle _commonStyle = new BuilderPdfStyle();

        // Object Created
        // Object Created
        protected PdfPTable AddInWrapperContainer(PdfPTable table)
        {
            PdfPCell _wrapperCell = CreateWrapperCell();

            _wrapperCell.Border = 0;
            _wrapperCell.PaddingLeft = this._commonStyle.PaddingLeftTableFirstCell;
            _wrapperCell.AddElement(table);

            return CreateWrapperContainer(_wrapperCell);
        }

        protected PdfPTable CreateWrapperContainer()
        {
            PdfPTable _wrapper = new PdfPTable(1);

            _wrapper.WidthPercentage = 100;
            _wrapper.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

            return _wrapper;
        }

        protected PdfPTable CreateWrapperContainer(PdfPCell wrapperCell)
        {
            PdfPTable _wrapperContainer = this.CreateWrapperContainer();

            _wrapperContainer.AddCell(wrapperCell);

            return _wrapperContainer;
        }

        protected void AddWrapperContainer(ref PdfPTable _wrapper, PdfPCell wrapperCell)
        {
            _wrapper.AddCell(wrapperCell);
        }

        protected PdfPCell CreateWrapperCell()
        {
            return new PdfPCell() { Border = 0 };
        }

        protected PdfPCell CreateWrapperCell(float paddingLeft)
        {
            return new PdfPCell() { Border = 0, PaddingLeft = paddingLeft };
        }

        protected PdfPTable HeaderTableLogo(string fullName)
        {
            PdfPTable headerLogo = new PdfPTable(1);

            iTextSharp.text.Image logoImage = iTextSharp.text.Image.GetInstance(fullName);
            logoImage.Alignment = Element.ALIGN_CENTER;
            logoImage.ScalePercent(60);

            PdfPCell cell = new PdfPCell(logoImage);

            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BorderWidth = 0f;
            headerLogo.AddCell(cell);

            return headerLogo;
        }

        protected PdfPTable TitleNode(string text)
        {
            Image _image = this.LoadImage(this.BuilderPdfGeneratorCommonStyle.Image_Header);

            PdfPTable tb = new PdfPTable(2);
            tb.HorizontalAlignment = 0;
            //tb.TotalWidth = 550f;
            tb.WidthPercentage = 100f;
            //  tb.LockedWidth = true;

            tb.SetWidths(new float[] { 4.5f, 90f });
            tb.SpacingBefore = BuilderPdfGeneratorCommonStyle.TitleNodeSpacingBefore;

            #region Cell Image

            PdfPCell cellImage = new PdfPCell();

            cellImage.BackgroundColor = new BaseColor(102, 178, 255);
            //cellImage.BorderWidth = 0f;
            cellImage.AddElement(_image);
            cellImage.Rowspan = 1;
            cellImage.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cellImage.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellImage.BorderWidthTop = 0.5f;
            cellImage.BorderWidthBottom = 0.7f;
            cellImage.BorderWidthRight = 0f;
            cellImage.BorderWidthLeft = 0.5f;
            cellImage.BorderColor = new BaseColor(51, 153, 255);
            cellImage.Padding = 5f;


            tb.AddCell(cellImage);

            #endregion

            #region Cell Text

            PdfPCell cellText = new PdfPCell(new Phrase(text, BuilderPdfGeneratorCommonStyle.Font_Helvetica_8_Black_Bold_TitleNode));
            cellText.Rowspan = 1;
            cellText.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cellText.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            //cellText.BorderWidth = 0f;
            cellText.BackgroundColor = new BaseColor(102, 178, 255);

            cellText.BorderWidthTop = 0.5f;
            cellText.BorderWidthBottom = 0.7f;
            cellText.BorderWidthRight = 0.5f;
            cellText.BorderWidthLeft = 0;
            cellText.BorderColor = new BaseColor(51, 153, 255);
            cellText.Padding = 5f;

            tb.AddCell(cellText);
            #endregion

            return tb;
        }

        protected PdfPTable AddCheckBoxControl(bool checkedValue, string text, iTextSharp.text.Font fontText)
        {
            // Image
            iTextSharp.text.Image logoImage;

            if (checkedValue) { logoImage = iTextSharp.text.Image.GetInstance(BuilderPdfGeneratorCommonStyle.Image_CheckedControl); }
            else { logoImage = iTextSharp.text.Image.GetInstance(BuilderPdfGeneratorCommonStyle.Image_UnCheckedControl); }

            logoImage.Alignment = Element.ALIGN_LEFT;
            logoImage.ScalePercent(40);

            PdfPCell _cellWrapper = CreateWrapperCell();
            PdfPCell _cellWrapperText = CreateWrapperCell();

            _cellWrapper.HorizontalAlignment = Element.ALIGN_LEFT;
            _cellWrapper.VerticalAlignment = Element.ALIGN_MIDDLE;
            _cellWrapper.BorderWidth = 0f;
            _cellWrapper.AddElement(logoImage);
            _cellWrapperText.Top = 0f;
            _cellWrapperText.BorderWidth = 0f;
            _cellWrapperText.VerticalAlignment = Element.ALIGN_TOP;
            _cellWrapperText.AddElement(new Phrase(text, fontText));

            PdfPTable result = new PdfPTable(2);

            result.SetWidths(new float[] { 1f, 9f });
            result.AddCell(_cellWrapper);
            result.AddCell(_cellWrapperText);

            return result;
        }

        protected PdfPTable AddRadioButtonControl(bool checkedValue, string text, iTextSharp.text.Font fontText)
        {
            // Image
            iTextSharp.text.Image logoImage;

            if (checkedValue) { logoImage = iTextSharp.text.Image.GetInstance(BuilderPdfGeneratorCommonStyle.Image_RadioButtonCheckedControl); }
            else { logoImage = iTextSharp.text.Image.GetInstance(BuilderPdfGeneratorCommonStyle.Image_RadioButtonUnCheckedControl); }

            logoImage.Alignment = Element.ALIGN_LEFT;
            logoImage.ScalePercent(50);

            PdfPCell _cellWrapper = CreateWrapperCell();
            PdfPCell _cellWrapperText = CreateWrapperCell();

            _cellWrapper.HorizontalAlignment = Element.ALIGN_LEFT;
            _cellWrapper.VerticalAlignment = Element.ALIGN_MIDDLE;
            _cellWrapper.BorderWidth = 0f;
            _cellWrapper.AddElement(logoImage);

            _cellWrapperText.Top = 0f;
            _cellWrapperText.BorderWidth = 0f;
            _cellWrapperText.VerticalAlignment = Element.ALIGN_TOP;
            _cellWrapperText.AddElement(new Phrase(text, fontText));

            PdfPTable result = new PdfPTable(2);

            result.SetWidths(new float[] { 2f, 8f });
            result.AddCell(_cellWrapper);
            result.AddCell(_cellWrapperText);

            return result;
        }

        protected Image LoadImage(string fullPath)
        {
            try
            {
                iTextSharp.text.Image _image = iTextSharp.text.Image.GetInstance(fullPath);

                return _image;
            }
            catch (Exception) { }

            return null;
        }

        protected Font AddFont(string fontName, int size, BaseColor bColor)
        {
            Font resultFont = FontFactory.GetFont(fontName, size, bColor);

            return resultFont;
        }

        protected PdfPTable CreateTableKeyValues(TableKeyValues col, List<LabelProperty> source)
        {
            switch (col)
            {
                case TableKeyValues.SingleLine:
                    return CreateTableKeyValuesSingleColumn(source);
                case TableKeyValues.OneColumn:
                    return CreateTableKeyValuesOneColumn(source);
                case TableKeyValues.TwoColumns:
                    return CreateTableKeyValuesTwoColumn(source);
               
                default:
                    return null;
            }
        }
        protected PdfPTable CreateTableThreeColumnKeyValues(List<LabelPropertyThreeTable > source)
        {
            return this.CreateTableKeyValuesThreeColumnWithHeader(source);
        }
        protected PdfPTable CreateTableFourColumnKeyValues(List<LabelPropertyFourTable> source)
        {
            return this.CreateTableKeyValuesFourColumnWithHeader(source);
        }
        protected PdfPTable CreateTableTwoColumnKeyValues(List<LabelPropertyTwoTable > source)
        {
            return this.CreateTableKeyValuesTwoColumnWithHeader  (source);
        }
        protected PdfPTable CreateTableFiveColumnKeyValues(List<LabelPropertyFiveTable> source)
        {
            return this.CreateTableKeyValuesFiveColumnWithHeader(source);
        }
        protected PdfPTable CreateTableSixColumnKeyValues(List<LabelPropertySixTable> source)
        {
            return this.CreateTableKeyValuesSixColumnWithHeader(source);
        }

        protected PdfPTable TitleHeader(string text)
        {
            return this.TitleHeader(text, BuilderPdfGeneratorCommonStyle.Font_Helvetica_8_Black_Bold);
        }

        protected PdfPTable TitleSection(string text)
        {
            return this.TitleHeader(text, BuilderPdfGeneratorCommonStyle.Font_Helvetica_8_Black_Bold_TitleHeader);
        }

        protected PdfPTable TitleHeader(string text, iTextSharp.text.Font fontText)
        {
            PdfPTable tb = new PdfPTable(1);

            tb.HorizontalAlignment = 0;
            tb.TotalWidth = 550f;
            tb.LockedWidth = true;
            tb.SetWidths(new float[] { 1f });

            #region Cell Text
            PdfPCell cellText = new PdfPCell(new Phrase(text, fontText));

            cellText.Rowspan = 1;
            cellText.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            cellText.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellText.BorderWidth = 0f;
            cellText.PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingTop = BuilderPdfGeneratorCommonStyle.PaddingTopTableFirstCell;
            cellText.PaddingBottom = BuilderPdfGeneratorCommonStyle.PaddingTopTableFirstCell;

            tb.AddCell(cellText);
            #endregion

            return tb;
        }

        protected PdfPTable TitleHeaderSubmit(string textLabel, string textValue, int align)
        {
            PdfPTable tb = new PdfPTable(1);

            tb.HorizontalAlignment = 0;
            tb.WidthPercentage = 100;
            tb.SetWidths(new float[] { 1f });

            #region Title Header Submit
            Phrase titleHeaderSubmit = new Phrase();
            Font tLabel = BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black_Bold;
            Font tValue = BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black;

            titleHeaderSubmit.Add(new Chunk(textLabel, tValue));
            titleHeaderSubmit.Add(new Chunk(textValue, tLabel));

            PdfPCell cellText = new PdfPCell(titleHeaderSubmit);

            cellText.Rowspan = 1;
            cellText.HorizontalAlignment = align;
            cellText.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellText.BorderWidth = 0f;
            cellText.PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingTop = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingBottom = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

            tb.AddCell(cellText);
            #endregion

            return tb;
        }

        protected PdfPTable TitleHeaderApprover(string textLabel, string textValue, int align)
        {
            PdfPTable tb = new PdfPTable(1);

            tb.HorizontalAlignment = 0;
            tb.WidthPercentage = 100;
            tb.SetWidths(new float[] { 1f });

            #region Title Header Submit
            Phrase titleHeaderSubmit = new Phrase();
            Font tLabel = BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black;
            Font tValue = BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black_Bold;

            titleHeaderSubmit.Add(new Chunk(textLabel, tValue));
            titleHeaderSubmit.Add(new Chunk(textValue, tLabel));

            PdfPCell cellText = new PdfPCell(titleHeaderSubmit);

            cellText.Rowspan = 1;
            cellText.HorizontalAlignment = align; // PdfPCell.ALIGN_RIGHT;
            cellText.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellText.BorderWidth = 0f;
            cellText.PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingTop = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingBottom = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

            tb.AddCell(cellText);
            #endregion

            return tb;
        }


        protected PdfPTable TitleHeaderApproverOnOneLine(string textLabelApproval, string textValueApproval, string textLabelSubmit, string txtValueSubmit)
        {
            PdfPTable tb = new PdfPTable(2);

            tb.HorizontalAlignment = 0;
            tb.WidthPercentage = 100;
            tb.SetWidths(new float[] { 1f, 1f });

            #region Title Header Approval
            Phrase titleHeaderApproval = new Phrase();
            Font tLabelApproval = BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black;
            Font tValueApproval = BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black_Bold;

            titleHeaderApproval.Add(new Chunk(textLabelApproval, tValueApproval));
            titleHeaderApproval.Add(new Chunk(textValueApproval, tLabelApproval));

            PdfPCell cellText = new PdfPCell(titleHeaderApproval);

            cellText.Rowspan = 1;
            cellText.HorizontalAlignment = PdfPCell.ALIGN_LEFT; // PdfPCell.ALIGN_RIGHT;
            cellText.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellText.BorderWidth = 0f;
            cellText.PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingTop = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingBottom = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

            tb.AddCell(cellText);
            #endregion

            #region Title Header Submit
            Phrase titleHeaderSubmit = new Phrase();
            Font tLabelSubmit = BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black;
            Font tValueSubmit = BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black_Bold;

            titleHeaderSubmit.Add(new Chunk("Submitted To " + textLabelSubmit, tLabelSubmit));
            titleHeaderSubmit.Add(new Chunk(txtValueSubmit, tValueSubmit));

            cellText = new PdfPCell(titleHeaderSubmit);

            cellText.Rowspan = 1;
            cellText.HorizontalAlignment = PdfPCell.ALIGN_RIGHT; // PdfPCell.ALIGN_RIGHT;
            cellText.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cellText.BorderWidth = 0f;
            cellText.PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingTop = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;
            cellText.PaddingBottom = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

            tb.AddCell(cellText);
            #endregion

            return tb;
        }


        protected PdfPTable CreateTableInlineColumns(int columns)
        {
            PdfPTable table = new PdfPTable(columns);

            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;

            return table;
        }

        protected PdfPTable CreateGrid(List<string> columns, List<string> rows)
        {
            PdfPTable table = CreateTableInlineColumns(columns.Count);

            table.WidthPercentage = 100;
            table.SpacingBefore = BuilderPdfGeneratorCommonStyle.TitleNodeSpacingBefore;

            PdfPRow rowColumns = AddRowColumnsTableInline(columns.ToArray());

            table.Rows.Add(rowColumns);
            table.Rows.Add(AddRowTableInline(rows.ToArray()));

            PdfPCell wrapperCell = CreateWrapperCell();

            wrapperCell.Border = 0;
            wrapperCell.PaddingLeft = 15f;
            wrapperCell.AddElement(table);

            PdfPTable wrapper = CreateWrapperContainer(wrapperCell);

            return wrapper;
        }

        protected PdfPTable CreateWrapperGrid(List<string> columns)
        {
            PdfPTable table = CreateTableInlineColumns(columns.Count);

            table.WidthPercentage = 100;
            table.SpacingBefore = BuilderPdfGeneratorCommonStyle.TitleNodeSpacingBefore;

            PdfPRow rowColumns = AddRowColumnsTableInline(columns.ToArray());

            table.Rows.Add(rowColumns);

            return table;
        }

        protected PdfPRow AddRowWrapperGrid(List<string> row)
        {
            return this.AddRowTableInline(row.ToArray());
        }

        // Private Methods
        private void AddRowInline(PdfPTable table, List<LabelProperty> values)
        {
            if (table == null) return;

            PdfPRow _row = CreatePdfRow(ConvertLabelProperty(values));

            table.Rows.Add(_row);
        }

        private PdfPRow AddRowColumnsTableInline(string[] cells)
        {
            if (cells == null || cells.Length <= 0)
                return new PdfPRow(new PdfPCell[] { new PdfPCell(new Phrase("No Element")) });

            List<PdfPCell> resultCells = new List<PdfPCell>();

            for (var i = 1; i <= cells.Length; i++)
            {

                resultCells.Add(AddCellTableValue(cells[i - 1], true, (new BaseColor(226, 226, 226)), BuilderPdfGeneratorCommonStyle.Font_Helvetica_8_Black_Bold));
            }

            resultCells[0].PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

            PdfPRow row = new PdfPRow(resultCells.ToArray());

            return row;
        }

        private PdfPRow AddRowTableInline(string[] cells)
        {
            if (cells == null || cells.Length <= 0)
                return new PdfPRow(new PdfPCell[] { new PdfPCell(new Phrase("No Element")) });

            List<PdfPCell> resultCells = new List<PdfPCell>();

            for (var i = 1; i <= cells.Length; i++)
            {
                resultCells.Add(AddCellTableText(cells[i - 1], true, BuilderPdfGeneratorCommonStyle.Font_Helvetica_8_Black));
            }

            resultCells[0].PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

            PdfPRow row = new PdfPRow(resultCells.ToArray());

            return row;
        }

        private PdfPTable CreateTableKeyValuesSingleColumn(List<LabelProperty> source)
        {
            PdfPTable table = new PdfPTable(1);

            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;
            table.SpacingBefore = 5f;

            foreach (LabelProperty lProperty in source)
            {

                if (lProperty.FilterCondition == null ||
                    ((!string.IsNullOrEmpty(lProperty.ReferenceCondition) && lProperty.FilterCondition.ValidateReference(lProperty.ReferenceCondition)) ||
                    (string.IsNullOrEmpty(lProperty.ReferenceCondition) && lProperty.FilterCondition.Validate(lProperty.LabelValue))))
                {
                    PdfPCell labelText = new PdfPCell();

                    labelText.AddElement(new Paragraph(new Chunk(lProperty.LabelName, BuilderPdfGeneratorCommonStyle.Font_Helvetica_8_Black_Bold)));
                    labelText.Border = 0;
                    labelText.PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

                    PdfPCell valueText = new PdfPCell();

                    valueText.AddElement(new Paragraph(new Chunk(lProperty.LabelValue, BuilderPdfGeneratorCommonStyle.Font_Helvetica_8_Black)));
                    valueText.Border = 0;
                    valueText.PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

                    table.AddCell(labelText);
                    table.AddCell(valueText);
                }
            }

            return table;
        }

        private PdfPTable CreateTableKeyValuesOneColumn(List<LabelProperty> source)
        {
            PdfPTable table = new PdfPTable(2);

            table.WidthPercentage = 100f;
            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;

            table.SetWidths(new float[] { 1.5f, 2f });

            List<LabelProperty> sourceBlock;
            int incrementSkip = 0;

            for (int i = 1; i <= (source.Count); i++)
            {
                if (source[i - 1].FilterCondition == null ||
                    ((!string.IsNullOrEmpty(source[i - 1].ReferenceCondition) && source[i - 1].FilterCondition.ValidateReference(source[i - 1].ReferenceCondition)) ||
                    (string.IsNullOrEmpty(source[i - 1].ReferenceCondition) && source[i - 1].FilterCondition.Validate(source[i - 1].LabelValue))))
                {

                    if (i == 1)
                    {
                        sourceBlock = source.Take(1).ToList();
                        AddRowInline(table, sourceBlock);
                    }
                    else
                    {
                        sourceBlock = source.Skip(incrementSkip).Take(1).ToList();
                        AddRowInline(table, sourceBlock);
                    }
                }
                incrementSkip += 1;
            }

            return table;
        }

        private PdfPTable CreateTableKeyValuesTwoColumn(List<LabelProperty> source)
        {
            PdfPTable table = new PdfPTable(4);

            table.TotalWidth = 550f;
            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;

            List<LabelProperty> sourceBlock;
            int incrementSkip = 0;

            for (int i = 1; i <= (source.Count / 2); i++)
            {

                if (i == 1)
                {
                    sourceBlock = source.Take(2).ToList();
                    AddRowInline(table, sourceBlock);
                }
                else
                {
                    sourceBlock = source.Skip(incrementSkip).Take(2).ToList();
                    AddRowInline(table, sourceBlock);
                }
                incrementSkip += 2;
            }

            return table;
        }

        private PdfPTable CreateTableKeyValuesThreeColumnWithHeader(List<LabelPropertyThreeTable> source)
        {
            PdfPTable table = new PdfPTable(3);

            table.TotalWidth = 850f;
            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;
            table.SpacingBefore = BuilderPdfGeneratorCommonStyle.TitleNodeSpacingBefore;
            table.SpacingAfter = 15f;
            int rowProcess = 0;

            foreach (LabelPropertyThreeTable label in source)
            {
                if (rowProcess == 0)
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue1, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue2, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));

                }
                else
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName , true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue1 , true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue2 , true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));

                }

                rowProcess +=1;
            }


            return table;
        }
        private PdfPTable CreateTableKeyValuesFourColumnWithHeader(List<LabelPropertyFourTable> source)
        {
            PdfPTable table = new PdfPTable(4);

            table.TotalWidth = 850f;
            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;
            table.SpacingBefore = BuilderPdfGeneratorCommonStyle.TitleNodeSpacingBefore;
            table.SpacingAfter = 15f;
            int rowProcess = 0;

            foreach (LabelPropertyFourTable label in source)
            {
                if (rowProcess == 0)
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue1, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue2, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue3, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));

                }
                else
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue1, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue2, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue3, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                }

                rowProcess += 1;
            }


            return table;
        }
        private PdfPTable CreateTableKeyValuesTwoColumnWithHeader(List<LabelPropertyTwoTable> source)
        {
            PdfPTable table = new PdfPTable(2);

            table.TotalWidth = 850f;
            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;
            table.SpacingBefore = BuilderPdfGeneratorCommonStyle.TitleNodeSpacingBefore;
            table.SpacingAfter = 15f;
            int rowProcess = 0;

            foreach (LabelPropertyTwoTable label in source)
            {
                if (rowProcess == 0)
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    

                }
                else
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    

                }

                rowProcess += 1;
            }


            return table;
        }
        private PdfPTable CreateTableKeyValuesFiveColumnWithHeader(List<LabelPropertyFiveTable> source)
        {
            PdfPTable table = new PdfPTable(5);

            table.TotalWidth = 1050f;
            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;
            table.SpacingBefore = BuilderPdfGeneratorCommonStyle.TitleNodeSpacingBefore;
            table.SpacingAfter = 15f;
            int rowProcess = 0;

            foreach (LabelPropertyFiveTable label in source)
            {
                if (rowProcess == 0)
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue1, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue2, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue3, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue4, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));

                }
                else
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue1, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue2, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue3, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue4, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));

                }

                rowProcess += 1;
            }


            return table;
        }

        private PdfPTable CreateTableKeyValuesSixColumnWithHeader(List<LabelPropertySixTable> source)
        {
            PdfPTable table = new PdfPTable(6);

            table.TotalWidth = 1050f;
            table.HorizontalAlignment = 0;
            table.DefaultCell.Border = 0;
            table.SpacingBefore = BuilderPdfGeneratorCommonStyle.TitleNodeSpacingBefore;
            table.SpacingAfter = 15f;
            int rowProcess = 0;

            foreach (LabelPropertySixTable label in source)
            {
                if (rowProcess == 0)
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue1, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue2, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue3, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue4, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue5, true, BaseColor.LIGHT_GRAY, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                }
                else
                {
                    table.AddCell(this.AddCellTableValue(label.PropertyName, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue1, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue2, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue3, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue4, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                    table.AddCell(this.AddCellTableValue(label.PropertyValue5, true, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));

                }

                rowProcess += 1;
            }


            return table;
        }

        private PdfPRow CreatePdfRow(string[] cells)
        {
            if (cells == null || cells.Length <= 0)
                return new PdfPRow(new PdfPCell[] { new PdfPCell(new Phrase("No Element")) });

            List<PdfPCell> resultCells = new List<PdfPCell>();

            for (var i = 1; i <= cells.Length; i++)
            {
                if (i % 2 == 0)
                {
                    resultCells.Add(AddCellTableValue(cells[i - 1], false, BaseColor.WHITE, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black));
                }
                else
                {
                    resultCells.Add(AddCellTableText(cells[i - 1], false, BuilderPdfGeneratorCommonStyle.Font_Arial_8_Black_Bold));
                }
            }

            resultCells[0].PaddingLeft = BuilderPdfGeneratorCommonStyle.PaddingLeftTableFirstCell;

            PdfPRow row = new PdfPRow(resultCells.ToArray());

            return row;
        }

        private PdfPCell AddCellTableText(string text, bool border, Font font)
        {
            PdfPCell result = new PdfPCell();

            if (!border) result.BorderWidth = 0f;

            result.Rowspan = 1;
            result.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            result.AddElement(new Phrase((text == null) ? "" : text, font));

            return result;
        }

        private PdfPCell AddCellTableValue(string text, bool border, BaseColor bColor, Font font)
        {
            PdfPCell result = new PdfPCell();

            if (!border) result.BorderWidth = 0f;

            result.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            result.BackgroundColor = bColor; // (new BaseColor(226, 226, 226)); 
            result.AddElement(new Phrase((text == null) ? "" : text, font));

            return result;
        }

        //Common Style
        protected BuilderPdfStyle BuilderPdfGeneratorCommonStyle
        {
            get
            {
                if (this._commonStyle == null) this._commonStyle = new BuilderPdfStyle();

                return this._commonStyle;
            }
        }

        //Public Methods
        protected string[] ConvertLabelProperty(List<LabelProperty> values)
        {
            List<string> result = new List<string>();

            foreach (LabelProperty lp in values)
            {
                result.Add(lp.LabelName);
                result.Add(lp.LabelValue);
            }

            return result.ToArray();
        }

        protected XmlDocument CreateXmlDocument(string triggerXml)
        {
            try
            {
                if (string.IsNullOrEmpty(triggerXml)) return null;

                XmlDocument doc = new XmlDocument();

                doc.LoadXml(triggerXml);

                return doc;
            }
            catch (Exception) { return null; }
        }

        public PdfPTable CreateLogoHeader(string filename)
        {
            PdfPTable tbHeader = new PdfPTable(1);

            tbHeader.WidthPercentage = 100f;
            tbHeader.SetWidths(new float[] {  1f });

            
            PdfPCell cellLogo = this.CreateWrapperCell();
            PdfPTable headerLogo = HeaderTableLogo(filename);

            cellLogo.AddElement(headerLogo);
                      
            tbHeader.AddCell(cellLogo);
           
            return tbHeader;
        }

        public enum TableKeyValues
        {
            SingleLine,
            OneColumn,
            TwoColumns,
            ThreeColumnsWithHeader
        }

        /// <summary>
        /// Author: BN
        /// <para/>Date: 29/05/2019
        /// <para/>Description: format string data value in dd/MM/YYYY
        /// </summary>
        /// <param name="strDate">string Date to convert in dd/mm/yyyy</param>
        /// <param name="blnHours">boolean. If true shows date and hour</param>
        /// <returns>string formatted</returns>
        public string FormatDateDDMMYYYY(string strDate, bool blnHours)
        {
            try
            {
                DateTimeFormatInfo DateInfo = CultureInfo.CurrentCulture.DateTimeFormat;
                DateTime dtDate = Convert.ToDateTime(String.Format("{0:dd/MM/yyyy}", strDate.Trim()), CultureInfo.CurrentCulture);

                if (!blnHours) { return dtDate.ToString("dd/MM/yyyy"); }
                else { return dtDate.ToString("dd/MM/yyyy H:mm"); }
            }
            catch (Exception) { return ""; }
        }

        /// <summary>
        /// Author: BN
        /// <para/>Date: 06/06/2019
        /// <para/>Description: string value to formatted with space in each word
        /// </summary>
        /// <param name="strValue">Action string to divide</param>
        /// <returns>Return string value with space in every word </returns>
        public string GetValueByAction(string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) { return ""; }

            try
            {
                if (strValue == "SpaceAvalability") { strValue = "Flight and " + strValue; }
                return Regex.Replace(Regex.Replace(strValue, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string GetTitleTriggerProcess(XmlDocument triggerXml)
        {
            return this.GetNodeAttributeByTriggerXml(triggerXml, "Process_Title");
        }

        public string GetNodeAttributeByTriggerXml(XmlDocument triggerXml, string xmlAttribute)
        {

            if (triggerXml == null) return "";

            try
            {

                XmlNode triggerNode = triggerXml.DocumentElement.SelectSingleNode("//ROOT/Staff/Action/Process/Trigger");

                if (triggerNode == null) return "";

                return triggerNode.SelectSingleNode(string.Format("//{0}", xmlAttribute)).InnerText;


            }
            catch (Exception)
            {
                return "@";
            }

        }

    }
}
