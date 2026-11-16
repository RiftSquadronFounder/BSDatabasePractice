using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BSDatabasePractice;

public partial class MainWindow : Window
{
    private int? editingId;

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void WindowLoaded(object sender, RoutedEventArgs e) => await RunAsync(LoadPartnersAsync);
    private async void RefreshClick(object sender, RoutedEventArgs e) => await RunAsync(LoadPartnersAsync);
    private async void AddClick(object sender, RoutedEventArgs e) => await RunAsync(() => OpenEditorAsync(null));
    private void BackClick(object sender, RoutedEventArgs e) => ShowPage(PartnerPage, "Партнёры");

    private async void EditClick(object sender, RoutedEventArgs e)
    {
        await RunAsync(() => OpenEditorAsync((int)((Button)sender).Tag));
    }

    private async Task LoadPartnersAsync()
    {
        await using var db = new PracticeDb();
        await db.Database.MigrateAsync();
        var partners = await db.Partners.OrderBy(partner => partner.Id)
            .Select(partner => new
            {
                partner.Id,
                Heading = partner.Type.Name + " | " + partner.Name,
                partner.Director,
                partner.Phone,
                partner.Rating,
                TotalSold = partner.Sales.Sum(sale => (long?)sale.Quantity) ?? 0
            }).ToListAsync();

        PartnerList.ItemsSource = partners.Select(partner => new
        {
            partner.Id,
            partner.Heading,
            partner.Director,
            Phone = DisplayPhone(partner.Phone),
            partner.Rating,
            Discount = Calculations.Discount(partner.TotalSold)
        }).ToList();
        AddButton.IsEnabled = true;
    }

    private static string DisplayPhone(string phone)
    {
        if (phone.Length == 13 && phone.Count(char.IsDigit) == 10)
            return "+7 " + phone;
        return phone;
    }

    private async Task OpenEditorAsync(int? id)
    {
        await using var db = new PracticeDb();
        var types = await db.PartnerTypes.AsNoTracking().OrderBy(type => type.Name).ToListAsync();
        var partner = id == null ? new Partner() : await db.Partners.FindAsync(id.Value);
        if (partner == null)
            throw new InvalidOperationException("Партнёр не найден. Обновите список партнёров.");

        editingId = id;
        PartnerTypeField.ItemsSource = types;
        PartnerTypeField.SelectedValue = partner.PartnerTypeId;
        PartnerName.Text = partner.Name;
        DirectorField.Text = partner.Director;
        AddressField.Text = partner.Address;
        PhoneField.Text = partner.Phone;
        EmailField.Text = partner.Email;
        RatingField.Text = partner.Rating.ToString();
        ShowPage(EditorPage, id == null ? "Добавление партнёра" : "Редактирование партнёра");
    }

    private async void SaveClick(object sender, RoutedEventArgs e)
    {
        var name = PartnerName.Text.Trim();
        var director = DirectorField.Text.Trim();
        var address = AddressField.Text.Trim();
        var phone = PhoneField.Text.Trim();
        var email = EmailField.Text.Trim();

        if (PartnerTypeField.SelectedValue is not int typeId || name.Length == 0 ||
            director.Length == 0 || address.Length == 0 || phone.Length == 0 || email.Length == 0)
        {
            Warn("Заполните все поля и выберите тип партнёра.");
            return;
        }

        if (!int.TryParse(RatingField.Text, out int rating) || rating < 0)
        {
            Warn("Введите целое число от 0 до 2147483647 в поле «Рейтинг».");
            return;
        }

        if (!MailAddress.TryCreate(email, out var mail) || mail.Address != email)
        {
            Warn("Введите адрес электронной почты в формате name@example.ru.");
            return;
        }

        if (phone.Count(char.IsDigit) < 10 || phone.Any(c => !char.IsDigit(c) && !"+-() ".Contains(c)))
        {
            Warn("Введите номер телефона, например +7 900 123 45 67.");
            return;
        }

        await RunAsync(async () =>
        {
            await using var db = new PracticeDb();
            var partner = editingId == null ? new Partner() : await db.Partners.FindAsync(editingId.Value);
            if (partner == null)
                throw new InvalidOperationException("Партнёр не найден. Вернитесь к списку и обновите его.");

            partner.Name = name;
            partner.PartnerTypeId = typeId;
            partner.Director = director;
            partner.Address = address;
            partner.Phone = phone;
            partner.Email = email;
            partner.Rating = rating;
            if (editingId == null)
                db.Partners.Add(partner);

            await db.SaveChangesAsync();
            ShowPage(PartnerPage, "Партнёры");
            await LoadPartnersAsync();
        });
    }

    private async void HistoryClick(object sender, RoutedEventArgs e)
    {
        int id = (int)((Button)sender).Tag;
        await RunAsync(async () =>
        {
            await using var db = new PracticeDb();
            var name = await db.Partners.Where(partner => partner.Id == id)
                .Select(partner => partner.Name).SingleOrDefaultAsync();
            if (name == null)
                throw new InvalidOperationException("Партнёр не найден. Обновите список партнёров.");

            var sales = await db.Sales.Where(sale => sale.PartnerId == id)
                .OrderByDescending(sale => sale.SaleDate).ThenBy(sale => sale.Id)
                .Select(sale => new { Product = sale.Product.Name, sale.Quantity, Date = sale.SaleDate })
                .ToListAsync();

            HistoryPartner.Text = name;
            SalesGrid.ItemsSource = sales;
            EmptyHistory.Visibility = sales.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            ShowPage(HistoryPage, "История продаж");
        });
    }

    private void ShowPage(UIElement page, string title)
    {
        PartnerPage.Visibility = page == PartnerPage ? Visibility.Visible : Visibility.Collapsed;
        EditorPage.Visibility = page == EditorPage ? Visibility.Visible : Visibility.Collapsed;
        HistoryPage.Visibility = page == HistoryPage ? Visibility.Visible : Visibility.Collapsed;
        BackButton.Visibility = page == PartnerPage ? Visibility.Collapsed : Visibility.Visible;
        Logo.Visibility = PartnerPage.Visibility;
        Heading.Text = title;
        Title = title;
    }

    private void Warn(string message)
    {
        MessageBox.Show(this, message, "Проверьте данные", MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    private async Task RunAsync(Func<Task> action)
    {
        Layout.IsEnabled = false;
        try
        {
            await action();
        }
        catch (Exception exception)
        {
            string message = exception switch
            {
                DbUpdateException => "Не удалось сохранить данные. Проверьте подключение и повторите попытку.",
                PostgresException { SqlState: "28P01" } => "PostgreSQL отклонил пароль. Проверьте PGUSER и PGPASSWORD в файле .env.",
                NpgsqlException => "Не удалось выполнить запрос. Проверьте настройки в файле .env и доступность PostgreSQL.",
                InvalidOperationException => exception.Message,
                _ => "Операция не выполнена. Повторите попытку."
            };
            MessageBox.Show(this, message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            Layout.IsEnabled = true;
        }
    }
}
