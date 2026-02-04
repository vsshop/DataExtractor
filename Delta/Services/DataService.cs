using Delta.Domain.Enums;
using Delta.Domain.Models.MTable;

namespace Delta.Services;
public class DataService
{
    static Table employeesTable = new Table()
    {
        Id = Guid.Parse("e39d3625-029a-48fc-a316-e1214337b235"),
        Title = "Employees Table",
        State = StateCode.Validate,
        Columns = new()
        {
            new Column()
            {
                Id = Guid.Parse("42d9f4c8-b3b5-44af-9ad6-b0eaa55a0acb"),
                Title = "Id",
                Index = 0
            },
            new Column()
            {
                Id = Guid.Parse("ece9715a-f554-43d8-a55e-2d6e8a8953cf"),
                Title = "Name",
                Index = 1
            },
            new Column()
            {
                Id = Guid.Parse("fd462a28-6735-4c38-8263-57cee1ec1708"),
                Title = "Salary",
                Index = 2
            },
        },
        Rows = new List<Row>()
        {
            new Row()
            {
                Id = Guid.Parse("dc79a2a8-87c3-4c99-bb3a-505b1709bff7"),
                Cells = new() {"1", "Jon", "5000"}
            },
            new Row()
            {
                Id = Guid.Parse("bfd8437d-9fea-4861-8574-b3130b4cf7bc"),
                Cells = new() {"2", "Lili", "12000"}
            },
            new Row()
            {
                Id = Guid.Parse("dece7e53-7e78-41cd-8b6f-e5967c9eabad"),
                Cells = new() {"3", "Esma", "10000"}
            },
            new Row()
            {
                Id = Guid.Parse("9b70eeb6-a7b5-4afc-bf94-ddcf42774bd5"),
                Cells = new() {"4", "Arnold", "51000"}
            },
            new Row()
            {
                Id = Guid.Parse("6e5d9995-b3c0-4071-8cbb-1244e3bbf699"),
                Cells = new() {"5", "Kevin", "3000"}
            },
            new Row()
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-4a5b-9c8d-7e6f5a4b3c2d"),
                Cells = new() {"6", "Sarah", "my.test@gmail.com"}
            }
        }
    };

    static Table productsTable = new Table()
    {
        Id = Guid.Parse("f7a8b9c0-d1e2-4f3a-b4c5-d6e7f8a9b0c1"),
        Title = "Products Inventory",
        State = StateCode.Validate,
        Columns = new()
        {
            new Column()
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                Title = "Product ID",
                Index = 0
            },
            new Column()
            {
                Id = Guid.Parse("b2c3d4e5-f6a7-8b9c-0d1e-2f3a4b5c6d7e"),
                Title = "Product Name",
                Index = 1
            },
            new Column()
            {
                Id = Guid.Parse("c3d4e5f6-a7b8-9c0d-1e2f-3a4b5c6d7e8f"),
                Title = "Category",
                Index = 2
            },
            new Column()
            {
                Id = Guid.Parse("d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9a"),
                Title = "Price",
                Index = 3
            },
            new Column()
            {
                Id = Guid.Parse("e5f6a7b8-c9d0-1e2f-3a4b-5c6d7e8f9a0b"),
                Title = "Stock",
                Index = 4
            },
        },
        Rows = new List<Row>()
        {
            new Row() { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Cells = new() {"P001", "Laptop Dell XPS 15", "Electronics", "1299.99", "45"} },
            new Row() { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Cells = new() {"P002", "Wireless Mouse Logitech", "Accessories", "29.99", "150"} },
            new Row() { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Cells = new() {"P003", "Mechanical Keyboard RGB", "Accessories", "89.99", "78"} },
            new Row() { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Cells = new() {"P004", "4K Monitor 27 inch", "Electronics", "599.99", "32"} },
            new Row() { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Cells = new() {"P005", "USB-C Hub 7-in-1", "Accessories", "49.99", "200"} },
            new Row() { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), Cells = new() {"P006", "Ergonomic Office Chair", "Furniture", "299.99", "25"} },
            new Row() { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), Cells = new() {"P007", "Standing Desk Electric", "Furniture", "499.99", "invalid"} },
            new Row() { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), Cells = new() {"P008", "Webcam HD 1080p", "Electronics", "79.99", "-5"} },
            new Row() { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), Cells = new() {"P009", "Noise Canceling Headphones", "Electronics", "149.99", "0"} },
            new Row() { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Cells = new() {"P010", "LED Desk Lamp", "Furniture", "test.price", "60"} },
            new Row() { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Cells = new() {"P011", "MacBook Pro 16 inch", "Electronics", "2499.00", "18"} },
            new Row() { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Cells = new() {"P012", "iPad Air 5th Gen", "Electronics", "749.99", "55"} },
            new Row() { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Cells = new() {"P013", "Magic Trackpad", "Accessories", "129.00", "120"} },
            new Row() { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Cells = new() {"P014", "Wireless Charger 15W", "Accessories", "39.99", "180"} },
            new Row() { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Cells = new() {"P015", "External SSD 1TB", "Electronics", "119.99", "95"} },
            new Row() { Id = Guid.Parse("10101010-1010-1010-1010-101010101010"), Cells = new() {"P016", "Gaming Mouse Razer", "Accessories", "69.99", "140"} },
            new Row() { Id = Guid.Parse("20202020-2020-2020-2020-202020202020"), Cells = new() {"P017", "Ultrawide Monitor 34 inch", "Electronics", "899.00", "22"} },
            new Row() { Id = Guid.Parse("30303030-3030-3030-3030-303030303030"), Cells = new() {"P018", "Mesh Office Chair", "Furniture", "349.99", "38"} },
            new Row() { Id = Guid.Parse("40404040-4040-4040-4040-404040404040"), Cells = new() {"P019", "Document Scanner", "Electronics", "199.99", "0"} },
            new Row() { Id = Guid.Parse("50505050-5050-5050-5050-505050505050"), Cells = new() {"P020", "Printer All-in-One", "Electronics", "279.99", "42"} },
            new Row() { Id = Guid.Parse("60606060-6060-6060-6060-606060606060"), Cells = new() {"P021", "Bookshelf 5-Tier", "Furniture", "159.99", "28"} },
            new Row() { Id = Guid.Parse("70707070-7070-7070-7070-707070707070"), Cells = new() {"P022", "Filing Cabinet 3-Drawer", "Furniture", "189.99", "15"} },
            new Row() { Id = Guid.Parse("80808080-8080-8080-8080-808080808080"), Cells = new() {"P023", "Desk Organizer Set", "Office Supplies", "24.99", "250"} },
            new Row() { Id = Guid.Parse("90909090-9090-9090-9090-909090909090"), Cells = new() {"P024", "Whiteboard Magnetic", "Office Supplies", "79.99", "65"} },
            new Row() { Id = Guid.Parse("a1a1a1a1-a1a1-a1a1-a1a1-a1a1a1a1a1a1"), Cells = new() {"P025", "Laptop Stand Aluminum", "Accessories", "44.99", "175"} },
            new Row() { Id = Guid.Parse("b2b2b2b2-b2b2-b2b2-b2b2-b2b2b2b2b2b2"), Cells = new() {"P026", "Cable Management Kit", "Accessories", "19.99", "300"} },
            new Row() { Id = Guid.Parse("c3c3c3c3-c3c3-c3c3-c3c3-c3c3c3c3c3c3"), Cells = new() {"P027", "Monitor Arm Dual", "Accessories", "99.99", "52"} },
            new Row() { Id = Guid.Parse("d4d4d4d4-d4d4-d4d4-d4d4-d4d4d4d4d4d4"), Cells = new() {"P028", "Graphics Tablet Wacom", "Electronics", "349.99", "33"} },
            new Row() { Id = Guid.Parse("e5e5e5e5-e5e5-e5e5-e5e5-e5e5e5e5e5e5"), Cells = new() {"P029", "Studio Microphone USB", "Electronics", "129.99", "88"} },
            new Row() { Id = Guid.Parse("f6f6f6f6-f6f6-f6f6-f6f6-f6f6f6f6f6f6"), Cells = new() {"P030", "Ring Light 18 inch", "Electronics", "89.99", "110"} },
            new Row() { Id = Guid.Parse("a7a7a7a7-a7a7-a7a7-a7a7-a7a7a7a7a7a7"), Cells = new() {"P031", "Green Screen Backdrop", "Accessories", "59.99", "75"} },
            new Row() { Id = Guid.Parse("b8b8b8b8-b8b8-b8b8-b8b8-b8b8b8b8b8b8"), Cells = new() {"P032", "Portable SSD 2TB", "Electronics", "199.99", "invalid_stock"} },
            new Row() { Id = Guid.Parse("c9c9c9c9-c9c9-c9c9-c9c9-c9c9c9c9c9c9"), Cells = new() {"P033", "Docking Station USB-C", "Electronics", "179.99", "68"} },
            new Row() { Id = Guid.Parse("d0d0d0d0-d0d0-d0d0-d0d0-d0d0d0d0d0d0"), Cells = new() {"P034", "Wireless Keyboard Compact", "Accessories", "54.99", "145"} },
            new Row() { Id = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), Cells = new() {"P035", "Ergonomic Mouse Vertical", "Accessories", "39.99", "190"} },
            new Row() { Id = Guid.Parse("f2f2f2f2-f2f2-f2f2-f2f2-f2f2f2f2f2f2"), Cells = new() {"P036", "Desk Mat XL Gaming", "Accessories", "29.99", "210"} },
            new Row() { Id = Guid.Parse("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"), Cells = new() {"P037", "Smart Speaker Echo", "Electronics", "99.99", "125"} },
            new Row() { Id = Guid.Parse("b4b4b4b4-b4b4-b4b4-b4b4-b4b4b4b4b4b4"), Cells = new() {"P038", "Security Camera Indoor", "Electronics", "49.99", "156"} },
            new Row() { Id = Guid.Parse("c5c5c5c5-c5c5-c5c5-c5c5-c5c5c5c5c5c5"), Cells = new() {"P039", "Power Strip Surge Protector", "Accessories", "24.99", "-10"} },
            new Row() { Id = Guid.Parse("d6d6d6d6-d6d6-d6d6-d6d6-d6d6d6d6d6d6"), Cells = new() {"P040", "UPS Battery Backup", "Electronics", "159.99", "44"} },
            new Row() { Id = Guid.Parse("e7e7e7e7-e7e7-e7e7-e7e7-e7e7e7e7e7e7"), Cells = new() {"P041", "Label Maker Bluetooth", "Office Supplies", "69.99", "82"} },
            new Row() { Id = Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"), Cells = new() {"P042", "Paper Shredder Cross-Cut", "Office Supplies", "119.99", "36"} },
            new Row() { Id = Guid.Parse("a9a9a9a9-a9a9-a9a9-a9a9-a9a9a9a9a9a9"), Cells = new() {"P043", "Conference Speaker Phone", "Electronics", "229.99", "28"} },
            new Row() { Id = Guid.Parse("babababa-baba-baba-baba-babababababa"), Cells = new() {"P044", "Footrest Ergonomic", "Furniture", "price_error", "95"} },
            new Row() { Id = Guid.Parse("cbcbcbcb-cbcb-cbcb-cbcb-cbcbcbcbcbcb"), Cells = new() {"P045", "Laptop Cooling Pad", "Accessories", "34.99", "168"} },
            new Row() { Id = Guid.Parse("dcdcdcdc-dcdc-dcdc-dcdc-dcdcdcdcdcdc"), Cells = new() {"P046", "Bluetooth Presenter Remote", "Accessories", "19.99", "220"} },
            new Row() { Id = Guid.Parse("dedadeda-deda-deda-deda-dedadedadeda"), Cells = new() {"P047", "Portable Monitor 15.6 inch", "Electronics", "249.99", "0"} },
            new Row() { Id = Guid.Parse("efefefef-efef-efef-efef-efefefefefef"), Cells = new() {"P048", "Desktop PC Intel i7", "Electronics", "1599.99", "12"} },
            new Row() { Id = Guid.Parse("f0f0f0f0-f0f0-f0f0-f0f0-f0f0f0f0f0f0"), Cells = new() {"P049", "Mechanical Numpad", "Accessories", "44.99", "135"} },
            new Row() { Id = Guid.Parse("a1b1c1d1-e1f1-a1b1-c1d1-e1f1a1b1c1d1"), Cells = new() {"P050", "Anti-Fatigue Floor Mat", "Furniture", "79.99", "58"} }
        }
    };

    static List<Table> tables = new() { employeesTable, productsTable };
    public List<Table> Tables => tables;
}
