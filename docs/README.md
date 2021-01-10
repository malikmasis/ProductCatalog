# ProductCatalog
Managing Product Catalog - RESTful service - EFCore - MySQL - AutoMapper - Swagger
youtube link-1: https://www.youtube.com/watch?v=gIKpvwmSJh0&t=11s&ab_channel=MalikMasis
youtube link-2: https://www.youtube.com/watch?v=6QfyYf7RoQg&ab_channel=MalikMasis

##### Projeyi Ayağa Kaldırmak

Proje Code First mantığıyla oluşturulmuştur. Bu şekilde kod tarafından veri tabanını ayağa kaldırabileceğiz.
Veritabanı olarak my sql kullanıldı. Hem open source olması hem de platform bağımsız çalışması dolayısıyla seçildi.
Bununla birlikte postgresql kullanılmak istenirse hızlı geçiş yapılabilir.

```
* add-migration Initial -> .net core console için : dotnet ef migrations add Initial
* update-database -> .net core console için : dotnet ef database update
```

Not: https://dev.mysql.com/doc/connector-net/en/connector-net-installation-binary-mysql-installer.html

MySQL Installer yüklenmesi gerekmektedir.

### Projede yapılanlar

- EFCore ve MySQL kullanıldı.
- Swagger eklendi.
- Mapping işlemleri için AutoMapper eklendi.
- CRUD işlemleri ve excel/csv import yapıldı.
- EFCore attribute ve model builder ile validation yapıldı. (Fluent Validation de kullanılabilir.)
- DDD kullanılmamasının nedeni business kısmının karmaşık olmayışındandır.
- Unit testlerde Mock ve InMemoryDatabase kullanıldı.
- Integration Test ve WebApplicationFactory yapısı kullanıldı.

Not: Postman collection de eklenmiştir.