using System;
using System.Globalization;
using System.Windows.Forms;

public class NumericUpDownFormatted : NumericUpDown
{
    /// <summary>
    /// Chuỗi format (mặc định "N4").
    /// Ví dụ: "N0" = 1,234 | "N2" = 1,234.56 | "C" = $1,234.56
    /// </summary>
    public string FormatString { get; set; } = "N4";

    /// <summary>
    /// Culture cho format số (mặc định: CurrentCulture).
    /// </summary>
    public CultureInfo FormatCulture { get; set; } = CultureInfo.CurrentCulture;

    public NumericUpDownFormatted()
    {
        this.DecimalPlaces = 4;
        this.ThousandsSeparator = true;
    }

    protected override void OnTextBoxTextChanged(object source, EventArgs e)
    {
        // ⛔ Bỏ qua khi đang ở DesignMode để tránh crash VS Designer
        if (this.DesignMode)
        {
            base.OnTextBoxTextChanged(source, e);
            return;
        }

        var txt = this.Controls[1] as TextBox;
        if (txt == null) return;

        int oldSelection = txt.SelectionStart;
        int oldLength = txt.Text.Length;

        if (decimal.TryParse(
                txt.Text.Replace(",", "").Replace("$", ""),
                NumberStyles.Any,
                FormatCulture,
                out decimal val))
        {
            string newText = val.ToString(FormatString, FormatCulture);

            // Chỉ format nếu có sự khác biệt
            if (txt.Text != newText)
            {
                txt.Text = newText;

                int diff = txt.Text.Length - oldLength;
                int newSelection = oldSelection + diff;

                if (newSelection < 0) newSelection = 0;
                if (newSelection > txt.Text.Length) newSelection = txt.Text.Length;

                txt.SelectionStart = newSelection;
            }
        }

        base.OnTextBoxTextChanged(source, e);
    }
}
