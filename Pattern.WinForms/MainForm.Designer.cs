namespace Pattern.WinForms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;

    private TabControl tabControl;
    private TabPage tabFacade, tabObserver, tabStrategy;

    // Facade
    private Label lblMovieTitle;
    private TextBox txtMovieTitle;
    private Button btnStartMovie, btnStopMovie, btnClearFacade;
    private RichTextBox rtbFacadeLog;

    // Observer
    private Label lblCustomerName, lblStatus, lblOrderName;
    private TextBox txtCustomerName;
    private Button btnAddObserver, btnRemoveObserver, btnSetStatus, btnClearObserver;
    private ListBox lstObservers;
    private ComboBox cmbStatus;
    private RichTextBox rtbObserverLog;

    // Strategy
    private Label lblStrategyInput, lblResultCaption, lblResult;
    private TextBox txtStrategyInput;
    private RadioButton rdoUpper, rdoLower, rdoTitle;
    private Button btnFormat;
    private GroupBox grpFormatter;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();

        Text = "Design Patterns Demo";
        ClientSize = new Size(720, 560);
        MinimumSize = new Size(600, 480);
        StartPosition = FormStartPosition.CenterScreen;
        Font = new Font("Segoe UI", 10f);
        AutoScaleMode = AutoScaleMode.Font;
        AutoScaleDimensions = new SizeF(7F, 15F);

        tabControl = new TabControl { Dock = DockStyle.Fill };

        BuildFacadeTab();
        BuildObserverTab();
        BuildStrategyTab();

        tabControl.TabPages.AddRange([tabFacade, tabObserver, tabStrategy]);
        Controls.Add(tabControl);
    }

    // ─── Facade Tab ─────────────────────────────────────────────────
    private void BuildFacadeTab()
    {
        tabFacade = new TabPage("Facade — Home Theater");

        // Root layout: top controls + fill log
        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(10),
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

        // Top section: input row + buttons row
        var topSection = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            AutoSize = true
        };
        topSection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        topSection.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        topSection.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        // Input row
        var inputRow = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 8)
        };
        inputRow.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        inputRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        inputRow.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        lblMovieTitle = new Label
        {
            Text = "Movie title:",
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 6, 8, 0)
        };
        txtMovieTitle = new TextBox
        {
            Text = "Inception",
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 3, 0, 0)
        };
        inputRow.Controls.Add(lblMovieTitle, 0, 0);
        inputRow.Controls.Add(txtMovieTitle, 1, 0);

        // Buttons row
        var btnRow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Margin = new Padding(0, 0, 0, 10)
        };
        btnStartMovie  = new Button { Text = "▶  Start Movie", AutoSize = true, Padding = new Padding(12, 6, 12, 6), Margin = new Padding(0, 0, 8, 0) };
        btnStopMovie   = new Button { Text = "■  Stop Movie",  AutoSize = true, Padding = new Padding(12, 6, 12, 6), Margin = new Padding(0, 0, 8, 0) };
        btnClearFacade = new Button { Text = "Clear Log",      AutoSize = true, Padding = new Padding(12, 6, 12, 6), ForeColor = Color.Gray };
        btnRow.Controls.AddRange([btnStartMovie, btnStopMovie, btnClearFacade]);

        topSection.Controls.Add(inputRow, 0, 0);
        topSection.Controls.Add(btnRow, 0, 1);

        // Log
        rtbFacadeLog = new RichTextBox
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            BackColor = Color.FromArgb(24, 24, 24),
            ForeColor = Color.LightGreen,
            Font = new Font("Consolas", 10.5f),
            BorderStyle = BorderStyle.None,
            Margin = new Padding(0)
        };

        root.Controls.Add(topSection, 0, 0);
        root.Controls.Add(rtbFacadeLog, 0, 1);

        btnStartMovie.Click  += btnStartMovie_Click;
        btnStopMovie.Click   += btnStopMovie_Click;
        btnClearFacade.Click += btnClearFacade_Click;

        tabFacade.Controls.Add(root);
    }

    // ─── Observer Tab ───────────────────────────────────────────────
    private void BuildObserverTab()
    {
        tabObserver = new TabPage("Observer — Delivery");

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 4,
            Padding = new Padding(10),
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // order label
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // add/remove row
        root.RowStyles.Add(new RowStyle(SizeType.Absolute, 110)); // listbox
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // status row
        root.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // log

        // Row 0: order label
        lblOrderName = new Label
        {
            Text = "Order: №4578",
            AutoSize = true,
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            ForeColor = Color.DimGray,
            Margin = new Padding(0, 0, 0, 8)
        };

        // Row 1: add customer
        var addRow = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 4,
            RowCount = 1,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 6)
        };
        addRow.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        addRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        addRow.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        addRow.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        addRow.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        lblCustomerName = new Label { Text = "Customer name:", Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(0, 6, 8, 0) };
        txtCustomerName = new TextBox { Dock = DockStyle.Fill, Margin = new Padding(0, 3, 8, 0) };
        btnAddObserver    = new Button { Text = "Add",    AutoSize = true, Padding = new Padding(12, 6, 12, 6), BackColor = Color.FromArgb(220, 240, 220), Margin = new Padding(0, 0, 6, 0) };
        btnRemoveObserver = new Button { Text = "Remove", AutoSize = true, Padding = new Padding(12, 6, 12, 6), BackColor = Color.FromArgb(245, 220, 220) };

        addRow.Controls.Add(lblCustomerName, 0, 0);
        addRow.Controls.Add(txtCustomerName, 1, 0);
        addRow.Controls.Add(btnAddObserver, 2, 0);
        addRow.Controls.Add(btnRemoveObserver, 3, 0);

        // Row 2: listbox
        lstObservers = new ListBox
        {
            Dock = DockStyle.Fill,
            BorderStyle = BorderStyle.FixedSingle,
            Margin = new Padding(0, 0, 0, 6)
        };

        // Row 3: status
        var statusRow = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            Margin = new Padding(0, 0, 0, 8)
        };
        lblStatus = new Label { Text = "Status:", Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(0, 8, 8, 0) };
        cmbStatus = new ComboBox { Width = 180, DropDownStyle = ComboBoxStyle.DropDownList, Margin = new Padding(0, 4, 8, 0) };
        btnSetStatus     = new Button { Text = "Set Status", AutoSize = true, Padding = new Padding(12, 6, 12, 6), BackColor = Color.FromArgb(220, 230, 245), Margin = new Padding(0, 0, 8, 0) };
        btnClearObserver = new Button { Text = "Clear Log",  AutoSize = true, Padding = new Padding(12, 6, 12, 6), ForeColor = Color.Gray };
        statusRow.Controls.AddRange([lblStatus, cmbStatus, btnSetStatus, btnClearObserver]);

        // Row 4: log
        rtbObserverLog = new RichTextBox
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            BackColor = Color.FromArgb(24, 24, 24),
            ForeColor = Color.Cyan,
            Font = new Font("Consolas", 10.5f),
            BorderStyle = BorderStyle.None,
            Margin = new Padding(0)
        };

        root.RowCount = 5;
        root.Controls.Add(lblOrderName, 0, 0);
        root.Controls.Add(addRow, 0, 1);
        root.Controls.Add(lstObservers, 0, 2);
        root.Controls.Add(statusRow, 0, 3);
        root.Controls.Add(rtbObserverLog, 0, 4);

        btnAddObserver.Click    += btnAddObserver_Click;
        btnRemoveObserver.Click += btnRemoveObserver_Click;
        btnSetStatus.Click      += btnSetStatus_Click;
        btnClearObserver.Click  += btnClearObserver_Click;

        tabObserver.Controls.Add(root);
    }

    // ─── Strategy Tab ───────────────────────────────────────────────
    private void BuildStrategyTab()
    {
        tabStrategy = new TabPage("Strategy — Text Formatter");

        var root = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 5,
            Padding = new Padding(10),
        };
        root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // input
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // group
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // button
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // result label
        root.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // result value

        // Row 0: input
        var inputRow = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 10)
        };
        inputRow.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        inputRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        inputRow.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        lblStrategyInput = new Label { Text = "Input text:", Anchor = AnchorStyles.Left, AutoSize = true, Margin = new Padding(0, 6, 8, 0) };
        txtStrategyInput = new TextBox { Text = "hello world", Dock = DockStyle.Fill, Margin = new Padding(0, 3, 0, 0) };
        inputRow.Controls.Add(lblStrategyInput, 0, 0);
        inputRow.Controls.Add(txtStrategyInput, 1, 0);

        // Row 1: formatter group
        grpFormatter = new GroupBox
        {
            Text = "Formatter strategy",
            Dock = DockStyle.Fill,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 10),
            Padding = new Padding(10, 8, 10, 8)
        };
        var radioPanel = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false
        };
        rdoUpper = new RadioButton { Text = "UpperCase", Checked = true, AutoSize = true, Margin = new Padding(0, 0, 20, 0) };
        rdoLower = new RadioButton { Text = "LowerCase", AutoSize = true, Margin = new Padding(0, 0, 20, 0) };
        rdoTitle = new RadioButton { Text = "TitleCase",  AutoSize = true };
        radioPanel.Controls.AddRange([rdoUpper, rdoLower, rdoTitle]);
        grpFormatter.Controls.Add(radioPanel);

        // Row 2: button
        btnFormat = new Button
        {
            Text = "▶  Format",
            AutoSize = true,
            Padding = new Padding(16, 8, 16, 8),
            BackColor = Color.FromArgb(220, 230, 245),
            Font = new Font("Segoe UI", 10f, FontStyle.Bold),
            Margin = new Padding(0, 0, 0, 16)
        };

        // Row 3: result caption
        lblResultCaption = new Label
        {
            Text = "Result:",
            AutoSize = true,
            ForeColor = Color.DimGray,
            Margin = new Padding(0, 0, 0, 4)
        };

        // Row 4: result value
        lblResult = new Label
        {
            Text = "",
            Dock = DockStyle.Fill,
            MinimumSize = new Size(0, 56),
            Font = new Font("Consolas", 18f, FontStyle.Bold),
            ForeColor = Color.FromArgb(0, 80, 160),
            BackColor = Color.FromArgb(240, 245, 255),
            BorderStyle = BorderStyle.FixedSingle,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(10, 0, 0, 0),
            Margin = new Padding(0)
        };

        btnFormat.Click += btnFormat_Click;

        root.Controls.Add(inputRow, 0, 0);
        root.Controls.Add(grpFormatter, 0, 1);
        root.Controls.Add(btnFormat, 0, 2);
        root.Controls.Add(lblResultCaption, 0, 3);
        root.Controls.Add(lblResult, 0, 4);

        tabStrategy.Controls.Add(root);
    }
}
