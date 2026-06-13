using Pattern.Core.Facade;
using Pattern.Core.Observer;
using Pattern.Core.Strategy;

namespace Pattern.WinForms;

public partial class MainForm : Form
{
    // Facade
    private HomeTheaterFacade? _theater;

    // Observer
    private DeliveryOrder? _order;
    private readonly Dictionary<string, Customer> _customers = [];

    // Strategy
    private TextEditor _editor = new(new UpperCaseFormatter());

    public MainForm()
    {
        InitializeComponent();
        InitializeFacade();
        InitializeObserver();
    }

    private void InitializeFacade()
    {
        _theater = new HomeTheaterFacade(new Projector(), new SoundSystem(), new StreamingService());
    }

    private void InitializeObserver()
    {
        _order = new DeliveryOrder("Order №4578");
        cmbStatus.Items.AddRange(["Processed", "Shipped", "Delivered"]);
        cmbStatus.SelectedIndex = 0;
    }

    // ─── Facade Tab ────────────────────────────────────────────────
    private void btnStartMovie_Click(object sender, EventArgs e)
    {
        string title = txtMovieTitle.Text.Trim();
        if (string.IsNullOrEmpty(title)) return;

        RedirectTo(rtbFacadeLog, () => _theater!.StartMovie(title));
    }

    private void btnStopMovie_Click(object sender, EventArgs e)
    {
        RedirectTo(rtbFacadeLog, () => _theater!.StopMovie());
    }

    private void btnClearFacade_Click(object sender, EventArgs e) => rtbFacadeLog.Clear();

    // ─── Observer Tab ───────────────────────────────────────────────
    private void btnAddObserver_Click(object sender, EventArgs e)
    {
        string name = txtCustomerName.Text.Trim();
        if (string.IsNullOrEmpty(name) || _customers.ContainsKey(name)) return;

        var customer = new WinFormsCustomer(name, rtbObserverLog);
        _customers[name] = customer;
        _order!.AddObserver(customer);
        lstObservers.Items.Add(name);
        txtCustomerName.Clear();
    }

    private void btnRemoveObserver_Click(object sender, EventArgs e)
    {
        if (lstObservers.SelectedItem is not string name) return;
        if (!_customers.TryGetValue(name, out var customer)) return;

        _order!.RemoveObserver(customer);
        _customers.Remove(name);
        lstObservers.Items.Remove(name);
    }

    private void btnSetStatus_Click(object sender, EventArgs e)
    {
        if (cmbStatus.SelectedItem is not string status) return;
        AppendLog(rtbObserverLog, $"--- Setting status: {status} ---");
        _order!.Status = status;
    }

    private void btnClearObserver_Click(object sender, EventArgs e) => rtbObserverLog.Clear();

    // ─── Strategy Tab ───────────────────────────────────────────────
    private void btnFormat_Click(object sender, EventArgs e)
    {
        ITextFormatter formatter = rdoUpper.Checked ? new UpperCaseFormatter()
            : rdoLower.Checked ? new LowerCaseFormatter()
            : (ITextFormatter)new TitleCaseFormatter();

        _editor.SetFormatter(formatter);
        lblResult.Text = _editor.FormatText(txtStrategyInput.Text);
    }

    // ─── Helpers ────────────────────────────────────────────────────
    private static void RedirectTo(RichTextBox rtb, Action action)
    {
        var writer = new RichTextBoxWriter(rtb);
        var old = Console.Out;
        Console.SetOut(writer);
        action();
        Console.SetOut(old);
    }

    private static void AppendLog(RichTextBox rtb, string text)
    {
        rtb.AppendText(text + Environment.NewLine);
        rtb.ScrollToCaret();
    }
}

// Customer that writes to RichTextBox instead of Console
class WinFormsCustomer(string name, RichTextBox rtb) : Customer(name)
{
    public override void Update(string status)
    {
        string msg = $"{Name} notified: {status}";
        rtb.Invoke(() =>
        {
            rtb.AppendText(msg + Environment.NewLine);
            rtb.ScrollToCaret();
        });
    }
}

// Redirects Console.Write to RichTextBox
class RichTextBoxWriter(RichTextBox rtb) : TextWriter
{
    public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;

    public override void WriteLine(string? value)
    {
        rtb.Invoke(() =>
        {
            rtb.AppendText((value ?? "") + Environment.NewLine);
            rtb.ScrollToCaret();
        });
    }

    public override void Write(string? value)
    {
        if (!string.IsNullOrEmpty(value))
            rtb.Invoke(() => rtb.AppendText(value));
    }
}
