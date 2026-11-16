using Microsoft.EntityFrameworkCore;

namespace BSDatabasePractice;

internal static class SeedData
{
    public static void AddTo(ModelBuilder model)
    {
        model.Entity<PartnerType>().HasData(
            new PartnerType { Id = 1, Name = "ЗАО" },
            new PartnerType { Id = 2, Name = "ООО" },
            new PartnerType { Id = 3, Name = "ПАО" },
            new PartnerType { Id = 4, Name = "ОАО" });

        model.Entity<ProductType>().HasData(
            new ProductType { Id = 1, Name = "Ламинат", Coefficient = 2.35m },
            new ProductType { Id = 2, Name = "Массивная доска", Coefficient = 5.15m },
            new ProductType { Id = 3, Name = "Паркетная доска", Coefficient = 4.34m },
            new ProductType { Id = 4, Name = "Пробковое покрытие", Coefficient = 1.5m });

        model.Entity<MaterialType>().HasData(
            new MaterialType { Id = 1, Name = "Тип материала 1", DefectRate = 0.001m },
            new MaterialType { Id = 2, Name = "Тип материала 2", DefectRate = 0.0095m },
            new MaterialType { Id = 3, Name = "Тип материала 3", DefectRate = 0.0028m },
            new MaterialType { Id = 4, Name = "Тип материала 4", DefectRate = 0.0055m },
            new MaterialType { Id = 5, Name = "Тип материала 5", DefectRate = 0.0034m });

        model.Entity<Partner>().HasData(
            new Partner { Id = 1, PartnerTypeId = 1, Name = "База Строитель", Director = "Иванова Александра Ивановна", Email = "aleksandraivanova@ml.ru", Phone = "493 123 45 67",
                Address = "652050, Кемеровская область, город Юрга, ул. Лесная, 15", TaxNumber = "2222455179", Rating = 7 },
            new Partner { Id = 2, PartnerTypeId = 2, Name = "Паркет 29", Director = "Петров Василий Петрович", Email = "vppetrov@vl.ru", Phone = "987 123 56 78",
                Address = "164500, Архангельская область, город Северодвинск, ул. Строителей, 18", TaxNumber = "3333888520", Rating = 7 },
            new Partner { Id = 3, PartnerTypeId = 3, Name = "Стройсервис", Director = "Соловьев Андрей Николаевич", Email = "ansolovev@st.ru", Phone = "812 223 32 00",
                Address = "188910, Ленинградская область, город Приморск, ул. Парковая, 21", TaxNumber = "4440391035", Rating = 7 },
            new Partner { Id = 4, PartnerTypeId = 4, Name = "Ремонт и отделка", Director = "Воробьева Екатерина Валерьевна", Email = "ekaterina.vorobeva@ml.ru", Phone = "444 222 33 11",
                Address = "143960, Московская область, город Реутов, ул. Свободы, 51", TaxNumber = "1111520857", Rating = 5 },
            new Partner { Id = 5, PartnerTypeId = 1, Name = "МонтажПро", Director = "Степанов Степан Сергеевич", Email = "stepanov@stepan.ru", Phone = "912 888 33 33",
                Address = "309500, Белгородская область, город Старый Оскол, ул. Рабочая, 122", TaxNumber = "5552431140", Rating = 10 });

        model.Entity<Product>().HasData(
            new Product { Id = 1, ProductTypeId = 3, Name = "Паркетная доска Ясень темный однополосная 14 мм", Article = "8758385", MinimumPrice = 4456.9m },
            new Product { Id = 2, ProductTypeId = 3, Name = "Инженерная доска Дуб Французская елка однополосная 12 мм", Article = "8858958", MinimumPrice = 7330.99m },
            new Product { Id = 3, ProductTypeId = 1, Name = "Ламинат Дуб дымчато-белый 33 класс 12 мм", Article = "7750282", MinimumPrice = 1799.33m },
            new Product { Id = 4, ProductTypeId = 1, Name = "Ламинат Дуб серый 32 класс 8 мм с фаской", Article = "7028748", MinimumPrice = 3890.41m },
            new Product { Id = 5, ProductTypeId = 4, Name = "Пробковое напольное клеевое покрытие 32 класс 4 мм", Article = "5012543", MinimumPrice = 5450.59m });

        model.Entity<Sale>().HasData(
            new Sale { Id = 1, PartnerId = 1, ProductId = 1, Quantity = 15500, SaleDate = new DateOnly(2023, 3, 23) },
            new Sale { Id = 2, PartnerId = 1, ProductId = 3, Quantity = 12350, SaleDate = new DateOnly(2023, 12, 18) },
            new Sale { Id = 3, PartnerId = 1, ProductId = 4, Quantity = 37400, SaleDate = new DateOnly(2024, 6, 7) },
            new Sale { Id = 4, PartnerId = 2, ProductId = 2, Quantity = 35000, SaleDate = new DateOnly(2022, 12, 2) },
            new Sale { Id = 5, PartnerId = 2, ProductId = 5, Quantity = 1250, SaleDate = new DateOnly(2023, 5, 17) },
            new Sale { Id = 6, PartnerId = 2, ProductId = 3, Quantity = 1000, SaleDate = new DateOnly(2024, 6, 7) },
            new Sale { Id = 7, PartnerId = 2, ProductId = 1, Quantity = 7550, SaleDate = new DateOnly(2024, 7, 1) },
            new Sale { Id = 8, PartnerId = 3, ProductId = 1, Quantity = 7250, SaleDate = new DateOnly(2023, 1, 22) },
            new Sale { Id = 9, PartnerId = 3, ProductId = 2, Quantity = 2500, SaleDate = new DateOnly(2024, 7, 5) },
            new Sale { Id = 10, PartnerId = 4, ProductId = 4, Quantity = 59050, SaleDate = new DateOnly(2023, 3, 20) },
            new Sale { Id = 11, PartnerId = 4, ProductId = 3, Quantity = 37200, SaleDate = new DateOnly(2024, 3, 12) },
            new Sale { Id = 12, PartnerId = 4, ProductId = 5, Quantity = 4500, SaleDate = new DateOnly(2024, 5, 14) },
            new Sale { Id = 13, PartnerId = 5, ProductId = 3, Quantity = 50000, SaleDate = new DateOnly(2023, 9, 19) },
            new Sale { Id = 14, PartnerId = 5, ProductId = 4, Quantity = 670000, SaleDate = new DateOnly(2023, 11, 10) },
            new Sale { Id = 15, PartnerId = 5, ProductId = 1, Quantity = 35000, SaleDate = new DateOnly(2024, 4, 15) },
            new Sale { Id = 16, PartnerId = 5, ProductId = 2, Quantity = 25000, SaleDate = new DateOnly(2024, 6, 12) });
    }
}
