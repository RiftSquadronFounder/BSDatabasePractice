using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BSDatabasePractice.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'6', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DefectRate = table.Column<decimal>(type: "numeric(10,6)", precision: 10, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PartnerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'5', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'5', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Coefficient = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'6', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartnerTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Director = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    Phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    TaxNumber = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partners_PartnerTypes_PartnerTypeId",
                        column: x => x.PartnerTypeId,
                        principalTable: "PartnerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'6', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductTypeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Article = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    MinimumPrice = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:IdentitySequenceOptions", "'17', '1', '', '', 'False', '1'")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PartnerId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    SaleDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MaterialTypes",
                columns: new[] { "Id", "DefectRate", "Name" },
                values: new object[,]
                {
                    { 1, 0.001m, "Тип материала 1" },
                    { 2, 0.0095m, "Тип материала 2" },
                    { 3, 0.0028m, "Тип материала 3" },
                    { 4, 0.0055m, "Тип материала 4" },
                    { 5, 0.0034m, "Тип материала 5" }
                });

            migrationBuilder.InsertData(
                table: "PartnerTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ЗАО" },
                    { 2, "ООО" },
                    { 3, "ПАО" },
                    { 4, "ОАО" }
                });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Coefficient", "Name" },
                values: new object[,]
                {
                    { 1, 2.35m, "Ламинат" },
                    { 2, 5.15m, "Массивная доска" },
                    { 3, 4.34m, "Паркетная доска" },
                    { 4, 1.5m, "Пробковое покрытие" }
                });

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "Address", "Director", "Email", "Name", "PartnerTypeId", "Phone", "Rating", "TaxNumber" },
                values: new object[,]
                {
                    { 1, "652050, Кемеровская область, город Юрга, ул. Лесная, 15", "Иванова Александра Ивановна", "aleksandraivanova@ml.ru", "База Строитель", 1, "493 123 45 67", 7, "2222455179" },
                    { 2, "164500, Архангельская область, город Северодвинск, ул. Строителей, 18", "Петров Василий Петрович", "vppetrov@vl.ru", "Паркет 29", 2, "987 123 56 78", 7, "3333888520" },
                    { 3, "188910, Ленинградская область, город Приморск, ул. Парковая, 21", "Соловьев Андрей Николаевич", "ansolovev@st.ru", "Стройсервис", 3, "812 223 32 00", 7, "4440391035" },
                    { 4, "143960, Московская область, город Реутов, ул. Свободы, 51", "Воробьева Екатерина Валерьевна", "ekaterina.vorobeva@ml.ru", "Ремонт и отделка", 4, "444 222 33 11", 5, "1111520857" },
                    { 5, "309500, Белгородская область, город Старый Оскол, ул. Рабочая, 122", "Степанов Степан Сергеевич", "stepanov@stepan.ru", "МонтажПро", 1, "912 888 33 33", 10, "5552431140" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Article", "MinimumPrice", "Name", "ProductTypeId" },
                values: new object[,]
                {
                    { 1, "8758385", 4456.9m, "Паркетная доска Ясень темный однополосная 14 мм", 3 },
                    { 2, "8858958", 7330.99m, "Инженерная доска Дуб Французская елка однополосная 12 мм", 3 },
                    { 3, "7750282", 1799.33m, "Ламинат Дуб дымчато-белый 33 класс 12 мм", 1 },
                    { 4, "7028748", 3890.41m, "Ламинат Дуб серый 32 класс 8 мм с фаской", 1 },
                    { 5, "5012543", 5450.59m, "Пробковое напольное клеевое покрытие 32 класс 4 мм", 4 }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "PartnerId", "ProductId", "Quantity", "SaleDate" },
                values: new object[,]
                {
                    { 1, 1, 1, 15500, new DateOnly(2023, 3, 23) },
                    { 2, 1, 3, 12350, new DateOnly(2023, 12, 18) },
                    { 3, 1, 4, 37400, new DateOnly(2024, 6, 7) },
                    { 4, 2, 2, 35000, new DateOnly(2022, 12, 2) },
                    { 5, 2, 5, 1250, new DateOnly(2023, 5, 17) },
                    { 6, 2, 3, 1000, new DateOnly(2024, 6, 7) },
                    { 7, 2, 1, 7550, new DateOnly(2024, 7, 1) },
                    { 8, 3, 1, 7250, new DateOnly(2023, 1, 22) },
                    { 9, 3, 2, 2500, new DateOnly(2024, 7, 5) },
                    { 10, 4, 4, 59050, new DateOnly(2023, 3, 20) },
                    { 11, 4, 3, 37200, new DateOnly(2024, 3, 12) },
                    { 12, 4, 5, 4500, new DateOnly(2024, 5, 14) },
                    { 13, 5, 3, 50000, new DateOnly(2023, 9, 19) },
                    { 14, 5, 4, 670000, new DateOnly(2023, 11, 10) },
                    { 15, 5, 1, 35000, new DateOnly(2024, 4, 15) },
                    { 16, 5, 2, 25000, new DateOnly(2024, 6, 12) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTypes_Name",
                table: "MaterialTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Partners_PartnerTypeId",
                table: "Partners",
                column: "PartnerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerTypes_Name",
                table: "PartnerTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Article",
                table: "Products",
                column: "Article",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_Name",
                table: "ProductTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_PartnerId",
                table: "Sales",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PartnerTypes");

            migrationBuilder.DropTable(
                name: "ProductTypes");
        }
    }
}
