using System;
using System.Windows.Forms;

namespace PaymentManagerView
{
	class MonthPaidCell : DataGridViewTextBoxCell
	{
		public MonthPaidCell()
			: base() {
			Style.Format = "d";
		}

		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle) {
			// Set the value of the editing control to the current cell value.
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			MonthPaidControl ctl = DataGridView.EditingControl as MonthPaidControl;
			ctl.Value = (DateTime)GetValue(rowIndex);
		}

		public override Type EditType {
			get {
				return typeof(MonthPaidControl);
			}
		}

		public override Type ValueType {
			get {
				return typeof(DateTime);
			}
		}

		public override object DefaultNewRowValue {
			get {
				return DateTime.Now;
			}
		}

		public DateTime EditedValue{
			get{
				MonthPaidControl ctl = DataGridView.EditingControl as MonthPaidControl;
				return ctl.Value;
			}
		}

		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter valueTypeConverter, System.ComponentModel.TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context){
			return ( (DateTime) value ).ToString ( "y" );
		}

		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, System.ComponentModel.TypeConverter formattedValueTypeConverter, System.ComponentModel.TypeConverter valueTypeConverter) {
			DateTime res = DateTime.Parse ( formattedValue.ToString() );
			return new DateTime ( res.Year, res.Month, 1 );
		}

	}

	class MonthPaidControl : DateTimePicker, IDataGridViewEditingControl
	{
		private DataGridView dataGrid;
		private bool valueChanged = false;
		private int rowIndex;

		public MonthPaidControl()
			: base() {
			Format = DateTimePickerFormat.Short;
		}

		///<summary>
		///Changes the control's user interface (UI) to be consistent with the specified cell style.
		///</summary>
		///
		///<param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to use as the model for the UI.</param>
		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle) {
			Font = dataGridViewCellStyle.Font;
			CalendarForeColor = dataGridViewCellStyle.ForeColor;
			CalendarMonthBackground = dataGridViewCellStyle.BackColor;
		}

		///<summary>
		///Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView" /> should process.
		///</summary>
		///
		///<returns>
		///true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.
		///</returns>
		///
		///<param name="key">A <see cref="T:System.Windows.Forms.Keys" /> that represents the key that was pressed.</param>
		///<param name="dataGridViewWantsInputKey">true when the <see cref="T:System.Windows.Forms.DataGridView" /> wants to process the <see cref="T:System.Windows.Forms.Keys" /> in <paramref name="keyData" />; otherwise, false.</param>
		public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey) {
			switch(key & Keys.KeyCode) {
				case Keys.Left:
				case Keys.Up:
				case Keys.Down:
				case Keys.Right:
				case Keys.Home:
				case Keys.End:
				case Keys.PageDown:
				case Keys.PageUp:
					return true;
				default:
					return !dataGridViewWantsInputKey;
			}
		}

		///<summary>
		///Retrieves the formatted value of the cell.
		///</summary>
		///
		///<returns>
		///An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.
		///</returns>
		///
		///<param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which the data is needed.</param>
		public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context) {
			return EditingControlFormattedValue;
		}

		///<summary>
		///Prepares the currently selected cell for editing.
		///</summary>
		///
		///<param name="selectAll">true to select all of the cell's content; otherwise, false.</param>
		public void PrepareEditingControlForEdit(bool selectAll) {

		}

		///<summary>
		///Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the cell.
		///</summary>
		///
		///<returns>
		///The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being edited; null if there is no associated <see cref="T:System.Windows.Forms.DataGridView" />.
		///</returns>
		///
		public DataGridView EditingControlDataGridView {
			get {
				return dataGrid;
			}
			set {
				dataGrid = value;
			}
		}

		///<summary>
		///Gets or sets the formatted value of the cell being modified by the editor.
		///</summary>
		///
		///<returns>
		///An <see cref="T:System.Object" /> that represents the formatted value of the cell.
		///</returns>
		///
		public object EditingControlFormattedValue {
			get {
				return Value.ToShortDateString();
			}
			set {
				if(value is String) {
					Value = DateTime.Parse((String)value);
				}
			}
		}

		///<summary>
		///Gets or sets the index of the hosting cell's parent row.
		///</summary>
		///
		///<returns>
		///The index of the row that contains the cell, or –1 if there is no parent row.
		///</returns>
		///
		public int EditingControlRowIndex {
			get {
				return rowIndex;
			}
			set {
				rowIndex = value;
			}
		}

		///<summary>
		///Gets or sets a value indicating whether the value of the editing control differs from the value of the hosting cell.
		///</summary>
		///
		///<returns>
		///true if the value of the control differs from the cell value; otherwise, false.
		///</returns>
		///
		public bool EditingControlValueChanged {
			get {
				return valueChanged;
			}
			set {
				valueChanged = value;
			}
		}

		///<summary>
		///Gets the cursor used when the mouse pointer is over the <see cref="P:System.Windows.Forms.DataGridView.EditingPanel" /> but not over the editing control.
		///</summary>
		///
		///<returns>
		///A <see cref="T:System.Windows.Forms.Cursor" /> that represents the mouse pointer used for the editing panel. 
		///</returns>
		///
		public Cursor EditingPanelCursor {
			get {
				return base.Cursor;
			}
		}

		///<summary>
		///Gets or sets a value indicating whether the cell contents need to be repositioned whenever the value changes.
		///</summary>
		///
		///<returns>
		///true if the contents need to be repositioned; otherwise, false.
		///</returns>
		///
		public bool RepositionEditingControlOnValueChange {
			get {
				return false;
			}
		}

		protected override void OnValueChanged(EventArgs eventargs) {
			valueChanged = true;
			EditingControlDataGridView.NotifyCurrentCellDirty(true);
			base.OnValueChanged(eventargs);
		}
	}
}
