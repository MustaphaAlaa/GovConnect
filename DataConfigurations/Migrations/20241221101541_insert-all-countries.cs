using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataConfigurations.Migrations
{
    /// <inheritdoc />
    public partial class insertallcountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryId", "CountryCode", "CountryName" },
                values: new object[,]
                {
                    { 1, "AFG", "Afghanistan" },
                    { 2, "ALB", "Albania" },
                    { 3, "DZA", "Algeria" },
                    { 4, "AND", "Andorra" },
                    { 5, "AGO", "Angola" },
                    { 6, "ATG", "Antigua and Barbuda" },
                    { 7, "ARG", "Argentina" },
                    { 8, "ARM", "Armenia" },
                    { 9, "AUS", "Australia" },
                    { 10, "AUT", "Austria" },
                    { 11, "AZE", "Azerbaijan" },
                    { 12, "BHS", "Bahamas" },
                    { 13, "BHR", "Bahrain" },
                    { 14, "BGD", "Bangladesh" },
                    { 15, "BRB", "Barbados" },
                    { 16, "BLR", "Belarus" },
                    { 17, "BEL", "Belgium" },
                    { 18, "BLZ", "Belize" },
                    { 19, "BEN", "Benin" },
                    { 20, "BTN", "Bhutan" },
                    { 21, "BOL", "Bolivia" },
                    { 22, "BIH", "Bosnia and Herzegovina" },
                    { 23, "BWA", "Botswana" },
                    { 24, "BRA", "Brazil" },
                    { 25, "BRN", "Brunei" },
                    { 26, "BGR", "Bulgaria" },
                    { 27, "BFA", "Burkina Faso" },
                    { 28, "BDI", "Burundi" },
                    { 29, "CPV", "Cabo Verde" },
                    { 30, "KHM", "Cambodia" },
                    { 31, "CMR", "Cameroon" },
                    { 32, "CAN", "Canada" },
                    { 33, "CAF", "Central African Republic" },
                    { 34, "TCD", "Chad" },
                    { 35, "CHL", "Chile" },
                    { 36, "CHN", "China" },
                    { 37, "COL", "Colombia" },
                    { 38, "COM", "Comoros" },
                    { 39, "COD", "Congo Democratic Republic of the" },
                    { 40, "COG", "Congo Republic of the" },
                    { 41, "CRI", "Costa Rica" },
                    { 42, "CIV", "Cote d Ivoire" },
                    { 43, "HRV", "Croatia" },
                    { 44, "CUB", "Cuba" },
                    { 45, "CYP", "Cyprus" },
                    { 46, "CZE", "Czechia" },
                    { 47, "DNK", "Denmark" },
                    { 48, "DJI", "Djibouti" },
                    { 49, "DMA", "Dominica" },
                    { 50, "DOM", "Dominican Republic" },
                    { 51, "ECU", "Ecuador" },
                    { 52, "EGY", "Egypt" },
                    { 53, "SLV", "El Salvador" },
                    { 54, "GNQ", "Equatorial Guinea" },
                    { 55, "ERI", "Eritrea" },
                    { 56, "EST", "Estonia" },
                    { 57, "SWZ", "Eswatini" },
                    { 58, "ETH", "Ethiopia" },
                    { 59, "FJI", "Fiji" },
                    { 60, "FIN", "Finland" },
                    { 61, "FRA", "France" },
                    { 62, "GAB", "Gabon" },
                    { 63, "GMB", "Gambia" },
                    { 64, "GEO", "Georgia" },
                    { 65, "DEU", "Germany" },
                    { 66, "GHA", "Ghana" },
                    { 67, "GRC", "Greece" },
                    { 68, "GRD", "Grenada" },
                    { 69, "GTM", "Guatemala" },
                    { 70, "GIN", "Guinea" },
                    { 71, "GNB", "Guinea Bissau" },
                    { 72, "GUY", "Guyana" },
                    { 73, "HTI", "Haiti" },
                    { 74, "HND", "Honduras" },
                    { 75, "HUN", "Hungary" },
                    { 76, "ISL", "Iceland" },
                    { 77, "IND", "India" },
                    { 78, "IDN", "Indonesia" },
                    { 79, "IRN", "Iran" },
                    { 80, "IRQ", "Iraq" },
                    { 81, "IRL", "Ireland" },
                    { 82, "ISR", "Israel" },
                    { 83, "ITA", "Italy" },
                    { 84, "JAM", "Jamaica" },
                    { 85, "JPN", "Japan" },
                    { 86, "JOR", "Jordan" },
                    { 87, "KAZ", "Kazakhstan" },
                    { 88, "KEN", "Kenya" },
                    { 89, "KIR", "Kiribati" },
                    { 90, "PRK", "Korea North" },
                    { 91, "KOR", "Korea South" },
                    { 92, "XKX", "Kosovo" },
                    { 93, "KWT", "Kuwait" },
                    { 94, "KGZ", "Kyrgyzstan" },
                    { 95, "LAO", "Laos" },
                    { 96, "LVA", "Latvia" },
                    { 97, "LBN", "Lebanon" },
                    { 98, "LSO", "Lesotho" },
                    { 99, "LBR", "Liberia" },
                    { 100, "LBY", "Libya" },
                    { 101, "LIE", "Liechtenstein" },
                    { 102, "LTU", "Lithuania" },
                    { 103, "LUX", "Luxembourg" },
                    { 104, "MDG", "Madagascar" },
                    { 105, "MWI", "Malawi" },
                    { 106, "MYS", "Malaysia" },
                    { 107, "MDV", "Maldives" },
                    { 108, "MLI", "Mali" },
                    { 109, "MLT", "Malta" },
                    { 110, "MHL", "Marshall Islands" },
                    { 111, "MRT", "Mauritania" },
                    { 112, "MUS", "Mauritius" },
                    { 113, "MEX", "Mexico" },
                    { 114, "FSM", "Micronesia" },
                    { 115, "MDA", "Moldova" },
                    { 116, "MCO", "Monaco" },
                    { 117, "MNG", "Mongolia" },
                    { 118, "MNE", "Montenegro" },
                    { 119, "MAR", "Morocco" },
                    { 120, "MOZ", "Mozambique" },
                    { 121, "MMR", "Myanmar" },
                    { 122, "NAM", "Namibia" },
                    { 123, "NRU", "Nauru" },
                    { 124, "NPL", "Nepal" },
                    { 125, "NLD", "Netherlands" },
                    { 126, "NZL", "New Zealand" },
                    { 127, "NIC", "Nicaragua" },
                    { 128, "NER", "Niger" },
                    { 129, "NGA", "Nigeria" },
                    { 130, "MKD", "North Macedonia" },
                    { 131, "NOR", "Norway" },
                    { 132, "OMN", "Oman" },
                    { 133, "PAK", "Pakistan" },
                    { 134, "PLW", "Palau" },
                    { 135, "PSE", "Palestine" },
                    { 136, "PAN", "Panama" },
                    { 137, "PNG", "Papua New Guinea" },
                    { 138, "PRY", "Paraguay" },
                    { 139, "PER", "Peru" },
                    { 140, "PHL", "Philippines" },
                    { 141, "POL", "Poland" },
                    { 142, "PRT", "Portugal" },
                    { 143, "QAT", "Qatar" },
                    { 144, "ROU", "Romania" },
                    { 145, "RUS", "Russia" },
                    { 146, "RWA", "Rwanda" },
                    { 147, "KNA", "Saint Kitts and Nevis" },
                    { 148, "LCA", "Saint Lucia" },
                    { 149, "VCT", "Saint Vincent and the Grenadines" },
                    { 150, "WSM", "Samoa" },
                    { 151, "SMR", "San Marino" },
                    { 152, "STP", "Sao Tome and Principe" },
                    { 153, "SAU", "Saudi Arabia" },
                    { 154, "SEN", "Senegal" },
                    { 155, "SRB", "Serbia" },
                    { 156, "SYC", "Seychelles" },
                    { 157, "SLE", "Sierra Leone" },
                    { 158, "SGP", "Singapore" },
                    { 159, "SVK", "Slovakia" },
                    { 160, "SVN", "Slovenia" },
                    { 161, "SLB", "Solomon Islands" },
                    { 162, "SOM", "Somalia" },
                    { 163, "ZAF", "South Africa" },
                    { 164, "ESP", "Spain" },
                    { 165, "LKA", "Sri Lanka" },
                    { 166, "SDN", "Sudan" },
                    { 167, "SUR", "Suriname" },
                    { 168, "SWE", "Sweden" },
                    { 169, "CHE", "Switzerland" },
                    { 170, "SYR", "Syria" },
                    { 171, "TWN", "Taiwan" },
                    { 172, "TJK", "Tajikistan" },
                    { 173, "TZA", "Tanzania" },
                    { 174, "THA", "Thailand" },
                    { 175, "TLS", "Timor Leste" },
                    { 176, "TGO", "Togo" },
                    { 177, "TON", "Tonga" },
                    { 178, "TTO", "Trinidad and Tobago" },
                    { 179, "TUN", "Tunisia" },
                    { 180, "TUR", "Turkey" },
                    { 181, "TKM", "Turkmenistan" },
                    { 182, "TUV", "Tuvalu" },
                    { 183, "UGA", "Uganda" },
                    { 184, "UKR", "Ukraine" },
                    { 185, "ARE", "United Arab Emirates" },
                    { 186, "GBR", "United Kingdom" },
                    { 187, "USA", "United States of America" },
                    { 188, "URY", "Uruguay" },
                    { 189, "UZB", "Uzbekistan" },
                    { 190, "VUT", "Vanuatu" },
                    { 191, "VAT", "Vatican City" },
                    { 192, "VEN", "Venezuela" },
                    { 193, "VNM", "Vietnam" },
                    { 194, "YEM", "Yemen" },
                    { 195, "ZMB", "Zambia" },
                    { 196, "ZWE", "Zimbabwe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "CountryId",
                keyValue: 196);
        }
    }
}
