using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace InvoiceApp
{
    public class LineItem
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

    }

    public interface ITotalCalculator
    {
        double ComputeTotal(List<LineItem> _items);
    }
    public class TotalCalculator : ITotalCalculator
    {
        public double ComputeTotal(List<LineItem> _items)
        {
            double subtotal = 0.0;
            foreach (var it in _items)
                subtotal += it.UnitPrice * it.Quantity;

            return subtotal;
        }

    }

    public interface IDiscountCalculator
    {
        double ComputeDiscount(double discounts, double subtotal);
    }
    public class PercentOffDiscountCalculator : IDiscountCalculator
    {
        public double ComputeDiscount(double discounts, double subtotal)
        {
            return subtotal * (discounts / 100.0);
        }
    }
    public class FlatOffDiscountCalculator : IDiscountCalculator
    {
        public double ComputeDiscount(double discounts, double subtotal)
        {
            return discounts;
        }
    }

    public interface ITaxCalculator
    {
        double CalculateTax(double subtotal, double discount);
    }
    public class TaxCalculator : ITaxCalculator
    {
        public double CalculateTax(double subtotal, double discount)
        {
            return (subtotal - discount) * 0.18;
        }
    }

    public interface IInvoiceAmountCalculator
    {
        double CalculateInvoiceAmount(double subtotal, double discount, double tax);
    }
    public class InvoiceAmountCalculator : IInvoiceAmountCalculator
    {
        public double CalculateInvoiceAmount(double subtotal, double discount, double tax)
        {
            return subtotal - discount + tax;
        }
    }

    public interface IPDFGenerator
    {
        string GeneratePdf(List<LineItem> _lineItems, double _subtotal, double _discount, double _tax, double _grandTotal);
    }
    public class PDFGenerator : IPDFGenerator
    {
        public string GeneratePdf(List<LineItem> lineItems, double subtotal, double discount, double tax, double grandTotal)
        {
            var pdf = new StringBuilder();
            pdf.AppendLine("INVOICE");
            foreach (var it in lineItems)
            {
                pdf.AppendLine($"{it.Sku} x{it.Quantity} @ {it.UnitPrice}");
            }
            pdf.AppendLine($"Subtotal: {subtotal}");
            pdf.AppendLine($"Discounts: {discount}");
            pdf.AppendLine($"Tax: {tax}");
            pdf.AppendLine($"Total: {grandTotal}");
            return pdf.ToString();
        }

    }

    public interface IEmailService
    {
        void SendEmail(string email);
    }
    public class EmailService : IEmailService
    {
        public void SendEmail(string _email)
        {
            if (!string.IsNullOrEmpty(_email))
            {
                Console.WriteLine($"[SMTP] Sending invoice to {_email}...");
            }
        }
    }

    public interface ILogger
    {
        void LogInfo(string email, double total);
    }
    public class Logger : ILogger
    {
        public void LogInfo(string email, double total)
        {
            Console.WriteLine($"[LOG] Invoice processed for {email} total={total}");
        }
    }

    public interface IInvoiceService
    {
        string Process();
    }
    public class InvoiceService : IInvoiceService
    {
        private List<LineItem> _items;
        private Dictionary<string, double> _discounts;
        string _email;
        ITotalCalculator _ITotalCalculator;
        ITaxCalculator _ITaxCalculator;
        IInvoiceAmountCalculator _IInvoiceAmountCalculator;
        IPDFGenerator _IPDFGenerator;
        ILogger _ILogger;
        IEmailService _IEmailService;
        Dictionary<string, IDiscountCalculator> _registry;

        public InvoiceService(
            List<LineItem> items,
            Dictionary<string, double> discounts,
            string email,
            ITotalCalculator totalCalculator,
            ITaxCalculator taxCalc,
            IInvoiceAmountCalculator invoiceCalulator,
            IPDFGenerator pdfGen,
            IEmailService emailServ,
            ILogger loggerServ,
            Dictionary<string, IDiscountCalculator> registry)
        {
            _items = items;
            _discounts = discounts;
            _email = email;
            _ITotalCalculator = totalCalculator;
            _ITaxCalculator = taxCalc;
            _IInvoiceAmountCalculator = invoiceCalulator;
            _IPDFGenerator = pdfGen;
            _IEmailService = emailServ;
            _ILogger = loggerServ;
            _registry = registry;
        }

        public string Process()
        {
            // pricing
            double subtotal = 0.0;
            subtotal = _ITotalCalculator.ComputeTotal(_items);

            // discounts
            double discountTotal = 0.0;
            foreach (var kv in _discounts)
            {
                var k = kv.Key;
                var v = kv.Value;
                discountTotal += _registry[k].ComputeDiscount(v, subtotal);
            }

            // tax
            double tax = _ITaxCalculator.CalculateTax(subtotal, discountTotal);
            double grand = _IInvoiceAmountCalculator.CalculateInvoiceAmount(subtotal, discountTotal, tax);

            // rendering (pretend PDF)
            var pdf = _IPDFGenerator.GeneratePdf(_items, subtotal, discountTotal, tax, grand);

            // email I/O
            _IEmailService.SendEmail(_email);

            // logging
            _ILogger.LogInfo(_email, grand);

            return pdf.ToString();
        }
    }


    public class InvoiceProgram
    {
        public static void Main(string[] args)
        {
            var items = new List<LineItem>
            {
                new LineItem { Sku = "ITEM-001", Quantity = 3, UnitPrice = 100.0 },
                new LineItem { Sku = "ITEM-002", Quantity = 1, UnitPrice = 250.0 }
            };
            var discounts = new Dictionary<string, double>
            {
                { "percent_off", 10.0 }
            };

            var registry = new Dictionary<string, IDiscountCalculator>
            {
                { "percent_off", new PercentOffDiscountCalculator() },
                { "flat_off", new FlatOffDiscountCalculator() },
            };
            var invoice = new InvoiceService(items,
            discounts,
            "customer@example.com",
            new TotalCalculator(),
            new TaxCalculator(),
            new InvoiceAmountCalculator(),
            new PDFGenerator(),
            new EmailService(),
            new Logger(),
            registry);
            Console.WriteLine(invoice.Process());
        }
    }
}



