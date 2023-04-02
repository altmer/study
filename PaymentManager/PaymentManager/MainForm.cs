using System;
using System.Windows.Forms;
using PaymentManager.Properties;
using PaymentManagerController;
using PaymentManagerModel;
//using Microsoft.Office.Interop.Excel;
//using Application=Microsoft.Office.Interop.Excel.Application;

namespace PaymentManagerView
{
	public partial class MainForm : Form
	{
		private readonly BindingSource customerSource = new BindingSource();
		private readonly BindingSource paymentSource = new BindingSource();

		private readonly PaymentController control = PaymentController.GetInstance();

		private decimal rate;

		public MainForm(){
			components = null;

			InitializeComponent();

			rate = Settings.Default.Rate;
			rateLabel.Text = rate.ToString ( "F2" );
		}

		public static bool IsNullOrEmpty(object obj) {
			return obj == null || String.IsNullOrEmpty(obj.ToString().Trim());
		}

		public static void ShowError(object what) {
			MessageBox.Show(null, what.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void MainForm_Load(object sender, EventArgs e) {

			control.BindCustomers ( customerSource );
			CustomerGrid.DataSource = customerSource;
			CustomerGrid.Columns [ 1 ].HeaderText = "Имя";
			CustomerGrid.Columns [ 2 ].HeaderText = "Фамилия";
			CustomerGrid.Columns [ 2 ].DisplayIndex = 1;
			CustomerGrid.Columns [ 3 ].HeaderText = "Отчество";
			CustomerGrid.Columns [ 4 ].HeaderText = "Номер участка";

			control.BindPayments(paymentSource);
			PaymentGrid.DataSource = paymentSource;
			PaymentGrid.Columns [ 1 ].HeaderText = "Дата платежа";
			PaymentGrid.Columns [ 1 ].CellTemplate = new PaymentDateCell();
			PaymentGrid.Columns [ 2 ].HeaderText = "Оплаченный месяц";
			PaymentGrid.Columns [ 2 ].CellTemplate = new MonthPaidCell();
			PaymentGrid.Columns [ 3 ].HeaderText = "Кол-во электроэнергии (кВт*ч)";
			PaymentGrid.Columns [ 3 ].DefaultCellStyle.Format = "F2";
			PaymentGrid.Columns [ 4 ].HeaderText = "Тариф (руб/(кВт*ч))";
			PaymentGrid.Columns [ 4 ].DefaultCellStyle.Format = "F2";
			PaymentGrid.Columns [ 5 ].HeaderText = "Клиент";
			PaymentGrid.Columns [ 5 ].DisplayIndex = 1;
			PaymentGrid.Columns [ 5 ].CellTemplate = new CustomerIdCell();

			var col = new DataGridViewColumn(new PaymentSumCell()){
			                                                      	Name = "Sum", 
																	HeaderText = "Сумма (руб.)", 
																	ReadOnly = true,
																	SortMode = DataGridViewColumnSortMode.Automatic
			                                                      };

			PaymentGrid.Columns.Add(col);

			paymentMonthFrom.Value = DateTimePicker.MinDateTime;
			paymentMonthTo.Value = DateTimePicker.MaxDateTime;

			paymentDateFrom.Value = DateTimePicker.MinDateTime;
			paymentDateTo.Value = DateTimePicker.MaxDateTime;

			PaymentGrid.Columns[0].Visible = false;
			CustomerGrid.Columns[0].Visible = false;

			InitCustomerIDComboBox();

			control.Backup();
		}

		private void CustomerGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
			if (IsNullOrEmpty ( e.FormattedValue ) ){
				CustomerGrid.Rows [ e.RowIndex ].ErrorText = "Введенное значение не может быть пустым!";
				e.Cancel = true;
			}

			if (CustomerGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode && 
						CustomerGrid.Columns[e.ColumnIndex].Name.Equals ( CustomerColumns.Number.ToString() ) 
							&& ! control.IsCustomerUpdateValid (e.RowIndex, e.ColumnIndex, e.FormattedValue) ){
				CustomerGrid.Rows[e.RowIndex].ErrorText = "Уже есть клиент с таким номером участка!";
				e.Cancel = true;
			}

				
		}

		private void CustomerGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			CustomerGrid.Rows [ e.RowIndex ].ErrorText = String.Empty;
			CustomerGrid.Rows [ e.RowIndex ].Cells [ e.ColumnIndex ].Value =
				CustomerGrid.Rows [ e.RowIndex ].Cells [ e.ColumnIndex ].Value.ToString().Trim();

			control.AcceptChanges();
			InitCustomerIDComboBox();
		}

		private void PaymentGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
			if (IsNullOrEmpty ( e.FormattedValue ) ){
				PaymentGrid.Rows [ e.RowIndex ].ErrorText = "Введенное значение не может быть пустым!";
				e.Cancel = true;
				return;
			}

			if (PaymentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ValueType == typeof(Decimal) ){
				decimal res;
				if ( !Decimal.TryParse ( e.FormattedValue.ToString(), out res )){
					PaymentGrid.Rows[e.RowIndex].ErrorText = "Введенное значение не является числом!";
					e.Cancel = true;
				}
				if (res < 0){
					PaymentGrid.Rows[e.RowIndex].ErrorText = "Введено отрицательное число!";
					e.Cancel = true;
				}
				return;
			}

			if (PaymentGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].IsInEditMode){
				if ( PaymentGrid.Columns [ e.ColumnIndex ].Name.Equals ( PaymentColumns.CustomerId.ToString() ) ){
					if (!control.IsPaymentUpdateValid (e.RowIndex, e.ColumnIndex - 1, 
												((CustomerIdCell)PaymentGrid[e.ColumnIndex, e.RowIndex]).EditedValue  )){
						PaymentGrid.Rows[e.RowIndex].ErrorText = "Для этого клиента уже есть платёж за этот месяц.";
						e.Cancel = true;
					}
				}

				if (PaymentGrid.Columns[e.ColumnIndex].Name.Equals ( PaymentColumns.MonthPaid.ToString()) ){
					if (!control.IsPaymentUpdateValid (e.RowIndex, e.ColumnIndex - 1, 
												((MonthPaidCell)PaymentGrid[e.ColumnIndex, e.RowIndex]).EditedValue  )){
						PaymentGrid.Rows[e.RowIndex].ErrorText = "Для этого клиента уже есть платёж за этот месяц.";
						e.Cancel = true;
					}
				}
			}


		}

		private void PaymentGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
			PaymentGrid.Rows[e.RowIndex].ErrorText = String.Empty;
			control.AcceptChanges();
		}


		private void addCustomerButton_Click(object sender, EventArgs e) {
			var dialog = new CustomerDialog();
			bool fin = false;

			while (!fin){
				if(dialog.ShowDialog() == DialogResult.OK) {
					if ( IsNullOrEmpty ( dialog.LastName )){
						ShowError ( "Поле 'Фамилия' не может быть пустым." );
						continue;
					}
					if(IsNullOrEmpty(dialog.FirstName)) {
						ShowError("Поле 'Имя' не может быть пустым.");
						continue;
					}
					if(IsNullOrEmpty(dialog.MiddleName)) {
						ShowError("Поле 'Отчество' не может быть пустым.");
						continue;
					}
					if(IsNullOrEmpty(dialog.Number)) {
						ShowError("Поле 'Номер участка' не может быть пустым.");
						continue;
					}


					if (!control.AddCustomer(new Customer {
					                             	LastName = dialog.LastName,
					                             	FirstName = dialog.FirstName,
					                             	MiddleName = dialog.MiddleName,
					                             	Number = dialog.Number
					                             } ) ){
						ShowError ( "Не удалось добавить клиента. Возможно, уже существует клиент с таким же номером участка." );
					}else{
						fin = true;
						InitCustomerIDComboBox();
					}

				}
				else {
					fin = true;
				}
			}

		}

		private void addPaymentButton_Click(object sender, EventArgs e) {
			AddPayment ( null );
		}

		private void addPAymentMenuItem_Click(object sender, EventArgs e) {
			if (CustomerGrid.SelectedRows.Count > 0)
				AddPayment ( control.GetCustomer (  (Guid) CustomerGrid.SelectedRows[0].Cells[CustomerColumns.Id.ToString()].Value ) );
			else{
				AddPayment ( null );
			}
		}

		private void AddPayment(Customer to){
			var dialog = to == null ? new PaymentDialog() : new PaymentDialog(to);
			
			bool fin = false;

			while(!fin) {
				if(dialog.ShowDialog() == DialogResult.OK) {
					if(IsNullOrEmpty(dialog.Amount) || dialog.Amount < 0) {
						ShowError("Поле 'Кол-во электроэнергии' должно содержать неотрицательное число.");
						continue;
					}
					if(IsNullOrEmpty(dialog.PaymentDate)) {
						ShowError("Поле 'Дата платежа' не может быть пустым.");
						continue;
					}
					if(IsNullOrEmpty(dialog.MonthPaid)) {
						ShowError("Поле 'Оплаченный месяц' не может быть пустым.");
						continue;
					}
					if(IsNullOrEmpty(dialog.Customer)) {
						ShowError("Выберите клиента.");
						continue;
					}



					if(!control.AddPayment(new Payment {
						Amount = dialog.Amount.Value,
						Rate = rate,
						MonthPaid = dialog.MonthPaid,
						CustomerID = dialog.Customer.ID,
						PaymentDate = dialog.PaymentDate
					}
								  )) {
						ShowError("Не удалось добавить платёж. Возможно, уже существует платёж для этого клиента за данный месяц.");

					}
					else {
						fin = true;

						if(debtCustomersButton.Checked) {
							ApplyDebtFilter();
						}
					}
				}
				else {
					fin = true;
				}
			}
		}

		private void rateLabel_Click(object sender, EventArgs e) {
			var dialog = new RateDialog(rate);
			bool fin = false;

			while(!fin) {
				if(dialog.ShowDialog() == DialogResult.OK) {
					if(IsNullOrEmpty(dialog.Rate)) {
						ShowError("Введите число.");
						continue;
					}

					fin = true;

					rate = dialog.Rate.Value;
					rateLabel.Text = rate.ToString();
					Settings.Default.Rate = rate;
					Settings.Default.Save();
				}
				else {
					fin = true;
				}
			}
		}

		private void deleteButton_Click(object sender, EventArgs e) {
			if ( tabControl.SelectedTab.Text.Equals ( "Клиенты" ) ){
				if(MessageBox.Show("Вы уверены, что хотите удалить выделенных клиентов? Будет также удалена вся информация о платежах этих клиентов.", "Удаление", MessageBoxButtons.YesNo,
											MessageBoxIcon.Question) == DialogResult.Yes){
					foreach (DataGridViewRow row in CustomerGrid.SelectedRows){
						control.RemoveCustomer ( (Guid) row.Cells [ CustomerColumns.Id.ToString() ].Value );
					}
					InitCustomerIDComboBox();
				}
			}
			else{
				if(MessageBox.Show("Вы уверены, что хотите удалить выделенные платежи?", "Удаление", MessageBoxButtons.YesNo,
											MessageBoxIcon.Question) == DialogResult.Yes){
					foreach (DataGridViewRow row in PaymentGrid.SelectedRows){
						control.RemovePayment((Guid)row.Cells[PaymentColumns.Id.ToString()].Value);
					}
				}
			}
		}

		private void allCustomersButton_CheckedChanged(object sender, EventArgs e) {
			if (allCustomersButton.Checked){
				control.ApplyFilter ( new CustomerFilter() );
			}
		}

		private void filteredCustomersButton_CheckedChanged(object sender, EventArgs e) {
			if (filteredCustomersButton.Checked){
				ApplyCustomerFilter();
				customerFilterBox.Enabled = true;
			}else{
				customerFilterBox.Enabled = false;
			}
		}

		private void debtCustomersButton_CheckedChanged(object sender, EventArgs e) {
			if(debtCustomersButton.Checked) {
				ApplyDebtFilter();
				debtMonth.Enabled = true;
			}
			else {
				debtMonth.Enabled = false;
			}
		}

		private void ApplyDebtFilter(){
			control.ApplyDebtCustomersFilter(debtMonth.Value);
		}

		private void customerFilter_Changed(object sender, EventArgs e) {
			ApplyCustomerFilter();
		}

		private void ApplyCustomerFilter(){
			var filter = new CustomerFilter();

			if (!IsNullOrEmpty ( customerFilterFirstNameBox.Text )){
				filter.Value.FirstName = customerFilterFirstNameBox.Text;
			}

			if(!IsNullOrEmpty(customerFilterLastNameBox.Text)) {
				filter.Value.LastName = customerFilterLastNameBox.Text;
			}

			if(!IsNullOrEmpty(customerFilterMiddleNameBox.Text)) {
				filter.Value.MiddleName = customerFilterMiddleNameBox.Text;
			}

			if(!IsNullOrEmpty(customerFilterNumberBox.Text)) {
				filter.Value.Number = customerFilterNumberBox.Text;
			}

			control.ApplyFilter ( filter );
		}

		private void debtMonth_ValueChanged(object sender, EventArgs e) {
			control.ApplyDebtCustomersFilter ( debtMonth.Value );
		}

		private void allPaymentsButton_CheckedChanged(object sender, EventArgs e) {
			if (allPaymentsButton.Checked){
				control.ApplyFilter ( new PaymentFilter() );
			}
		}

		private void filteredPaymentsButton_CheckedChanged(object sender, EventArgs e) {
			if(filteredPaymentsButton.Checked) {
				ApplyPaymentFilter();
				paymentFilterBox.Enabled = true;
			}
			else {
				paymentFilterBox.Enabled = false;
			}

		}

		private void ApplyPaymentFilter(){
			var filter = new PaymentFilter();

			decimal tempDecimal;

			if(!IsNullOrEmpty(paymentAmountFrom.Text) && Decimal.TryParse(paymentAmountFrom.Text, out tempDecimal) && 
				!IsNullOrEmpty(paymentAmountTo.Text) && Decimal.TryParse(paymentAmountTo.Text, out tempDecimal)){
				filter.From.Amount = Decimal.Parse ( paymentAmountFrom.Text );
				filter.To.Amount = Decimal.Parse ( paymentAmountTo.Text );
			}

			if(!IsNullOrEmpty(paymentRateFrom.Text) && Decimal.TryParse(paymentRateFrom.Text, out tempDecimal) && 
				!IsNullOrEmpty(paymentRateTo.Text) && Decimal.TryParse(paymentRateTo.Text, out tempDecimal)) {
				filter.From.Rate = Decimal.Parse(paymentRateFrom.Text);
				filter.To.Rate = Decimal.Parse(paymentRateTo.Text);
			}

			if(!IsNullOrEmpty(paymentSumFrom.Text) && Decimal.TryParse(paymentSumFrom.Text, out tempDecimal) && 
				!IsNullOrEmpty(paymentSumTo.Text) && Decimal.TryParse(paymentSumTo.Text, out tempDecimal)) {
				filter.SumFrom = Decimal.Parse(paymentSumFrom.Text);
				filter.SumTo = Decimal.Parse(paymentSumTo.Text);
			}

			filter.From.PaymentDate = new DateTime(paymentDateFrom.Value.Year, paymentDateFrom.Value.Month, paymentDateFrom.Value.Day);
			filter.To.PaymentDate = new DateTime(paymentDateTo.Value.Year, paymentDateTo.Value.Month, paymentDateTo.Value.Day);

			filter.From.MonthPaid = new DateTime(paymentMonthFrom.Value.Year, paymentMonthFrom.Value.Month, 1);
			filter.To.MonthPaid = new DateTime(paymentMonthTo.Value.Year, paymentMonthTo.Value.Month, 1);

			if (!IsNullOrEmpty ( paymentCustomerID.SelectedItem ) && !((Customer) paymentCustomerID.SelectedItem ).ID.Equals (Guid.Empty )){
				filter.From.CustomerID = filter.To.CustomerID = ( (Customer) paymentCustomerID.SelectedItem ).ID;
			}

			control.ApplyFilter ( filter );
		}

		private void paymentFilter_Changed(object sender, EventArgs e) {
			ApplyPaymentFilter();
		}

		private void paymentMonthFrom_Enter(object sender, EventArgs e) {
			paymentMonthFrom.Value = new DateTime(paymentMonthFrom.Value.Year, paymentMonthFrom.Value.Month, 1);
			paymentMonthTo.Value = new DateTime(paymentMonthTo.Value.Year, paymentMonthTo.Value.Month, 1);
		}

		private void InitCustomerIDComboBox(){
			paymentCustomerID.Items.Clear();

			paymentCustomerID.Items.Add ( new Customer ( Guid.Empty ){
			                                                         	FirstName = "Все клиенты"
			                                                         } );

			paymentCustomerID.Items.AddRange ( control.GetAllCustomers().ToArray() );
			paymentCustomerID.SelectedIndex = 0;

		}

		private void exportToExcelButton_Click(object sender, EventArgs e) {
            /*
			if (dialogExcelFile.ShowDialog() == DialogResult.OK){
				DataGridView dataGridView = tabControl.SelectedTab.Text.Equals ( "Клиенты" ) ? CustomerGrid : PaymentGrid;

				Application xlApp = new ApplicationClass();
				xlApp.Workbooks.Add(Type.Missing);
				xlApp.Columns.ColumnWidth = 30;

				int columnXls = 1;
				for (int column = 0; column < dataGridView.ColumnCount; column++){
					if(!dataGridView.Columns[column].Visible) continue;
					xlApp.Cells[1, columnXls++] = dataGridView.Columns[column].HeaderText;
				}

				for(int row = 0; row < dataGridView.RowCount; row++) {
					columnXls = 1;
					for(int column = 0; column < dataGridView.ColumnCount; column++) {
						if (!dataGridView.Columns[column].Visible) continue;
						DataGridViewCell cell = dataGridView.Rows[row].Cells[column];
						xlApp.Cells[row + 2, columnXls++] = cell.FormattedValue;
					}
				}
 
				xlApp.ActiveWorkbook.SaveCopyAs ( dialogExcelFile.FileName );
				xlApp.ActiveWorkbook.Saved = true;
				xlApp.Quit();
			}
             * */
		}

		private void paymentMonthFrom_KeyPress(object sender, KeyPressEventArgs e) {
			paymentMonthFrom.Value = new DateTime(paymentMonthFrom.Value.Year, paymentMonthFrom.Value.Month, 1);
			paymentMonthTo.Value = new DateTime(paymentMonthTo.Value.Year, paymentMonthTo.Value.Month, 1);
		}

		private void debtMonth_Enter(object sender, EventArgs e) {
			debtMonth.Value = new DateTime(debtMonth.Value.Year, debtMonth.Value.Month, 1);
		}

		private void debtMonth_KeyPress(object sender, KeyPressEventArgs e) {
			debtMonth.Value = new DateTime(debtMonth.Value.Year, debtMonth.Value.Month, 1);
		}


	}
}
