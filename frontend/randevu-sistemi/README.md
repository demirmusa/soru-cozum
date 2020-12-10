## Randevu Uygulaması

Sizden belirtilen Front-end Framework'lerinden biriyle (Vue.js, Angular veya ReactJs) geliştirmiş, bir web uygulaması bekliyoruz. Bu uygulama kişinin randevu listesini tutacak bir uygulama olup, temek fonksiyonları barındıracaktır.

Beklenen fonksiyonlar:

* Kendi için taklit servisler ile çalışması (Mock Api)

* Sayfa yönlendirme (Routing)

* Kimlik doğrulama sistemi (Authentication)

* Ortam değişkenleri barındırması (örn: test, production vs)

* Ekranlar:

    * Giriş

    * Randevular

    * Randevu Ekle

    * Randevu Güncelle

      

### Çözüm açıklaması

#### Projeyi Çalıştırma

Proje angular ile geliştirildi. Projeyi test edebilmek için proje dizininde `npm start` komutunu çalıştırmanız yeterli. Ardından açılan `http://localhost:4200/` adresinden projeyi test edebilirsiniz.

#### Proje açıklaması

Projede 2 tane ana servis var birisi `AuthenticationService ` diğeri `RandevuService`.  Bu servisler httpclient yardımıyla sunucuyla bağlantı kurarak işlem yapan basit crud servisleri.

Bknz: [authentication.service.ts](https://github.com/demirmusa/soru-cozum/blob/main/frontend/randevu-sistemi/src/app/_services/authentication.service.ts) [randevu.service.ts](https://github.com/demirmusa/soru-cozum/tree/main/frontend/randevu-sistemi/src/app/_services/randevu.service.ts)

Bir backend olmayacağı için bu servislerin gideceği endpointlerin mocklanması gerekiyordu. Bunun için angular [http interceptor](https://angular.io/api/common/http/HttpInterceptor)larından yararlanıldı. 

Gelen istek urlleri eğer mocklanan servislerin isteklerine ait ise ilgili sonuç sunucuya gitmeden dönüldü.

https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_helpers/fakeBackend.interceptor.ts#L48-L65

Örneğin authentication için:

https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_helpers/fakeBackend.interceptor.ts#L68-L81

Randevu servisi için: 

https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_helpers/fakeBackend.interceptor.ts#L84-L111

dönüşleri yapıldı.

Randevu verilerinin depolanması için ise yazılan mock servis localstorage kullanıldı. Bknz:[FakeRandevuBackend](https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_helpers/fakeRandevuBackend.ts)



Sayfa yönlendirmeleri https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/app-routing.module.ts#L8-L9 de yapıldı. Ve HomeComponente login olmayan kullanıcıların girmesini engellemek adına bir [AuthGuard](https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_helpers/auth.guard.ts) eklendi.



Kimlik doğrulama için sayfa yönlendirmelerinde kontrol sağlayan [AuthGuard](https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_helpers/auth.guard.ts), her http isteğinde otomatik olarak aktif kullanıcının JWT tokenını isteğe ekleyen [JwtAuthInterceptor](https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_helpers/jwtAuth.interceptor.ts) ve her hangi bir istekte serverdan HTTP 401 hatası aldığında otomatik logout işlemi yapan bir error handle interceptorı([ErrorInterceptor](https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_helpers/error.interceptor.ts)) yazıldı.



Server adresi ortam değişkenlerinde tutuldu ve buradan kullanıldı: 

https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_services/authentication.service.ts#L23

https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/app/_services/randevu.service.ts#L23

https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/environments/environment.ts#L7

https://github.com/demirmusa/soru-cozum/blob/3d0624574c888f051aa8e7a3ea2f458cfa0bd816/frontend/randevu-sistemi/src/environments/environment.prod.ts#L3

İlgili sayfalar için gerekli componentler yazıldı.